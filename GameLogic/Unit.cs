namespace MyGame
{
    abstract class Unit
    {
        public static int Price { get; protected set; }

        public int Lives { get; protected set; }
        public int Damage { get; protected set; }
        public int Armor { get; protected set; }
        public int Income { get; protected set; }

        public void Move()
        {
            if (Lives == 0) return;
            Lives--;
        }
    }
}
