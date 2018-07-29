namespace CustomCollections.Generic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Generic Queue
    /// </summary>
    /// <typeparam name="T">Type of elements inside</typeparam>
    /// <seealso cref="System.Collections.Generic.IEnumerable{T}" />
    [ComVisible(false)]
    public class Queue<T> : IEnumerable<T>
    {
        #region private vars        
        /// <summary>
        /// The default capacity
        /// </summary>
        private const int DefaultCapacity = 16;

        /// <summary>
        /// The version
        /// </summary>
        private int version;

        /// <summary>
        /// The head
        /// </summary>
        private int head;

        /// <summary>
        /// The tail
        /// </summary>
        private int tail;

        /// <summary>
        /// The inner array
        /// </summary>
        private T[] innerArray;

        /// <summary>
        /// The capacity
        /// </summary>
        private int capacity;
        #endregion

        #region .ctors        
        /// <summary>
        /// Initializes a new instance of the <see cref="Queue{T}"/> class.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
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
            this.capacity = this.Count;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Queue{T}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <exception cref="ArgumentOutOfRangeException">capacity</exception>
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
        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count { get; private set; }
        #endregion

        #region queue methods        
        /// <summary>
        /// Enqueues the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Enqueue(T item)
        {
            if (this.Count == this.capacity)
            {
                T[] buffer = new T[this.capacity != 0 ? this.capacity * 2 : 1];

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

        /// <summary>
        /// Dequeues this instance.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            Array.Clear(this.innerArray, 0, this.innerArray.Length);
            this.Count = 0;
            this.head = this.tail = 0;
            this.version++;
        }

        /// <summary>
        /// Determines whether [contains] [the specified item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified item]; otherwise, <c>false</c>.
        /// </returns>
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

        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        /// <exception cref="ArgumentOutOfRangeException">Throws when arrayIndex is out of range</exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            ValidateOnNull(array, nameof(array));

            if (this.Count == 0)
            {
                return;
            }

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

        /// <summary>
        /// Trims the excess.
        /// </summary>
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

        /// <summary>
        /// Peeks this instance.
        /// </summary>
        /// <returns>
        /// The last element of the queue
        /// </returns>
        public T Peek()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("You can't peek from empty queue!");
            }

            return this[0];
        }

        /// <summary>
        /// Converts to the array.
        /// </summary>
        /// <returns></returns>
        public T[] ToArray()
        {
            T[] array = new T[this.Count];
            this.CopyTo(array, 0);
            return array;
        }
        #endregion

        #region private methods        
        /// <summary>
        /// Gets or sets the <see cref="T" /> with the specified i.
        /// </summary>
        /// <value>
        /// The <see cref="T" />.
        /// </value>
        /// <param name="i">The index.</param>
        /// <returns>
        /// element at index</returns>
        private T this[int i]
        {
            get => this.innerArray[(this.head + i) % this.innerArray.Length];
            set
            {
                this.innerArray[(this.head + i) % this.innerArray.Length] = value;
            }
        }

        /// <summary>
        /// Validates the on null.
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="name">The name.</param>
        private static void ValidateOnNull<V>(V obj, string name) where V : class
        {
            if (obj is null)
            {
                throw new ArgumentNullException($"{name} is null");
            }
        }
        #endregion

        #region IEnumerable<T> implementation        
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        public Enumerator GetEnumerator()
        {
            return new Queue<T>.Enumerator(this);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
                    => this.GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
                    => this.GetEnumerator();

        /// <summary>
        /// Enumerator of the <see cref="Queue{T}" /></summary>
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
