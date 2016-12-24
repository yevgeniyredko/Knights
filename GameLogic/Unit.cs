using System;

namespace MyGame
{
    [Serializable]
    abstract class Unit
    {
        public int Lives { get; protected set; }
        public int Damage { get; protected set; }
        public int Armor { get; protected set; }
        public int Income { get; protected set; }

        public void Move()
        {
            if (Lives == 0) return;
            Lives--;
        }

        public void Die()
        {
            if (Lives == 0) throw new Exception("Unit already died!");
            Lives = 0;
        }
    }
}
