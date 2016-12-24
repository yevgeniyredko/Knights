using System;

namespace MyGame
{
    [Serializable]
    class Farmer : Unit
    {
        public static readonly int Price = 500;

        public Farmer()
        {
            Damage = 1;
            Armor = 1;
            Lives = 10;
            Income = 150;
        }
    }
}
