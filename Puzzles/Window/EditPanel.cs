using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Puzzles.PuzzlePattern;

namespace Puzzles.Window
{
    public delegate void CreateNewPuzzleDelegate(PuzzleArgs args);
    public delegate void EditExistingDelegate(String args);


    class EditPanel : UserControl
    {
        Panel optionsPanel;
        NewPuzzleView newPanel;
        ExistingPuzzleView existingPanel;
        Panel editPanel;

        Panel actualPanel;

        new public MainForm Parent
        { get { return (MainForm)base.Parent; } }


        public EditPanel() : base()
        {
            this.Dock = DockStyle.Fill;
        }
        

        public void Initialize()
        {
            this.Size = this.Parent.Size;

            InitializeOptionPanel();
            actualPanel = optionsPanel;
            this.Controls.Add(actualPanel);
        }

        protected void InitializeOptionPanel()
        {
            optionsPanel = new Panel();
            optionsPanel.Name = "Opcje";
            optionsPanel.Dock = DockStyle.Fill;
            optionsPanel.Size = this.Size;


            PictureBox title;
            Button newButton;
            Button editButton;
            Button backButton;


            title = new PictureBox();
            title.Name = "Title";
            title.Size = new System.Drawing.Size(350, 70);
            title.Image = System.Drawing.Image.FromFile("Images/title.png");
            title.Left = (this.ClientSize.Width - title.Width) / 2;
            title.Top = (this.ClientSize.Height - title.Height - 150) / 2;
            title.SizeMode = PictureBoxSizeMode.StretchImage;

            optionsPanel.Controls.Add(title);


            newButton = new Button();
            newButton.Name = "New";
            newButton.Text = "Create new puzzle";
            newButton.Size = new System.Drawing.Size(150, 60);
            newButton.Left = (optionsPanel.ClientSize.Width - newButton.Width) / 2;
            newButton.Top = (optionsPanel.ClientSize.Height - newButton.Height + 15) / 2;
            newButton.MouseClick += ChoosedNew;

            optionsPanel.Controls.Add(newButton);


            editButton = new Button();
            editButton.Name = "Edit";
            editButton.Text = "Edit existing puzzle";
            editButton.Size = new System.Drawing.Size(150, 60);
            editButton.Left = (optionsPanel.ClientSize.Width - newButton.Width) / 2;
            editButton.Top = (optionsPanel.ClientSize.Height - newButton.Height + 165) / 2;
            editButton.MouseClick += ChoosedExisting;

            optionsPanel.Controls.Add(editButton);


            backButton = new Button();
            backButton.Name = "Back";
            backButton.Text = "Back";
            backButton.Size = new System.Drawing.Size(50, 30);
            backButton.Left = (optionsPanel.ClientSize.Width - backButton.Width - 10);
            backButton.Top = (optionsPanel.ClientSize.Height - backButton.Height - 10);
            backButton.MouseClick += ((MainForm)this.Parent).ChoosedMenu;
            backButton.MouseClick += this.ChoosedBack;

            optionsPanel.Controls.Add(backButton);
        }
        protected void InitializeNewPanel()
        {
            newPanel = new NewPuzzleView(this);
            newPanel.CreateNewPuzzleEvent += CreateNewPuzzleHandler;
        }
        protected void InitializeExistingPanel()
        {
            existingPanel = new ExistingPuzzleView(this);
            existingPanel.EditExistingEvent += EditExistingPuzzleHandler;
        }
        protected void InitializeEditPanel()
        {
            editPanel = new EditPuzzlePanel(this);

            this.Controls.Remove(actualPanel);
            actualPanel = editPanel;
            this.Controls.Add(actualPanel);
        }


        protected void ChoosedNew(object sender, MouseEventArgs args)
        {
            InitializeNewPanel();
            this.Controls.Remove(actualPanel);
            actualPanel = newPanel;
            this.Controls.Add(actualPanel);
        }
        protected void ChoosedExisting(object sender, MouseEventArgs args)
        {
            InitializeExistingPanel();
            this.Controls.Remove(actualPanel);
            actualPanel = existingPanel;
            this.Controls.Add(actualPanel);
        }
        public void ChoosedBack(object sender = null, MouseEventArgs args = null)
        {
            Parent.ChoosedMenu();

            this.Controls.Remove(actualPanel);
            actualPanel = optionsPanel;
            this.Controls.Add(actualPanel);
        }


        public void CreateNewPuzzleHandler(PuzzleArgs args)
        {
            Type puzzleType = (args.typeName);
            Activator.CreateInstance(puzzleType, true, args.height, args.width, args.name);

            InitializeEditPanel();

        }
        public void EditExistingPuzzleHandler(String name)
        {
            Puzzle.FromFile(name, true);
            InitializeEditPanel();
        }


        public void MakeResize(ResizeArgs args)
        {
            this.Size = new System.Drawing.Size(args.width, args.height);
        }
    }

    class NewPuzzleView : Panel
    {
        PictureBox title;
        Button createButton;
        Button backButton;
        Label nameLabel;
        TextBox nameText;
        Label heightLabel;
        TextBox heightText;
        Label widthLabel;
        TextBox widthText;
        Label typeLabel;
        ComboBox typeList;

        EditPanel parent;

        public NewPuzzleView(EditPanel parent) : base()
        {
            this.Name = "New";
            this.Dock = DockStyle.Fill;
            this.parent = parent;
            this.Size = parent.Size;

            InitailizeControls();
        }

        private void InitailizeControls()
        {
            title = new PictureBox();
            title.Name = "Title";
            title.Size = new System.Drawing.Size(350, 70);
            title.Image = System.Drawing.Image.FromFile("Images/title.png");
            title.Left = (this.ClientSize.Width - title.Width) / 2;
            title.Top = 20;
            title.SizeMode = PictureBoxSizeMode.StretchImage;

            this.Controls.Add(title);

            
            createButton = new Button();
            createButton.Name = "Create";
            createButton.Text = "Create new puzzle";
            createButton.Size = new System.Drawing.Size(100, 60);
            createButton.Left = 275;
            createButton.Top = 110;
            createButton.MouseClick += CreateButtonHandle;

            this.Controls.Add(createButton);

            
            backButton = new Button();
            backButton.Name = "Back";
            backButton.Text = "Back";
            backButton.Size = new System.Drawing.Size(100, 60);
            backButton.Left = 275;
            backButton.Top = 190;
            backButton.MouseClick += ((MainForm)parent.Parent).ChoosedMenu;
            backButton.MouseClick += parent.ChoosedBack;

            this.Controls.Add(backButton);

            
            nameLabel = new Label();
            nameLabel.Name = "NameLabel";
            nameLabel.Text = "Name";
            nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            nameLabel.Size = new System.Drawing.Size(50, 20);
            nameLabel.Left = 10;
            nameLabel.Top = 110;

            this.Controls.Add(nameLabel);
            
        
            nameText = new TextBox();
            nameText.Name = "NameText";
            nameText.Text = "Name";
            nameText.Size = new System.Drawing.Size(125, 20);
            nameText.Left = 75;
            nameText.Top = 110;

            this.Controls.Add(nameText);

            
            heightLabel = new Label();
            heightLabel.Name = "HeightLabel";
            heightLabel.Text = "Height";
            heightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            heightLabel.Size = new System.Drawing.Size(50, 20);
            heightLabel.Left = 10;
            heightLabel.Top = 150;

            this.Controls.Add(heightLabel);

            
            heightText = new TextBox();
            heightText.Name = "HeightText";
            heightText.Text = "0";
            heightText.Size = new System.Drawing.Size(125, 20);
            heightText.Left = 75;
            heightText.Top = 150;

            this.Controls.Add(heightText);

            
            widthLabel = new Label();
            widthLabel.Name = "WidthLabel";
            widthLabel.Text = "Width";
            widthLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            widthLabel.Size = new System.Drawing.Size(50, 20);
            widthLabel.Left = 10;
            widthLabel.Top = 190;

            this.Controls.Add(widthLabel);

            
            widthText = new TextBox();
            widthText.Name = "WidthText";
            widthText.Text = "0";
            widthText.Size = new System.Drawing.Size(125, 20);
            widthText.Left = 75;
            widthText.Top = 190;

            this.Controls.Add(widthText);

            
            typeLabel = new Label();
            typeLabel.Name = "TypeLabel";
            typeLabel.Text = "Type";
            typeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            typeLabel.Size = new System.Drawing.Size(50, 20);
            typeLabel.Left = 10;
            typeLabel.Top = 230;

            this.Controls.Add(typeLabel);

            
            typeList = new ComboBox();
            typeList.Name = "TypeList";
            typeList.Text = "Type";
            typeList.Size = new System.Drawing.Size(125, 20);
            typeList.Left = 75;
            typeList.Top = 230;

            foreach (PuzzleType x in SubclassesOfPuzzle.subclasses)
                typeList.Items.Add(x);

            this.Controls.Add(typeList);
        }

        public event CreateNewPuzzleDelegate CreateNewPuzzleEvent;

        public void CreateButtonHandle(object sender, MouseEventArgs args)
        {
            if (typeList.SelectedItem == null)
            {
                NoType();
                return;
            }

            int height = Int32.Parse(heightText.Text);
            int width = Int32.Parse(widthText.Text);
            String name = nameText.Text;
            Type choosedType = ((PuzzleType)typeList.SelectedItem).Type;

            if (width <= 0)
                NoWidth();
            else if (height <= 0)
                NoHeight();
            else if ((width>25 || height>25) && !BigSize())
                return;
            else if (!((MainForm)Parent.Parent).readyPuzzles.ContainsKey(name) || DuplicateName())
                CreateNewPuzzleEvent(new PuzzleArgs(height, width, choosedType, name));
        }

        private void NoType()
        {
            DialogResult message = MessageBox.Show("Choose type of puzzle.", 
                                "No puzzle type",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
        }
        private void NoWidth()
        {
            DialogResult message = MessageBox.Show("Can't creaate puzzle without width.",
                                "Bad width value",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
        }
        private void NoHeight()
        {
            DialogResult message = MessageBox.Show("Can't creaate puzzle without height.",
                                "Bad height value",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
        }
        private bool BigSize()
        {
            DialogResult message = MessageBox.Show(
                                "Are you sure you want to create so big puzzle? It can be too big to show on screen",
                                "Big size",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning);

            if (message == DialogResult.Yes)
                return true;
            else
                return false;
        }
        private bool DuplicateName()
        {
            DialogResult message = MessageBox.Show(
                                "Puzzle with this name already exist, do you realy want to override it?",
                                "Duplicate name",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning);

            if (message == DialogResult.Yes)
                return true;
            else
                return false;
        }

    }

    class ExistingPuzzleView : Panel
    {
        PictureBox title;
        Button editButton;
        Button deleteButton;
        Button backButton;
        Label listLabel;
        ListBox puzzlesList;

        Label widthLabel;
        Label widthText;
        Label heightLabel;
        Label heightText;
        Label typeLabel;
        Label typeText;

        public EditPanel parent;

        public ExistingPuzzleView(EditPanel parent) : base()
        {
            this.Name = "Edit";
            this.Dock = DockStyle.Fill;
            this.parent = parent;
            this.Size = parent.Size;

            InitailizeConstrols();
            puzzlesList.SelectedValueChanged += ChoosedChange;
        }


        private void InitailizeConstrols()
        {
            title = new PictureBox();
            title.Size = new System.Drawing.Size(350, 70);
            title.Image = System.Drawing.Image.FromFile("Images/title.png");
            title.Left = (this.ClientSize.Width - title.Width) / 2;
            title.Top = 20;
            title.SizeMode = PictureBoxSizeMode.StretchImage;

            this.Controls.Add(title);


            editButton = new Button();
            editButton.Name = "Edit";
            editButton.Text = "Edit";
            editButton.Size = new System.Drawing.Size(100, 40);
            editButton.Left = 275;
            editButton.Top = 105;
            editButton.MouseClick += EditButtonClick;

            this.Controls.Add(editButton);


            deleteButton = new Button();
            deleteButton.Name = "Delete";
            deleteButton.Text = "Delete";
            deleteButton.Size = new System.Drawing.Size(100, 40);
            deleteButton.Left = 275;
            deleteButton.Top = 155;
            deleteButton.MouseClick += DeleteButtonClick;

            this.Controls.Add(deleteButton);


            backButton = new Button();
            backButton.Name = "Back";
            backButton.Text = "Back";
            backButton.Size = new System.Drawing.Size(100, 40);
            backButton.Left = 275;
            backButton.Top = 205;
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

        public event EditExistingDelegate EditExistingEvent;

        public void EditButtonClick(object sender, MouseEventArgs args)
        {
            if(puzzlesList.SelectedItem == null)
            {
                NoPuzzle();
                return;
            }
            EditExistingEvent( "ReadyPuzzles/" + ((String)puzzlesList.SelectedItem) + ".puz");
        }
        public void DeleteButtonClick(object sender, MouseEventArgs args)
        {
            if (puzzlesList.SelectedItem == null)
                return;

            DialogResult msg = MessageBox.Show(
                                    "Are you sure you want to delete that puzzle? That can't be undone.", 
                                    "Warining", 
                                    MessageBoxButtons.YesNo, 
                                    MessageBoxIcon.Warning);
                
            if (msg == DialogResult.Yes)
            {
                parent.Parent.readyPuzzles.Remove((String)puzzlesList.SelectedItem);
                System.IO.File.Delete("ReadyPuzzles/" + ((String)puzzlesList.SelectedItem) + ".puz");
                puzzlesList.Items.Remove((String)puzzlesList.SelectedItem);
                puzzlesList.Refresh();
            }
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

        private void NoPuzzle()
        {
            DialogResult message = MessageBox.Show("First choose any puzzle.", 
                                "No puzzle choosed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Question);
        }
    }

    class EditPuzzlePanel : Panel
    {
        int button_size;
        EditPanel parent;

        PuzzlePanel gamePanel;
        Button doneButton;
        Button backButton;

        public EditPuzzlePanel(EditPanel parent) : base()
        {
            this.parent = parent;
            this.Name = "Edit";
            this.Dock = DockStyle.Fill;
            this.Location = new System.Drawing.Point(0, 0);

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


            this.Controls.Add(gamePanel);

            doneButton = new Button();
            doneButton.Name = "Done";
            doneButton.Text = "Done";
            doneButton.Size = new System.Drawing.Size(90, 50);
            doneButton.Left = (this.Size.Width - doneButton.Width - 10);
            doneButton.Top = 10;
            doneButton.MouseClick += this.ChoosedDone;

            this.Controls.Add(doneButton);


            backButton = new Button();
            doneButton.Name = "Back";
            backButton.Text = "Back";
            backButton.Size = new System.Drawing.Size(90, 50);
            backButton.Left = (this.Size.Width - backButton.Width - 10);
            backButton.Top = (this.Size.Height - backButton.Height - 10);
            backButton.MouseClick += this.ChoosedBack;

            this.Controls.Add(backButton);
        }

        public void ChoosedDone(object sender, MouseEventArgs args)
        {
            if(!PuzzleController.Instance.IsSolved())
            {
                DialogResult message = MessageBox.Show("Puzzle that you want to save is incorrect.",
                                        "Incorrect puzzle",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                return;
            }
            Puzzle.Instance.ToFile(); 
            parent.Parent.readyPuzzles[Puzzle.Instance.Name] = Puzzle.Instance.Info;

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


    public class PuzzleArgs : EventArgs
    {
        public int height, width;
        public Type typeName;
        public String name;

        private PuzzleArgs() { }
        public PuzzleArgs(int height, int width, Type typeName, String name)
        {
            this.height = height;
            this.width = width;
            this.typeName = typeName;
            this.name = name;
        }
    }

}
