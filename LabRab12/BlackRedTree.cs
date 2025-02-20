using System;
using System.Collections;
using System.Collections.Generic;
using static System.Diagnostics.Activity;
namespace LabRab12
{
    public class RedBlackTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        private enum NodeColor { Red, Black }

        private class Node<T>
        {
            public T Value;
            public Node<T> Left, Right, Parent;
            public NodeColor Color;

            public Node(T value)
            {
                Value = value;
                Color = NodeColor.Red; // Новые узлы всегда красные
                Left = Right = Parent = null;
            }
        }

        private Node<T> root;

        public RedBlackTree()
        {
            root = null;
        }
        public RedBlackTree(RedBlackTree<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            foreach (var item in other)
            {
                this.Add(item);
            }
        }
        public int Count()
        {
            return GetCount(root);
        }

        private int GetCount(Node<T> node)
        {
            if (node == null)
                return 0;
            return 1 + GetCount(node.Left) + GetCount(node.Right);
        }
        public bool Contains(T value)
        {
            return FindNode(root, value) != null;
        }
        public RedBlackTree<T> DeepClone()
        {
            RedBlackTree<T> clone = new RedBlackTree<T>();
            foreach (var item in this)
            {
                if (item is ICloneable cloneable)
                {
                    clone.Add((T)cloneable.Clone());
                }
                else
                {
                    clone.Add(item);
                }
            }
            return clone;
        }
        public RedBlackTree<T> ShallowCopy()
        {
            RedBlackTree<T> copy = new RedBlackTree<T>();
            foreach (var item in this)
            {
                copy.Add(item);
            }
            return copy;
        }
        public void Clear()
        {
            root = null;
        }
        public int CountByKey(Func<T, string> keySelector, string key)
        {
            return CountByKeyRecursive(root, keySelector, key);
        }

        private int CountByKeyRecursive(Node<T> node, Func<T, string> keySelector, string key)
        {
            if (node == null)
                return 0;

            int count = 0;
            if (keySelector(node.Value) == key)
                count++;

            count += CountByKeyRecursive(node.Left, keySelector, key);
            count += CountByKeyRecursive(node.Right, keySelector, key);

            return count;
        }
        public void Add(T value)
        {
            Node<T> newNode = new Node<T>(value);
            if (root == null)
            {
                root = newNode;
                root.Color = NodeColor.Black;
            }
            else
            {
                InsertNode(root, newNode);
                BalanceTreeAfterInsertion(newNode);
            }
        }
        private void InsertNode(Node<T> root, Node<T> newNode)
        {
            if (newNode.Value.CompareTo(root.Value) < 0)
            {
                if (root.Left == null)
                {
                    root.Left = newNode;
                    newNode.Parent = root;
                }
                else
                {
                    InsertNode(root.Left, newNode);
                }
            }
            else
            {
                if (root.Right == null)
                {
                    root.Right = newNode;
                    newNode.Parent = root;
                }
                else
                {
                    InsertNode(root.Right, newNode);
                }
            }
        }

        private void BalanceTreeAfterInsertion(Node<T> node)
        {
            while (node != root && node.Parent.Color == NodeColor.Red)
            {
                if (node.Parent == node.Parent.Parent.Left)
                {
                    Console.WriteLine($"Balancing node: {node.Value}"); // Отладочный вывод
                    Node<T> uncle = node.Parent.Parent.Right;
                    if (uncle != null && uncle.Color == NodeColor.Red)
                    {
                        // Случай 1: Дядя красный
                        node.Parent.Color = NodeColor.Black;
                        uncle.Color = NodeColor.Black;
                        node.Parent.Parent.Color = NodeColor.Red;
                        node = node.Parent.Parent;
                    }
                    else
                    {
                        // Случай 2: Дядя черный
                        if (node == node.Parent.Right)
                        {
                            node = node.Parent;
                            RotateLeft(node);
                        }
                        // Случай 3: Перекрашивание и поворот
                        node.Parent.Color = NodeColor.Black;
                        node.Parent.Parent.Color = NodeColor.Red;
                        RotateRight(node.Parent.Parent);
                    }
                }
                else
                {
                    Node<T> uncle = node.Parent.Parent.Left;
                    if (uncle != null && uncle.Color == NodeColor.Red)
                    {
                        // Случай 1: Дядя красный
                        node.Parent.Color = NodeColor.Black;
                        uncle.Color = NodeColor.Black;
                        node.Parent.Parent.Color = NodeColor.Red;
                        node = node.Parent.Parent;
                    }
                    else
                    {
                        // Случай 2: Дядя черный
                        if (node == node.Parent.Left)
                        {
                            node = node.Parent;
                            RotateRight(node);
                        }
                        // Случай 3: Перекрашивание и поворот
                        node.Parent.Color = NodeColor.Black;
                        node.Parent.Parent.Color = NodeColor.Red;
                        RotateLeft(node.Parent.Parent);
                    }
                }
            }
            root.Color = NodeColor.Black; // Корень всегда черный
        }

        public void Remove(T value)
        {
            Node<T> nodeToDelete = FindNode(root, value);
            if (nodeToDelete == null)
            {
                Console.WriteLine("Node not found.");
                return; // Узел не найден
            }

            Node<T> child = null;
            Node<T> nodeToFix = null;

            if (nodeToDelete.Left == null || nodeToDelete.Right == null)
            {
                nodeToFix = nodeToDelete;
            }
            else
            {
                nodeToFix = FindSuccessor(nodeToDelete);
            }

            if (nodeToFix.Left != null)
            {
                child = nodeToFix.Left;
            }
            else
            {
                child = nodeToFix.Right;
            }

            if (child != null)
            {
                child.Parent = nodeToFix.Parent;
            }

            if (nodeToFix.Parent == null)
            {
                root = child;
            }
            else if (nodeToFix == nodeToFix.Parent.Left)
            {
                nodeToFix.Parent.Left = child;
            }
            else
            {
                nodeToFix.Parent.Right = child;
            }

            if (nodeToFix != nodeToDelete)
            {
                nodeToDelete.Value = nodeToFix.Value;
            }

            if (nodeToFix.Color == NodeColor.Black)
            {
                BalanceTreeAfterDeletion(child, nodeToFix.Parent);
            }
        }

        private Node<T> FindNode(Node<T> node, T value)
        {
            if (node == null || value.CompareTo(node.Value) == 0)
            {
                return node;
            }

            if (value.CompareTo(node.Value) < 0)
            {
                return FindNode(node.Left, value);
            }
            else
            {
                return FindNode(node.Right, value);
            }
        }

        private Node<T> FindSuccessor(Node<T> node)
        {
            if (node.Right != null)
            {
                return FindMinimum(node.Right);
            }

            Node<T> parent = node.Parent;
            while (parent != null && node == parent.Right)
            {
                node = parent;
                parent = parent.Parent;
            }
            return parent;
        }

        private Node<T> FindMinimum(Node<T> node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node;
        }

        private void BalanceTreeAfterDeletion(Node<T> node, Node<T> parent)
        {
            while (node != root && (node == null || node.Color == NodeColor.Black))
            {
                if (node == parent.Left)
                {
                    Node<T> sibling = parent.Right;
                    if (sibling.Color == NodeColor.Red)
                    {
                        sibling.Color = NodeColor.Black;
                        parent.Color = NodeColor.Red;
                        RotateLeft(parent);
                        sibling = parent.Right;
                    }

                    if ((sibling.Left == null || sibling.Left.Color == NodeColor.Black) &&
                        (sibling.Right == null || sibling.Right.Color == NodeColor.Black))
                    {
                        sibling.Color = NodeColor.Red;
                        node = parent;
                        parent = node.Parent;
                    }
                    else
                    {
                        if (sibling.Right == null || sibling.Right.Color == NodeColor.Black)
                        {
                            if (sibling.Left != null)
                            {
                                sibling.Left.Color = NodeColor.Black;
                            }
                            sibling.Color = NodeColor.Red;
                            RotateRight(sibling);
                            sibling = parent.Right;
                        }

                        sibling.Color = parent.Color;
                        parent.Color = NodeColor.Black;
                        if (sibling.Right != null)
                        {
                            sibling.Right.Color = NodeColor.Black;
                        }
                        RotateLeft(parent);
                        node = root;
                    }
                }
                else
                {
                    Node<T> sibling = parent.Left;
                    if (sibling.Color == NodeColor.Red)
                    {
                        sibling.Color = NodeColor.Black;
                        parent.Color = NodeColor.Red;
                        RotateRight(parent);
                        sibling = parent.Left;
                    }

                    if ((sibling.Right == null || sibling.Right.Color == NodeColor.Black) &&
                        (sibling.Left == null || sibling.Left.Color == NodeColor.Black))
                    {
                        sibling.Color = NodeColor.Red;
                        node = parent;
                        parent = node.Parent;
                    }
                    else
                    {
                        if (sibling.Left == null || sibling.Left.Color == NodeColor.Black)
                        {
                            if (sibling.Right != null)
                            {
                                sibling.Right.Color = NodeColor.Black;
                            }
                            sibling.Color = NodeColor.Red;
                            RotateLeft(sibling);
                            sibling = parent.Left;
                        }

                        sibling.Color = parent.Color;
                        parent.Color = NodeColor.Black;
                        if (sibling.Left != null)
                        {
                            sibling.Left.Color = NodeColor.Black;
                        }
                        RotateRight(parent);
                        node = root;
                    }
                }
            }

            if (node != null)
            {
                node.Color = NodeColor.Black;
            }
        }

        private void RotateLeft(Node<T> node)
        {
            Node<T> rightChild = node.Right;
            node.Right = rightChild.Left;
            if (rightChild.Left != null)
            {
                rightChild.Left.Parent = node;
            }
            rightChild.Parent = node.Parent;
            if (node.Parent == null)
            {
                root = rightChild;
            }
            else if (node == node.Parent.Left)
            {
                node.Parent.Left = rightChild;
            }
            else
            {
                node.Parent.Right = rightChild;
            }
            rightChild.Left = node;
            node.Parent = rightChild;
        }

        private void RotateRight(Node<T> node)
        {
            Node<T> leftChild = node.Left;
            node.Left = leftChild.Right;
            if (leftChild.Right != null)
            {
                leftChild.Right.Parent = node;
            }
            leftChild.Parent = node.Parent;
            if (node.Parent == null)
            {
                root = leftChild;
            }
            else if (node == node.Parent.Right)
            {
                node.Parent.Right = leftChild;
            }
            else
            {
                node.Parent.Left = leftChild;
            }
            leftChild.Right = node;
            node.Parent = leftChild;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Numerator<T>(root);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        private class Numerator<T> : IEnumerator<T>
        {
            private Node<T> root;
            private Stack<Node<T>> stack;
            private Node<T> current;

            public Numerator(Node<T> root)
            {
                this.root = root;
                stack = new Stack<Node<T>>();
                Reset();
            }

            public T Current
            {
                get
                {
                    if (current == null)
                        throw new InvalidOperationException("Нумератор не на корне или достиг конца.");
                    return current.Value;
                }

            }

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (stack.Count == 0)
                    return false;

                current = stack.Pop();

                if (current.Right != null)
                {
                    stack.Push(current.Right);
                }
                if (current.Left != null)
                {
                    stack.Push(current.Left);
                }

                return true;
            }

            public void Reset()
            {
                stack.Clear();
                current = null;
                if (root != null)
                {
                    stack.Push(root);
                }
            }

            public void Dispose() { }
        }
    }

}