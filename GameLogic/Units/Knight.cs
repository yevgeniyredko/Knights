using System;

namespace MyGame
{
    [Serializable]
    class Knight : Unit
    {
        public static readonly int Price = 1000;

        public Knight()
        {
            Damage = 3;
            Armor = 5;
            Lives = 14;
            Income = -70;
        }
    }
}
