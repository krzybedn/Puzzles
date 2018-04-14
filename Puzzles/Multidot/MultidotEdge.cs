using System;
using Puzzles.PuzzlePattern;

namespace Puzzles.Multidot
{
    class MultidotEdge : Edge
    {
        private int[][] verticalData;
        private int[][] horisontalData;

        private bool[][] verticalDataSolved;
        private bool[][] horisontalDataSolved;

        public MultidotEdge()
        {
            verticalData = new int[n][];
            verticalDataSolved = new bool[n][];
            for (int i = 0; i < n; i++)
            {
                verticalData[i] = new int[m + 1];
                verticalDataSolved[i] = new bool[m + 1];
                for (int j = 0; j < m + 1; j++)
                {
                    verticalData[i][j] = 0;
                    verticalDataSolved[i][j] = false;
                }
            }

            horisontalData = new int[n + 1][];
            horisontalDataSolved = new bool[n + 1][];
            for (int i = 0; i < n + 1; i++)
            {
                horisontalData[i] = new int[m];
                horisontalDataSolved[i] = new bool[m];
                for (int j = 0; j < m; j++)
                {
                    horisontalData[i][j] = 0;
                    horisontalDataSolved[i][j] = false;
                }
            }

            view = new MultidotEdgeView();
        }

        public MultidotEdge(string data) : base()
        {
            string[] lines = data.Split('\n');

            verticalData = new int[n][];
            verticalDataSolved = new bool[n][];
            for (int i = 0; i < n; i++)
            {
                string[] singleLine = lines[i].Split(' ');

                verticalData[i] = new int[m + 1];
                verticalDataSolved[i] = new bool[m + 1];
                for (int j = 0; j < m + 1; j++)
                {
                    verticalData[i][j] = 0;
                    verticalDataSolved[i][j] = Convert.ToBoolean(singleLine[j]);
                }
            }
            
            horisontalData = new int[n + 1][];
            horisontalDataSolved = new bool[n + 1][];
            for (int i = 0; i < n + 1; i++)
            {
                string[] singleLine = lines[i+n].Split(' ');

                horisontalData[i] = new int[m];
                horisontalDataSolved[i] = new bool[m];
                for (int j = 0; j < m; j++)
                {
                    horisontalData[i][j] = 0;
                    horisontalDataSolved[i][j] = Convert.ToBoolean(singleLine[j]);
                }
            }

            view = new MultidotEdgeView();

        }

        public int[][] VerticalData
        {
            get { return verticalData; }
            set { verticalData = value; }
        }
        public int[][] HorisontalData
        {
            get { return horisontalData; }
            set { horisontalData = value; }
        }
        public bool[][] VerticalDataSolved
        {
            get { return verticalDataSolved; }
            set { verticalDataSolved = value; }
        }
        public bool[][] HorisontalDataSolved
        {
            get { return horisontalDataSolved; }
            set { horisontalDataSolved = value; }
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m + 1; j++)
                    result += verticalDataSolved[i][j] + " ";
                result += "\n";
            }

            for (int i = 0; i < n + 1; i++)
            {
                for (int j = 0; j < m; j++)
                    result += horisontalDataSolved[i][j] + " ";
                result += "\n";
            }

            return result;
        }
    }

    class MultidotEdgeView : EdgeView
    {
        public override PictureBoxImage ChooseImage(int x, int y, object vertical)
        {
            if ((bool)vertical)
            {
                if (((MultidotEdge)element).VerticalData[x][y] == 1)
                    return images[2];
                else if (((MultidotEdge)element).VerticalData[x][y] == 2)
                    return images[4];
                else
                    return images[0];
            }
            else
            {
                if (((MultidotEdge)element).HorisontalData[x][y] == 1)
                    return images[3];
                else if (((MultidotEdge)element).HorisontalData[x][y] == 2)
                    return images[5];
                else
                    return images[1];
            }
        }
        public override PictureBoxImage ChooseImage_EditMode(int x, int y, object vertical)
        {
            if ((bool)vertical)
            {
                if (((MultidotEdge)element).VerticalDataSolved[x][y])
                    return images[2];
                else
                    return images[0];
            }
            else
            {
                if (((MultidotEdge)element).HorisontalDataSolved[x][y])
                    return images[3];
                else
                    return images[1];
            }
        }
        
        protected override void InitializeImages()
        {
            images = new PictureBoxImage[6];
            images[0] = new PictureBoxImage("Images/Multidot/edge_vertical_empty.png");
            images[1] = new PictureBoxImage("Images/Multidot/edge_horisontal_empty.png");
            images[2] = new PictureBoxImage("Images/Multidot/edge_vertical_circled_rectangle.png");
            images[3] = new PictureBoxImage("Images/Multidot/edge_horisontal_circled_rectangle.png");
            images[4] = new PictureBoxImage("Images/Multidot/edge_vertical_cross.png");
            images[5] = new PictureBoxImage("Images/Multidot/edge_horisontal_cross.png");
        }
    }
}
