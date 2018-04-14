
namespace Puzzles.PuzzlePattern
{
    public abstract class Field : Element
    {
        public Field() : base() { }
        public Field(string data) : base(data) { }
    }


    public abstract class FieldView : ElementView
    {
        public FieldView() : base(Puzzle.Instance.ButtonSize*2, Puzzle.Instance.ButtonSize * 2)
        {
            typeName = "Field";
            element = Field.Instance;
        }
    }
}
