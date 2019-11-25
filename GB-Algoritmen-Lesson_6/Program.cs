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
    class Program
    {
        static Dictionary<string, Act> dict = new Dictionary<string, Act>
        {
            { "1", new Hash() },
            { "2", new BinarySearchTree() },
            { "3", new StudentAnswer() },
        };

        static void Main(string[] args)
        {
            var ex = new Extension();
            var q = new Questions();
            var n = "";
            WriteLine("С# - Алгоритмы и структуры данных. Задание 6.");
            WriteLine("Кузнецов");
            var list = new HashSet<char>(dict.Select(x => x.Key[0]));

            while (n != "0")
            {
                WriteLine("Введите номер интересующей вас задачи:" + Environment.NewLine +
                    "1. Реализовать простейшую хэш-функцию. На вход функции подается строка, на выходе сумма кодов символов." + Environment.NewLine +
                    "2. Переписать программу, реализующее двоичное дерево поиска." + Environment.NewLine +
                    "       а) Добавить в него обход дерева различными способами; " + Environment.NewLine +
                    "       б) Реализовать поиск в двоичном дереве поиска; " + Environment.NewLine +
                    "       в)*Добавить в программу обработку командной строки с помощью которой можно указывать из какого файла считывать данные, каким образом обходить дерево." + Environment.NewLine +
                    "3.*Разработать базу данных студентов из двух полей “Имя”, “Возраст”, “Табельный номер” в которой использовать все знания, полученные на уроках." + Environment.NewLine +
                    "       Считайте данные в двоичное дерево поиска. Реализуйте поиск по какому-нибудь полю базы данных(возраст, вес)" + Environment.NewLine +
                    "0. Нажмите для выхода из программы.");

                n = q.Question<int>("Введите ", list, true);
                if (n == "0") break;
                dict[n].Work();
            }

            Console.ReadKey();
        }
    }    
}
