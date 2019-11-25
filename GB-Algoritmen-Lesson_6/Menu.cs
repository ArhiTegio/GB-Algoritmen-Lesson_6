using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryFastDecisions;
using static System.Console;
using System.Reflection;

namespace GB_Algoritmen_Lesson_6
{
    class Hash : Act
    {
        public override void Work()
        {
            var arrayEng_NumSymbol = new HashSet<char>() { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm', 'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P', 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'Z', 'X', 'C', 'V', 'B', 'N', 'M', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', };
            WriteLine($"Хэш-сумма: { (new Questions()).Question<int>("Введите значение для получения хэш суммы:", arrayEng_NumSymbol, true).GetHash() }");
        }
    }

    class BinarySearchTree : Act
    {
        public override void Work()
        {
            var dict = new Dictionary<int, TypeBinarySearchTree>()             
            {
                { 1,  TypeBinarySearchTree.RootLeftRight},
                { 2,  TypeBinarySearchTree.LeftRootRight},
                { 3,  TypeBinarySearchTree.LeftRightRoot},
            };

            var q = new Questions();
            var r = new Random();
            var tree = new BinaryTree<int>();
            var array_Num = new HashSet<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', };
            var arrayEng_NumSymbol = new HashSet<char>() { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm', 'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P', 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'Z', 'X', 'C', 'V', 'B', 'N', 'M', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', };
            var num = int.Parse(q.Question<int>("Введите количество значений в бинарном дереве:", array_Num, true));

            for(int i = 0; i < num; ++i) 
                tree.Add(r.Next(0, 100));

            var file = q.Question<int>("Введите имя файла для теста записи и чтения бинарного дерева:", arrayEng_NumSymbol, true);
            tree.SaveFromFile(file);

            var tree2 = new BinaryTree<int>();
            tree2.LoadFromFile(file);

            var cr = int.Parse(q.Question<int>("Введите критерий значения не более:", new HashSet<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', }, true));
            var typeSearch = dict[int.Parse(q.Question<int>("Введите тип поиска в бинарном дереве(1-КЛП, 2-ЛКП, 3-ЛПК):", new HashSet<char>() { '1', '2', '3', }, true))];
            var many = int.Parse(q.Question<int>("Вам нужно список (1) или одно значение (2):", new HashSet<char>() { '1', '2', }, true));

            if(many == 1) WriteLine($"Список найденных значений составит: { tree2.BinarySearchTreeList(x => x <= cr, typeSearch).Select(x => x.ToString()).Aggregate((x, y) => $"{x}, {y}") }");            
            else WriteLine($"Первое найденное значение: {tree2.BinarySearchTree(x => x <= cr, typeSearch).ToString()}");            
        }
    }


    class StudentAnswer : Act
    {
        public override void Work()
        {
            var tree = new BinaryTree<Student>();
            var s = new StudentAct();
            s.FillWithRandomData();
            var list = s.GetFromBD();
            var q = new Questions();

            foreach (var e in list)            
                tree.Add(e);

            var cr = int.Parse(q.Question<int>("Введите критерий поиска (1 - имя, 2 - возраст):", new HashSet<char>() { '1', '2', }, true));

            if (cr == 1)
            {
                var name = q.Question<string>("Введите начальные буквы имени:", new HashSet<char>() { 'й','ц','у','к','е','н','г','ш','щ','з','х','ъ','ф','ы','в','а','п','р','о','л','д','ж','э','я','ч','с','м','и','т','ь','б','ю','Й','Ц','У','К','Е','Н','Г','Ш','Щ','З','Х','Ъ','Ф','Ы','В','А','П','Р','О','Л','Д','Ж','Э','Я','Ч','С','М','И','Т','Ь','Б','Ю', }, true ).ToLower();
                var l = tree.BinarySearchTreeList(x =>
                {
                    var n = x.Name.ToLower();
                    if (name.Length > n.Length)
                        return false;
                    for(int i = 0; i < name.Length; ++i)                    
                        if(n[i] != name[i])                        
                            return false;
                    return true;
                }, TypeBinarySearchTree.RootLeftRight);
                WriteLine($"Список найденных студентов:");
                foreach(var e in l)                
                    WriteLine($"Табельный номер {e.Id}, имя {e.Name}, возраст {e.Age};");
            }
            else
            {
                var age = int.Parse(q.Question<string>("Введите максимальное :", new HashSet<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', }, true).ToLower());
                var l = tree.BinarySearchTreeList(x => x.Age >= age, TypeBinarySearchTree.RootLeftRight);
                WriteLine($"Список найденных студентов:");
                foreach (var e in l)
                    WriteLine($"Табельный номер {e.Id}, имя {e.Name}, возраст {e.Age};");
            }
        }
    }
}
