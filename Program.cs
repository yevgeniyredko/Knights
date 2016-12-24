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
        [STAThread]
        static void Main()
        {
            GameModel model;
            try
            {
                model = Serializer.ReadFromBinary<GameModel>("current.dat");
            }
            catch
            {
                model = null;
            }
            Application.EnableVisualStyles();
            Application.Run(new MainWindow(model));
            
        }
    }
}
