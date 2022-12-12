using System;
using System.Collections;
using System.Collections.Generic;

namespace CS_Lab1_PIN_24_Khan_Anna
{

    enum Education { Specialist, Bachelor, SecondEducation };
    enum TimeFrame { Year, TwoYears, Long };

    class Program
    {

        static void Main(string[] args)
        {
            string line = "-------------------------------------------";
            /* Вариант 3
             * 
             * 1
             * Создать два объекта типа Team с совпадающими данными и проверить, 
             * что ссылки на объекты не равны, а объекты равны, вывести значения хэш-кодов для объектов.
             */

            Team team1 = new Team("Rosmen", 101);
            Team team2 = new Team("Rosmen", 101);

            Console.WriteLine("Сравнение ссылок объектов Team: " + ReferenceEquals(team1, team2));
            Console.WriteLine("Сравнение данных объектов Team: " + team1.Equals(team2));
            Console.WriteLine("{0}: {1} {2}", "Хэш-коды объектов Team", team1.GetHashCode(), team2.GetHashCode());

            Console.WriteLine(line);
            /* 2
             * В блоке try/catch присвоить свойству с номером регистрации некорректное значение, 
             * в обработчике исключения вывести сообщение, переданное через объект-исключение.
             */

            Console.WriteLine("Присвоение свойству с номером регистрации некорректного значения -7: ");
            try
            {
                team1.Number = -7;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            Console.WriteLine(line);
            /* 3
             * Создать объект типа ResearchTeam, добавить элементы в список 
             * публикаций и список участников проекта 
             */

            ResearchTeam researchTeam = new ResearchTeam();

            Person[] persons = {
                new Person("Maria", "Volkova", DateTime.Now),
                new Person("Diana", "Jons", DateTime.Now),
                new Person("Philip", "Pullman", DateTime.Now)
            };

            Paper[] papers = {
                new Paper("CS the best", new Person(), new DateTime(1991,1,3)),
                new Paper("Northern lights", persons[1], new DateTime(2021,1,10)),
                new Paper("Semiconductors", persons[1], new DateTime(2021,6,13))
            };
            researchTeam.AddMembers(persons);
            researchTeam.AddPublications(papers);

            /* 4
             * Вывести значение свойства Team для объекта типа ResearchTeam.
             */

            Console.WriteLine(researchTeam.ToString());

            Console.WriteLine(line);

            /* 5
             * С помощью метода DeepCopy() создать полную копию объекта ResearchTeam. 
             * Изменить данные в исходном объекте ResearchTeam и вывести копию и исходный объект, 
             * полная копия исходного объекта должна остаться без изменений.
             */

            ResearchTeam researchTeam1 = new ResearchTeam();
            researchTeam1 = (ResearchTeam)researchTeam.DeepCopy();
            researchTeam.Number++;
            Console.WriteLine(researchTeam.ToString());
            Console.WriteLine(researchTeam1.ToString());

            Console.WriteLine(line);

            /* 6
             * С помощью оператора foreach для итератора, определенного в классе ResearchTeam, 
             * вывести список участников проекта, которые не имеют публикаций.
             */

            Console.WriteLine("\nCписок участников проекта, которые не имеют публикаций: \n");
            foreach (Person person in researchTeam.GetMembersNotAuthors())
            {
                Console.WriteLine(person);
            }

            Console.WriteLine(line);

            /*  7
             *  С помощью оператора foreach для итератора с параметром, определенного в классе ResearchTeam,
             *  вывести список всех публикаций, вышедших за последние два года. 
             */

            Console.WriteLine("\nCписок всех публикаций, вышедших за последние два года: \n");
            foreach (Paper paper in researchTeam.GetPublicationsLastNYears(2))
            {
                Console.WriteLine(paper);
            }

            Console.WriteLine(line);

            /* 8
             * С помощью оператора foreach для объекта типа ResearchTeam вывести список участников проекта, 
             * у которых есть публикации.
             */

            ArrayList arrayList = new ArrayList();
            ResearchTeam.ResearchTeamEnumerator researchTeamEnum = new ResearchTeam.ResearchTeamEnumerator(researchTeam);
            Console.WriteLine("\nCписок участников проекта, у которых есть публикации: \n");
            while (researchTeamEnum.MoveNext())
            {
                arrayList.Add(researchTeamEnum.Current);
            }

            for (int i = 0; i < arrayList.Count; i++)
            {
                if (arrayList.IndexOf(arrayList[i]) == arrayList.LastIndexOf(arrayList[i]) ||
                    arrayList.LastIndexOf(arrayList[i]) == i)
                {
                    Console.WriteLine(arrayList[i]);
                }
            }



            Console.WriteLine(line);

            /* 9
             * Определить итератор для перебора участников проекта (объектов типа Person), имеющих более одной публикации, 
             * для этого определить метод, содержащий блок итератора и использующий оператор yield. 
             */
            Console.WriteLine("\nCписок участников проекта, имеющих более одной публикации: \n");
            foreach (Person person in researchTeam.GetMembersMoreOnePublications())
            {
                Console.WriteLine(person);
            }

            Console.WriteLine(line);

            /* 10
             * Определить итератор для перебора публикаций (объектов типа Paper), вышедших за последний год, для этого 
             * определить метод, содержащий блок итератора и использующий оператор yield. 
             */


            Console.WriteLine("\nCписок публикаций (объектов типа Paper), вышедших за последний год: \n");
            foreach (Paper paper in researchTeam.GetPublicationsLastYear())
            {
                Console.WriteLine(paper);
            }

            Console.WriteLine(line);

        }
    }


}

