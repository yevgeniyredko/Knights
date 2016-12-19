using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class Kingdom
    {
        public Kingdom(int farmerCount, int spearmanCount, int knightCount, int money)
        {
            Log = new List<string>();
            AddUnits(farmerCount, spearmanCount, knightCount);
            this.Money = money;      
        }

        public int Money { get; private set; }

        private List<Unit> units = new List<Unit>();
        private void AddUnits(int farmerCount, int spearmanCount, int knightCount)
        {
            for (int i = 0; i < farmerCount; i++)
                units.Add(new Farmer());
            for (int i = 0; i < spearmanCount; i++)
                units.Add(new Spearman());
            for (int i = 0; i < knightCount; i++)
                units.Add(new Knight());
        }
        public void BuyUnits(int farmerCount, int spearmanCount, int knightCount)
        {
            if (farmerCount * Farmer.Price + spearmanCount * Spearman.Price + knightCount * Knight.Price < Money)
                throw new ArgumentException("No money!");
            AddUnits(farmerCount, spearmanCount, knightCount);
        }
        private int UnitsCount(Type unitType)
        {
            return units
                    .Where(u => u.GetType() == unitType)
                    .Where(u => u.Lives > 0)
                    .Count();
        }
        public int FarmerCount { get { return UnitsCount(typeof(Farmer)); } }
        public int SpearmanCount { get { return UnitsCount(typeof(Spearman)); } }
        public int KnightCount { get { return UnitsCount(typeof(Knight)); } }

        private Cubic cubic = new Cubic();
        private void CheckCubicValue(CubicValues cubicValue)
        {
            switch (cubicValue)
            {
                case CubicValues.Add1000Gold:
                    Money += 1000;
                    break;
                case CubicValues.Add2000Gold:
                    Money += 2000;
                    break;
                case CubicValues.AddFarmer:
                    units.Add(new Farmer());
                    break;
                case CubicValues.AddKnight:
                    units.Add(new Knight());
                    break;
                case CubicValues.AddSpearman:
                    units.Add(new Spearman());
                    break;
                case CubicValues.BarbariansRaid:
                    Money -= 1000;
                    break;
            }
        }
        private List<CubicValues> cubicValues = new List<CubicValues>();
        private bool LastTwo
        {
            get
            {
                var length = cubicValues.Count;
                return length > 1 && cubicValues[length - 2] == cubicValues[length - 1];
            }
        }
        private bool LastThree
        {
            get
            {
                var length = cubicValues.Count;
                return length > 2 && cubicValues[length - 3] == cubicValues[length - 2]
                                  && cubicValues[length - 2] == cubicValues[length - 1];
            }
        }
        private bool Is666
        {
            get
            {
                var length = cubicValues.Count;
                return LastThree && cubicValues[length - 1] == CubicValues.BarbariansRaid;
            }
        }

        public List<string> Log { get; private set; }

        public void Move()
        {
            var cubicValue = cubic.Next();
            cubicValues.Add(cubicValue);
            CheckCubicValue(cubicValue);

            Console.WriteLine($"Cubic value: {cubicValue}");

            foreach (var u in units.Where(u => u.Lives > 0))
            {
                this.Money += u.Income;
                u.Move();
            }
           
            if (Is666)
            {
                units.Clear();
                units.Add(new Farmer());
            }
            if(LastThree)
                Money += 1500;
            else if (LastTwo)
                Money += 500;
        }  
    }
}
