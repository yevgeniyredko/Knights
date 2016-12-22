using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame
{
    enum GameModes
    {
        SinglePlayer,
        MultiPlayer
    }

    class GameModel
    {
        public GameModel()
        {
            CurrentMode = GameModes.SinglePlayer;
            kingdom1 = new Kingdom(4, 3, 2, 1000);
            first = true;
        }

        public readonly GameModes CurrentMode;

        public IKingdom CurrentKingdom { get { return first ? kingdom1 : kingdom2; } }

        private bool first;

        IKingdom kingdom1;
        IKingdom kingdom2;
    }
}
