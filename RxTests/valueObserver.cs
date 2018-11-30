using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxTests
{
    public class valueObserver<T> : IObserver<T>
    {
        public T valueStored;
        private Action<T> action;

        public valueObserver(int valIn, Action<T> actionIn)
        {
            action = actionIn;
            valueStored = default(T);
        }

        public void OnCompleted()
        {
            valueStored = default(T);
        }

        public void OnError(Exception error)
        {
            valueStored = default(T);
        }

        public void OnNext(T value)
        {
            valueStored = value;
            action(value);
        }
    }
}
