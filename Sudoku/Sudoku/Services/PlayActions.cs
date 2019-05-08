using Sudoku.Models;
using Sudoku.Util;
using Sudoku.ViewModels;
using Sudoku.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace Sudoku.Services
{
    class PlayActions
    {
        private PlayVM viewModel;
        public PlayActions(PlayVM viewModel)
        {
            this.viewModel = viewModel;

            viewModel.Timer = new System.Windows.Threading.DispatcherTimer();
            viewModel.Timer.Tick += new EventHandler(Timer_Tick);
        }

        /* Actions */
        public void NewGame(object param)
        {
            UniformGrid grid = param as UniformGrid;
            if (grid.Children.Count > 0)
                grid.Children.Clear();

            /* Reset the GameOver and the default Editable Tile value */
            viewModel.TileChanged = 0;
            viewModel.GameOverText = "";

            /* Load the corresponding Level based on the Table Size */
            List<Tile> level;
            switch (viewModel.LevelSize)
            {
                case PlayVM.TableSize._4x4:
                    {
                        grid.Columns = 4;
                        viewModel.LevelColumns = 4;
                        level = DataManager.Instance.LoadLevel(PlayVM.TableSize._4x4);
                        break;
                    }
                case PlayVM.TableSize._6x6:
                    {
                        grid.Columns = 6;
                        viewModel.LevelColumns = 6;
                        level = DataManager.Instance.LoadLevel(PlayVM.TableSize._6x6);
                        break;
                    }
                case PlayVM.TableSize._9x9:
                    {
                        grid.Columns = 9;
                        viewModel.LevelColumns = 9;
                        level = DataManager.Instance.LoadLevel(PlayVM.TableSize._9x9);
                        break;
                    }

                default:
                    {
                        grid.Columns = 4;
                        viewModel.LevelColumns = 4;
                        level = DataManager.Instance.LoadLevel(PlayVM.TableSize._4x4);
                        break;
                    }
            }
            viewModel.CurrentLevel = level;

            /* Load the Sudoku Grid */
            LoadGrid(level, grid, param, true);

            /* Increment the Games Played by the current user */
            DataManager.Instance.CurrentUser.GamesPlayed++;
            DataManager.Instance.SaveAllUsers();

            /* Start the Timer of the Game */
            viewModel.Delay = DataManager.Instance.TimerSeconds;
            StartTimer();
        }

        public void SaveGame(object param)
        {
            DataManager.Instance.CurrentUser.SaveGame = viewModel.CurrentLevel;
            DataManager.Instance.CurrentUser.LevelSize = viewModel.LevelSize;
            DataManager.Instance.CurrentUser.SecondsRemaining = System.Convert.ToInt32(viewModel.SecondsRemaining);
            DataManager.Instance.SaveAllUsers();
        }

        public void OpenGame(object param)
        {
            if (DataManager.Instance.CurrentUser.SaveGame == null || DataManager.Instance.CurrentUser.SaveGame.Count == 0)
                return;

            UniformGrid grid = param as UniformGrid;
            if (grid.Children.Count > 0)
                grid.Children.Clear();

            /* Reset the GameOver and the default Editable Tile value */
            viewModel.TileChanged = 0;
            viewModel.GameOverText = "";

            switch (DataManager.Instance.CurrentUser.LevelSize)
            {
                case PlayVM.TableSize._4x4:
                    {
                        grid.Columns = 4;
                        viewModel.LevelColumns = 4;
                        break;
                    }
                case PlayVM.TableSize._6x6:
                    {
                        grid.Columns = 6;
                        viewModel.LevelColumns = 6;
                        break;
                    }
                case PlayVM.TableSize._9x9:
                    {
                        grid.Columns = 9;
                        viewModel.LevelColumns = 9;
                        break;
                    }

                default:
                    break;
            }

            /* Set the current Level Size and Tiles */
            viewModel.LevelSize = DataManager.Instance.CurrentUser.LevelSize;
            viewModel.CurrentLevel = DataManager.Instance.CurrentUser.SaveGame; ;

            /* Load the Sudoku Grid */
            LoadGrid(DataManager.Instance.CurrentUser.SaveGame, grid, param, false);

            /* Set the values from the Loaded Game that the User saved */
            SetLoadedValues(grid);

            CancelTimer();
            viewModel.Delay = DataManager.Instance.CurrentUser.SecondsRemaining;
            StartTimer();
        }

        public void Statistics(object param)
        {
            Statistics stats = new Statistics();
            stats.ShowDialog();
        }

        public void Exit(object param)
        {
            Play playWindow = param as Play;
            if (playWindow != null)
            {
                playWindow.Hide();

                MainWindow mainWindow = new MainWindow();
                mainWindow.ShowDialog();
            }
        }


        /* Methods */
        public void Size4x4(object param)
        {
            viewModel.LevelSize = PlayVM.TableSize._4x4;
        }

        public void Size6x6(object param)
        {
            viewModel.LevelSize = PlayVM.TableSize._6x6;
        }

        public void Size9x9(object param)
        {
            viewModel.LevelSize = PlayVM.TableSize._9x9;
        }

        public void SizeStandard(object param)
        {
            Size9x9(param);
        }

        private void CancelTimer()
        {
            viewModel.Timer.Stop();
            viewModel.Timer.IsEnabled = false;
            viewModel.Delay = DataManager.Instance.TimerSeconds;
            viewModel.SecondsRemaining = "";
        }

        private void StartTimer()
        {
            viewModel.Deadline = DateTime.Now.AddSeconds(viewModel.Delay);
            viewModel.Timer.Start();
        }

        private void GameFailed()
        {
            CancelTimer();
            viewModel.GameOverText = "YOU LOST";
            DataManager.Instance.SaveAllUsers();
        }

        private void GameSuccess()
        {
            CancelTimer();
            viewModel.GameOverText = "YOU WON";
            DataManager.Instance.CurrentUser.GamesWon++;
            DataManager.Instance.SaveAllUsers();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int secondsRemaining = (viewModel.Deadline - DateTime.Now).Seconds;
            if (secondsRemaining == 0)
            {
                /* If the Timer soconds end then notify that the game ended with Failure */
                GameFailed();
            }
            else
            {
                /* Else display the Remaining Seconds */
                viewModel.SecondsRemaining = secondsRemaining.ToString();
            }
        }

        public void Check(object param)
        {
            if (viewModel.CurrentLevel == null)
                return;

            UniformGrid grid = param as UniformGrid;
            int checkIndex = -1;
            int value = 0;

            for (int i = 0; i < grid.Children.Count; i++)
            {
                Button button = grid.Children[i] as Button;
                TextBox text = grid.Children[i] as TextBox;

                if (button != null)
                {
                    try
                    {
                        value = System.Convert.ToInt32(button.Content.ToString());
                    }
                    catch (Exception)
                    {
                        return;
                    }
                }
                else if (text != null)
                {
                    try
                    {
                        value = System.Convert.ToInt32(text.Text);
                    }
                    catch (Exception)
                    {
                        return;
                    }                    
                }

                if (value == System.Convert.ToInt32(viewModel.TileChanged) &&
                    value != viewModel.CurrentLevel[i].Value)
                {
                    /* For one element Check */
                    checkIndex = i;
                    break;
                }
            }

            /* If there is a Change on one of the Tile */
            if (checkIndex != -1)
            {
                TextBox text = grid.Children[checkIndex] as TextBox;                
                List<Tile> level = viewModel.CurrentLevel;
                int colCount = viewModel.LevelColumns;
                bool canBePlaced = true;
                bool gameOver = true;

                int row = checkIndex / colCount;
                int column = checkIndex % colCount;

                /* Top -> Bottom Check */
                for (int i = 0; i < colCount; i++)
                {
                    if (level.ElementAt(i * colCount + column).Value == value)
                    {
                        canBePlaced = false;
                        break;
                    }
                }

                /* Left -> Right Check */
                for (int i = 0; i < colCount; i++)
                {
                    if (level.ElementAt(row * colCount + i).Value == value)
                    {
                        canBePlaced = false;
                        break;
                    }
                }

                if (canBePlaced)
                {
                    level[checkIndex].Value = value;
                    text.Text = viewModel.TileChanged.ToString();

                    /* Check if the Game is Finish */
                    foreach(Tile tile in level)
                    {
                        if (tile.Value == 0)
                            gameOver = false;
                    }

                    if (gameOver)
                    {
                        /* If the Sudoku table was filled then notify that the game ended with Success */
                        GameSuccess();
                    }
                }
                else
                {
                    text.Text = "0";
                }
            }

        }

        private void SetLoadedValues(UniformGrid grid)
        {
            for (int i = 0; i < grid.Children.Count; i++)
            {
                TextBox text = grid.Children[i] as TextBox;
                if (text != null)
                {
                    text.Text = viewModel.CurrentLevel.ElementAt(i).Value.ToString();
                }
            }
        }

        private TextBox NewEditableTile(Tile tile, object param)
        {
            TextBox textBox = new TextBox();
            textBox.Text = tile.Value.ToString();
            textBox.TextAlignment = System.Windows.TextAlignment.Center;
            textBox.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            Binding textBinding = new Binding();
            textBinding.Source = viewModel;
            textBinding.Path = new System.Windows.PropertyPath("TileChanged");
            textBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(textBox, TextBox.TextProperty, textBinding);

            System.Windows.Input.KeyGesture key = new System.Windows.Input.KeyGesture(System.Windows.Input.Key.Enter);

            KeyBinding keyBinding = new KeyBinding();
            keyBinding.Command = viewModel.CheckCommand;
            keyBinding.CommandParameter = param;
            keyBinding.Key = Key.Enter;

            textBox.InputBindings.Add(keyBinding);

            textBox.FontSize = 20;
            textBox.FontFamily = new System.Windows.Media.FontFamily("Verdana");
            return textBox;
        }

        private Button NewNonEditableTile(Tile tile)
        {
            Button button = new Button();
            button.Content = tile.Value.ToString();
            button.FontSize = 20;
            button.FontFamily = new System.Windows.Media.FontFamily("Verdana");
            return button;
        }

        private void LoadGrid(List<Tile> level, UniformGrid grid, object param, bool isNew)
        {
            foreach (Tile tile in level)
            {
                /* In case is a New Game we tell the program if is Editable or Not */
                if (isNew)
                {
                    if (tile.Value != 0)
                        tile.IsEditable = false;
                    else
                        tile.IsEditable = true;
                }

                /* We create an Editable or NonEditable Tile based on the Current Tile property */
                if (tile.IsEditable)
                {
                    grid.Children.Add(NewEditableTile(tile, param));
                }
                else
                {
                    grid.Children.Add(NewNonEditableTile(tile));
                }
            }
        }

    }
}
