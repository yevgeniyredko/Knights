using System;

namespace MyGame
{
    [Serializable]
    class Spearman : Unit
    {
        public static readonly int Price = 500;

        public Spearman()
        {
            Damage = 2;
            Armor = 2;
            Lives = 12;
            Income = -45;
        }
    }
}
