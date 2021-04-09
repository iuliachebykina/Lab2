using System;
using NUnit.Framework;

namespace Lab2
{
    [TestFixture]
    public class RbtTests
    {
        [Test]
        public void TestEmptyConstructor()
        {
            var rbt = new RedBlackTree<string>();
            Assert.AreEqual(0, rbt.Size());
            Assert.AreEqual(default, rbt.GetRoot().Data);
        }

        [Test]
        public void TestRbtConstructor()
        {
            var rbt = new RedBlackTree<int>();
            var arr = new[] {1, 2, 3};
            foreach (var item in arr)
            {
                rbt.Insert(item);
            }

            var rbt2 = new RedBlackTree<int>(rbt);
            Assert.AreEqual(rbt.Size(), rbt2.Size());
            Assert.AreEqual(rbt.GetRoot(), rbt2.GetRoot());
        }

        [Test]
        public void TestInsertChar()
        {
            var arr = new[] {'a', 'b', 'c'};
            var rbt = new RedBlackTree<char>();
            foreach (var item in arr)
            {
                rbt.Insert(item);
            }

            Assert.AreEqual(arr.Length, rbt.Size());
        }

        [Test]
        public void TestInsertInt()
        {
            var arr = new[] {1, 2, 3};
            var rbt = new RedBlackTree<int>();
            foreach (var item in arr)
            {
                rbt.Insert(item);
            }

            Assert.AreEqual(arr.Length, rbt.Size());
        }

        [Test]
        public void TestInsertString()
        {
            var arr = new[] {"abc", "def", "ght"};
            var rbt = new RedBlackTree<string>();
            foreach (var item in arr)
            {
                rbt.Insert(item);
            }

            Assert.AreEqual(arr.Length, rbt.Size());
        }


        [Test]
        public void TestInOrder()
        {
            var arr = new[] {1, 2, 3, 4, 5};
            var rbt = new RedBlackTree<int>();
            foreach (var item in arr)
            {
                rbt.Insert(item);
            }

            Assert.AreEqual(arr, rbt.InOrder());
        }

        [Test]
        public void TestPostOrder()
        {
            var arr = new[] {1, 2, 3, 4, 5};
            var res = new[] {1, 3, 5, 4, 2};
            var rbt = new RedBlackTree<int>();
            foreach (var item in arr)
            {
                rbt.Insert(item);
            }

            Assert.AreEqual(res, rbt.PostOrder());
        }

        [Test]
        public void TestPreOrder()
        {
            var arr = new[] {1, 2, 3, 4, 5};
            var res = new[] {2, 1, 4, 3, 5};
            var rbt = new RedBlackTree<int>();
            foreach (var item in arr)
            {
                rbt.Insert(item);
            }

            Assert.AreEqual(res, rbt.PreOrder());
        }

        [Test]
        public void TestDelete()
        {
            var arr = new[] {1, 2, 3, 4, 5};
            var res = new[] {1, 2, 4, 5};
            var rbt = new RedBlackTree<int>();
            foreach (var item in arr)
            {
                rbt.Insert(item);
            }

            rbt.DeleteNode(3);
            Assert.AreEqual(res, rbt.InOrder());
            Assert.AreEqual(arr.Length - 1, rbt.Size());
        }

        [Test]
        public void TestWrongDelete()
        {
            var arr = new[] {1, 2, 3, 4, 5};
            var rbt = new RedBlackTree<int>();
            foreach (var item in arr)
            {
                rbt.Insert(item);
            }

            var exception = Assert.Throws<ArgumentException>(() => { rbt.DeleteNode(7); });
            if (exception != null)
                Assert.AreEqual("Ноды нет в дереве", exception.Message);
            Assert.AreEqual(arr.Length, rbt.Size());
        }

        [Test]
        public void TestSearchTree()
        {
            var arr = new[] {1, 2, 3, 4, 5};
            var rbt = new RedBlackTree<int>();
            foreach (var item in arr)
            {
                rbt.Insert(item);
            }

            var n = rbt.SearchTree(4);
            Assert.AreEqual(4, n.Data);
            Assert.AreEqual(3, n.Left.Data);
            Assert.AreEqual(5, n.Right.Data);
        }

        [Test]
        public void TestGetRoot()
        {
            var arr = new[] {1, 2, 3, 4, 5};
            var rbt = new RedBlackTree<int>();
            foreach (var item in arr)
            {
                rbt.Insert(item);
            }

            var r = rbt.GetRoot();
            Assert.AreEqual(null, r.Parent);
            Assert.AreEqual(2, r.Data);
            Assert.AreEqual(1, r.Left.Data);
            Assert.AreEqual(4, r.Right.Data);
            Assert.AreEqual(Color.Black.ToString(), r.Color.ToString());
        }
    }
}