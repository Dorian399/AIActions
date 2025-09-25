using AIActions.AI;
using AIActions.AI.Results;
using AIActions.Configs;
using AIActions.ExternalData;
using AIActions.Python;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AIActions.AI.AIRequester;

namespace AIActions
{
    public partial class ExecutionWindow : Form
    {
        public PromptWindow? ParentWindow;
        private bool SuccessfullyExecuted=false;
        private string _prompt;
        private ParsedConfig _configs;
        private string _folderOrFile;
        private ParsedResult _result;
        private CancellationTokenSource _cancellationTokenSource;
        public ExecutionWindow(string prompt,ParsedConfig configs,string folderOrFile)
        {
            _prompt = prompt;
            _configs = configs;
            _folderOrFile = folderOrFile;
            InitializeComponent();
        }

        private void ExecutionWindow_StatusChanged(string value, StatusCode code, ParsedResult? parsedResult, string? error)
        {
            InfoLabel.Text = value;
            if (value.Length > 140)
                InfoLabel.TextAlign = ContentAlignment.TopCenter;
            else
                InfoLabel.TextAlign = ContentAlignment.MiddleCenter;

            if(code == StatusCode.Error && !String.IsNullOrWhiteSpace(error))
            {
                ShowCodeButton.Text = "Show error";
                ShowCodeButton.SetPreviewText(error, title: "Error preview");
                ShowCodeButton.Enabled = true;
                return;
            }

            if (code == StatusCode.Accepted && !String.IsNullOrWhiteSpace(parsedResult.Script))
            {
                _result = parsedResult;
                ShowCodeButton.SetPreviewText(parsedResult.Script, title: "Code preview");
                ShowCodeButton.Enabled = true;
                Confirm.Enabled = true;
                return;
            }
        }

        private async void ExecutionWindow_Load(object sender, EventArgs e)
        {
            if (_prompt != null)
            {
                CurPromptLabel.Text += _prompt;
            }
            _cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = _cancellationTokenSource.Token;

            AIRequester requester = new AIRequester();
            requester.OnStatusChanged += ExecutionWindow_StatusChanged;
            await requester.SendPrompt(_configs,_folderOrFile, _prompt, token);
        }

        private async void ExecutionWindow_ConfirmClicked(object sender, EventArgs e)
        {
            TabsWindow.SelectedIndex = 1;
            CancellationToken token = _cancellationTokenSource.Token;

            string workingDirectory = Path.GetFullPath(_folderOrFile);

            ResultExec executor = new ResultExec();
            executor.OnOutput = text =>
            {
                if (STDOut != null)
                {
                    STDOut.AppendText(text+"\n");
                    STDOut.SelectionStart = STDOut.Text.Length;
                    STDOut.ScrollToCaret();
                }
            };
            
            await executor.ExecuteResults(
                _result,
                _prompt,
                workingDirectory,
                uiControl: this,
                token   
            );

        }

        private void ExecutionWindow_CancelClicked(object sender, EventArgs e)
        {
            Close();
        }

        private void ExecutionWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            _cancellationTokenSource.Cancel();
            if (ParentWindow != null)
            {
                if (SuccessfullyExecuted)
                    ParentWindow.Close();
                else
                    ParentWindow.Show();
            }
        }
    }
}
