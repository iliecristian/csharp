using Sudoku.Models;
using Sudoku.Util;
using Sudoku.ViewModels;
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
        }

        /* Actions */
        public void NewGame(object param)
        {
            UniformGrid grid = param as UniformGrid;
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
              
            int index = 0;
            foreach (Tile tile in level)
            {
                if (tile.Value == 0)
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

                    textBox.InputBindings.Add(keyBinding); //.InputBindings.Add(new System.Windows.Input.InputBinding(viewModel.CheckCommand, key));
                    
                    grid.Children.Add(textBox);
                }
                else
                {
                    Button button = new Button();
                    button.Content = tile.Value.ToString();

                    grid.Children.Add(button);
                }
                index++;
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
                    value = System.Convert.ToInt32(button.Content.ToString());
                }
                else if (text != null)
                {
                    value = System.Convert.ToInt32(text.Text);
                }

                if (value == System.Convert.ToInt32(viewModel.TileChanged) &&
                    value != viewModel.CurrentLevel[i].Value)
                {
                    /* For one element Check */
                    checkIndex = i;
                    break;
                }
            }

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
                    viewModel.GameOver = gameOver;
                }
                else
                {
                    text.Text = "0";
                }
            }

        }
    }
}
