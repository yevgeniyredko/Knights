using System;
using System.Windows.Forms;
using System.Drawing;

namespace MyGame
{
    class MainWindow : Form
    {
        public MainWindow(GameModel game)
        {
            #region Create form layout

            // Form style
            MinimumSize = new Size(600, 341);
            Text = "Knights";
            BackColor = Color.White;        
            Menu = new MainMenu();
            var statusBar = new StatusBar()
            {
                Dock = DockStyle.Bottom
            };

            // Common parameters of controls
            var margin = 10;
            var labelFont = new Font("Microsoft Sans Serif", 12);
            var countLabelSize = new Size(ClientSize.Width / 2 - 2 * margin, 20);
            var buttonHeight = 30;

            // Create controls
            var moneyCount = new Label
            {
                Size = countLabelSize,
                Location = new Point(margin, margin),
                Font = labelFont
            };
            var knightCount = new Label
            {
                Size = countLabelSize,
                Location = new Point(margin, moneyCount.Bottom),
                Font = labelFont
            };
            var spearmanCount = new Label
            {
                Size = countLabelSize,
                Location = new Point(margin, knightCount.Bottom),
                Font = labelFont
            };
            var farmerCount = new Label
            {
                Size = countLabelSize,
                Location = new Point(margin, spearmanCount.Bottom),
                Font = labelFont
            };

            var move = new Button
            {
                Size = new Size(ClientSize.Width / 2 - 2 * margin, buttonHeight),
                Location = new Point(margin, farmerCount.Bottom + margin),
                Text = "Move",
                Font = labelFont,
                FlatStyle = FlatStyle.Flat
            };


            var showShop = new Button
            {
                Size = new Size(ClientSize.Width / 4 - 2 * margin, 30),
                Location = new Point(margin, move.Bottom + margin),
                Text = "Shop",
                Font = labelFont,
                FlatStyle = FlatStyle.Flat
            };

            var shopPanel = new Panel
            {
                Size = new Size(ClientSize.Width / 4 - margin, 105),
                Location = new Point(showShop.Right + margin, move.Bottom + margin),
                Visible = false
            };

            var buyKnightLabel = new Label
            {
                Size = new Size(shopPanel.Width / 3 * 2, 20),
                Location = new Point(0, 0),
                Text = "Knights:",
                Font = labelFont
            };
            var buySpearmanLabel = new Label
            {
                Size = buyKnightLabel.Size,
                Location = new Point(0, buyKnightLabel.Bottom + 5),
                Text = "Spearmen:",
                Font = labelFont
            };
            var buyFarmerLabel = new Label
            {
                Size = buyKnightLabel.Size,
                Location = new Point(0, buySpearmanLabel.Bottom + 5),
                Text = "Farmers:",
                Font = labelFont
            };
            
            var buyKnightCount = new NumericUpDown
            {
                Size = new Size(shopPanel.Width - buyKnightLabel.Width, 20),
                Location = new Point(buyKnightLabel.Right, 0),
            };
            var buySpearmanCount = new NumericUpDown
            {
                Size = buyKnightCount.Size,
                Location = new Point(buySpearmanLabel.Right, buyKnightLabel.Bottom + margin / 2)
            };
            var buyFarmerCount = new NumericUpDown
            {
                Size = buyKnightCount.Size,
                Location = new Point(buyFarmerLabel.Right, buySpearmanLabel.Bottom + margin / 2)
            };


            var singleplayer = new Button()
            {
                Size = new Size(ClientSize.Width / 4 - 2 * margin, 30),
                Location = new Point(margin, showShop.Bottom + margin),
                Text = "Single Player",
                Font = labelFont,
                FlatStyle = FlatStyle.Flat,
                Visible = false
            };

            var multiplayer = new Button()
            {
                Size = new Size(ClientSize.Width / 4 - 2 * margin, 30),
                Location = new Point(margin, singleplayer.Bottom + margin),
                Text = "Multiplayer",
                Font = labelFont,
                FlatStyle = FlatStyle.Flat,
                Visible = false
            };

            var buy = new Button
            {
                Size = new Size(shopPanel.Width, 30),
                Location = new Point(0, buyFarmerLabel.Bottom + margin / 2),
                Text = "Buy",
                Font = labelFont,
                FlatStyle = FlatStyle.Flat
            };


            var separator = new Label()
            {
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(1, ClientSize.Height - statusBar.Height),
                Location = new Point(ClientSize.Width / 2, 0),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom
            };


            
            var log = new ListBox
            {
                Size = new Size(ClientSize.Width / 2, ClientSize.Height - statusBar.Height),
                Location = new Point(moneyCount.Right + margin, 0),
                Font = labelFont,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom,
                BackColor = Color.White,               
                BorderStyle =  BorderStyle.None
            };

            #endregion

            #region Create main menu

            var newGame = new MenuItem("New game")
            {
                Shortcut = Shortcut.CtrlN,
            };
            var loadGame = new MenuItem("Load game")
            {
                Shortcut = Shortcut.CtrlO
            };
            var saveGame = new MenuItem("Save game")
            {
                Shortcut = Shortcut.CtrlS
            };
            var exit = new MenuItem("Exit", (o, e) => Close())
            {
                Shortcut = Shortcut.AltF4
            };

            #endregion

            Action update = () =>
            {
                moneyCount.Text = $"Money: {game.CurrentKingdom.MoneyCount}";
                knightCount.Text = $"Knights: {game.CurrentKingdom.KnightCount}";
                spearmanCount.Text = $"Spearmen: {game.CurrentKingdom.SpearmanCount}";
                farmerCount.Text = $"Farmers: {game.CurrentKingdom.FarmerCount}";

                log.Items.Clear();
                log.Items.AddRange(game.CurrentKingdom.Log);
                log.TopIndex = log.Items.Count - 1;

                statusBar.Text = $"Current player: player{(game.IsFirstPlayer ? 1 : 2)}";
            };

            Action createNewGame = () =>
            {
                move.Enabled = false;
                showShop.Enabled = false;
                shopPanel.Visible = false;
                moneyCount.Text = "";
                knightCount.Text = "";
                spearmanCount.Text = "";
                farmerCount.Text = "";
                log.Items.Clear();
                singleplayer.Visible = true;
                multiplayer.Visible = true;
            };

            #region Add controls and menu

            Menu.MenuItems.Add("Game", new MenuItem[]
                {
                    newGame,
                    loadGame,
                    saveGame,
                    new MenuItem("-"),
                    exit
                });

            shopPanel.Controls.AddRange(new Control[]
            {
                buyKnightLabel, buyKnightCount,
                buySpearmanLabel, buySpearmanCount,
                buyFarmerLabel, buyFarmerCount,
                buy
            });

            Controls.AddRange(new Control[] 
            {
                moneyCount, knightCount, spearmanCount, farmerCount,
                move,
                showShop, shopPanel,
                singleplayer, multiplayer,
                separator,
                log,
                statusBar
            });

            #endregion       

            #region Add Click events

            newGame.Click += (sender, args) => createNewGame();

            saveGame.Click += (sender, args) =>
            {
                var dialog = new SaveFileDialog()
                {
                    Filter = "data files|*.dat"
                };
                dialog.ShowDialog();
                if (dialog.FileName == "") return;
                Serializer.WriteToBinary(dialog.FileName, game);
            };

            loadGame.Click += (sender, args) =>
            {
                var dialog = new OpenFileDialog()
                {
                    Filter = "data files|*.dat"
                };
                dialog.ShowDialog();
                if (dialog.FileName == "") return;
                game = Serializer.ReadFromBinary<GameModel>(dialog.FileName);
                update();
            };

            move.Click += (sender, args) =>
            {
                game.CurrentKingdom.Move();
                try
                {
                    game.ChangePlayer();
                }
                catch
                {
                    // Is ts single player, nothing will happen
                }
                update();
                if (game.CurrentKingdom.IsBankrupt)
                    MessageBox.Show("You are bankrupt!");            
            };

            buy.Click += (sender, args) =>
            {
                try
                {
                    game.CurrentKingdom.BuyUnits((int)buyFarmerCount.Value, (int)buySpearmanCount.Value, (int)buyKnightCount.Value);
                    update();                    
                }
                catch
                {
                    MessageBox.Show("No money!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                buyFarmerCount.Value = 0;
                buyKnightCount.Value = 0;
                buySpearmanCount.Value = 0;
            };

            showShop.Click += (sender, args) =>
            {
                shopPanel.Visible = !shopPanel.Visible;
            };

            singleplayer.Click += (sender, args) =>
            {
                game = new GameModel(GameModes.SinglePlayer);
                move.Enabled = true;
                showShop.Enabled = true;
                singleplayer.Visible = false;
                multiplayer.Visible = false;
                update();
            };

            multiplayer.Click += (sender, args) =>
            {
                game = new GameModel(GameModes.MultiPlayer);
                move.Enabled = true;
                showShop.Enabled = true;
                singleplayer.Visible = false;
                multiplayer.Visible = false;
                update();
            };

            #endregion

            #region On launch

            if (game == null) createNewGame();
            else update();

            #endregion

            FormClosed += (sender, args) => Serializer.WriteToBinary("current.dat", game);
        }
    }
}