namespace CustomCollections.Generic
{
    using System;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Binary Tree Collection
    /// </summary>
    /// <typeparam name="T">Type of elements inside</typeparam>
    /// <seealso cref="System.Collections.Generic.IEnumerable{T}" />
    public sealed class BinarySearchTree<T> : IEnumerable<T>
    {
        #region private fields        
        /// <summary>
        /// The version
        /// </summary>
        private int version;

        /// <summary>
        /// The comparison
        /// </summary>
        private readonly Comparison<T> comparison;

        /// <summary>
        /// The root
        /// </summary>
        private Node<T> root;
        #endregion

        #region ctors        
        /// <summary>
        /// Initializes a new instance of the <see cref="BinarySearchTree{T}"/> class.
        /// </summary>
        public BinarySearchTree() : this((Comparison<T>)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinarySearchTree{T}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public BinarySearchTree(IComparer<T> comparer)
            : this(comparer != null ? comparer.Compare : (Comparison<T>)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinarySearchTree{T}"/> class.
        /// </summary>
        /// <param name="comparison">The comparison.</param>
        /// <exception cref="ArgumentException">
        /// Trows when comparer is default and the <typeparamref name="T"/> is not implements <see cref="IComparable{T}"/>
        /// </exception>
        public BinarySearchTree(Comparison<T> comparison)
        {
            switch (comparison)
            {
                case null when !typeof(T).GetInterfaces().Contains(typeof(IComparable<T>)):
                    throw new ArgumentException($"{nameof(T)} must implement {nameof(IComparable<T>)}");
                case null:
                    this.comparison = Comparer<T>.Default.Compare;
                    return;
            }

            this.comparison = comparison;
        }
        #endregion

        #region public api        
        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count of elements.
        /// </value>
        public int Count { get; private set; }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(T item)
        {
            version++;
            
            this.root = AddRec(this.root, item);
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
            return this.Contains(this.root, item);
        }

        /// <summary>
        /// PreOrder traversal.
        /// </summary>
        /// <returns>sequence of traversed values</returns>
        /// <exception cref="InvalidOperationException">Throws when the collection was changer during enumeration</exception>
        public IEnumerable<T> PreOrder()
        {
            IEnumerable<T> Go(Node<T> node)
            {
                int currentVersion = this.version;
                if (node == null)
                {
                    yield break;
                }

                yield return node.Value;

                if (node.Left != null)
                {
                    foreach (var item in Go(node.Left))
                    {
                        yield return item;
                    }
                }

                if (node.Right != null)
                {
                    foreach (var item in Go(node.Right))
                    {
                        yield return item;
                    }
                }

                if (this.version != currentVersion)
                {
                    throw new InvalidOperationException("bad enumeration");
                }
            }

            return Go(root);

        }

        /// <summary>
        /// InOrder traversal.
        /// </summary>
        /// <returns>sequence of traversed values</returns>
        /// <exception cref="InvalidOperationException">Throws when the collection was changer during enumeration</exception>
        public IEnumerable<T> InOrder()
        {
            IEnumerable<T> Go(Node<T> node)
            {
                int currentVersion = this.version;
                if (node == null)
                {
                    yield break;
                }

                if (node.Left != null)
                {
                    foreach (var item in Go(node.Left))
                    {
                        yield return item;
                    }
                }

                yield return node.Value;

                if (node.Right != null)
                {
                    foreach (var item in Go(node.Right))
                    {
                        yield return item;
                    }
                }

                if (this.version != currentVersion)
                {
                    throw new InvalidOperationException("bad");
                }
            }

            return Go(root);
        }

        /// <summary>
        /// PostOrder traversal.
        /// </summary>
        /// <returns>sequence of traversed values</returns>
        /// <exception cref="InvalidOperationException">Throws when the collection was changer during enumeration</exception>
        public IEnumerable<T> PostOrder()
        {
            IEnumerable<T> Go(Node<T> node)
            {
                int currentVersion = this.version;

                if (node == null)
                {
                    yield break;
                }

                if (node.Left != null)
                {
                    foreach (var item in Go(node.Left))
                    {
                        yield return item;
                    }
                }

                if (node.Right != null)
                {
                    foreach (var item in Go(node.Right))
                    {
                        yield return item;
                    }
                }

                yield return node.Value;

                if (this.version != currentVersion)
                {
                    throw new InvalidOperationException("bad");
                }
            }

            return Go(root);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return InOrder().GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region private methods        
        /// <summary>
        /// Adds the record.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="value">The value.</param>
        /// <returns>root node</returns>
        private Node<T> AddRec(Node<T> node, T value)
        {
            if (node == null)
            {
                Count++;
                return new Node<T>(value);
            }

            int comparisonResult = this.comparison(node.Value, value);

            if (comparisonResult > 0)
            {
                node.Left = AddRec(node.Left, value);
            }
            else if (comparisonResult < 0)
            {
                node.Right = AddRec(node.Right, value);
            }

            return node;
        }

        /// <summary>
        /// Determines whether [contains] [the specified node].
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified node]; otherwise, <c>false</c>.
        /// </returns>
        private bool Contains(Node<T> node, T value)
        {
            if (node == null)
            {
                return false;
            }

            int comparisonResult = this.comparison(node.Value, value);

            if (comparisonResult > 0)
            {
                return Contains(node.Left, value);
            }
            if (comparisonResult < 0)
            {
                return Contains(node.Right, value);
            }
            if (EqualityComparer<T>.Default.Equals(node.Value, value))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region inner classes
#pragma warning disable 693
        /// <summary>
        /// Node class
        /// </summary>
        /// <typeparam name="T">Type of value inside</typeparam>
        private sealed class Node<T>
#pragma warning restore 693
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="BinarySearchTree" /> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public Node(T value)
            {
                this.Value = value;
            }

            /// <summary>
            /// Gets or sets the left.
            /// </summary>
            /// <value>
            /// The left.
            /// </value>
            public Node<T> Left { get; internal set; }

            /// <summary>
            /// Gets or sets the right.
            /// </summary>
            /// <value>
            /// The right.
            /// </value>
            public Node<T> Right { get; internal set; }

            /// <summary>
            /// Gets the value.
            /// </summary>
            /// <value>
            /// The value.
            /// </value>
            public T Value { get; }
        }
        #endregion
    }
}
