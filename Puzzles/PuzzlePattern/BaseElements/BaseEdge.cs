using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.PuzzlePattern.BaseElements
{
    public class BaseEdge : Edge
    {
        public BaseEdge()
        {
            view = new BaseEdgeView();
        }
        public BaseEdge(string data) : this()
        { }
    }

    class BaseEdgeView : EdgeView
    {
        public override PictureBoxImage ChooseImage(int x, int y, object vertical)
        {

            if ((bool)vertical)
                return this.images[0];
            else
                return this.images[1];
        }

        protected override void InitializeImages()
        {
            images = new PictureBoxImage[2];
            images[0] = new PictureBoxImage("Images/PuzzlePattern/edge_vertically.png");
            images[1] = new PictureBoxImage("Images/PuzzlePattern/edge_horizontal.png");
        }
    }
}
