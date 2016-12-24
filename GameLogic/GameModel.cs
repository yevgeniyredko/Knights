using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MyGame
{
    enum GameModes
    {
        SinglePlayer,
        MultiPlayer
    }

    [Serializable]
    class GameModel
    {
        public GameModel(GameModes mode)
        {
            NewGame(mode);
        }

        public bool IsFirstPlayer { get; private set; }
        public IKingdom CurrentKingdom => IsFirstPlayer ? kingdom1 : kingdom2;

        public void NewGame(GameModes mode)
        {
            CurrentMode = mode;
            kingdom1 = new Kingdom(4, 3, 2, 1000);
            if (mode == GameModes.MultiPlayer)
            {
                Thread.Sleep(10);
                kingdom2 = new Kingdom(4, 3, 2, 1000);
            }            
            IsFirstPlayer = true;
        }
        public void ChangePlayer()
        {
            if (CurrentMode == GameModes.SinglePlayer)
                throw new Exception();
            IsFirstPlayer = !IsFirstPlayer;
        }

        private GameModes CurrentMode;
        private IKingdom kingdom1;
        private IKingdom kingdom2;
    }
}
