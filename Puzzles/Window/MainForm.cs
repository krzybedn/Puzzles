using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Puzzles.PuzzlePattern;

namespace Puzzles.Window
{
    public delegate void MakeResizeDelegate(ResizeArgs args);

    class MainForm : Form
    {
        PlayPanel levelSolvePanel;
        EditPanel levelEditorPanel;
        Panel menuPanel;

        public Dictionary<String, PuzzleData> readyPuzzles;

        public MainForm() : base()
        {
            this.Text = "Puzzles";
            this.Size = new System.Drawing.Size(400, 300);
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;


            Initialize();


            InitiateMenuPanel();
            
            levelEditorPanel = new EditPanel();
            this.Controls.Add(levelEditorPanel);
            levelEditorPanel.Initialize();
            levelEditorPanel.Visible = false;

            levelSolvePanel = new PlayPanel();
            this.Controls.Add(levelSolvePanel);
            levelSolvePanel.Initialize();
            levelSolvePanel.Visible = false;
            

            MakeResizeEvent += MakeResize;
            MakeResizeEvent += levelEditorPanel.MakeResize;
            MakeResizeEvent += levelSolvePanel.MakeResize;

            this.FormClosing += MainForm_FormClosing;
        }

        private void Initialize()
        {
            readyPuzzles = new Dictionary<string, PuzzleData>();
            System.IO.StreamReader file = new System.IO.StreamReader("ReadyPuzzles/List.ls");
            while (!file.EndOfStream)
            {
                PuzzleData newElem = new PuzzleData(file.ReadLine());
                readyPuzzles.Add(newElem.name, newElem);
            }
            file.Close();
        }

        private void InitiateMenuPanel()
        {
            Button playButton;
            Button editButton;
            PictureBox title;

            menuPanel = new Panel();
            this.Controls.Add(menuPanel);
            menuPanel.Size = this.Size;
            menuPanel.Dock = DockStyle.Fill;

            title = new PictureBox();
            title.Size = new System.Drawing.Size(350, 70);
            title.Image = System.Drawing.Image.FromFile("Images/title.png");
            title.Left = (this.ClientSize.Width - title.Width) / 2;
            title.Top = (this.ClientSize.Height - title.Height - 150) / 2;
            title.SizeMode = PictureBoxSizeMode.StretchImage;

            menuPanel.Controls.Add(title);


            playButton = new Button();
            playButton.Text = "Play!";
            playButton.Size = new System.Drawing.Size(150, 60);
            playButton.Left = (this.ClientSize.Width - playButton.Width) / 2;
            playButton.Top = (this.ClientSize.Height - playButton.Height + 15) / 2;
            playButton.MouseClick += playButton_Click;

            menuPanel.Controls.Add(playButton);


            editButton = new Button();
            editButton.Text = "Edit";
            editButton.Size = new System.Drawing.Size(150, 60);
            editButton.Left = (this.ClientSize.Width - editButton.Width) / 2;
            editButton.Top = (this.ClientSize.Height - editButton.Height + 165) / 2;
            editButton.MouseClick += editButton_Click;

            menuPanel.Controls.Add(editButton);
        }


        private void playButton_Click(object sender, EventArgs e)
        {
            menuPanel.Visible = false;
            levelSolvePanel.Visible = true;
        }
        private void editButton_Click(object sender, EventArgs e)
        {
            menuPanel.Visible = false;
            levelEditorPanel.Visible = true;
        }

        public void ChoosedMenu(object sender = null, EventArgs e = null)
        {
            MakeResizeEvent(new ResizeArgs(300, 400));
            levelEditorPanel.Visible = false;
            levelSolvePanel.Visible = false;
            menuPanel.Visible = true;
        }


        public event MakeResizeDelegate MakeResizeEvent;
        public void MakeResize(ResizeArgs args)
        {
            this.Size = new System.Drawing.Size(args.width, args.height);
        }

        private void MainForm_FormClosing(Object sender, FormClosingEventArgs e)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter("readyPuzzles/List.ls");
            foreach(KeyValuePair<String,PuzzleData> elem in readyPuzzles)
            {
                file.WriteLine(elem.Value);
            }
            file.Close();
        }
    }

    public class ResizeArgs : EventArgs
    {
        public int height;
        public int width;
        public ResizeArgs(int height, int width)
        {
            this.width = width;
            this.height = height;
        }
    }
}
