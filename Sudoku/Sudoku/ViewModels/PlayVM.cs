using Sudoku.Commands;
using Sudoku.Models;
using Sudoku.Services;
using Sudoku.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sudoku.ViewModels
{
    class PlayVM : BaseVM
    {
        private PlayActions actions;

        /* Properties */
        private bool canExecuteCommnad = true;
        public bool CanExecuteCommand
        {
            get
            {
                return canExecuteCommnad;
            }
            set
            {
                if (canExecuteCommnad == value)
                    return;
                canExecuteCommnad = value;
            }
        }

        public enum TableSize
        {
            _4x4,
            _6x6,
            _9x9
        }

        public string UserImage { get => DataManager.Instance.CurrentUser.Image; }
        public string UserName { get => DataManager.Instance.CurrentUser.Name; }
        public TableSize LevelSize = TableSize._4x4;
        public int LevelColumns { get; set; }
        public List<Tile> CurrentLevel { get; set; }

        private int tileChanged;
        public int TileChanged
        {
            get
            {
                return tileChanged;
            }
            set
            {
                tileChanged = value;
            }
        }

        private bool gameOver;
        public bool GameOver
        {
            get { return gameOver; }
            set
            {
                if (value == gameOver)
                    return;
                gameOver = value;

                if (value == true)
                {
                    GameOverText = "GAME OVER";
                }
            }
        }

        private string gameOverText;
        public string GameOverText
        {
            get { return gameOverText; }
            set
            {
                gameOverText = value;
                OnPropertyChanged("GameOverText");
            }
        }

        /* Constructors */
        public PlayVM()
        {
            this.actions = new PlayActions(this);
        }

        /* Commands */
        private ICommand newGameCommand;
        public ICommand NewGameCommand
        {
            get
            {
                if (newGameCommand == null)
                    newGameCommand = new RelayCommand(actions.NewGame, param => CanExecuteCommand);
                return newGameCommand;
            }
        }

        private ICommand checkCommand;
        public ICommand CheckCommand
        {
            get
            {
                if (checkCommand == null)
                    checkCommand = new RelayCommand(actions.Check, param => CanExecuteCommand);
                return checkCommand;
            }
        }
    }
}
