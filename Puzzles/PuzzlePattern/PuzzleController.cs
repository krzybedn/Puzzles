using System;

namespace Puzzles.PuzzlePattern
{

    public delegate void SolvedDelegate();

    public abstract class PuzzleController
    {
        #region Instance
        static PuzzleController instance;
        public static PuzzleController Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        protected int n, m;
        protected Edge edges;
        protected Field fields;
        protected Point points;
        protected Puzzle puzzle;

        public event SolvedDelegate SolvedEvent;

        public PuzzleController()
        {
            instance = this;
            this.puzzle = Puzzle.Instance;
            this.n = puzzle.N;
            this.m = puzzle.M;
            this.edges = puzzle.Edges;
            this.fields = puzzle.Fields;
            this.points = puzzle.Points;
        }

        public void Handler(object sender, System.Windows.Forms.MouseEventArgs args)
        {
            MouseButton click;
            switch(args.Button)
            {
                case System.Windows.Forms.MouseButtons.Left:
                    click = MouseButton.Left;
                    break;
                case System.Windows.Forms.MouseButtons.Right:
                    click = MouseButton.Right;
                    break;
                case System.Windows.Forms.MouseButtons.Middle:
                    click = MouseButton.Middle;
                    break;
                default:
                    click = MouseButton.None;
                    break;
            }
            Char spliter = ' ';
            String name = ((System.Windows.Forms.PictureBox)sender).Name;
            String[] splited = name.Split(spliter);
            if(splited.Length < 3)
                throw new Exception("Uknown button type");
            String typeName = splited[0];
            int x = Int32.Parse(splited[1]);
            int y = Int32.Parse(splited[2]);
            if (!EditMode)
            {
                MakeChange_SolveMode(click, typeName, x, y, splited[3]);

                if (IsSolved())
                    SolvedEvent();
            }
            else
            {
                MakeChange_EditMode(click, typeName, x, y, splited[3]);
            }

        }

        public event ModelChangeDelegate ModelChangeEvent;
        protected void RefreshButton(ElementClickArgs args)
        {
            ModelChangeEvent(args);
        }


        public abstract void MakeChange_SolveMode(MouseButton click, string type, int x, int y, object additionalArg = null);
        public abstract void MakeChange_EditMode(MouseButton click, string type, int x, int y, object additionalArg = null);


        public abstract bool IsSolved();


        protected bool EditMode
        {
            get{ return puzzle.EditMode; }
        }
    }


    public class ElementClickArgs : EventArgs
    {
        public int x,y;
        public string typeName;
        public object additionalArgs;

        private ElementClickArgs() { }
        public ElementClickArgs(int x, int y, string typeName, object additionalArgs)
        {
            this.x = x;
            this.y = y;
            this.typeName = typeName;
            this.additionalArgs = additionalArgs;
        }
    }

    public class MouseButton
    {
        private string val;

        private MouseButton(string val)
        { this.val = val; }

        public static bool operator ==(MouseButton a, MouseButton b)
        {
            return a.val == b.val;
        }
        public static bool operator !=(MouseButton a, MouseButton b)
        {
            return a.val != b.val;
        }
        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            MouseButton p = obj as MouseButton;
            if ((System.Object)p == null)
            {
                return false;
            }

            return this.val == p.val;
        }
        public bool Equals(MouseButton p)
        {
            if ((object)p == null)
            {
                return false;
            }
            
            return this.val == p.val;
        }

        public override int GetHashCode()
        {
            return val.GetHashCode();
        }

        public static MouseButton Left
        { get { return new MouseButton("Left"); } }
        public static MouseButton Right
        { get { return new MouseButton("Right"); } }
        public static MouseButton Middle
        { get { return new MouseButton("Middle"); } }
        public static MouseButton None
        { get { return new MouseButton(""); } }
    }
}
