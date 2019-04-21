using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketControlApp
{
    //Создайте консольное приложение, которое позволяет вести учёт посетителей в некоторое заведение 
    // (время, когда зашёл и вышел, имя, номер удостоверения, цель посещения). 
    //  Сделайте приложение таким образом, чтобы им мог пользоваться охранник, 
    //  который ничего не понимает в компьютерах.

    class Program
    {
        // Max int value
        public static int IntParser(int from, int to = 2147483647)
        {
            int result;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out result) && result >= from && result <= to)
                {
                    return result;
                }
            }
        }

        static void Main(string[] args)
        {
            using (var context = new AppContext())
            {
                int chouse = 0;
                while (true)
                {
                    Console.WriteLine("Введите");
                    Console.WriteLine("1 - Новый посетитель");
                    Console.WriteLine("2 - Посмотреть посетителей");
                    Console.WriteLine("3 - Посетитель вышел");
                    Console.WriteLine("4 - Выход");

                    chouse = IntParser(1, 4);

                    if (chouse == 1)
                    {
                        Person person = new Person();
                        Console.WriteLine("Введите его имя");
                        person.Name = Console.ReadLine();

                        Console.WriteLine("Введите номер документа посетителя");
                        person.DocumentNumber = Console.ReadLine();

                        Console.WriteLine("Введите цель посещения");
                        person.VisitPurpose = Console.ReadLine();

                        person.ArrivalDate = DateTime.Now;

                        context.People.Add(person);
                        context.SaveChanges();
                    }
                    else if (chouse == 2)
                    {
                        foreach (var person in context.People)
                        {
                            Console.WriteLine("Имя: " + person.Name);
                            Console.WriteLine("Номер документа: " + person.DocumentNumber);
                            Console.WriteLine("Дата прибытия в заведение:" + person.ArrivalDate.ToUniversalTime());
                            if (person.ExitDate is null) Console.WriteLine("Человек находится в заведении");
                            else Console.WriteLine("Дата ухода: " + person.ExitDate.Value.ToUniversalTime());
                            Console.WriteLine("Цель посещения: " + person.VisitPurpose);
                            Console.WriteLine();
                        }
                    }
                    else if (chouse == 3)
                    {
                        while (true)
                        {
                            int personId = 1;
                            foreach (var person in context.People)
                            {
                                if (person.ExitDate is null)
                                {
                                    Console.WriteLine("Id = " + personId);
                                    Console.WriteLine("Имя: " + person.Name);
                                    Console.WriteLine("Номер документа: " + person.DocumentNumber);
                                    Console.WriteLine("Дата прибытия в заведение:" + person.ArrivalDate.ToShortDateString());
                                    Console.WriteLine();
                                }
                                personId++;
                            }

                            Console.WriteLine("Введите Id посетителя");
                            Console.WriteLine("Если хотите выйти введите 0");
                            int selectPersonId = IntParser(0, personId);

                            if (selectPersonId == 0) break;

                            personId = 1;
                            for (int i = 0; i < context.People.Count(); i++)
                            {
                                if (selectPersonId == personId)
                                {
                                    context.People.ToList()[i].ExitDate = DateTime.Now;
                                    context.SaveChanges();
                                    break;
                                }
                                personId++;
                            }
                            Console.WriteLine("Человек успешно вышел");
                            break;
                        }
                    }
                    else break;
                }
            }
        }
    }
}
