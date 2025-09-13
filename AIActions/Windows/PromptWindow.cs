using AIActions.Configs;
using System.Diagnostics;

namespace AIActions
{
    public partial class PromptWindow : Form
    {
        public bool ShouldHide = false;

        private string _currentFolder;
        private ParsedConfig _currentConfig;
        public PromptWindow(string? folderOrFile,ParsedConfig? configFile)
        {
            _currentFolder = folderOrFile;
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

        private int PromptWindow_CheckData()
        {
            int code = 0;
            if (!Path.Exists(_currentFolder))
            {
                MessageBox.Show("Missing file or folder. Make sure to run this program from the context menu (Right click).");
                this.Close();
                code+=2;
            }
            if (_currentConfig == null)
            {
                // Redirect to config chooser.
                code+=4;
            }
            return code;
        }

        private void PromptWindow_UpdateData(string? folderOrFile, ParsedConfig? configFile)
        {
            _currentFolder = folderOrFile;
            _currentConfig = configFile;
        }

        private void PromptWindow_Load(object sender, EventArgs e)
        {
            int errorCode = PromptWindow_CheckData();
            if(errorCode == 0)
            {
                this.MakePopup();
            }
        }

        private void RunAction_Click(object sender, EventArgs e)
        {
            string promptText = PromptBox.Text;
            ExecutionWindow ExecWin = new ExecutionWindow(promptText);
            ExecWin.ParentWindow = this;
            this.Hide();
            ExecWin.Show();
        }

        private void Settings_Click(object sender, EventArgs e)
        {

        }

        private void PromptBox_EnterPressed(object sender, EventArgs e)
        {
            RunAction_Click(sender, e);
        }
    }
}
