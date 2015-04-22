using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FirstFloor.ModernUI.Presentation;

namespace DartCounter
{
    class GameViewModel : INotifyPropertyChanged
    {
        public GameViewModel()
        {
            this.P1Scores = new ObservableCollection<string>();
            this.P2Scores = new ObservableCollection<string>();
            this.previousScores = new List<int>();

            this.AddScore = new RelayCommand(o => this.NewScore());
            this.StartGame = new RelayCommand(o => this.StartNewGame());

            this.Simple = true;
        }

        private int p1Score;
        private int p2Score;
        private int startNumber;

        public int currentAccumlatedScore;

        public int CurrentAccumlatedScore
        {
            get { return this.currentAccumlatedScore; }
            set { this.currentAccumlatedScore = value; this.OnPropertyChanged("CurrentAccumlatedScore"); }
        }

        public int dartCount;

        public int DartCount
        {
            get { return this.dartCount; }
            set { this.dartCount = value; this.OnPropertyChanged("DartCount"); }
        }

        public int StartNumber
        {
            get
            {
                return startNumber;
            }
            set
            {
                startNumber = value;
                OnPropertyChanged("StartNumber");
            }
        }

        public int actualScore;

        public int ActualScore
        {
            get
            {
                return actualScore;
            }
            set
            {
                actualScore = value;
                OnPropertyChanged("ActualScore");
            }
        }

        private List<int> previousScores;

        private ObservableCollection<string> p1Scores;
        private ObservableCollection<string> p2Scores;

        public ObservableCollection<string> P1Scores
        {
            get
            {
                return p1Scores;
            }
            set
            {
                p1Scores = value;
                this.OnPropertyChanged("P1Scores");
            }
        }

        public ObservableCollection<string> P2Scores
        {
            get
            {
                return p2Scores;
            }
            set
            {
                p2Scores = value;
                this.OnPropertyChanged("P2Scores");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ICommand AddScore { get; set; }

        public ICommand StartGame { get; set; }

        private int actualPlayer = 1;

        public int ActualPlayer
        {
            get
            {
                return actualPlayer;
            }
            set
            {
                actualPlayer = value;
                OnPropertyChanged("ActualPlayer");
            }
        }

        private bool simple;

        public bool Simple
        {
            get
            {
                return simple;
            }
            set
            {
                simple = value;
                OnPropertyChanged("Simple");
            }
        }

        private bool @double;

        public bool Double
        {
            get
            {
                return @double;
            }
            set
            {
                @double = value;
                OnPropertyChanged("Double");
            }
        }

        private bool triple;

        public bool Triple
        {
            get
            {
                return @triple;
            }
            set
            {
                @triple = value;
                OnPropertyChanged("Triple");
            }
        }

        private void NewScore()
        {
            int multiplier = 1;
            if (@double)
                multiplier = 2;
            else if (triple)
                multiplier = 3;

            //implement radio button binding logic

            ActualScore = ActualScore * multiplier;
            CurrentAccumlatedScore += ActualScore;

            previousScores.Add(ActualScore);

            this.ActualScore = 0;

            if (this.dartCount == 3)
            {
                //add score
                if (actualPlayer == 1)
                {
                    if (p1Score >= currentAccumlatedScore)
                    {
                        p1Score -= currentAccumlatedScore;
                    }

                    p1Scores.Add(String.Format("{0} ({1}, {2}, {3}) ", p1Score, previousScores[0], previousScores[1], previousScores[2]));
                }
                else
                {
                    if (p2Score >= currentAccumlatedScore)
                    {
                        p2Score -= currentAccumlatedScore;
                    }

                    P2Scores.Add(String.Format("{0} ({1}, {2}, {3}) ", p2Score, previousScores[0], previousScores[1], previousScores[2]));
                }

                //reset dartCount
                this.DartCount = 1;
                this.previousScores.Clear();

                //reset accumlated score
                this.CurrentAccumlatedScore = 0;

                //change player
                if (actualPlayer == 1)
                {
                    ActualPlayer = 2;
                }
                else
                {
                    ActualPlayer = 1;
                }
            }
            else
            {
                DartCount++;
            }

            //reset multiplier
            this.Simple = true;
        }

        private void StartNewGame()
        {
            p1Scores.Clear();
            p1Scores.Add(startNumber.ToString());
            p1Score = startNumber;


            p2Scores.Clear();
            p2Scores.Add(startNumber.ToString());
            p2Score = startNumber;

            this.DartCount = 1;
            this.ActualPlayer = 1;
            this.CurrentAccumlatedScore = 0;
            this.ActualScore = 0;
        }
    }
}
