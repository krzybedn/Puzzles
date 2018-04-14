
namespace Puzzles.PuzzlePattern
{
    public abstract class Point : Element
    {
        public Point() : base() { }
        public Point(string data) : base(data) { }
    }


    public abstract class PointView : ElementView
    {
        public PointView() : base(Puzzle.Instance.ButtonSize, Puzzle.Instance.ButtonSize)
        {
            typeName = "Point";
            element = Point.Instance;
        }
    }
}