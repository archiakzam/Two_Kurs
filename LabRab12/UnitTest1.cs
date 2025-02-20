using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LabRab12.Test
{
    [TestClass]
    public class RedBlackTreeConstructorAndBasicMethodsTests
    {
        [TestMethod]
        
        [ExpectedException(typeof(ArgumentNullException))]
        public void CopyConstructor_ShouldThrowArgumentNullException_WhenSourceIsNull()
        {
            
            RedBlackTree<int> sourceTree = null;

            
            var copiedTree = new RedBlackTree<int>(sourceTree);

            
            
        }

        [TestMethod]
        public void CopyConstructor_ShouldCreateDeepCopyOfTree()
        {
            
            var sourceTree = new RedBlackTree<int>();
            sourceTree.Add(10);
            sourceTree.Add(20);
            sourceTree.Add(30);

            
            var copiedTree = new RedBlackTree<int>(sourceTree);

            
            Assert.AreEqual(sourceTree.Count(), copiedTree.Count()); // ���������� ��������� ���������
            Assert.IsTrue(copiedTree.Contains(10)); // �������� �����������
            Assert.IsTrue(copiedTree.Contains(20));
            Assert.IsTrue(copiedTree.Contains(30));
        }

        [TestMethod]
        public void Count_ShouldReturnZero_ForEmptyTree()
        {
            
            var tree = new RedBlackTree<int>();

            
            int count = tree.Count();

            
            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void Count_ShouldReturnCorrectNumberOfElements_ForNonEmptyTree()
        {
            
            var tree = new RedBlackTree<int>();
            tree.Add(10);
            tree.Add(20);
            tree.Add(30);

            
            int count = tree.Count();

            
            Assert.AreEqual(3, count);
        }

        [TestMethod]
        public void Contains_ShouldReturnTrue_ForExistingElement()
        {
            
            var tree = new RedBlackTree<int>();
            tree.Add(10);
            tree.Add(20);

            
            bool contains = tree.Contains(20);

            
            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void Contains_ShouldReturnFalse_ForNonExistingElement()
        {
            
            var tree = new RedBlackTree<int>();
            tree.Add(10);
            tree.Add(20);

            
            bool contains = tree.Contains(30);

            
            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void Contains_ShouldReturnFalse_ForEmptyTree()
        {
            
            var tree = new RedBlackTree<int>();

            
            bool contains = tree.Contains(10);

            
            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void DeepClone_ShouldCreateIndependentCopy()
        {
            
            var tree = new RedBlackTree<int>();
            tree.Add(10);
            tree.Add(20);
            tree.Add(30);

            
            var clonedTree = tree.DeepClone();
            clonedTree.Add(40);

            
            Assert.AreEqual(3, tree.Count()); // �������� ������ �� ����������
            Assert.AreEqual(4, clonedTree.Count()); // ������������� ������ �������� ����� �������
            Assert.IsTrue(clonedTree.Contains(40)); // ����� ������� �������� � ����
            Assert.IsFalse(tree.Contains(40)); // �������� ������ �� �������� ����� �������
        }

        [TestMethod]
        public void DeepClone_ShouldCloneCloneableElements()
        {
            
            var tree = new RedBlackTree<CloneablePersona>();
            var alice = new CloneablePersona { Name = "Alice" };
            var bob = new CloneablePersona { Name = "Bob" };
            tree.Add(alice);
            tree.Add(bob);

            
            var clonedTree = tree.DeepClone();
            var clonedAlice = clonedTree.Find(alice);

            // �������� �� null ����� ���������� ��������
            Assert.IsNotNull(clonedAlice, "������������� ������� 'Alice' �� ������ � ������.");

            clonedAlice.Name = "Alice Modified";

            
            var originalAlice = tree.Find(alice);
            Assert.IsNotNull(originalAlice, "������������ ������� 'Alice' �� ������ � ������.");
            Assert.AreEqual("Alice", originalAlice.Name); // �������� ������ �� ����������
            Assert.AreEqual("Alice Modified", clonedAlice.Name); // ���� �������
        }

        [TestMethod]
        public void ShallowCopy_ShouldCreateCopyWithSharedReferences()
        {
            
            var tree = new RedBlackTree<int>();
            tree.Add(10);
            tree.Add(20);
            tree.Add(30);

            
            var copiedTree = tree.ShallowCopy();
            copiedTree.Add(40);

            
            Assert.AreEqual(3, tree.Count()); // �������� ������ ����������
            Assert.AreEqual(4, copiedTree.Count()); // ����� �������� ����� �������
            Assert.IsTrue(copiedTree.Contains(40)); // ����� �������� ����� �������
        }

        [TestMethod]
        public void Clear_ShouldRemoveAllElements()
        {
            
            var tree = new RedBlackTree<int>();
            tree.Add(10);
            tree.Add(20);
            tree.Add(30);

            
            tree.Clear();

            
            Assert.AreEqual(0, tree.Count()); // ������ �����
            Assert.IsFalse(tree.Contains(10)); // �������� �������
            Assert.IsFalse(tree.Contains(20));
            Assert.IsFalse(tree.Contains(30));
        }

        [TestMethod]
        public void CountByKey_ShouldReturnCorrectCount()
        {
            
            var tree = new RedBlackTree<Persona>();
            tree.Add(new Persona { Name = "Alice" });
            tree.Add(new Persona { Name = "Bob" });
            tree.Add(new Persona { Name = "Alice" });

            
            int count = tree.CountByKey(p => p.Name, "Alice");

            
            Assert.AreEqual(2, count); // ��� �������� � ������ "Alice"
        }

        [TestMethod]
        public void CountByKey_ShouldReturnZero_ForNonExistingKey()
        {
            
            var tree = new RedBlackTree<Persona>();
            tree.Add(new Persona { Name = "Alice" });
            tree.Add(new Persona { Name = "Bob" });

            
            int count = tree.CountByKey(p => p.Name, "Charlie");

            
            Assert.AreEqual(0, count); // ��� ��������� � ������ "Charlie"
        }

        [TestMethod]
        public void CountByKey_ShouldReturnZero_ForEmptyTree()
        {
            
            var tree = new RedBlackTree<Persona>();

            
            int count = tree.CountByKey(p => p.Name, "Alice");

            
            Assert.AreEqual(0, count); // ������ �����
        }

        [TestMethod]
        public void Add_ShouldAddElementToEmptyTree()
        {
            
            var tree = new RedBlackTree<int>();

            
            tree.Add(10);

            
            Assert.IsTrue(tree.Contains(10)); // ������� ��������
            Assert.AreEqual(1, tree.Count()); // ���������� ��������� ����� 1
        }

        [TestMethod]
        public void Add_ShouldBalanceTreeAfterInsertion()
        {
            
            var tree = new RedBlackTree<int>();

            
            tree.Add(10);
            tree.Add(20);
            tree.Add(30);

            
            Assert.IsTrue(tree.Contains(10)); // �������� ���������
            Assert.IsTrue(tree.Contains(20));
            Assert.IsTrue(tree.Contains(30));
            Assert.AreEqual(3, tree.Count()); // ���������� ��������� ����� 3
        }

        [TestMethod]
        public void Remove_ShouldRemoveElementFromTree()
        {
            
            var tree = new RedBlackTree<int>();
            tree.Add(10);
            tree.Add(20);
            tree.Add(30);

            
            tree.Remove(20);

            
            Assert.IsFalse(tree.Contains(20)); // ������� ������
            Assert.IsTrue(tree.Contains(10)); // ��������� �������� ��������
            Assert.IsTrue(tree.Contains(30));
            Assert.AreEqual(2, tree.Count()); // ���������� ��������� �����������
        }

        [TestMethod]
        public void Remove_ShouldBalanceTreeAfterDeletion()
        {
            
            var tree = new RedBlackTree<int>();
            tree.Add(10);
            tree.Add(20);
            tree.Add(30);
            tree.Add(40);
            tree.Add(50);

            
            tree.Remove(30);

            
            Assert.IsFalse(tree.Contains(30)); // ������� ������
            Assert.IsTrue(tree.Contains(10)); // ��������� �������� ��������
            Assert.IsTrue(tree.Contains(20));
            Assert.IsTrue(tree.Contains(40));
            Assert.IsTrue(tree.Contains(50));
            Assert.AreEqual(4, tree.Count()); // ���������� ��������� �����������
        }

        [TestMethod]
        public void Remove_ShouldHandleRemovingRootNode()
        {
            
            var tree = new RedBlackTree<int>();
            tree.Add(10);
            tree.Add(20);
            tree.Add(5);

            
            tree.Remove(10);

            
            Assert.IsFalse(tree.Contains(10)); // �������� ������� ������
            Assert.IsTrue(tree.Contains(20)); // ��������� �������� ��������
            Assert.IsTrue(tree.Contains(5));
            Assert.AreEqual(2, tree.Count()); // ���������� ��������� �����������
        }

        [TestMethod]
        public void Remove_ShouldDoNothingIfElementNotFound()
        {
            
            var tree = new RedBlackTree<int>();
            tree.Add(10);
            tree.Add(20);

            
            tree.Remove(30);

            
            Assert.IsTrue(tree.Contains(10)); // �������� ��������
            Assert.IsTrue(tree.Contains(20));
            Assert.AreEqual(2, tree.Count()); // ���������� ��������� �� ����������
        }

    }

    // ��������������� ����� ��� ������������ DeepClone
    public class CloneablePersona : IComparable<CloneablePersona>, ICloneable
    {
        public string Name { get; set; }

        public int CompareTo(CloneablePersona other)
        {
            return Name.CompareTo(other.Name);
        }

        public object Clone()
        {
            return new CloneablePersona { Name = this.Name };
        }
    }

    // ��������������� ����� ��� ������������ CountByKey
    public class Persona : IComparable<Persona>
    {
        public string Name { get; set; }

        public int CompareTo(Persona other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}