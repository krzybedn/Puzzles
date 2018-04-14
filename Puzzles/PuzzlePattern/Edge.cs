
namespace Puzzles.PuzzlePattern
{
    public abstract class Edge : Element
    {
        public Edge() : base() { }
        public Edge(string data) : base(data) { }
    }


    public abstract class EdgeView : ElementView
    {
        public EdgeView() : base(Puzzle.Instance.ButtonSize, Puzzle.Instance.ButtonSize * 2)
        {
            typeName = "Edge";
            element = Edge.Instance;
        }

        public override System.Drawing.Size ChooseSize(object vertical)
        {
            if ((bool)vertical)
                return new System.Drawing.Size(picture_height, picture_width);
            else
                return new System.Drawing.Size(picture_width, picture_height);
        }
    }
}