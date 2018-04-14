using System;
using Puzzles.PuzzlePattern;

namespace Puzzles.Multidot
{
    public class Multidot : Puzzle
    {
        public Multidot(bool editMode, int n, int m, String name) : base(editMode, n, m, name)
        {
            Initialize(newController: new MultidotController(), edgeElement: new MultidotEdge(), pointElement: new MultidotPoint(), fieldElement: new MultidotField());
        }
        public Multidot(bool editMode, string data) : base(editMode, data) 
        { }
    }
}
