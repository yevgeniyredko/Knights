namespace MyGame
{
    interface IKingdom
    {
        void Move();
        void BuyUnits(int farmerCount, int spearmanCount, int knightCount);

        int MoneyCount { get; }
        int FarmerCount { get; }
        int SpearmanCount { get; }
        int KnightCount { get; }
        bool IsBankrupt { get; }

        string[] Log { get; }
    }
}
