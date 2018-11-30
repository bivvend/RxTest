using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive;
using System.Reactive.Subjects;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Timers;

namespace RxTests
{
    class RxTestModel: IObservable<string>  //Implements a string value as a packet
    {
        System.Timers.Timer counter = new System.Timers.Timer();
        

        public Subject<int> numSubject = new Subject<int>();
        public Subject<string> oddSubject = new Subject<string>();

        public BehaviorSubject<int> numBSubject = new BehaviorSubject<int>(0);

        public IObservable<EventPattern<ElapsedEventArgs>> times3Obs;

        public IObservable<int> times5Obs;        


        private int _number;
        public int number 
        {
            get => _number;
            set
            {
                _number = value;
                numSubject.OnNext(number);
                numBSubject.OnNext(number);
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

            times3Obs = Observable.FromEventPattern<ElapsedEventHandler, ElapsedEventArgs>(h => counter.Elapsed += h, h => counter.Elapsed -= h);

            times5Obs = Observable.FromEventPattern<ElapsedEventHandler, ElapsedEventArgs>(h => counter.Elapsed += h, h => counter.Elapsed -= h).Select(h => number);

        }

        private void Counter_Elapsed(object sender, ElapsedEventArgs e)
        {
            number++;
        }
        
        public IDisposable Subscribe(IObserver<string> observer)
        {
            return oddSubject.Subscribe(observer);
        }
    }
}
