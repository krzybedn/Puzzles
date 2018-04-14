using System;
using System.Windows.Forms;

namespace Puzzles.PuzzlePattern
{
    public class PuzzlePanel : Panel
    {
        #region Instance
        static PuzzlePanel instance;
        public static PuzzlePanel Instance
        {
            get
            {
                if (instance == null)
                    instance = new PuzzlePanel();

                return instance;
            }
        }
        #endregion

        private PictureBox[][] pictures;
        private int n, m;
        private EdgeView edges;
        private FieldView fields;
        private PointView points;

        private int button_size;

        public PuzzlePanel() : base()
        {
            instance = this;
            this.button_size = Puzzle.Instance.ButtonSize;
            this.n =  Puzzle.Instance.N;
            this.m =  Puzzle.Instance.M;
            this.edges = (EdgeView)Puzzle.Instance.Edges.View;
            this.fields = (FieldView)Puzzle.Instance.Fields.View;
            this.points = (PointView)Puzzle.Instance.Points.View;

            this.Size = new System.Drawing.Size(button_size * (m * 3 + 1), button_size * (n * 3 + 1));

            PreparePictures();
        }

        private void PreparePictures()
        {
            pictures = new PictureBox[2 * n + 1][];
            for (int i = 0; i <= 2 * n; i++)
                pictures[i] = new PictureBox[2 * m + 1];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    pictures[2 * i][2 * j] = SingleElementView.Show(points, i, j, i * 3 * button_size, j * 3 * button_size);
                    pictures[2 * i][2 * j + 1] = SingleElementView.Show(edges, i, j, i * 3 * button_size, j * 3 * button_size + button_size, false);
                    
                    pictures[2 * i + 1][2 * j] = SingleElementView.Show(edges, i, j, i * 3 * button_size + button_size, j * 3 * button_size, true);
                    pictures[2 * i + 1][2 * j + 1] = SingleElementView.Show(fields, i, j, i * 3 * button_size + button_size, j * 3 * button_size + button_size);
                }
                pictures[2 * i][2 * m] = SingleElementView.Show(points, i, m, i * 3 * button_size, m * 3 * button_size);
                pictures[2 * i + 1][2 * m] = SingleElementView.Show(edges, i, m, i * 3 * button_size + button_size, m * 3 * button_size, true);
            }

            for (int j = 0; j < m; j++)
            {
                pictures[2 * n][2 * j] = SingleElementView.Show(points, n, j, n * 3 * button_size, j * 3 * button_size);
                pictures[2 * n][2 * j + 1] = SingleElementView.Show(edges, n, j, n * 3 * button_size, j * 3 * button_size + button_size, false);
            }

            pictures[2 * n][2 * m] = SingleElementView.Show(points, n, m, n * 3 * button_size, m * 3 * button_size);
            for (int i = 0; i <= 2 * n; i++)
                for (int j = 0; j <= 2 * m; j++)
                    Controls.Add(pictures[i][j]);
        }
        

        public void Refresh(ElementClickArgs args)
        {
            if (args.typeName == "Point")
            {
                SingleElementView.RefreshPicture(points, pictures[2 * args.x][2 * args.y], args.x, args.y);
            }
            else if (args.typeName == "Edge")
            {
                bool vertical = Convert.ToBoolean(args.additionalArgs);
                SingleElementView.RefreshPicture(edges, pictures[2 * args.x+ Convert.ToInt32(vertical)][2 * args.y + Convert.ToInt32(!vertical)], args.x, args.y,vertical);
            }
            else if (args.typeName == "Field")
            {
                SingleElementView.RefreshPicture(fields, pictures[2 * args.x + 1][2 * args.y + 1], args.x, args.y);
            }
            Refresh();
        }
    }
}
