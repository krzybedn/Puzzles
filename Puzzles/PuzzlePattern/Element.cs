using System;
using System.Windows.Forms;

namespace Puzzles.PuzzlePattern
{
    public abstract class Element
    {
        protected int n, m;
        protected ElementView view;

        #region Instance
        static Element instance;
        public static Element Instance
        {
            get
            {
                if (instance == null)
                    throw new Exception("No Element construktor called");

                return instance;
            }
        }
        #endregion

        public Element()
        {
            instance = this;
            this.n = Puzzle.Instance.N;
            this.m = Puzzle.Instance.M;
        }

        public Element(string data) : this()
        { }


        public override string ToString()
        {
            return "";
        }
        

        public ElementView View
        {
            get { return view; }
        }
    }


    public abstract class ElementView
    {
        protected int n, m;
        protected Element element;
        protected PictureBoxImage[] images;
        protected int picture_height, picture_width;
        public String typeName;

        public ElementView(int picture_height, int picture_width)
        {
            this.n = Puzzle.Instance.N;
            this.m = Puzzle.Instance.M;
            this.picture_height = picture_height;
            this.picture_width = picture_width;
            InitializeImages();
        }

        protected abstract void InitializeImages();


        public virtual System.Drawing.Size ChooseSize(object additionalArgument = null)
        {
            return new System.Drawing.Size(picture_width, picture_height);
        }
        public abstract PictureBoxImage ChooseImage(int x, int y, object additionalArgument = null);
        public virtual PictureBoxImage ChooseImage_EditMode(int x, int y, object additionalArgument = null)
        {
            return ChooseImage(x, y, additionalArgument);
        }

    }


    static class SingleElementView
    {
        static public PictureBox Show(ElementView view, int id_height, int id_width, int loc_height, int loc_width, object additionalArgument = null)
        {
            PictureBox picture = new PictureBox();
            picture.Name = view.typeName + " " + id_height + " " + id_width + " " + additionalArgument;
            picture.Size = view.ChooseSize(additionalArgument);
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            if (Puzzle.Instance.EditMode)
                picture.Image = view.ChooseImage_EditMode(id_height, id_width, additionalArgument).Value;
            else
                picture.Image = view.ChooseImage(id_height, id_width, additionalArgument).Value;
            picture.MouseClick += PuzzleController.Instance.Handler;
            picture.Location = new System.Drawing.Point(loc_width, loc_height);
            return picture;
        }

        static public void RefreshPicture(ElementView view, PictureBox picture, int id_height, int id_width, object additionalArgument = null)
        {
            if (Puzzle.Instance.EditMode)
                picture.Image = view.ChooseImage_EditMode(id_height, id_width, additionalArgument).Value;
            else
                picture.Image = view.ChooseImage(id_height, id_width, additionalArgument).Value;
        }
    }


    public class PictureBoxImage
    {
        protected System.Drawing.Image val;

        public PictureBoxImage(string name)
        {
            val = System.Drawing.Image.FromFile(name);
        }

        public System.Drawing.Image Value
        { get { return val; } }
    }
}
