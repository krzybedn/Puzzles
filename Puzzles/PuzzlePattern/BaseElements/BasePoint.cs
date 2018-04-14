using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.PuzzlePattern.BaseElements
{
    class BasePoint : Point
    {
        public BasePoint() : base()
        {
            view = new BasePointView();
        }
        public BasePoint(string data) : this()
        { }
}

    class BasePointView : PointView
    {
        public override PictureBoxImage ChooseImage(int x, int y, object noting = null)
        {
            if (x == 0)
            {
                if (y == 0)
                    return images[0];
                else if (y == m)
                    return images[2];
                else
                    return images[1];
            }
            else if (x == n)
            {
                if (y == 0)
                    return images[6];
                else if (y == m)
                    return images[8];
                else
                    return images[7];
            }
            else if (y == 0)
                return images[3];
            else if (y == m)
                return images[5];
            else
                return images[4];
        }

        protected override void InitializeImages()
        {
            images = new PictureBoxImage[9];
            images[0] = new PictureBoxImage("Images/PuzzlePattern/point_lattice_up_left.png");
            images[1] = new PictureBoxImage("Images/PuzzlePattern/point_lattice_up.png");
            images[2] = new PictureBoxImage("Images/PuzzlePattern/point_lattice_up_right.png");
            images[3] = new PictureBoxImage("Images/PuzzlePattern/point_lattice_left.png");
            images[4] = new PictureBoxImage("Images/PuzzlePattern/point_lattice_center.png");
            images[5] = new PictureBoxImage("Images/PuzzlePattern/point_lattice_right.png");
            images[6] = new PictureBoxImage("Images/PuzzlePattern/point_lattice_down_left.png");
            images[7] = new PictureBoxImage("Images/PuzzlePattern/point_lattice_down.png");
            images[8] = new PictureBoxImage("Images/PuzzlePattern/point_lattice_down_right.png");
        }
    }

}
