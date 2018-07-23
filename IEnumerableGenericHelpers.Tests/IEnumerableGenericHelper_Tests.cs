using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralTestTypes;
using NUnit.Framework;
using IEnumerableHelpersLib;

namespace IEnumerableGenericHelpers.Tests
{
    [TestFixture]
    public class IEnumerableGenericHelper_Tests
    {
        #region Filter tests
        [Test]
        public void Can_Filter_Int32()
        {
            const int CasesCount = 3;

            int[][] cases = new int[CasesCount][]
            {
                new int[] {1, 2, 3, 4, 5},
                new int[] { },
                new int[] {1, 3, 5, 7, 9}
            };

            int[][] expected = new int[CasesCount][]
            {
                new int[] {2, 4},
                new int[] { },
                new int[] { }
            };

            for (int i = 0; i < CasesCount; i++)
            {
                CollectionAssert.AreEqual(expected[i], cases[i].Filter(x => x % 2 == 0).ToArray());
            }
        }

        [Test]
        public void Can_Filter_String()
        {
            const int CasesCount = 3;

            string[][] cases = new string[CasesCount][]
            {
                new[] {"1", "2", "3", "4", "5"},
                new string[] { },
                new[] {"1", "3", "5", "7", "9"}
            };

            string[][] expected = new string[CasesCount][]
            {
                new [] {"2", "4"},
                new string[] { },
                new string[] { }
            };

            for (int i = 0; i < CasesCount; i++)
            {
                CollectionAssert.AreEqual(expected[i], cases[i].Filter(x => int.Parse(x) % 2 == 0).ToArray());
            }
        }

        [Test]
        public void Filter_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ((IEnumerable<int>)null).Filter(x => x > 1));
            Assert.Throws<ArgumentNullException>(() => new int[] { }.Filter(null));
        }
        #endregion

        #region Map tests
        [Test]
        public void Can_Map_Int32()
        {
            const int CasesCount = 3;

            int[][] cases = new int[CasesCount][]
            {
                new int[] {1, 2, 3, 4, 5},
                new int[] { },
                new int[] {1, 3, 5, 7, 9}
            };

            int[][] expected = new int[CasesCount][]
            {
                new int[] {1, 4, 9, 16, 25},
                new int[] { },
                new int[] {1, 9, 25, 49, 81}
            };

            for (int i = 0; i < CasesCount; i++)
            {
                CollectionAssert.AreEqual(expected[i], cases[i].Map(x => x * x).ToArray());
            }
        }

        [Test]
        public void Can_Map_String()
        {
            const int CasesCount = 2;

            string[][] cases = new string[CasesCount][]
            {
                new[] {"Valera", "Chadovich", "Nesvizh", "Belarus", "EPAM", "dotnet"},
                new string[] { }
            };

            string[][] expected = new string[CasesCount][]
            {
                new[] {"VALERA", "CHADOVICH", "NESVIZH", "BELARUS", "EPAM", "DOTNET"},
                new string[] { }
            };

            for (int i = 0; i < CasesCount; i++)
            {
                CollectionAssert.AreEqual(expected[i], cases[i].Map(x => x.ToUpperInvariant()).ToArray());
            }
        }
        
        [Test]
        public void Map_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ((IEnumerable<int>)null).Filter(x => x > 1));
            Assert.Throws<ArgumentNullException>(() => new int[] { }.Filter(null));
        }
        #endregion
    }
}
