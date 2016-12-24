using System;

namespace MyGame
{
    enum CubicValues
    {
        AddFarmer,
        AddSpearman,
        AddKnight,
        Add100Gold,
        Add200Gold,
        BarbariansRaid
    }

    [Serializable]
    class Cubic
    {
        private Random rnd = new Random();

        public CubicValues Next()
        {
            return (CubicValues)rnd.Next(6);
        }
    }
}
