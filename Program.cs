using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Transactions;

namespace lab1
{
    public class Program
    {
        public const int N = 5;
        static public UserSet u = new UserSet();
        static public UserSet[] sets = new UserSet[N];
        static public string message = "";

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

        static void AddElems()
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("   1) Универсум");
            Console.WriteLine("   2) Множество");
            Console.WriteLine("   3) Назад");
            ref UserSet target = ref u;
            bool exitFlag1 = false;
            while (!exitFlag1)
            {
                int ans = GetFromUser("Введите действие [1-3]");
                switch (ans)
                {
                    case 1:
                        exitFlag1 = true;
                        break;
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
                        target = ref sets[num];
                        exitFlag1 = true;
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Введено неверное число");
                        break;
                }

                Console.WriteLine("Выберите действие:");
                Console.WriteLine("   1) Ручной ввод");
                Console.WriteLine("   2) Случайный ввод");
                Console.WriteLine("   3) Назад");
                bool exitFlag2 = false;
                while (!exitFlag2)
                {
                    ans = GetFromUser("Введите действие [1-3]");
                    switch (ans)
                    {
                        case 1:
                        case 2:
                            if (target == u || u.Size() > 0)
                            {
                                int count = GetFromUser("Введите количество элементов, которые будут введены", true);
                                var rand = new Random();
                                for (int i = 0; i < count; ++i)
                                {
                                    if (ans == 1)
                                    {
                                        int toAdd;
                                        do
                                        {
                                            toAdd = GetFromUser("Введите число для добавления");
                                            if (target != u && !u.Contains(toAdd))
                                            {
                                                Console.WriteLine("Введённое число не содержится в Универсуме");
                                            }
                                        } while (target != u && !u.Contains(toAdd));
                                        target.Add(toAdd);
                                    }
                                    else
                                    {
                                        if (target == u)
                                        {                                            
                                            u.Add(rand.Next() % 100);
                                        }
                                        else
                                        {
                                            target.Add(u.Rand());
                                        }
                                    }
                                }
                            }
                            else
                            {
                                message = ("Сначала необходимо задать Универсум");
                            }
                            exitFlag2 = true;
                            break;
                        case 3:
                            exitFlag2 = true;
                            break;
                        default:
                            Console.WriteLine("Введено неверное число");
                            break;
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine();
        }

        static void Clear()
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("   1) Универсум");
            Console.WriteLine("   2) Множество");
            Console.WriteLine("   3) Назад");            
            int ans = GetFromUser("Введите действие [1-3]");
            bool exitFlag = false;
            while (!exitFlag)
            {
                switch (ans)
                {
                    case 1:
                        u.Clear();
                        exitFlag = true;
                        break;
                    case 2:
                        int num;
                        exitFlag = true;
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
                        sets[num].Clear();
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Введено неверное число");
                        break;
                }
            }            
        }

        static bool CheckInsertion()
        {
            Console.Clear();
            Console.WriteLine(message);
            Console.Write("Универсум: ");
            u.Print();
            Console.WriteLine();
            for (int i = 0; i < sets.Length; ++i)
            {
                int num = i + 1;
                Console.Write("Множество " + num + ":");
                sets[i].Print();
                Console.WriteLine();
            }
            Console.WriteLine("   1) Проверка вхождения элемента");
            Console.WriteLine("   2) Проверка вхождения множества");
            Console.WriteLine("   3) Назад");
            int ans1 = GetFromUser("Введите действие [1-3]");
            while (1 != 0)
            {
                switch (ans1)
                {
                    case 1:
                        Console.WriteLine("   1) Проверка вхождения в Универсум");
                        Console.WriteLine("   2) Проверка вхождения в множество");
                        Console.WriteLine("   3) Назад");
                        int ans2 = GetFromUser("Введите действие [1-3]");
                        bool exitFlag = false;
                        while (!exitFlag)
                        {
                            switch (ans2)
                            {
                                case 1:
                                    int toCheck = GetFromUser("Введите число для проверки");
                                    return u.Contains(toCheck);
                                case 2:
                                    toCheck = GetFromUser("Введите число для проверки");
                                    int setIndex;
                                    do
                                    {
                                        setIndex = GetFromUser("Введите номер множества для проверки вхождения элемента");
                                        --setIndex;
                                        if (setIndex < 0 || setIndex > sets.GetLength(0) - 1)
                                        {
                                            Console.WriteLine("Введен неверный номер");
                                        }
                                    } while (setIndex < 0 || setIndex > sets.GetLength(0) - 1);
                                    return sets[setIndex].Contains(toCheck);
                                case 3:
                                    exitFlag = true;
                                    break;
                                default:
                                    Console.WriteLine("Введено неверное число");
                                    break;
                            }
                        }
                        break;
                    case 2:
                        int setIndexInside;
                        do
                        {
                            setIndexInside = GetFromUser("Введите номер множества [1-5], которое должно входить в другое", true);
                            --setIndexInside;
                            if (setIndexInside >= sets.GetLength(0))
                            {
                                Console.WriteLine("Номер долен быть в пределах от 1 до 5");
                            }
                        }
                        while (setIndexInside >= sets.GetLength(0));
                        int setIndexOutside;
                        do
                        {
                            setIndexOutside = GetFromUser("Введите номер множества [1-5], в которое должно входить первое", true);
                            --setIndexOutside;
                            if (setIndexOutside >= sets.GetLength(0))
                            {
                                Console.WriteLine("Номер долен быть в пределах от 1 до 5");
                            }
                        }
                        while (setIndexOutside >= sets.GetLength(0));
                        return sets[setIndexOutside].Contains(sets[setIndexInside]);
                    case 3:
                        return false;
                    default:
                        Console.WriteLine("Введено неверное число");
                        break;
                }
                Console.Clear();
                Console.WriteLine(message);
                Console.Write("Универсум: ");
                u.Print();
                Console.WriteLine();
                for (int i = 0; i < sets.Length; ++i)
                {
                    int num = i + 1;
                    Console.Write("Множество " + num + ":");
                    sets[i].Print();
                    Console.WriteLine();
                }
                Console.WriteLine("   1) Проверка вхождения элемента");
                Console.WriteLine("   2) Проверка вхождения множества");
                Console.WriteLine("   3) Назад");
                ans1 = GetFromUser("Введите действие [1-3]");
            }
        }        

        static void Calculate()
        {
            int num1;
            do
            {
                num1 = GetFromUser("Введите номер левого множества", true);
                --num1;
                if (num1 >= sets.Length || num1 < 0)
                {
                    Console.WriteLine("Номер долен быть в пределах от 1 до 5");
                }
            }
            while (num1 >= sets.Length || num1 < 0);
            int num2;
            do
            {
                num2 = GetFromUser("Введите номер правого множества", true);
                --num2;
                if (num2 >= sets.Length || num1 < 0)
                {
                    Console.WriteLine("Номер долен быть в пределах от 1 до 5");
                }
            }
            while (num2 >= sets.Length || num1 < 0);
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
                var ans = new UserSet();
                switch (usrAns)
                {
                    case 1:
                        ans = sets[num1].IntersectWith(sets[num2]);
                        Console.Write("Результат: ");
                        message = "Результат действия: " + ans.ToString();
                        exitFlag = true;
                        break;
                    case 2:
                        ans = sets[num1].UnionWith(sets[num2]);
                        Console.Write("Результат: ");
                        message = "Результат действия: " + ans.ToString();
                        exitFlag = true;
                        break;
                    case 3:
                        ans.Add(sets[num1].ExceptWith(sets[num2]));
                        Console.Write("Результат: ");
                        message = "Результат действия: " + ans.ToString();
                        exitFlag = true;
                        break;
                    case 4:
                        ans = sets[num1].SymmetricExceptWith(sets[num2]);
                        Console.Write("Результат: ");
                        message = "Результат действия: " + ans.ToString();
                        exitFlag = true;
                        break;
                    case 5:
                        message = "";
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
        
        public static void Complete()
        {
            int setIndex;
            do
            {
                setIndex = GetFromUser("Введите номер множества для дополнения", true);
                --setIndex;
                if (setIndex >= sets.Length || setIndex < 0)
                {
                    Console.WriteLine("Номер долен быть в пределах от 1 до 5");
                }
            } while (setIndex >= sets.Length || setIndex < 0);
            sets[setIndex] = sets[setIndex].UnionWith(u);
        }

        static int Main()
        {
            for (int i = 0; i < N; ++i)
            {
                sets[i] = new UserSet();
            }
            bool exitFlag = false;
            while(!exitFlag)
            {
                Console.Clear();
                Console.WriteLine(message);
                Console.Write("Универсум: ");
                u.Print();
                Console.WriteLine();
                for (int i = 0; i < sets.Length; ++i)
                {
                    int num = i + 1;
                    Console.Write("Множество " + num + ":");
                    sets[i].Print();
                    Console.WriteLine();
                }
                
                Console.WriteLine("Выберите действие из списка:");
                Console.WriteLine("   1) Добавить элементы");
                Console.WriteLine("   2) Очистить множество");                
                Console.WriteLine("   3) Выполнить операцию на множествах");
                Console.WriteLine("   4) Проверить вхождение");
                Console.WriteLine("   5) Дополнить до универсума");
                Console.WriteLine("   6) Выход");

                int answer = GetFromUser("Выберите действие [1-4]");
                Console.WriteLine();

                switch (answer)
                {
                    case 1:
                        AddElems();
                        break;
                    case 2:
                        Clear();
                        break;
                    case 3:
                        Calculate();
                        break;
                    case 4:
                        message = "Результат действия: " + CheckInsertion();
                        break;
                    case 5:
                        Complete();
                        break;
                    case 6:
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