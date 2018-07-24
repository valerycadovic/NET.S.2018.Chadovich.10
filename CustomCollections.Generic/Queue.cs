namespace CustomCollections.Generic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    [ComVisible(false)]     // Enumerator.Reset is not supported
    public class Queue<T> : IEnumerable<T>
    {
        #region private vars
        private const int DefaultCapacity = 16;

        private int version;

        private int head;

        private int tail;

        public T[] innerArray;

        private int capacity;
        #endregion

        #region .ctors
        public Queue(IEnumerable<T> sequence)
        {
            ValidateOnNull(sequence, nameof(sequence));

            List<T> list = new List<T>();

            foreach (var item in sequence)
            {
                list.Add(item);
            }

            this.innerArray = list.ToArray();
            this.Count = list.Count;
            this.head = 0;
            this.tail = this.Count;
        }

        public Queue(int capacity = DefaultCapacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(capacity)} must be positive");
            }

            this.capacity = capacity;
            this.innerArray = new T[this.capacity];
            this.Count = 0;
        }
        #endregion

        #region properties
        public int Count { get; private set; }
        #endregion

        #region queue methods
        public void Enqueue(T item)
        {
            if (this.Count == this.capacity)
            {
                T[] buffer = new T[this.capacity * 2];

                for (int i = 0; i < this.Count; i++)
                {
                    buffer[i] = this[i];
                }

                this.innerArray = buffer;
                this.capacity *= 2;
                this.head = 0;
                this.tail = this.Count == this.capacity ? 0 : this.Count;
            }

            this.innerArray[this.tail] = item;
            this.tail = (this.tail + 1) % this.innerArray.Length;
            this.Count++;
            this.version++;
        }

        public T Dequeue()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException($"{nameof(Dequeue)} is unavailable fo empty queues");
            }

            T pop = this.innerArray[this.head];
            this.innerArray[this.head] = default;
            this.head = (this.head + 1) % this.Count;
            this.Count--;

            this.version++;
            return pop;
        }

        public void Clear()
        {
            Array.Clear(this.innerArray, 0, this.innerArray.Length);
            this.Count = 0;
            this.head = this.tail = 0;
            this.version++;
        }

        public bool Contains(T item)
        {
            var comparer = EqualityComparer<T>.Default;

            for (int i = 0; i < this.Count; i++)
            {
                if (comparer.Equals(this[i], item))
                {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ValidateOnNull(array, nameof(array));

            if (arrayIndex < 0 || arrayIndex >= this.Count)
            {
                throw new ArgumentOutOfRangeException($"{nameof(arrayIndex)} is out of range");
            }

            if (arrayIndex + this.Count > array.Length)
            {
                throw new ArgumentOutOfRangeException($"{nameof(arrayIndex)} is out of range");
            }

            for (int i = 0; i < this.Count; i++)
            {
                array[i + arrayIndex] = this[i];
            }
        }

        public void TrimExcess()
        {
            T[] buffer = new T[this.Count];

            for (int i = 0; i < this.Count; i++)
            {
                buffer[i] = this[i];
            }

            this.innerArray = buffer;
            this.capacity = this.Count;
            this.head = 0;
            this.tail = this.Count;
            this.version++;
        }

        public T Peek()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("You can't peek from empty queue!");
            }

            return this[this.Count - 1];
        }

        public T[] ToArray()
        {
            T[] array = new T[this.Count];
            this.CopyTo(array, 0);
            return array;
        }
        #endregion

        #region private methods
        private T this[int i]
        {
            get => this.innerArray[(this.head + i) % this.innerArray.Length];
            set
            {
                this.innerArray[(this.head + i) % this.innerArray.Length] = value;
            }
        }

        private static void ValidateOnNull<V>(V obj, string name) where V : class
        {
            if (obj is null)
            {
                throw new ArgumentNullException($"{name} is null");
            }
        }
        #endregion

        #region IEnumerable<T> implementation
        public Enumerator GetEnumerator()
        {
            return new Queue<T>.Enumerator(this);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
            => this.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        public struct Enumerator : IEnumerator<T>
        {
            private int version;
            private Queue<T> queue;
            private int index;
            private T current;

            public T Current
            {
                get
                {
                    switch (this.index)
                    {
                        case -1:
                            throw new InvalidOperationException($"enumeration has been not started");
                        case -2:
                            throw new InvalidOperationException($"enumeration has been finished");
                        default:
                            return this.current;
                    }
                }
            }

            object IEnumerator.Current => this.Current;

            internal Enumerator(Queue<T> queue)
            {
                this.version = queue.version;
                this.queue = queue;
                this.index = -1;
                this.current = default;
            }

            public void Dispose()
            {
                this.index = -2;
                this.current = default;
            }

            public bool MoveNext()
            {
                if (this.version != queue.version)
                {
                    throw new InvalidOperationException("Enumeration must be unchangeable");
                }

                if (this.index == -2)
                {
                    return false;
                }

                this.index++;

                if (this.index == queue.Count)
                {
                    this.index = -2;
                    this.current = default;
                    return false;
                }

                this.current = queue[index];
                return true;
            }

            public void Reset()
            {
                throw new NotSupportedException($"{nameof(Reset)} is not supported");
            }
        }
        #endregion
    }
}
