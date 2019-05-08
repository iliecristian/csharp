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
using System.Windows.Threading;

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
        public int LevelColumns { get; set; }
        public TableSize LevelSize = TableSize._9x9;
        public List<Tile> CurrentLevel { get; set; }

        /* Properties - Timer */
        public DispatcherTimer Timer { get; set; }
        public int Delay = DataManager.Instance.TimerSeconds;
        public DateTime Deadline { get; set; }

        private string secondsRemaining;
        public string SecondsRemaining
        {
            get { return secondsRemaining; }
            set
            {
                if (secondsRemaining == value)
                    return;
                secondsRemaining = value;

                OnPropertyChanged("SecondsRemaining");
            }
        }
        
        private int tileChanged;
        public int TileChanged
        {
            get
            {
                return tileChanged;
            }
            set
            {
                if (value.GetType() ==  tileChanged.GetType())
                    tileChanged = value;
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

        private ICommand size4x4Command;
        public ICommand Size4x4Command
        {
            get
            {
                if (size4x4Command == null)
                    size4x4Command = new RelayCommand(actions.Size4x4, param => true);
                return size4x4Command;
            }
        }

        private ICommand size6x6Command;
        public ICommand Size6x6Command
        {
            get
            {
                if (size6x6Command == null)
                    size6x6Command = new RelayCommand(actions.Size6x6, param => true);
                return size6x6Command;
            }
        }

        private ICommand size9x9Command;
        public ICommand Size9x9Command
        {
            get
            {
                if (size9x9Command == null)
                    size9x9Command = new RelayCommand(actions.Size9x9, param => true);
                return size9x9Command;
            }
        }

        private ICommand sizeStandardCommand;
        public ICommand SizeStandardCommand
        {
            get
            {
                if (sizeStandardCommand == null)
                    sizeStandardCommand = new RelayCommand(actions.SizeStandard, param => true);
                return sizeStandardCommand;
            }
        }

        private ICommand saveGame;
        public ICommand SaveGame
        {
            get
            {
                if (saveGame == null)
                    saveGame = new RelayCommand(actions.SaveGame, param => true);
                return saveGame;
            }
        }

        private ICommand openGameCommand;
        public ICommand OpenGameCommand
        {
            get
            {
                if (openGameCommand == null)
                    openGameCommand = new RelayCommand(actions.OpenGame, param => true);
                return openGameCommand;
            }
        }

        private ICommand statisticsCommand;
        public ICommand StatisticsCommand
        {
            get
            {
                if (statisticsCommand == null)
                    statisticsCommand = new RelayCommand(actions.Statistics, param => true);
                return statisticsCommand;
            }
        }

        private ICommand exitCommand;
        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                    exitCommand = new RelayCommand(actions.Exit, param => true);
                return exitCommand;
            }
        }
    }
}
