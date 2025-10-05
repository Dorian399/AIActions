using AIActions.Configs;
using AIActions.Windows;
using System.Diagnostics;

namespace AIActions
{
    public partial class PromptWindow : Form
    {

        private string _currentFolderOrFile;
        private ParsedConfig _currentConfig;
        public PromptWindow(string? folderOrFile,ParsedConfig? configFile)
        {
            _currentFolderOrFile = folderOrFile;
            _currentConfig = configFile;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e) // Makes it so the window is manually shown.
        {
            Visible = false;
            ShowInTaskbar = false;
            Opacity = 0;

            base.OnLoad(e);
        }

        public void MakePopup()
        {
            ShowInTaskbar = true;
            Opacity = 255;
            this.Show();
        }

        private bool CheckData()
        {
            if (!Path.Exists(_currentFolderOrFile))
            {
                MessageBox.Show("Missing file or folder. Make sure to run this program from the context menu (Right click).");
                this.Close();
                return false;
            }
            if (_currentConfig == null)
            {
                ConfigsListWindow configsListWindow = new ConfigsListWindow();
                configsListWindow.Show();
                return false;
            }
            return true;
        }

        public void UpdateData(string? folderOrFile=null, ParsedConfig? configFile=null, bool shouldPopUp=false)
        {
            if(folderOrFile != null)
                _currentFolderOrFile = folderOrFile;
            if(configFile!=null)
                _currentConfig = configFile;

            if (CheckData())
            {
                this.Text += " " + _currentFolderOrFile;
                if(shouldPopUp)
                    this.MakePopup();
            }
            else
            {
                this.Close();
            }
        }

        private void PromptWindow_Load(object sender, EventArgs e)
        {
            if(CheckData())
            {
                this.Text += " "+_currentFolderOrFile; 
                this.MakePopup();
            }
        }

        private void RunAction_Click(object sender, EventArgs e)
        {
            string promptText = PromptBox.Text;
            ExecutionWindow ExecWin = new ExecutionWindow(promptText,_currentConfig,_currentFolderOrFile);
            ExecWin.ParentWindow = this;
            this.Hide();
            ExecWin.Show();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            // TO DO: Launch actual settings window.
        }

        private void PromptBox_EnterPressed(object sender, EventArgs e)
        {
            RunAction_Click(sender, e);
        }
    }
}
