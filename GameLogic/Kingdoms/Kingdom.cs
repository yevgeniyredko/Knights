using System;
using System.Collections.Generic;
using System.Linq;

namespace MyGame
{
    class Kingdom : IKingdom
    {
        public Kingdom(int farmerCount, int spearmanCount, int knightCount, int money)
        {
            AddUnits(farmerCount, spearmanCount, knightCount);
            this.MoneyCount = money;
        }

        public void Move()
        {
            if (IsBankrupt) return; //throw new Exception("Game over");
            Duel();
            BarbariansRaid();

            var cubicValue = cubic.Next();
            cubicValues.Add(cubicValue);
            CheckCubicValue(cubicValue);

            log.Add($"Cubic value: {cubicValue}");

            foreach (var u in units.Where(u => u.Lives > 0))
            {
                this.MoneyCount += u.Income;
                u.Move();
            }

            if (Is666)
            {
                units.Clear();
                units.Add(new Farmer());
            }
            if (IsLastThree)
                MoneyCount += 1500;
            else if (IsLastTwo)
                MoneyCount += 500;

            MoneyCount += 100;

            if (IsBankrupt) log.Add("Game over!");
        }
        public void BuyUnits(int farmerCount, int spearmanCount, int knightCount)
        {
            var price = farmerCount * Farmer.Price + spearmanCount * Spearman.Price + knightCount * Knight.Price;
            if (price > MoneyCount)
                throw new ArgumentException("No money!");
            AddUnits(farmerCount, spearmanCount, knightCount);
            MoneyCount -= price;
        }

        public int MoneyCount { get; private set; }
        public int FarmerCount { get { return UnitsCount(typeof(Farmer)); } }
        public int SpearmanCount { get { return UnitsCount(typeof(Spearman)); } }
        public int KnightCount { get { return UnitsCount(typeof(Knight)); } }
        public bool IsBankrupt { get { return !(MoneyCount > 0 && 
                    (FarmerCount > 0 || SpearmanCount > 0 || KnightCount > 0)); } }

        private readonly List<Unit> units = new List<Unit>();
        private void AddUnits(int farmerCount, int spearmanCount, int knightCount)
        {
            for (int i = 0; i < farmerCount; i++)
                units.Add(new Farmer());
            for (int i = 0; i < spearmanCount; i++)
                units.Add(new Spearman());
            for (int i = 0; i < knightCount; i++)
                units.Add(new Knight());
        }
        private int UnitsCount(Type unitType)
        {
            return units
                    .Where(u => u.GetType() == unitType)
                    .Where(u => u.Lives > 0)
                    .Count();
        }

        private void Duel()
        {
            var rnd = new Random();
            if (rnd.Next(100) < 90) return;
            var livingUnits = units.Where(u => u.Lives > 0).ToArray();
            if (livingUnits.Length < 3) return;  

            var unit1 = livingUnits[rnd.Next(livingUnits.Length)];
            var unit2 = livingUnits[rnd.Next(livingUnits.Length)];
            while (unit1 == unit2)
                unit2 = livingUnits[rnd.Next(livingUnits.Length)];

            Unit diedUnit;

            if (rnd.Next(10) > 4)
                diedUnit = unit1;
            else
                diedUnit = unit2;

            log.Add($"Duel! Died 1 {diedUnit.GetType().ToString().Split('.')[1]}");
        }
        private void BarbariansRaid()
        {
            if (cubicValues.Count < 20) return;
            var rnd = new Random();
            if (rnd.Next(100) < 95) return;

            var outcomingMoney = (double)rnd.Next(400, 2001);
            outcomingMoney -= KnightCount * (outcomingMoney * 0.05);

            MoneyCount -= (int)outcomingMoney;

            log.Add($"Barbarians stolen {outcomingMoney}");
        }

        private Cubic cubic = new Cubic();
        private void CheckCubicValue(CubicValues cubicValue)
        {
            switch (cubicValue)
            {
                case CubicValues.Add100Gold:
                    MoneyCount += 100;
                    break;
                case CubicValues.Add200Gold:
                    MoneyCount += 200;
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
                    MoneyCount -= 1000;
                    break;
            }
        }
        private readonly List<CubicValues> cubicValues = new List<CubicValues>();
        private bool IsLastTwo
        {
            get
            {
                var length = cubicValues.Count;
                return length > 1 && cubicValues[length - 2] == cubicValues[length - 1];
            }
        }
        private bool IsLastThree
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
                return IsLastThree && cubicValues[length - 1] == CubicValues.BarbariansRaid;
            }
        }

        public string[] Log { get { return log.ToArray(); } }
        private readonly List<string> log = new List<string>();
    }
}