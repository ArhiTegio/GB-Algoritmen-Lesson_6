using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace GB_Algoritmen_Lesson_6
{
    /// <summary>
    /// Бинарне дерево 
    /// </summary>
    /// <typeparam name="T">Тип значения хранящегося в бинарном дереве</typeparam>
    class BinaryTree<T>
    {
        private Stack<Node> stack = new Stack<Node>();
        public Node Head { get; set; }

        /// <summary>
        /// Узел бинарного дерева
        /// </summary>
        public class Node
        {
            T value = default(T);

            public Node(T value) => this.value = value;
            public Node()
            {

            }

            public Node Prev { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public T Value { get => value; set => this.value = value; }
        }
        
        /// <summary>
        /// Загрузить файл
        /// </summary>
        /// <param name="path"></param>
        public void LoadFromFile(string path)
        {
            Head = null;
            var list = FileOperation<T>.LoadFromXmlCollectionFormat(path);
            foreach (var e in list)
                this.Add(e);
        }

        /// <summary>
        /// Сохранить в файл
        /// </summary>
        /// <param name="path"></param>
        public void SaveFromFile(string path) => FileOperation<T>.SaveAsXmlCollectionFormat(this.BinarySearchTreeList(x => x != null, TypeBinarySearchTree.RootLeftRight), path);
        

        /// <summary>
        /// Ввод данных в бинарное дерево
        /// </summary>
        /// <param name="n"></param>
        public void Add(T n)
        {
            if(Head == null)
            {
                Head = new Node(n);
            }
            else
            {
                stack = new Stack<Node>();
                stack.Push(Head);
                var check = new Node();
                while(true)
                {
                    check = stack.Pop();
                    if ((dynamic)check.Value >= n)
                    {
                        if (check.Left == null)
                        {
                            check.Left = new Node(n);
                            check.Left.Prev = check;
                            break;
                        }
                        else
                            stack.Push(check.Left);
                    }
                    else
                    {
                        if (check.Right == null)
                        {
                            check.Right = new Node(n);
                            check.Right.Prev = check;
                            break;
                        }
                        else
                            stack.Push(check.Right);
                    }
                }
            }
        }

        /// <summary>
        /// Делегат критерия поиска
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public delegate bool Criterion(T value);

        /// <summary>
        /// Бинарный поиск отдельного значения по критерию поиска
        /// </summary>
        /// <param name="criterion">Делегат критерия поиска</param>
        /// <param name="type">Тип поиска</param>
        /// <returns></returns>
        public T BinarySearchTree(Criterion criterion, TypeBinarySearchTree type)
        {
            stack = new Stack<Node>();
            stack.Push(Head);
            var check = new Node();
            if (Head == null)
                return default(T);

            if (type == TypeBinarySearchTree.RootLeftRight)
            {
                if (criterion(Head.Value))
                    return Head.Value;
                while (stack.Count != 0)
                {
                    check = stack.Pop();
                    if (criterion(check.Value)) return check.Value;
                    if (check.Right != null) stack.Push(check.Right);
                    if (check.Left != null) stack.Push(check.Left);
                }

                if (Head.Right != null)
                {
                    stack.Push(Head.Right);
                }

                while (stack.Count != 0)
                {
                    check = stack.Pop();
                    if (criterion(check.Value)) return check.Value;
                    if (check.Left != null) stack.Push(check.Left);
                    if (check.Right != null) stack.Push(check.Right);
                }
            }

            if (type == TypeBinarySearchTree.LeftRootRight)
            {
                while (stack.Count != 0)
                {
                    check = stack.Pop();
                    if (criterion(check.Value)) return check.Value;
                    if (check.Right != null) stack.Push(check.Right);
                    if (check.Left != null) stack.Push(check.Left);
                }

                if (criterion(Head.Value)) return Head.Value;

                if (Head.Right != null) stack.Push(Head.Right);                

                while (stack.Count != 0)
                {
                    check = stack.Pop();
                    if (criterion(check.Value)) return check.Value;
                    if (check.Left != null) stack.Push(check.Left);
                    if (check.Right != null) stack.Push(check.Right);
                }
            }

            if (type == TypeBinarySearchTree.LeftRightRoot)
            {
                while (stack.Count != 0)
                {
                    check = stack.Pop();
                    if (criterion(check.Value)) return check.Value;
                    if (check.Right != null) stack.Push(check.Right);
                    if (check.Left != null) stack.Push(check.Left);
                }

                if (Head.Right != null) stack.Push(Head.Right);                

                while (stack.Count != 0)
                {
                    check = stack.Pop();
                    if (criterion(check.Value)) return check.Value;
                    if (check.Left != null) stack.Push(check.Left);
                    if (check.Right != null) stack.Push(check.Right);
                }

                if (criterion(Head.Value))
                    return Head.Value;
            }

            return default(T);
        }

        /// <summary>
        /// Бинарный поиск множества значений по критерию поиска
        /// </summary>
        /// <param name="criterion">Делегат критерия поиска</param>
        /// <param name="type">Тип поиска</param>
        /// <returns></returns>
        public List<T> BinarySearchTreeList(Criterion criterion, TypeBinarySearchTree type)
        {
            stack = new Stack<Node>();
            stack.Push(Head);
            var check = new Node();
            if (Head == null)
                return new List<T>();

            var list = new List<T>();
            if (type == TypeBinarySearchTree.RootLeftRight)
            {
                if (criterion(Head.Value))
                    list.Add(Head.Value);
                while (stack.Count != 0)
                {
                    check = stack.Pop();
                    if (criterion(check.Value)) list.Add(check.Value);
                    if (check.Right != null) stack.Push(check.Right);
                    if (check.Left != null) stack.Push(check.Left);
                }

                if (Head.Right != null)
                {
                    stack.Push(Head.Right);
                }

                while (stack.Count != 0)
                {
                    check = stack.Pop();
                    if (criterion(check.Value)) list.Add(check.Value);
                    if (check.Left != null) stack.Push(check.Left);
                    if (check.Right != null) stack.Push(check.Right);
                }
            }

            if (type == TypeBinarySearchTree.LeftRootRight)
            {
                while (stack.Count != 0)
                {
                    check = stack.Pop();
                    if (criterion(check.Value)) list.Add(check.Value);
                    if (check.Right != null) stack.Push(check.Right);
                    if (check.Left != null) stack.Push(check.Left);
                }

                if (criterion(Head.Value)) list.Add(Head.Value);

                if (Head.Right != null) stack.Push(Head.Right);

                while (stack.Count != 0)
                {
                    check = stack.Pop();
                    if (criterion(check.Value)) list.Add(check.Value);
                    if (check.Left != null) stack.Push(check.Left);
                    if (check.Right != null) stack.Push(check.Right);
                }
            }

            if (type == TypeBinarySearchTree.LeftRightRoot)
            {
                while (stack.Count != 0)
                {
                    check = stack.Pop();
                    if (criterion(check.Value)) list.Add(check.Value);
                    if (check.Right != null) stack.Push(check.Right);
                    if (check.Left != null) stack.Push(check.Left);
                }

                if (Head.Right != null) stack.Push(Head.Right);

                while (stack.Count != 0)
                {
                    check = stack.Pop();
                    if (criterion(check.Value)) list.Add(check.Value);
                    if (check.Left != null) stack.Push(check.Left);
                    if (check.Right != null) stack.Push(check.Right);
                }

                if (criterion(Head.Value))
                    list.Add(Head.Value);
            }

            return list;
        }
    }

    /// <summary>
    /// Тип поиска по бинарному дереву
    /// </summary>
    enum TypeBinarySearchTree
    {
        RootLeftRight,
        LeftRootRight,
        LeftRightRoot,
    }

}
