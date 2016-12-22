using System;
using System.Windows.Forms;
using System.Drawing;


namespace MyGame
{
    class MainWindow : Form
    {
        public MainWindow(GameModel model)
        {
            #region Create controls

            MinimumSize = new Size(600, 320);

            var moneyCount = new Label
            {
                Size = new Size(ClientSize.Width / 2, 30),
                Location = new Point(0, 0)
            };
            var knightCount = new Label
            {
                Size = moneyCount.Size,
                Location = new Point(0, moneyCount.Bottom)
            };
            var spearmanCount = new Label
            {
                Size = moneyCount.Size,
                Location = new Point(0, knightCount.Bottom)
            };
            var farmerCount = new Label
            {
                Size = moneyCount.Size,
                Location = new Point(0, spearmanCount.Bottom)
            };
            var move = new Button
            {
                Size = moneyCount.Size,
                Location = new Point(0, farmerCount.Bottom),
                Text = "Move"
            };

            var buyKnightLabel = new Label
            {
                Size = new Size(ClientSize.Width / 4, 30),
                Location = new Point(0, move.Bottom),
                Text = "Knights:"
            };
            var buyKnightCount = new NumericUpDown
            {
                Size = buyKnightLabel.Size,
                Location = new Point(buyKnightLabel.Right, move.Bottom)
            };

            var buySpearmanLabel = new Label
            {
                Size = buyKnightLabel.Size,
                Location = new Point(0, buyKnightLabel.Bottom),
                Text = "Spearmen:"
            };
            var buySpearmanCount = new NumericUpDown
            {
                Size = buyKnightLabel.Size,
                Location = new Point(buySpearmanLabel.Right, buyKnightLabel.Bottom)
            };

            var buyFarmerLabel = new Label
            {
                Size = buyKnightLabel.Size,
                Location = new Point(0, buySpearmanLabel.Bottom),
                Text = "Farmers:"
            };
            var buyFarmerCount = new NumericUpDown
            {
                Size = buyKnightLabel.Size,
                Location = new Point(buyFarmerLabel.Right, buySpearmanLabel.Bottom)
            };

            var buy = new Button
            {
                Size = moneyCount.Size,
                Location = new Point(0, buyFarmerLabel.Bottom),
                Text = "Buy"
            };

            var log = new ListBox
            {
                Size = new Size(290, ClientSize.Height),
                Location = new Point(moneyCount.Right, 0),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom,
                
            };

            #endregion

            #region Add controls

            Controls.AddRange(new Control[] 
            {
                moneyCount, knightCount, spearmanCount, farmerCount,
                move,
                buyKnightLabel, buyKnightCount,
                buySpearmanLabel, buySpearmanCount,
                buyFarmerLabel, buyFarmerCount,
                buy,
                log
            });

            #endregion

            Action update = () =>
            {
                moneyCount.Text = $"Money: {model.CurrentKingdom.MoneyCount}";
                knightCount.Text = $"Knights: {model.CurrentKingdom.KnightCount}";
                spearmanCount.Text = $"Spearmen: {model.CurrentKingdom.SpearmanCount}";
                farmerCount.Text = $"Farmers: {model.CurrentKingdom.FarmerCount}";
                log.Items.Clear();
                log.Items.AddRange(model.CurrentKingdom.Log);
                log.TopIndex = log.Items.Count - 1;
            };

            move.Click += (sender, args) =>
            {
                model.CurrentKingdom.Move();
                update();
                if (model.CurrentKingdom.IsBankrupt)
                    MessageBox.Show("You are bankrupt!");            
            };

            buy.Click += (sender, args) =>
            {
                try
                {
                    model.CurrentKingdom.BuyUnits((int)buyFarmerCount.Value, (int)buySpearmanCount.Value, (int)buyKnightCount.Value);
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

            update();
        }
    }
}
