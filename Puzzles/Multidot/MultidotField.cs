using System;
using Puzzles.PuzzlePattern;

namespace Puzzles.Multidot
{
    class MultidotField : PuzzlePattern.Field
    {
        private int[][] data;
        private int[][] used_count;
        public MultidotField()
        {
            data = new int[n][];
            used_count = new int [n][];
            for(int i=0; i<n; i++)
            {
                data[i] = new int[m];
                used_count[i] = new int[m];
                for (int j = 0; j < m; j++)
                {
                    data[i][j] = 5;
                    used_count[i][j] = 0; //code of empty field
                }
            }
            view = new MultidotFieldView();
        }

        public MultidotField(string imput) : base()
        {

            string[] lines = imput.Split('\n');

            data = new int[n][];
            for (int i = 0; i < n; i++)
            {
                string[] singleLine = lines[i].Split(' ');

                data[i] = new int[m];
                for (int j = 0; j < m; j++)
                {
                    data[i][j] = Convert.ToInt32(singleLine[j]);
                }
            }

            used_count = new int[n][];
            for (int i = 0; i < n; i++)
            {
                string[] singleLine = lines[n + i].Split(' ');
                
                used_count[i] = new int[m];
                for (int j = 0; j < m; j++)
                {
                    used_count[i][j] = Convert.ToInt32(singleLine[j]);
                }
            }
            view = new MultidotFieldView();
        }

        public int[][] Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }
        public int[][] Used
        {
            get
            {
                return used_count;
            }
            set
            {
                used_count = value;
            }
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (data[i][j] == 5)
                        result += "5";
                    else
                        result += used_count[i][j];
                    result += " ";
                }
                result += "\n";
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                    result +=used_count[i][j] + " ";
                result += "\n";
            }
            return result;
        }
    }

    class MultidotFieldView : PuzzlePattern.FieldView
    {
        public override PictureBoxImage ChooseImage(int x, int y, object nothing = null)
        {
            int val = ((MultidotField)element).Data[x][y];
            if (val == -1)
                return images[5];
            return images[val];
        }

        public override PictureBoxImage ChooseImage_EditMode(int x, int y, object nothing = null)
        {
            if (((MultidotField)element).Data[x][y] != 5)
            {
                int val = ((MultidotField)element).Used[x][y];
                if (val == -1)
                    return images[5];
                return images[val];
            }
            else
            {
                return images[5];
            }
        }

        protected override void InitializeImages()
        {
            images = new PictureBoxImage[6];
            images[0] =  new PictureBoxImage("Images/Multidot/field_0.png");
            images[1] =  new PictureBoxImage("Images/Multidot/field_1.png");
            images[2] =  new PictureBoxImage("Images/Multidot/field_2.png");
            images[3] =  new PictureBoxImage("Images/Multidot/field_3.png");
            images[4] =  new PictureBoxImage("Images/Multidot/field_4.png");
            images[5] =  new PictureBoxImage("Images/PuzzlePattern/field_empty.png");
        }
    }
}
