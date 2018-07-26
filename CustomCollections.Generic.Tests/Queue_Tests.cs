using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CustomCollections.Generic.Tests
{
    [TestFixture]
    public class Queue_Tests
    {
        #region System.Int32
        [TestCase(new int[] { 1, 2, 3, 4 }, new int[] { 1, 2, 3, 4, 7 })]
        [TestCase(new int[] { }, new int[] { 7 })]
        public void Can_Enqueue(int[] array, int[] expected)
        {
            Queue<int> queue = new Queue<int>(array);
            queue.Enqueue(7);
            int[] actual = new int[array.Length + 1];
            queue.CopyTo(actual, 0);

            Assert.AreEqual(array.Length + 1, queue.Count);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 1 }, new int[] { })]
        [TestCase(new int[] { 1, 2, 3, 4 }, new int[] { 2, 3, 4 })]
        public void Can_Dequeue(int[] array, int[] expected)
        {
            Queue<int> queue = new Queue<int>(array);
            queue.Dequeue();
            int[] actual = new int[array.Length - 1];
            queue.CopyTo(actual, 0);

            Assert.AreEqual(array.Length - 1, queue.Count);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Can_Enqueue_After_Dequeue()
        {
            Queue<int> queue = new Queue<int>();

            for (int i = 0; i < 4; i++)
            {
                queue.Enqueue(i);
            }

            queue.Dequeue();

            queue.Enqueue(4);

            CollectionAssert.AreEqual(new Queue<int>(new int[] { 1, 2, 3, 4 }), queue);
        }

        [Test]
        public void Can_Clear()
        {
            Queue<int> queue = new Queue<int>(new int[] { 1, 2, 2, 2, 3 });
            queue.Clear();
            Assert.AreEqual(new Queue<int>(), queue);
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5 }, 3, ExpectedResult = true)]
        [TestCase(new int[] { 1, 2, 3, 4, 5 }, 0, ExpectedResult = false)]
        public bool Can_Contains(int[] array, int item)
        {
            Queue<int> queue = new Queue<int>(array);
            return queue.Contains(item);
        }

        [Test]
        public void Can_Peek()
        {
            Queue<int> queue = new Queue<int>(new int[] { 1, 2, 3, 4 });

            var result = queue.Peek();

            Assert.AreEqual(1, result);
            Assert.AreEqual(4, queue.Count);
        }

        [Test]
        public void Peek_Throws_InvalidOperationException_If_Queue_Is_Empty()
        {
            Queue<int> queue = new Queue<int>();
            Assert.Throws<InvalidOperationException>(() => queue.Peek());
        }

        [Test]
        public void Dequeue_Throws_InvalidOperationException_If_Queue_Is_Empty()
        {
            Queue<int> queue = new Queue<int>();
            Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
        }

        [TestCase(new int[] {0, 1, 2, 3, 4})]
        [TestCase(new int[] { 1 })]
        [TestCase(new int[] { })]
        public void Can_Enumerate(int[] array)
        {
            Queue<int> queue = new Queue<int>(array);

            List<int> result = new List<int>();

            foreach (var item in queue)
            {
                result.Add(item);
            }

            CollectionAssert.AreEqual(queue.ToArray(), result.ToArray());
        }

        [TestCase(new int[] { 0, 1, 2, 3, 4 })]
        [TestCase(new int[] { 1 })]
        public void GetEnumerator_Throws_InvalidOperationException_If_Attempts_To_Change(int[] array)
        {
            Queue<int> queue = new Queue<int>(array);

            List<int> result = new List<int>();

            Assert.Throws<InvalidOperationException>(() =>
            {
                foreach (var item in queue)
                {
                    queue.Enqueue(2);
                    result.Add(item);
                }
            });
        }
        #endregion

        #region System.String
        [TestCase(new string[] { "1", "2", "3", "4" }, new string[] { "1", "2", "3", "4", "7" })]
        [TestCase(new string[] { }, new string[] { "7" })]
        public void Can_Enqueue_String(string[] array, string[] expected)
        {
            Queue<string> queue = new Queue<string>(array);
            queue.Enqueue("7");
            string[] actual = new string[array.Length + 1];
            queue.CopyTo(actual, 0);

            Assert.AreEqual(array.Length + 1, queue.Count);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(new [] { "1" }, new string[] { })]
        [TestCase(new [] { "1", "2", "3", "4" }, new [] { "2", "3", "4" })]
        public void Can_Dequeue_String(string[] array, string[] expected)
        {
            Queue<string> queue = new Queue<string>(array);
            queue.Dequeue();
            string[] actual = new string[array.Length - 1];
            queue.CopyTo(actual, 0);

            Assert.AreEqual(array.Length - 1, queue.Count);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Can_Enqueue_After_Dequeue_String()
        {
            Queue<string> queue = new Queue<string>();

            for (int i = 0; i < 4; i++)
            {
                queue.Enqueue(i.ToString());
            }

            queue.Dequeue();

            queue.Enqueue(4.ToString());

            CollectionAssert.AreEqual(new Queue<string>(new [] { "1", "2", "3", "4" }), queue);
        }

        [Test]
        public void Can_Clear_String()
        {
            Queue<string> queue = new Queue<string>(new string[] { "1", "2", "2", "2", "3" });
            queue.Clear();
            Assert.AreEqual(new Queue<string>(), queue);
        }

        [TestCase(new [] { "1", "2", "3", "4", "5" }, "3", ExpectedResult = true)]
        [TestCase(new [] { "1", "2", "3", "4", "5" }, "0", ExpectedResult = false)]
        public bool Can_Contains_string(string[] array, string item)
        {
            Queue<string> queue = new Queue<string>(array);
            return queue.Contains(item);
        }

        [Test]
        public void Can_Peek_string()
        {
            Queue<string> queue = new Queue<string>(new [] { "1", "2", "3", "4" });

            var result = queue.Peek();

            Assert.AreEqual("1", result);
            Assert.AreEqual(4, queue.Count);
        }

        [Test]
        public void Peek_Throws_InvalidOperationException_If_Queue_Is_Empty_string()
        {
            Queue<string> queue = new Queue<string>();
            Assert.Throws<InvalidOperationException>(() => queue.Peek());
        }

        [Test]
        public void Dequeue_Throws_InvalidOperationException_If_Queue_Is_Empty_string()
        {
            Queue<string> queue = new Queue<string>();
            Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
        }

        [Test]
        public void Can_Enumerate_string()
        {
            Queue<string> queue = new Queue<string>(new string[] { "0", "1", "2", "3", "4" });

            List<string> result = new List<string>();

            foreach (var item in queue)
            {
                result.Add(item);
            }

            CollectionAssert.AreEqual(queue.ToArray(), result.ToArray());
        }
        
        [Test]
        public void GetEnumerator_Throws_InvalidOperationException_If_Attempts_To_Change()
        {
            Queue<string> queue = new Queue<string>(new string[] { "0", "1", "2", "3", "4" });

            List<string> result = new List<string>();

            Assert.Throws<InvalidOperationException>(() =>
            {
                foreach (var item in queue)
                {
                    queue.Enqueue("2");
                    result.Add(item);
                }
            });
        }
        #endregion
    }
}
