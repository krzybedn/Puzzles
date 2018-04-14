using Puzzles.PuzzlePattern;

namespace Puzzles.Multidot
{
    class MultidotPoint : PuzzlePattern.Point
    {
        public MultidotPoint()
        {
            view = new MultidotPointView();
        }
        public MultidotPoint(string data) : this()
        { }
    }

    class MultidotPointView : PuzzlePattern.PointView
    {
        public override PictureBoxImage ChooseImage(int x, int y, object vertical)
        {
            return images[0];
        }
        protected override void InitializeImages()
        {
            images = new PictureBoxImage[1];
            images[0] =  new PictureBoxImage("Images/Multidot/point_circle.png");
        }
    }
}
