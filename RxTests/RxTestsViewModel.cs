using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RxTests
{
    class RxTestsViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IDisposable _sub;

        private IDisposable _sub2;

        private IDisposable _sub3;

        private valueObserver<int> multipleFiveObs;


        public RxTestModel model { get; set; }
        

        private string title;
        public string Title {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }

        }

        private string isOddString;
        public string IsOddString
        {
            get
            {
                return isOddString;
            }
            set
            {
                isOddString = value;
                OnPropertyChanged("IsOddString");
            }

        }

        private int number;
        public int Number
        {
            get => number;
            set
            {
                number = value;
                OnPropertyChanged("Number");
            }
        }

        private int factorTen;
        public int FactorTen
        {
            get => factorTen;
            set
            {
                factorTen = value;
                OnPropertyChanged("FactorTen");
            }
        }

        private int multipleOfThree;
        public int MultipleOfThree
        {
            get => multipleOfThree;
            set
            {
                multipleOfThree = value;
                OnPropertyChanged("MultipleOfThree");
            }
        }

        private int multipleOfFive;
        public int MultipleOfFive
        {
            get => multipleOfFive;
            set
            {
                multipleOfFive = value;
                OnPropertyChanged("MultipleOfFive");
            }
        }

        private int throttledNum;
        public int ThrottledNum
        {
            get => throttledNum;
            set
            {
                throttledNum = value;
                OnPropertyChanged("ThrottledNum");
            }
        }

        //ICommands

        public MVVMCommand factorTenCommand { get; set; }

        public RxTestsViewModel(RxTestModel modelIn)
        {
            model = modelIn;
            Action<int> updateAction = new Action<int>((num) =>
            {
                Number = num;   
            });
            model.numSubject.Subscribe(updateAction);

            _sub = model.Subscribe((oddString) =>
            {
                IsOddString = oddString;
            });

            Action<int> timesFiveAction = new Action<int>((val) =>
           {
               MultipleOfFive = val * 5;
           });

            multipleFiveObs = new valueObserver<int>(0, timesFiveAction);

            model.times5Obs.Subscribe(multipleFiveObs);

            _sub2 = model.numBSubject.Sample(TimeSpan.FromSeconds(3)).Subscribe((throt) =>
            {
                ThrottledNum = throt;
            });

            factorTenCommand = new MVVMCommand(factorTenClick, (x) => true);

            _sub3 = model.times3Obs.Subscribe(x =>
           {
               MultipleOfThree = Number * 3;
           });
        }

        private async void factorTenClick(object parameters)
        {
            FactorTen = await model.numBSubject.Where(val => val % 10 == 0).FirstAsync();
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void Dispose()
        {
            _sub.Dispose();
            _sub2.Dispose();
            _sub3.Dispose();
        }
    }
}
