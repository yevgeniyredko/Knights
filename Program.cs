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
            var kingdom = new Kingdom(4, 3, 2, 1000);
            while(true)
            {
                Console.WriteLine($"Money: {kingdom.Money}");
                Console.WriteLine($"Farmers: {kingdom.FarmerCount}");
                Console.WriteLine($"Spearmen: {kingdom.SpearmanCount}");
                Console.WriteLine($"Knights: {kingdom.KnightCount}");
                kingdom.Move();
                Thread.Sleep(1000);
            }
        }
    }
}
