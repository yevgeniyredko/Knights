using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MyGame
{
    class Program
    {
        static void Main()
        {
            var model = new GameModel();
            var window = new MainWindow(model);
            Application.EnableVisualStyles();
            Application.Run(window);
        }
    }
}
