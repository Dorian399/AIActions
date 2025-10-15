using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIActions.Windows.SettingsControls
{
    public partial class SettingsTabButton : UserControl
    {
        private List<Panel> _borderPanels = new List<Panel>();
        private DockStyle[] _borderAlignments = {DockStyle.Top, DockStyle.Left,DockStyle.Bottom,DockStyle.Right};
        private int _borderHoverThickness = 1;
        private int _borderActiveThickness = 1;
        private bool _isActive = false;
        private Color _defaultBackColor;
        private TabControl? _tabControl;

        public TabControl? TabControl
        {
            get => _tabControl;
            set
            {
                if (_tabControl != null)
                {
                    _tabControl.SelectedIndexChanged -= TabControl_SelectedIndexChanged;
                }

                _tabControl = value;

                if (_tabControl != null)
                {
                    _tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
                }

            }
        }
        public Color HoverBackgroundColor { get; set; } = Color.FromArgb(223,236,249);
        public Color BorderColor { get; set; } = SystemColors.Highlight;
        public Color ActiveBackgroundColor { get; set; } = SystemColors.GradientInactiveCaption;
        public Image Image { get { return pictureBox1.Image; } set { pictureBox1.Image = value; } }
        public Padding ImageMargin { get { return pictureBox1.Margin; } set { pictureBox1.Margin = value; } }
        public string LabelText { 
            get 
            { return label1.Text; } 
            set { label1.Text = value; } 
        }
        public SettingsTabButton()
        {
            InitializeComponent();

            // Create border
            for(int i=0;i<_borderAlignments.Length;i++)
            {
                Panel borderPanel = new Panel();
                borderPanel.Dock = _borderAlignments[i];
                if( i%2 == 0 )
                    borderPanel.Size = new Size(Size.Width,_borderHoverThickness);
                else
                    borderPanel.Size= new Size(_borderHoverThickness, Size.Height);
                borderPanel.BackColor = BorderColor;
                borderPanel.Visible = false;
                borderPanel.Enabled = false;
                this.Controls.Add(borderPanel);
                borderPanel.BringToFront();
                _borderPanels.Add(borderPanel);
            }

            _defaultBackColor = BackColor;

            MouseEnter += SettingsTabButton_MouseEnter;
            MouseLeave += SettingsTabButton_MouseLeave;
            MouseClick += SettingsTabButton_MouseClick;

            pictureBox1.MouseEnter += SettingsTabButton_MouseEnter;
            pictureBox1.MouseLeave += SettingsTabButton_MouseLeave;
            pictureBox1.MouseClick += SettingsTabButton_MouseClick;

            label1.MouseEnter += SettingsTabButton_MouseEnter;
            label1.MouseLeave += SettingsTabButton_MouseLeave;
            label1.MouseClick += SettingsTabButton_MouseClick;

        }

        private void SettingsTabButton_Load(object sender, EventArgs e)
        {
            TabControl_SelectedIndexChanged(this, EventArgs.Empty);
        }

        private void SetBorderThickness(int thickness)
        {
            foreach(Panel panel in _borderPanels)
            {
                if(panel.Dock == DockStyle.Top || panel.Dock == DockStyle.Bottom)
                    panel.Size = new Size(Size.Width,thickness);
                else
                    panel.Size = new Size(thickness, Size.Height);
            }
        }

        private void SetBorderVisible(bool visible)
        {
            foreach (Panel panel in _borderPanels)
            {
                panel.Visible = visible;
            }
        }

        private void SettingsTabButton_MouseEnter(object? sender, EventArgs e)
        {
            if (_isActive)
                return;
            SetBorderThickness(_borderHoverThickness);
            SetBorderVisible(true);
            BackColor = HoverBackgroundColor;
        }

        private void SettingsTabButton_MouseLeave(object? sender, EventArgs e)
        {
            if (_isActive)
            {
                BackColor = ActiveBackgroundColor;
                SetBorderThickness(_borderActiveThickness);
                SetBorderVisible(true);
            }
            else 
            {
                SetBorderVisible(false);
                BackColor = _defaultBackColor;
            }
        }

        private void SettingsTabButton_MouseClick(object? sender, MouseEventArgs e)
        {
            if (TabControl == null)
                return;

            foreach(TabPage page in TabControl.TabPages)
            {
                if (page.Name == label1.Text)
                {
                    TabControl.SelectedIndex = TabControl.TabPages.IndexOf(page);
                }
            }
        }

        private void TabControl_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (TabControl == null)
                return;
            TabPage? curTab = TabControl.SelectedTab;
            if (curTab != null && curTab.Name == label1.Text)
            {
                _isActive = true;
                SetBorderThickness(_borderActiveThickness);
                SetBorderVisible(true);
                BackColor = ActiveBackgroundColor;
            }
            else
            {
                _isActive = false;
                SetBorderVisible(false);
                BackColor= _defaultBackColor;
            }
        }
    }
}
