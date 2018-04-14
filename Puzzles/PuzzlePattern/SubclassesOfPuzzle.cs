using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Puzzles.PuzzlePattern
{
    static class SubclassesOfPuzzle
    {
        public static List<PuzzleType> subclasses;

        static SubclassesOfPuzzle()
        {
            subclasses = new List<PuzzleType>();
            foreach (Type type in Assembly.GetAssembly(typeof(Puzzle)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Puzzle))))
                subclasses.Add(new PuzzleType(type));

        }
    }

    class PuzzleType
    {
        Type type;

        public PuzzleType(Type type)
        {
            this.type = type;
        }

        public Type Type
        {
            get {return type;}
        }

        public override String ToString()
        {
            return type.Name;
        }
    }
}
