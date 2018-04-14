using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using Puzzles.PuzzlePattern;

namespace Puzzles.Window
{
    public delegate void SolvePuzzleDelegate(PuzzleArgs args);
    public delegate void ChoosedPuzzleDelegate(String args);
    
    class PlayPanel : UserControl
    {
        ChoosePuzzlePanel choosePanel;
        SolvePanel gamePanel;
        Panel actualPanel;

        new public MainForm Parent
        { get { return (MainForm)base.Parent; } }

        public PlayPanel() : base()
        {
            this.Dock = DockStyle.Fill;

            this.VisibleChanged += Activate;
        }


        public void Initialize()
        {
            this.Size = this.Parent.Size;
        }

        protected void InitializeChoosePanel()
        {
            choosePanel = new ChoosePuzzlePanel(this);
            choosePanel.ChoosedPuzzleEvent += ChoosedPuzzleHandler;
        }
        
        public void ChoosedPuzzleHandler(String name)
        {
            Puzzle.FromFile(name, false);

            gamePanel = new SolvePanel(this);

            this.Controls.Remove(actualPanel);
            actualPanel = gamePanel;
            this.Controls.Add(actualPanel);
        }

        public void Activate(object sender, EventArgs args)
        {
            if(this.Visible == true)
            {
                this.Controls.Remove(actualPanel);
                InitializeChoosePanel();
                 actualPanel = choosePanel;
                this.Controls.Add(actualPanel);
            }
        }
        public void ChoosedBack(object sender = null, MouseEventArgs args = null)
        {
            Parent.ChoosedMenu();
        }
        public void MakeResize(ResizeArgs args)
        {
            this.Size = new System.Drawing.Size(args.width, args.height);
        }

    }

    class ChoosePuzzlePanel : Panel
    {
        PictureBox title;
        Button solveButton;
        Button backButton;
        Label listLabel;
        ListBox puzzlesList;

        Label widthLabel;
        Label widthText;
        Label heightLabel;
        Label heightText;
        Label typeLabel;
        Label typeText;

        public PlayPanel parent;

        public ChoosePuzzlePanel(PlayPanel parent)
        {
            this.Name = "Edit";
            this.Dock = DockStyle.Fill;
            this.parent = parent;
            this.Size = parent.Size;

            Initailize();
            puzzlesList.SelectedValueChanged += ChoosedChange;
        }


        private void Initailize()
        {
            title = new PictureBox();
            title.Size = new System.Drawing.Size(350, 70);
            title.Image = System.Drawing.Image.FromFile("Images/title.png");
            title.Left = (this.ClientSize.Width - title.Width) / 2;
            title.Top = 20;
            title.SizeMode = PictureBoxSizeMode.StretchImage;

            this.Controls.Add(title);


            solveButton = new Button();
            solveButton.Name = "Solve";
            solveButton.Text = "Solve";
            solveButton.Size = new System.Drawing.Size(100, 60);
            solveButton.Left = 275;
            solveButton.Top = 110;
            solveButton.MouseClick += SolveButtonClick;

            this.Controls.Add(solveButton);


            backButton = new Button();
            backButton.Name = "Back";
            backButton.Text = "Back";
            backButton.Size = new System.Drawing.Size(100, 60);
            backButton.Left = 275;
            backButton.Top = 190;
            backButton.MouseClick += parent.Parent.ChoosedMenu;
            backButton.MouseClick += parent.ChoosedBack;

            this.Controls.Add(backButton);


            listLabel = new Label();
            listLabel.Name = "Type";
            listLabel.Text = "Choose puzzle";
            listLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            listLabel.Size = new System.Drawing.Size(125, 20);
            listLabel.Left = 10;
            listLabel.Top = 105;

            this.Controls.Add(listLabel);


            puzzlesList = new ListBox();
            puzzlesList.Name = "Puzzles";
            puzzlesList.Text = "Puzzles";
            puzzlesList.Size = new System.Drawing.Size(125, 120);
            puzzlesList.Left = 10;
            puzzlesList.Top = 130;

            foreach (KeyValuePair<String, PuzzleData> elem in parent.Parent.readyPuzzles)
                puzzlesList.Items.Add(elem.Key);

            this.Controls.Add(puzzlesList);


            typeLabel = new Label();
            typeLabel.Text = "Type";
            typeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            typeLabel.Size = new System.Drawing.Size(100, 20);
            typeLabel.Left = 150;
            typeLabel.Top = 105;

            this.Controls.Add(typeLabel);


            typeText = new Label();
            typeText.Text = "";
            typeText.BorderStyle = BorderStyle.Fixed3D;
            typeText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            typeText.Size = new System.Drawing.Size(100, 20);
            typeText.Left = 150;
            typeText.Top = 130;

            this.Controls.Add(typeText);


            heightLabel = new Label();
            heightLabel.Text = "Height";
            heightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            heightLabel.Size = new System.Drawing.Size(100, 20);
            heightLabel.Left = 150;
            heightLabel.Top = 155;

            this.Controls.Add(heightLabel);


            heightText = new Label();
            heightText.Text = "";
            heightText.BorderStyle = BorderStyle.Fixed3D;
            heightText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            heightText.Size = new System.Drawing.Size(100, 20);
            heightText.Left = 150;
            heightText.Top = 180;

            this.Controls.Add(heightText);


            widthLabel = new Label();
            widthLabel.Text = "Width";
            widthLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            widthLabel.Size = new System.Drawing.Size(100, 20);
            widthLabel.Left = 150;
            widthLabel.Top = 205;

            this.Controls.Add(widthLabel);


            widthText = new Label();
            widthText.Text = "";
            widthText.BorderStyle = BorderStyle.Fixed3D;
            widthText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            widthText.Size = new System.Drawing.Size(100, 20);
            widthText.Left = 150;
            widthText.Top = 230;

            this.Controls.Add(widthText);
        }

        public event ChoosedPuzzleDelegate ChoosedPuzzleEvent;

        public void SolveButtonClick(object sender, MouseEventArgs args)
        {
            if (puzzlesList.SelectedItem == null)
            {
                NoPuzzle();
                return;
            }
            ChoosedPuzzleEvent("ReadyPuzzles/" + ((String)puzzlesList.SelectedItem) + ".puz");
        }

        private void NoPuzzle()
        {
            DialogResult message = MessageBox.Show("First choose any puzzle.",
                                "No puzzle choosed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
        }

        private void ChoosedChange(object sender, EventArgs args)
        {
            if (puzzlesList.SelectedItem != null)
            {
                PuzzleData choosed = parent.Parent.readyPuzzles[(String)puzzlesList.SelectedItem];
                heightText.Text = choosed.height.ToString();
                widthText.Text = choosed.width.ToString();
                typeText.Text = choosed.puzzleType.Name;
            }
        }
    }

    class SolvePanel : Panel
    {
        int button_size;
        PlayPanel parent;

        PuzzlePanel gamePanel;
        Button backButton;
        Label timeLabel;

        Timer timer;
        DateTime timeBegin;

        public SolvePanel(PlayPanel parent) : base()
        {
            this.parent = parent;
            this.Name = "Edit";
            this.Dock = DockStyle.Fill;
            this.Location = new System.Drawing.Point(0, 0);
            timeBegin = DateTime.Now;

            InitializeConstrols();
        }

        private void InitializeConstrols()
        {
            button_size = 10;

            gamePanel = Puzzle.Instance.View;
            gamePanel.Left = 0;
            gamePanel.Top = 0;

            int height = gamePanel.Height;
            if (height < 130)
                height = 130;
            parent.Parent.MakeResize(new ResizeArgs(height + 4 * button_size, gamePanel.Width + (int)(1.6 * button_size) + 110));
            this.Size = parent.Size;

            PuzzleController.Instance.SolvedEvent += EndHandler;
            

            this.Controls.Add(gamePanel);

            timeLabel = new Label();
            timeLabel.Name = "Time";
            timeLabel.Text = string.Format("{0}:{1:00}:{2:00}", 0, 0, 0);
            timeLabel.Font = new System.Drawing.Font(
                                new System.Drawing.FontFamily(System.Drawing.Text.GenericFontFamilies.Serif), 
                                15, 
                                System.Drawing.FontStyle.Bold);
            timeLabel.Size = new System.Drawing.Size(90, 50);
            timeLabel.Left = (this.Size.Width - timeLabel.Width - 10);
            timeLabel.Top = 10;

            this.Controls.Add(timeLabel);


            backButton = new Button();
            backButton.Name = "Back";
            backButton.Text = "Back";
            backButton.Size = new System.Drawing.Size(90, 50);
            backButton.Left = (this.Size.Width - backButton.Width - 10);
            backButton.Top = (this.Size.Height - backButton.Height - 10);
            backButton.MouseClick += this.ChoosedBack;

            this.Controls.Add(backButton);


            timer = new Timer();
            timer.Interval = 500;
            timer.Tick += TimeUpdate;
            timer.Start();
        }

        private void TimeUpdate(Object myObject, EventArgs myEventArgs)
        {
            TimeSpan duration = DateTime.Now - timeBegin;
            timeLabel.Text = string.Format("{0}:{1:00}:{2:00}", duration.Hours, duration.Minutes, duration.Seconds);
        }

        public void EndHandler()
        {
            timer.Stop();
            DialogResult message = MessageBox.Show(string.Format(
                                "Congratulations, you solved that puzzle in {0}!", timeLabel.Text),
                                "Congratulations",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.None);

            parent.ChoosedBack();
        }

        public void ChoosedBack(object sender, MouseEventArgs args)
        {
            DialogResult message = MessageBox.Show("Are you sure you want quit? Changes will not be saved.", 
                                "Warning",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning);

            if (message == DialogResult.Yes)
                parent.ChoosedBack();
        }
    }
}
