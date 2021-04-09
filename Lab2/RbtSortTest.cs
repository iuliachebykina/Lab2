using System;
using NUnit.Framework;

namespace Lab2
{
    [TestFixture]
    public class RbtSortTests
    {
        [Test]
        public void TestNumbers()
        {
            var test = new[] {1, 2, 7, 3, 8, 9, 0, 4, 5, 6};
            var expected = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            var actual = new RbtSort<int>().Sort(test);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestSortedString()
        {
            var test = "abcd".ToCharArray();
            var actual = new RbtSort<char>().Sort(test);
            Console.WriteLine();
            Assert.AreEqual(test, actual);
        }

        [Test]
        public void TestString()
        {
            var test = "abcdjefdfnigkrsycgtrycocno54uxemafymwexdlhmfaklxdghkxflgfnfhlgfd".ToCharArray();
            var expected = "45aaabccccdddddeeeffffffffggggghhhijkkkllllmmmnnnoorrstuwxxxxyyy".ToCharArray();
            var actual = new RbtSort<char>().Sort(test);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestEmpty()
        {
            var test = new char[0];
            Assert.AreEqual(new char[0], new RbtSort<char>().Sort(test));
        }

        [Test]
        public void TestNull()
        {
            char[] test = null;
            Assert.Throws<ArgumentNullException>(() => { new RbtSort<char>().Sort(test); });
        }
    }
}