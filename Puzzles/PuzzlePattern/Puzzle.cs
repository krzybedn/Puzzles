using System;

namespace Puzzles.PuzzlePattern
{
    public delegate void ModelChangeDelegate(ElementClickArgs args);


    public class Puzzle
    {
        protected int n, m;
        protected Point points;
        protected Edge edges;
        protected Field fields;
        protected PuzzlePanel view;
        protected PuzzleController controller;
        protected bool editMode;
        protected String name;

        protected PuzzleData shortInfo;

        protected int button_size;

        #region Instance
        static Puzzle instance;
        public static Puzzle Instance
        {
            get
            {
                if (instance == null)
                    throw new Exception("No Puzzle construktor called");

                return instance;
            }
        }
        #endregion
        
        public Puzzle(bool editMode, int n, int m, String name)
        {
            instance = this;
            this.n = n;
            this.m = m;
            this.editMode = editMode;
            this.name = name;
            this.button_size = 10;
        }

        public Puzzle(bool editMode, string data)
        {
            string[] elementsSeparator = new string[] {"\n{nextElement}\n"};
            string[] nameSeparator = new string[] { "\n{nameEnd}\n" };
            
            string[] elements = data.Split(new [] { "\n{nextElement}\n" }, StringSplitOptions.None);
            string[] sizes = (elements[0]).Split(' ');
            string[] pointsData = elements[2].Split(nameSeparator, StringSplitOptions.None);
            string[] edgesData = elements[3].Split(nameSeparator, StringSplitOptions.None);
            string[] fieldsData = elements[4].Split(nameSeparator, StringSplitOptions.None);
            

            instance = this;
            this.n = Int32.Parse(sizes[0]);
            this.m = Int32.Parse(sizes[1]);
            this.name = sizes[2];
            this.editMode = editMode;
            this.button_size = 10;

            
            Initialize(
                    newController: (PuzzleController)Activator.CreateInstance(Type.GetType(elements[1])),
                    pointElement: (Point)Activator.CreateInstance(Type.GetType(pointsData[0]), pointsData[1]),
                    edgeElement: (Edge)Activator.CreateInstance(Type.GetType(edgesData[0]), edgesData[1]),
                    fieldElement: (Field)Activator.CreateInstance(Type.GetType(fieldsData[0]), fieldsData[1]));
            
        }

        protected void Initialize(
                PuzzleController newController,
                Point pointElement = null,
                Edge edgeElement = null,
                Field fieldElement = null)
        {
            shortInfo = new PuzzleData(name, this.GetType(), n, m);
            

            if (pointElement == null)
                points = new BaseElements.BasePoint();
            else
                points = pointElement;
            if (edgeElement == null)
                edges = new BaseElements.BaseEdge();
            else
                edges = edgeElement;
            if (fieldElement == null)
                fields = new BaseElements.BaseField();
            else
                fields = fieldElement;
            controller = newController;

            view = new PuzzlePanel();
            controller.ModelChangeEvent += view.Refresh;
        }

        public override string ToString()
        {
            string result;
            result = this.GetType().FullName + "\n";
            result += n + " " + m + " " + name + "\n";
            result += "{nextElement}\n" + controller + "\n";
            result += "{nextElement}\n" + points.GetType().FullName + "\n{nameEnd}\n" + points.ToString() + "\n";
            result += "{nextElement}\n" + edges.GetType().FullName + "\n{nameEnd}\n" + edges.ToString() + "\n";
            result += "{nextElement}\n" + fields.GetType().FullName + "\n{nameEnd}\n" + fields.ToString() + "\n";

            return result;
        }
            
        public void ToFile()
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter("ReadyPuzzles/"+name+".puz");
            file.WriteLine(ToString());
            file.Close();
        }

        public static Puzzle FromFile(string fileName, bool editMode)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(fileName);
            string typeName = sr.ReadLine();
            string data = sr.ReadToEnd();

            sr.Close();

            Puzzle res = (Puzzle)Activator.CreateInstance(Type.GetType(typeName), editMode, data);
            return res;
        }
        
        public int N
        { get { return n; } }
        public int M
        { get { return m; } }
        public String Name
        { get { return name; } }
        public int ButtonSize
        { get { return button_size; } }
        public Edge Edges
        { get { return edges; } }
        public Field Fields
        { get { return fields; } }
        public Point Points
        { get { return points; } }
        public PuzzlePanel View
        { get { return view; } }
        public bool EditMode
        {
            get { return editMode; }
            set { editMode = value; }
        }
        public PuzzleData Info
        { get { return shortInfo; } }
    }


    public class PuzzleData
    {
        public int height;
        public int width;
        public String name;
        public Type puzzleType;

        public PuzzleData(String name, Type type, int height, int width)
        {
            this.name = name;
            this.puzzleType = type;
            this.height = height;
            this.width = width;
        }

        public PuzzleData(String data)
        {
            String[] splited = data.Split(new string[] { " | " }, StringSplitOptions.None);
            this.name = splited[0];
            this.puzzleType = Type.GetType(splited[3]);
            this.height = Int32.Parse(splited[1]);
            this.width = Int32.Parse(splited[2]);
        }

        public override String ToString()
        {
            return name + " | " + height + " | " + width + " | " + puzzleType.FullName;
        }
    }
}