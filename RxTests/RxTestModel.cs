using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive;
using System.Reactive.Subjects;
using System.Reactive.Disposables;

namespace RxTests
{
    class RxTestModel: IObservable<string>  //Implements a string value as a packet
    {
        System.Timers.Timer counter = new System.Timers.Timer();
        

        public Subject<int> numSubject = new Subject<int>();
        public Subject<string> oddSubject = new Subject<string>();

        private int _number;
        public int number
        {
            get => _number;
            set
            {
                _number = value;
                numSubject.OnNext(number);
                if(value%2 == 0)
                {
                    oddSubject.OnNext("EVEN");
                }
                else
                {
                    oddSubject.OnNext("ODD");
                }
            }
        }

        private string _aString;
        public string aString
        {
            get => _aString;
            set
            {
                _aString = value;
            }
        }

        public RxTestModel()
        {
            number = 0;
            counter.Interval = 500;
            counter.Elapsed += Counter_Elapsed;
            counter.Start();
        }

        private void Counter_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            number++;
        }

        public IDisposable Subscribe(IObserver<string> observer)
        {
            return oddSubject.Subscribe(observer);
        }
    }
}
