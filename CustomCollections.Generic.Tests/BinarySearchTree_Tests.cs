using System;
using System.Collections.Generic;

namespace CustomCollections.Generic.Tests
{
    using System.Linq;
    using GeneralTestTypes;
    using NUnit.Framework;

    [TestFixture]
    public class BinarySearchTree_Tests
    {
        #region Integer & default comparer
        [Test]
        public void Can_Add_With_Default_Comparer()
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>
            {
                4, 4, 2, 2, 7
            };

            Assert.AreEqual(3, tree.Count);
            CollectionAssert.AreEqual(new int[] { 2, 4, 7 }, tree.InOrder().ToArray());
        }

        [TestCase(new int[] { 4, 2, 3, 1, 8, 7, 10 }, 3, ExpectedResult = true)]
        [TestCase(new int[] { 4, 2, 3, 1, 8, 7, 10 }, 5, ExpectedResult = false)]
        [TestCase(new int[] { }, 5, ExpectedResult = false)]
        [TestCase(new int[] { 2 }, 2, ExpectedResult = true)]
        [TestCase(new int[] { 2 }, 0, ExpectedResult = false)]
        public bool Can_Contains_With_Default_Comparer(int[] data, int wanted)
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();

            foreach (var item in data)
            {
                tree.Add(item);
            }

            return tree.Contains(wanted);
        }

        [TestCase(new int[] { 4, 2, 3, 1, 8, 7, 10 }, new int[] { 1, 2, 3, 4, 7, 8, 10 })]
        [TestCase(new int[] { }, new int[] { })]
        [TestCase(new int[] { 2 }, new int[] { 2 })]
        public void Can_Enumerate_InOrder_With_Default_Comparer(int[] data, int[] expected)
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();

            foreach (var item in data)
            {
                tree.Add(item);
            }

            CollectionAssert.AreEqual(expected, tree.InOrder().ToArray());
        }

        [TestCase(new int[] { 4, 2, 3, 1, 8, 7, 10 }, new int[] { 4, 2, 1, 3, 8, 7, 10 })]
        [TestCase(new int[] { }, new int[] { })]
        [TestCase(new int[] { 2 }, new int[] { 2 })]
        public void Can_Enumerate_PreOrder_With_Default_Comparer(int[] data, int[] expected)
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();

            foreach (var item in data)
            {
                tree.Add(item);
            }

            CollectionAssert.AreEqual(expected, tree.PreOrder().ToArray());
        }

        [TestCase(new int[] { 4, 2, 3, 1, 8, 7, 10 }, new int[] { 1, 3, 2, 7, 10, 8, 4 })]
        [TestCase(new int[] { }, new int[] { })]
        [TestCase(new int[] { 2 }, new int[] { 2 })]
        public void Can_Enumerate_PostOrder_With_Default_Comparer(int[] data, int[] expected)
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();

            foreach (var item in data)
            {
                tree.Add(item);
            }

            CollectionAssert.AreEqual(expected, tree.PostOrder().ToArray());
        }
        #endregion

        #region Integer & custom comparer
        [Test]
        public void Can_Add_With_Custom_Comparer()
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>(CompareByZerosCount)
            {
                70000, 707070, 404, 400, 2, 200
            };

            Assert.AreEqual(5, tree.Count);
            CollectionAssert.AreEqual(new int[] { 2, 404, 400, 707070, 70000 }, tree.InOrder().ToArray());
        }

        [TestCase(new int[] { 70000, 707070, 404, 400, 2, 200 }, 400, ExpectedResult = true)]
        [TestCase(new int[] { 70000, 707070, 404, 400, 2, 200 }, 200, ExpectedResult = false)]
        [TestCase(new int[] { }, 5, ExpectedResult = false)]
        public bool Can_Contains_With_Custom_Comparer(int[] data, int wanted)
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>(CompareByZerosCount);

            foreach (var item in data)
            {
                tree.Add(item);
            }

            return tree.Contains(wanted);
        }

        [TestCase(new int[] { 70000, 707070, 404, 400, 2, 200 }, new int[] { 2, 404, 400, 707070, 70000})]
        [TestCase(new int[] { }, new int[] {})]
        public void Can_Enumerate_InOrder_With_Custom_Comparer(int[] data, int[] expected)
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>(CompareByZerosCount);

            foreach (var item in data)
            {
                tree.Add(item);
            }

            CollectionAssert.AreEqual(expected, tree.InOrder().ToArray());
        }

        [TestCase(new int[] { 70000, 707070, 404, 400, 2, 200 }, new int[] { 2, 400, 404, 707070, 70000 })]
        [TestCase(new int[] { }, new int[] { })]
        public void Can_Enumerate_PostOrder_With_Custom_Comparer(int[] data, int[] expected)
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>(CompareByZerosCount);

            foreach (var item in data)
            {
                tree.Add(item);
            }

            CollectionAssert.AreEqual(expected, tree.PostOrder().ToArray());
        }

        [TestCase(new int[] { 70000, 707070, 404, 400, 2, 200 }, new int[] { 70000, 707070, 404, 2, 400 })]
        [TestCase(new int[] { }, new int[] { })]
        public void Can_Enumerate_PreOrder_With_Custom_Comparer(int[] data, int[] expected)
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>(CompareByZerosCount);

            foreach (var item in data)
            {
                tree.Add(item);
            }

            CollectionAssert.AreEqual(expected, tree.PreOrder().ToArray());
        }
        #endregion

        #region System.String & default comparer
        [Test]
        public void Can_Add_String_With_Default_Comparer()
        {
            BinarySearchTree<string> tree = new BinarySearchTree<string>()
            {
                "eee", "fff", "ddd", "ccc", "aaa", "bbb"
            };

            Assert.AreEqual(6, tree.Count);
            CollectionAssert.AreEqual(new string[] { "aaa", "bbb", "ccc", "ddd", "eee", "fff" }, tree.InOrder().ToArray());
        }

        [TestCase(new []{ "eee", "fff", "ddd", "ccc", "aaa", "bbb" }, "fff", ExpectedResult = true)]
        [TestCase(new [] { "eee", "fff", "ddd", "ccc", "aaa", "bbb" }, "ggg", ExpectedResult = false)]
        [TestCase(new string[] { }, "5", ExpectedResult = false)]
        public bool Can_Contains_With_Custom_Comparer(string[] data, string wanted)
        {
            BinarySearchTree<string> tree = new BinarySearchTree<string>();

            foreach (var item in data)
            {
                tree.Add(item);
            }

            return tree.Contains(wanted);
        }

        [TestCase(new[] { "eee", "fff", "ddd", "ccc", "aaa", "bbb" }, new [] {"aaa", "bbb", "ccc", "ddd", "eee", "fff"})]
        [TestCase(new string[] { }, new string[] { })]
        public void Can_Enumerate_String_InOrder_With_DEfault_Comparer(string[] data, string[] expected)
        {
            BinarySearchTree<string> tree = new BinarySearchTree<string>();

            foreach (var item in data)
            {
                tree.Add(item);
            }

            CollectionAssert.AreEqual(expected, tree.InOrder().ToArray());
        }

        [TestCase(new[] { "eee", "fff", "ddd", "ccc", "aaa", "bbb" }, new[] { "bbb", "aaa", "ccc", "ddd", "fff", "eee" })]
        [TestCase(new string[] { }, new string[] { })]
        public void Can_Enumerate_String_PostOrder_With_DEfault_Comparer(string[] data, string[] expected)
        {
            BinarySearchTree<string> tree = new BinarySearchTree<string>();

            foreach (var item in data)
            {
                tree.Add(item);
            }

            CollectionAssert.AreEqual(expected, tree.PostOrder().ToArray());
        }

        [TestCase(new[] { "eee", "fff", "ddd", "ccc", "aaa", "bbb" }, new[] { "eee", "ddd", "ccc", "aaa", "bbb", "fff" })]
        [TestCase(new string[] { }, new string[] { })]
        public void Can_Enumerate_String_PreOrder_With_Default_Comparer(string[] data, string[] expected)
        {
            BinarySearchTree<string> tree = new BinarySearchTree<string>();

            foreach (var item in data)
            {
                tree.Add(item);
            }

            CollectionAssert.AreEqual(expected, tree.PreOrder().ToArray());
        }
        #endregion

        #region System.String & custom comparer
        [Test]
        public void Can_Add_String_With_Custom_Comparer()
        {
            BinarySearchTree<string> tree = new BinarySearchTree<string>(CompareByLength)
            {
                "aaaa", "aa", "aaa", "a", "aaaaaa", "aaaaa"
            };

            Assert.AreEqual(6, tree.Count);
            CollectionAssert.AreEqual(new string[] { "a", "aa", "aaa", "aaaa", "aaaaa", "aaaaaa" }, tree.InOrder().ToArray());
        }

        [TestCase(new[] { "aaaa", "aa", "aaa", "a", "aaaaaa", "aaaaa" }, "aaaaa", ExpectedResult = true)]
        [TestCase(new[] { "aaaa", "aa", "aaa", "a", "aaaaaa", "aaaaa", "ggg" }, "ggg", ExpectedResult = false)]
        [TestCase(new string[] { }, "5", ExpectedResult = false)]
        public bool Can_Contains_String_With_Custom_Comparer(string[] data, string wanted)
        {
            BinarySearchTree<string> tree = new BinarySearchTree<string>(CompareByLength);

            foreach (var item in data)
            {
                tree.Add(item);
            }

            return tree.Contains(wanted);
        }

        [TestCase(new[] { "aaaa", "aa", "aaa", "a", "aaaaaa", "aaaaa" }, 
                  new[] { "a", "aa", "aaa", "aaaa", "aaaaa", "aaaaaa" })]
        [TestCase(new string[] { }, new string[] { })]
        public void Can_Enumerate_String_InOrder_With_Custom_Comparer(string[] data, string[] expected)
        {
            BinarySearchTree<string> tree = new BinarySearchTree<string>();

            foreach (var item in data)
            {
                tree.Add(item);
            }

            CollectionAssert.AreEqual(expected, tree.InOrder().ToArray());
        }

        [TestCase(new[] { "aaaa", "aa", "aaa", "a", "aaaaaa", "aaaaa" }, 
                  new[] { "aaaa", "aa", "a", "aaa", "aaaaaa", "aaaaa" })]
        [TestCase(new string[] { }, new string[] { })]
        public void Can_Enumerate_String_PreOrder_With_Custom_Comparer(string[] data, string[] expected)
        {
            BinarySearchTree<string> tree = new BinarySearchTree<string>(CompareByLength);

            foreach (var item in data)
            {
                tree.Add(item);
            }

            CollectionAssert.AreEqual(expected, tree.PreOrder().ToArray());
        }

        [TestCase(
            new[] { "aaaa", "aa", "aaa", "a", "aaaaaa", "aaaaa" }, 
            new[] { "a", "aaa", "aa", "aaaaa", "aaaaaa", "aaaa" })]
        [TestCase(new string[] { }, new string[] { })]
        public void Can_Enumerate_String_PostOrder_With_Custom_Comparer(string[] data, string[] expected)
        {
            BinarySearchTree<string> tree = new BinarySearchTree<string>(CompareByLength);

            foreach (var item in data)
            {
                tree.Add(item);
            }

            CollectionAssert.AreEqual(expected, tree.PostOrder().ToArray());
        }
        #endregion

        #region Reference Type & default comparer
        [Test]
        public void Can_Add_RT_With_Default_Comparer()
        {
            BinarySearchTree<Book> tree = new BinarySearchTree<Book>()
            {
                new Book(4),
                new Book(4),
                new Book(2),
                new Book(2),
                new Book(7)
            };

            Assert.AreEqual(3, tree.Count);
            CollectionAssert.AreEqual(new Book[] { new Book(2), new Book(4), new Book(7) }, tree.InOrder().ToArray());
        }

        [TestCase(new int[] { 4, 2, 3, 1, 8, 7, 10 }, 3, ExpectedResult = true)]
        [TestCase(new int[] { 4, 2, 3, 1, 8, 7, 10 }, 5, ExpectedResult = false)]
        [TestCase(new int[] { }, 5, ExpectedResult = false)]
        [TestCase(new int[] { 2 }, 2, ExpectedResult = true)]
        [TestCase(new int[] { 2 }, 0, ExpectedResult = false)]
        public bool Can_Contains_RT_With_Default_Comparer(int[] data, int wanted)
        {
            BinarySearchTree<Book> tree = new BinarySearchTree<Book>();

            foreach (var item in data)
            {
                tree.Add(new Book(item));
            }

            return tree.Contains(new Book(wanted));
        }

        [TestCase(new int[] { 4, 2, 3, 1, 8, 7, 10 }, new int[] { 1, 2, 3, 4, 7, 8, 10 })]
        [TestCase(new int[] { }, new int[] { })]
        [TestCase(new int[] { 2 }, new int[] { 2 })]
        public void Can_Enumerate_RT_InOrder_With_Default_Comparer(int[] data, int[] expected)
        {
            BinarySearchTree<Book> tree = new BinarySearchTree<Book>();

            foreach (var item in data)
            {
                tree.Add(new Book(item));
            }

            List<Book> books = expected.Select(item => new Book(item)).ToList();

            CollectionAssert.AreEqual(books, tree.InOrder().ToArray());
        }
        
        [TestCase(new int[] { 4, 2, 3, 1, 8, 7, 10 }, new int[] { 4, 2, 1, 3, 8, 7, 10 })]
        [TestCase(new int[] { }, new int[] { })]
        [TestCase(new int[] { 2 }, new int[] { 2 })]
        public void Can_Enumerate_RT_PreOrder_With_Default_Comparer(int[] data, int[] expected)
        {
            BinarySearchTree<Book> tree = new BinarySearchTree<Book>();

            foreach (var item in data)
            {
                tree.Add(new Book(item));
            }

            List<Book> books = expected.Select(item => new Book(item)).ToList();
            CollectionAssert.AreEqual(books, tree.PreOrder().ToArray());
        }

        [TestCase(new int[] { 4, 2, 3, 1, 8, 7, 10 }, new int[] { 1, 3, 2, 7, 10, 8, 4 })]
        [TestCase(new int[] { }, new int[] { })]
        [TestCase(new int[] { 2 }, new int[] { 2 })]
        public void Can_Enumerate_RT_PostOrder_With_Default_Comparer(int[] data, int[] expected)
        {
            BinarySearchTree<Book> tree = new BinarySearchTree<Book>();

            foreach (var item in data)
            {
                tree.Add(new Book(item));
            }

            List<Book> books = expected.Select(item => new Book(item)).ToList();

            CollectionAssert.AreEqual(books, tree.PostOrder().ToArray());
        }
        #endregion

        #region Reference Type & custom comparer
        [Test]
        public void Can_Add_RT_With_Custom_Comparer()
        {
            BinarySearchTree<Book> tree = new BinarySearchTree<Book>(CompareByZerosCount)
            {
                new Book(70000), new Book(707070), new Book(404), new Book(400), new Book(2), new Book(200)
            };

            Assert.AreEqual(5, tree.Count);
            CollectionAssert.AreEqual(new Book[]
                {
                    new Book(2),
                    new Book(404),
                    new Book(400),
                    new Book(707070),
                    new Book(70000)
                }, 
                tree.InOrder().ToArray());
        }

        [TestCase(new int[] { 70000, 707070, 404, 400, 2, 200 }, 400, ExpectedResult = true)]
        [TestCase(new int[] { 70000, 707070, 404, 400, 2, 200 }, 200, ExpectedResult = false)]
        [TestCase(new int[] { }, 5, ExpectedResult = false)]
        public bool Can_Contains_RT_With_Custom_Comparer(int[] data, int wanted)
        {
            BinarySearchTree<Book> tree = new BinarySearchTree<Book>(CompareByZerosCount);

            foreach (var item in data)
            {
                tree.Add(new Book(item));
            }

            return tree.Contains(new Book(wanted));
        }

        [TestCase(new int[] { 70000, 707070, 404, 400, 2, 200 }, new int[] { 2, 404, 400, 707070, 70000 })]
        [TestCase(new int[] { }, new int[] { })]
        public void Can_Enumerate_RT_InOrder_With_Custom_Comparer(int[] data, int[] expected)
        {
            BinarySearchTree<Book> tree = new BinarySearchTree<Book>(CompareByZerosCount);

            foreach (var item in data)
            {
                tree.Add(new Book(item));
            }
            List<Book> books = expected.Select(item => new Book(item)).ToList();
            CollectionAssert.AreEqual(books, tree.InOrder().ToArray());
        }

        [TestCase(new int[] { 70000, 707070, 404, 400, 2, 200 }, new int[] { 2, 400, 404, 707070, 70000 })]
        [TestCase(new int[] { }, new int[] { })]
        public void Can_Enumerate_RT_PostOrder_With_Custom_Comparer(int[] data, int[] expected)
        {
            BinarySearchTree<Book> tree = new BinarySearchTree<Book>(CompareByZerosCount);

            foreach (var item in data)
            {
                tree.Add(new Book(item));
            }

            List<Book> books = expected.Select(item => new Book(item)).ToList();
            CollectionAssert.AreEqual(books, tree.PostOrder().ToArray());
        }

        [TestCase(new int[] { 70000, 707070, 404, 400, 2, 200 }, new int[] { 70000, 707070, 404, 2, 400 })]
        [TestCase(new int[] { }, new int[] { })]
        public void Can_Enumerate_RT_PreOrder_With_Custom_Comparer(int[] data, int[] expected)
        {
            BinarySearchTree<Book> tree = new BinarySearchTree<Book>(CompareByZerosCount);

            foreach (var item in data)
            {
                tree.Add(new Book(item));
            }

            List<Book> books = expected.Select(item => new Book(item)).ToList();
            CollectionAssert.AreEqual(books, tree.PreOrder().ToArray());
        }
        #endregion

        #region Value Type & custom comparer
        [Test]
        public void Can_Add_VT_With_Default_Comparer()
        {
            BinarySearchTree<Point> tree = new BinarySearchTree<Point>(CompareByX)
            {
                new Point(4, 4), new Point(4, 5), new Point(2, 4), new Point(2, 4), new Point(7, 4)
            };

            Assert.AreEqual(3, tree.Count);
            CollectionAssert.AreEqual(new Point[]
            {
                new Point(2, 4),
                new Point(4, 4),
                new Point(7, 4)
            }, tree.InOrder().ToArray());
        }

        [TestCase(new int[] { 4, 2, 3, 1, 8, 7, 10 }, 3, ExpectedResult = true)]
        [TestCase(new int[] { 4, 2, 3, 1, 8, 7, 10 }, 5, ExpectedResult = false)]
        [TestCase(new int[] { }, 5, ExpectedResult = false)]
        [TestCase(new int[] { 2 }, 2, ExpectedResult = true)]
        [TestCase(new int[] { 2 }, 0, ExpectedResult = false)]
        public bool Can_Contains_VT_With_Default_Comparer(int[] data, int wanted)
        {
            BinarySearchTree<Point> tree = new BinarySearchTree<Point>(CompareByX);

            foreach (var item in data)
            {
                tree.Add(new Point(item, 10));
            }

            return tree.Contains(new Point(wanted, 10));
        }

        [TestCase(new int[] { 4, 2, 3, 1, 8, 7, 10 }, new int[] { 1, 2, 3, 4, 7, 8, 10 })]
        [TestCase(new int[] { }, new int[] { })]
        [TestCase(new int[] { 2 }, new int[] { 2 })]
        public void Can_Enumerate_VT_InOrder_With_Default_Comparer(int[] data, int[] expected)
        {
            BinarySearchTree<Point> tree = new BinarySearchTree<Point>(CompareByX);

            foreach (var item in data)
            {
                tree.Add(new Point(item, 10));
            }

            List<Point> books = expected.Select(item => new Point(item, 10)).ToList();
            CollectionAssert.AreEqual(books, tree.InOrder().ToArray());
        }

        [TestCase(new int[] { 4, 2, 3, 1, 8, 7, 10 }, new int[] { 4, 2, 1, 3, 8, 7, 10 })]
        [TestCase(new int[] { }, new int[] { })]
        [TestCase(new int[] { 2 }, new int[] { 2 })]
        public void Can_Enumerate_VT_PreOrder_With_Default_Comparer(int[] data, int[] expected)
        {
            BinarySearchTree<Point> tree = new BinarySearchTree<Point>(CompareByX);

            foreach (var item in data)
            {
                tree.Add(new Point(item, 10));
            }

            List<Point> books = expected.Select(item => new Point(item, 10)).ToList();
            CollectionAssert.AreEqual(books, tree.PreOrder().ToArray());
        }

        [TestCase(new int[] { 4, 2, 3, 1, 8, 7, 10 }, new int[] { 1, 3, 2, 7, 10, 8, 4 })]
        [TestCase(new int[] { }, new int[] { })]
        [TestCase(new int[] { 2 }, new int[] { 2 })]
        public void Can_Enumerate_VT_PostOrder_With_Default_Comparer(int[] data, int[] expected)
        {
            BinarySearchTree<Point> tree = new BinarySearchTree<Point>(CompareByX);

            foreach (var item in data)
            {
                tree.Add(new Point(item, 10));
            }

            List<Point> books = expected.Select(item => new Point(item, 10)).ToList();
            CollectionAssert.AreEqual(books, tree.PostOrder().ToArray());
        }
        #endregion

        #region Custom comparers
        private static int CompareByX(Point lhs, Point rhs)
        {
            if (lhs.X > rhs.X)
            {
                return 1;
            }

            if (lhs.X < rhs.X)
            {
                return -1;
            }

            return 0;
        }

        private static int CompareByZerosCount(Book lhs, Book rhs)
        {
            if (lhs == null || rhs == null)
            {
                throw new ArgumentNullException();
            }

            return CompareByZerosCount(lhs.Pages, rhs.Pages);
        }

        private static int CompareByZerosCount(int lhs, int rhs)
        {
            int lhs_count = lhs.ToString().Count(c => c == '0');
            int rhs_count = rhs.ToString().Count(c => c == '0');

            if (lhs_count > rhs_count)
            {
                return 1;
            }

            if (lhs_count < rhs_count)
            {
                return -1;
            }

            return 0;
        }

        private static int CompareByLength(string lhs, string rhs)
        {
            int lhs_len = lhs.Length;
            int rhs_len = rhs.Length;

            if (lhs_len > rhs_len)
            {
                return 1;
            }

            if (lhs_len < rhs_len)
            {
                return -1;
            }

            return 0;
        }
        #endregion
    }
}
