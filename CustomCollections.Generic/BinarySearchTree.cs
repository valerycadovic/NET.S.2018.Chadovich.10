namespace CustomCollections.Generic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public sealed class BinarySearchTree<T> : IEnumerable<T>
    {
        #region private fields
        private int version;

        private readonly Comparison<T> comparison;
        #endregion

        #region ctors
        public BinarySearchTree() : this((Comparison<T>)null)
        {
        }

        public BinarySearchTree(IComparer<T> comparer)
            : this(comparer != null ? comparer.Compare : (Comparison<T>)null)
        {
        }

        public BinarySearchTree(Comparison<T> compaison)
        {
            this.comparison = comparison ?? Comparer<T>.Default.Compare;
        }
        #endregion

        #region properties
        public Node<T> Root { get; private set; }
        #endregion

        #region public api
        public void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException($"{nameof(item)} is null");
            }

            this.Root = AddRec(this.Root, item);
            version++;
        }

        public bool Contains(T item)
        {
            return this.Contains(this.Root, item);
        }

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
                    throw new InvalidOperationException("bad");
                }
            }

            return Go(Root);

        }

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

            return Go(Root);
        }

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

            return Go(Root);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return InOrder().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region private methods
        private Node<T> AddRec(Node<T> node, T value)
        {
            if (node == null)
            {
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
            else if (comparisonResult < 0)
            {
                return Contains(node.Right, value);
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region inner classes
#pragma warning disable 693
        public sealed class Node<T>
#pragma warning restore 693
        {
            public Node(T value)
            {
                this.Value = value;
            }

            public Node<T> Left { get; internal set; }
            public Node<T> Right { get; internal set; }
            public T Value { get; }
        }
        #endregion
    }
}
