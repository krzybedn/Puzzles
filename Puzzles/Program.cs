using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzles
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //PuzzlePattern.Puzzle test =PuzzlePattern.Puzzle.FromFIle("../../ReadyPuzzles/Name.puz");// new Multidot.Multidot(7,7);//  new Multidot.Multidot(5, 5);//
                // Window.MainForm xxs = new Window.MainForm();
                Application.Run(new Window.MainForm());// new MenuView());// new Window.MainForm());
        }
    }
}
