namespace AIActions
{
    public partial class PromptWindow : Form
    {
        public PromptWindow()
        {
            InitializeComponent();
        }

        private void PromptWindow_Load(object sender, EventArgs e)
        {

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
