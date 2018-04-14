using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.PuzzlePattern.BaseElements
{
    class BaseField : Field
    {
        public BaseField()
        {
            view = new BaseFieldView();
        }
        public BaseField(string data) : this()
        { }
    }

    class BaseFieldView : FieldView
    {
        public override PictureBoxImage ChooseImage(int x, int y, object nothing)
        {
            return images[0];
        }

        protected override void InitializeImages()
        {
            images = new PictureBoxImage[1];
            images[0] = new PictureBoxImage("Images/PuzzlePattern/field_empty.png");
        }
    }
    
}
