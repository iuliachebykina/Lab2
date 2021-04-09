using System;
using System.Collections.Generic;

namespace Lab2
{
    public enum Color
    {
        Red,
        Black
    }

    public class Node<T>
    {
        public T Data;
        public Node<T> Parent;
        public Node<T> Left;
        public Node<T> Right;
        public Color Color;
    }


    public class RedBlackTree<T> where T : IComparable<T>

    {
        private Node<T> _root;
        private readonly Node<T> _nil;


        private int _count;
        private int _size;

        public int GetCount()
        {
            return _count;
        }

        public RedBlackTree()
        {
            _nil = new Node<T> {Color = Color.Black, Left = null, Right = null};
            _root = _nil;
            _count = 0;
            _size = 0;
        }

        public RedBlackTree(RedBlackTree<T> redBlackTree)
        {
            _nil = new Node<T> {Color = Color.Black, Left = null, Right = null};
            _root = redBlackTree.GetRoot();
            _count = 0;
            _size = redBlackTree.Size();
        }

        private void LeftRotate(Node<T> x)
        {
            var y = x.Right;
            x.Right = y.Left;
            if (y.Left != _nil)
            {
                y.Left.Parent = x;
            }

            y.Parent = x.Parent;
            if (x.Parent == null)
            {
                _root = y;
            }
            else if (x == x.Parent.Left)
            {
                x.Parent.Left = y;
                _count++;
            }
            else
            {
                x.Parent.Right = y;
                _count++;
            }


            y.Left = x;
            x.Parent = y;
        }

        private void RightRotate(Node<T> x)
        {
            var y = x.Left;
            x.Left = y.Right;
            if (y.Right != _nil)
            {
                y.Right.Parent = x;
            }

            y.Parent = x.Parent;
            if (x.Parent == null)
            {
                _root = y;
            }
            else if (x == x.Parent.Right)
            {
                x.Parent.Right = y;
                _count++;
            }
            else
            {
                x.Parent.Left = y;
                _count++;
            }

            y.Right = x;
            x.Parent = y;
        }


        public void Insert(T key)
        {
            _size++;
            var node = new Node<T>
            {
                Parent = null,
                Data = key,
                Left = _nil,
                Right = _nil,
                Color = Color.Red
            };

            Node<T> y = null;
            var x = _root;

            while (x != _nil)
            {
                y = x;
                if (node.Data.CompareTo(x.Data) < 0)
                {
                    x = x.Left;
                    _count++;
                }
                else
                {
                    x = x.Right;
                    _count++;
                }
            }


            node.Parent = y;
            if (y == null)
            {
                _root = node;
            }
            else if (node.Data.CompareTo(y.Data) < 0)
            {
                y.Left = node;
                _count++;
            }
            else
            {
                y.Right = node;
                _count++;
            }


            if (node.Parent == null)
            {
                node.Color = Color.Black;
                return;
            }

            if (node.Parent.Parent == null)
            {
                return;
            }

            FixInsert(node);
        }

        public List<T> PreOrder()
        {
            var res = new List<T>();
            return PreOrderHelper(_root, res);
        }

        public List<T> InOrder()
        {
            var res = new List<T>();
            return InOrderHelper(_root, res);
        }

        public List<T> PostOrder()
        {
            var res = new List<T>();
            return PostOrderHelper(_root, res);
        }

        private List<T> PreOrderHelper(Node<T> node, List<T> res)
        {
            if (node != _nil)
            {
                res.Add(node.Data);
                PreOrderHelper(node.Left, res);
                PreOrderHelper(node.Right, res);
            }

            return res;
        }

        private List<T> InOrderHelper(Node<T> node, List<T> res)
        {
            if (node != _nil)
            {
                InOrderHelper(node.Left, res);
                res.Add(node.Data);
                InOrderHelper(node.Right, res);
            }

            return res;
        }

        private List<T> PostOrderHelper(Node<T> node, List<T> res)
        {
            if (node != _nil)
            {
                PostOrderHelper(node.Left, res);
                PostOrderHelper(node.Right, res);
                res.Add(node.Data);
            }

            return res;
        }

        private void FixInsert(Node<T> k)
        {
            while (k.Parent.Color == Color.Red)
            {
                _count++;
                Node<T> u;
                if (k.Parent == k.Parent.Parent.Right)
                {
                    u = k.Parent.Parent.Left;
                    if (u.Color == Color.Red)
                    {
                        u.Color = Color.Black;
                        k.Parent.Color = Color.Black;
                        k.Parent.Parent.Color = Color.Red;
                        k = k.Parent.Parent;
                    }
                    else
                    {
                        if (k == k.Parent.Left)
                        {
                            k = k.Parent;
                            _count++;
                            RightRotate(k);
                        }

                        k.Parent.Color = Color.Black;
                        k.Parent.Parent.Color = Color.Red;
                        LeftRotate(k.Parent.Parent);
                    }
                }
                else
                {
                    u = k.Parent.Parent.Right;


                    if (u.Color == Color.Red)
                    {
                        u.Color = Color.Black;
                        k.Parent.Color = Color.Black;
                        k.Parent.Parent.Color = Color.Red;
                        k = k.Parent.Parent;
                    }
                    else
                    {
                        if (k == k.Parent.Right)
                        {
                            k = k.Parent;
                            _count++;
                            LeftRotate(k);
                        }

                        k.Parent.Color = Color.Black;
                        k.Parent.Parent.Color = Color.Red;
                        RightRotate(k.Parent.Parent);
                    }
                }

                if (k == _root)
                {
                    break;
                }
            }

            _root.Color = Color.Black;
        }

        private void FixDelete(Node<T> x)
        {
            while (x != _root && x.Color == Color.Black)
            {
                Node<T> s;
                if (x == x.Parent.Left)
                {
                    s = x.Parent.Right;
                    if (s.Color == Color.Red)
                    {
                        s.Color = Color.Black;
                        x.Parent.Color = Color.Red;
                        LeftRotate(x.Parent);
                        s = x.Parent.Right;
                    }

                    if (s.Left.Color == Color.Black && s.Right.Color == Color.Black)
                    {
                        s.Color = Color.Red;
                        x = x.Parent;
                    }
                    else
                    {
                        if (s.Right.Color == Color.Black)
                        {
                            s.Left.Color = Color.Black;
                            s.Color = Color.Red;
                            RightRotate(s);
                            s = x.Parent.Right;
                        }

                        s.Color = x.Parent.Color;
                        x.Parent.Color = Color.Black;
                        s.Right.Color = Color.Black;
                        LeftRotate(x.Parent);
                        x = _root;
                    }
                }
                else
                {
                    s = x.Parent.Left;
                    if (s.Color == Color.Red)
                    {
                        s.Color = Color.Black;
                        x.Parent.Color = Color.Red;
                        RightRotate(x.Parent);
                        s = x.Parent.Left;
                    }

                    if (s.Right.Color == Color.Black)
                    {
                        s.Color = Color.Red;
                        x = x.Parent;
                    }
                    else
                    {
                        if (s.Left.Color == Color.Black)
                        {
                            s.Right.Color = Color.Black;
                            s.Color = Color.Red;
                            LeftRotate(s);
                            s = x.Parent.Left;
                        }

                        s.Color = x.Parent.Color;
                        x.Parent.Color = Color.Black;
                        s.Left.Color = Color.Black;
                        RightRotate(x.Parent);
                        x = _root;
                    }
                }
            }

            x.Color = Color.Black;
        }


        private void RbTransplant(Node<T> u, Node<T> v)
        {
            if (u.Parent == null)
            {
                _root = v;
            }
            else if (u == u.Parent.Left)
            {
                u.Parent.Left = v;
            }
            else
            {
                u.Parent.Right = v;
            }

            v.Parent = u.Parent;
        }

        private void DeleteNodeHelper(Node<T> node, T key)
        {
            var z = _nil;
            Node<T> x;
            while (node != _nil)
            {
                if (node.Data.CompareTo(key) == 0)
                {
                    z = node;
                }

                node = node.Data.CompareTo(key) <= 0 ? node.Right : node.Left;
            }

            if (z == _nil)
            {
                throw new ArgumentException("Ноды нет в дереве");
            }

            var y = z;
            var yOriginalColor = y.Color;
            if (z.Left == _nil)
            {
                x = z.Right;
                RbTransplant(z, z.Right);
            }
            else if (z.Right == _nil)
            {
                x = z.Left;
                RbTransplant(z, z.Left);
            }
            else
            {
                y = Min(z.Right);
                yOriginalColor = y.Color;
                x = y.Right;
                if (y.Parent == z)
                {
                    x.Parent = y;
                }
                else
                {
                    RbTransplant(y, y.Right);
                    y.Right = z.Right;
                    y.Right.Parent = y;
                }

                RbTransplant(z, y);
                y.Left = z.Left;
                y.Left.Parent = y;
                y.Color = z.Color;
            }

            if (yOriginalColor == Color.Black)
            {
                FixDelete(x);
            }

            _size--;
        }

        private Node<T> SearchTreeHelper(Node<T> node, T key)
        {
            if (node == _nil || Equals(key, node.Data))
            {
                return node;
            }

            return SearchTreeHelper(key.CompareTo(node.Data) < 0 ? node.Left : node.Right, key);
        }

        public Node<T> SearchTree(T k)
        {
            return SearchTreeHelper(_root, k);
        }


        private Node<T> Min(Node<T> node)
        {
            while (node.Left != _nil)
            {
                node = node.Left;
            }

            return node;
        }

        private Node<T> Max(Node<T> node)
        {
            while (node.Right != _nil)
            {
                node = node.Right;
            }

            return node;
        }

        public Node<T> GetRoot()
        {
            return _root;
        }

        public void DeleteNode(T data)
        {
            DeleteNodeHelper(_root, data);
        }

        public int Size()
        {
            return _size;
        }
    }
}