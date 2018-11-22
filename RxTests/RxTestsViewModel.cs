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

            _sub2 = model.numBSubject.Sample(TimeSpan.FromSeconds(10)).Subscribe((throt) =>
            {
                ThrottledNum = throt;
            });

            factorTenCommand = new MVVMCommand(factorTenClick, (x) => true);
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
        }
    }
}
