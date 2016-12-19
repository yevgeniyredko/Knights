using System;

namespace MyGame
{
    enum CubicValues
    {
        AddFarmer,
        AddSpearman,
        AddKnight,
        Add1000Gold,
        Add2000Gold,
        BarbariansRaid
    }

    class Cubic
    {
        private Random random = new Random();

        public CubicValues Next()
        {
            return (CubicValues)random.Next(6);
        }
    }
}
