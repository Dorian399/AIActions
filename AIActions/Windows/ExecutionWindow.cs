using AIActions.AI;
using AIActions.AI.Results;
using AIActions.Configs;
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
            AIRequester requester = new AIRequester();
            requester.OnStatusChanged += ExecutionWindow_StatusChanged;
            await requester.SendPrompt(_configs,_folderOrFile, _prompt);
        }

        private void ExecutionWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
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
