using System;
using System.Collections.Specialized;
using System.Globalization;

namespace lab1
{
    class Program
    {
        static int GetFromUser(string msg, bool checkMinus = false)
        {
            bool finished = false;
            int inp = 0;
            string? buf;
            do
            {
                Console.Write(msg + ": ");
                buf = Console.ReadLine();
                if (buf == "" || buf == null)
                {
                    Console.WriteLine("Не введено число");
                }
                else if (!int.TryParse(buf, out inp))
                {
                    bool isInt = true;
                    for (int i = 0; i < buf.Length; ++i)
                    {
                        if ("0123456789".IndexOf(buf[i]) < 0)
                        {
                            isInt = false;
                        }
                    }
                    if (isInt)
                    {
                        Console.WriteLine("Число слишком большое");
                    }
                    else
                    {
                        Console.WriteLine("Число должно быть целым и состоять только из цифр");
                    }
                }
                else
                {
                    inp = int.Parse(buf);
                    if (checkMinus)
                    {
                        if (inp < 0)
                        {
                            inp = 0;
                            Console.WriteLine("Число должно быть положительным");
                        }
                        else
                        {
                            finished = true;
                        }
                    }
                    else
                    {
                        finished = true;
                    }

                }

            }
            while (!finished);
            return inp;
        }        

        static void PrintSet(in HashSet<int> set)
        {
            Console.Write("{ ");
            if (set.Count > 0)
            {
                foreach (int i in set)
                {
                    Console.Write(i + " ");
                }
            }
            else
            {
                Console.Write("- ");
            }
            Console.Write("}");
        }

        static void Calculate(in HashSet<int> [] sets)
        {
            int num1;
            do
            {
                num1 = GetFromUser("Введите номер множества 1 [1-5]", true);
                --num1;
                if (num1 >= sets.Length)
                {
                    Console.WriteLine("Номер долен быть в пределах от 1 до 5");
                }
            }
            while (num1 >= sets.Length);
            int num2;
            do
            {
                num2 = GetFromUser("Введите номер множества 2 [1-5]", true);
                --num2;
                if (num2 >= sets.Length)
                {
                    Console.WriteLine("Номер долен быть в пределах от 1 до 5");
                }
            }
            while (num2 >= sets.Length);
            HashSet<int> tmp = new HashSet<int>(sets[num1]);
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("   1) Пересечение множеств");
            Console.WriteLine("   2) Объединение множеств");
            Console.WriteLine("   3) Вычитание множеств");
            Console.WriteLine("   4) Симметрическая разность множеств");
            Console.WriteLine("   5) Назад");
            bool exitFlag = false;
            while (!exitFlag)
            {
                int usrAns = GetFromUser("Введите номер действия [1-5]");
                switch (usrAns)
                {
                    case 1:
                        tmp.IntersectWith(sets[num2]);
                        Console.Write("Результат: ");
                        PrintSet(tmp);
                        exitFlag = true;
                        break;
                    case 2:
                        tmp.UnionWith(sets[num2]);
                        Console.Write("Результат: ");
                        PrintSet(tmp);
                        exitFlag = true;
                        break;
                    case 3:
                        tmp.ExceptWith(sets[num2]);
                        Console.Write("Результат: ");
                        PrintSet(tmp);
                        exitFlag = true;
                        break;
                    case 4:
                        tmp.SymmetricExceptWith(sets[num2]);
                        Console.Write("Результат: ");
                        PrintSet(tmp);
                        exitFlag = true;
                        break;
                    case 5:
                        exitFlag = true;
                        break;
                    default:
                        Console.WriteLine("Введена неверная команда");
                        break;
                }
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        const int N = 5;

        static int Main()
        {
            HashSet<int> [] sets = new HashSet<int>[N];
            for (int i = 0; i < sets.Length; ++i)
            {
                sets[i] = new HashSet<int>();
            }            

            bool exitFlag = false;
            while(!exitFlag)
            {
                for (int i = 0; i < sets.Length; ++i)
                {
                    int num = i + 1;
                    Console.Write("Множество " + num + ":");
                    PrintSet(sets[i]);
                    Console.WriteLine();
                }

                Console.WriteLine("Выберите действие из списка:");
                Console.WriteLine("   1) Добавить элементы в множество");
                Console.WriteLine("   2) Очистить множество");
                Console.WriteLine("   3) Выполнить операцию на множествах");
                Console.WriteLine("   4) Выход");

                int answer = GetFromUser("Выберите действие [1-4]");
                Console.WriteLine();

                switch (answer)
                {
                    case 1:
                    case 2:                    
                        int num;
                        do
                        {
                            num = GetFromUser("Введите номер множества [1-5]", true);
                            --num;
                            if (num >= sets.Length)
                            {
                                Console.WriteLine("Номер долен быть в пределах от 1 до 5");
                            }
                        }
                        while (num >= sets.Length);
                        if (answer == 1)
                        {
                            int count = GetFromUser("Введите количество элементов для добавления", true);
                            for (int i = 0; i < count; ++i)
                            {
                                sets[num].Add(GetFromUser("Введите число для добавления"));
                            }
                        }
                        else
                        {
                            sets[num].Clear();
                        }                        
                        Console.WriteLine();
                        break;
                    case 3:
                        Calculate(in sets);
                        break;
                    case 4:
                        exitFlag = true;
                        break;
                    default:
                        Console.WriteLine("Введено неверное действие\n");
                        break;
                }
            }

            return 0;
        }
    }
}


