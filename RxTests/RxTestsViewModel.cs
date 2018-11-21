using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxTests
{
    class RxTestsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IDisposable _sub;

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
            
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
