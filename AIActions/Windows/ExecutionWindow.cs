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
using static System.Net.Mime.MediaTypeNames;

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

        private async Task startFakeProgress()
        {
            while(progressBar1.Value < 90)
            {
                progressBar1.Step = 1;
                progressBar1.PerformStep();
                await Task.Delay(510);
            }
        }

        private async Task ExecutionWindow_StatusChanged(string value, StatusCode code, ParsedResult? parsedResult, string? error)
        {
            InfoLabel.Text = value;
            if (value.Length > 140)
                InfoLabel.TextAlign = ContentAlignment.TopCenter;
            else
                InfoLabel.TextAlign = ContentAlignment.MiddleCenter;

            if (code != StatusCode.Processing)
            {
                progressBar1.Value = 100;
            }
            else
            {
                // Move progress bar
                progressBar1.Step = 10;
                progressBar1.PerformStep();
            }

            if (value == "Sending request...")
            {
                await startFakeProgress();
                return;
            }

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
            // Switch pages.
            TabsWindow.SelectedIndex = 1;

            // Remove the bottom layout (progress bar and buttons).
            TableLayoutRowStyleCollection rows = tableLayoutPanel1.RowStyles;
            int lastRowIndex = tableLayoutPanel1.RowStyles.Count - 1;
            foreach (RowStyle row in rows)
            {
                if (rows.IndexOf(row) == lastRowIndex)
                    row.Height = 0;
            }
            
            // Create and execute the script.
            CancellationToken token = _cancellationTokenSource.Token;

            string? workingDirectory = _folderOrFile;

            if (workingDirectory == null || workingDirectory == "")
            {
                if (STDOut != null && !STDOut.IsDisposed)
                    STDOut.AppendText($"Invalid path: {workingDirectory}, aborting execution.\n");
                return;
            }

            FileAttributes attr = File.GetAttributes(workingDirectory);

            if (!attr.HasFlag(FileAttributes.Directory))
                workingDirectory = Path.GetDirectoryName(workingDirectory);

            ResultExec executor = new ResultExec();

            executor.OnOutput = text =>
            {
                if (STDOut != null && !STDOut.IsDisposed)
                    STDOut.AppendText(text);
            };

            bool exitBool=false;

            try
            {
                exitBool = await executor.ExecuteResults(
                    _result,
                    _prompt,
                    workingDirectory,
                    uiControl: this,
                    token
                );
            }
            catch (Exception ex) {
                if (STDOut != null && !STDOut.IsDisposed)
                    STDOut.AppendText("\n Result execution failed: "+ex.Message + "\n");
            }

            if (exitBool)
            {
                SuccessfullyExecuted = true;
            }

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
