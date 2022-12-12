using System;
using System.Collections.Generic;

namespace CS_Lab3_PIN_24_Khan_Anna
{
    public enum TimeFrame { Long = 0, Year = 1, TwoYears = 2};

    class Program
    {
        static void Main(string[] args)
        {

            /* Вариант 3

              1. Создать объект ResearchTeam и вызвать методы, выполняющие сортировку списка публикаций List<Paper> 
              по разным критериям, после каждой сортировки вывести данные объекта. Выполнить сортировку

              по дате выхода публикации;
              по названию публикации;
              по фамилии автора.
            */

            ResearchTeam researchTeam = new ResearchTeam();

            Person[] persons = {
                new Person("Philip", "Pullman", DateTime.Now),
                new Person("Maria", "Volkova", DateTime.Now),
                new Person("Diana", "Jons", DateTime.Now)
            };

            Paper[] papers = {
                new Paper("Semiconductors", persons[0], new DateTime(2021,6,13)),
                new Paper("CS the best", persons[1], new DateTime(2021,1,10)),
                new Paper("Northern lights", persons[2], new DateTime(1991,1,3)),
            };

            researchTeam.AddMembers(persons);
            researchTeam.AddPublications(papers);


            researchTeam.SortPublicationsByData();
            Console.WriteLine("{0}: {1}", "Сортировка по дате выхода публикации", 
                researchTeam.ToString());
            Console.WriteLine("----------------------------------------------------------");

            researchTeam.SortPublicationsByTitle();
            Console.WriteLine("{0}: {1}", "Сортировка по названию публикации",
                researchTeam.ToString());
            Console.WriteLine("----------------------------------------------------------");

            researchTeam.SortPublicationsBySurnameAuthor();
            Console.WriteLine("{0}: {1}", "Сортировка по фамилии автора",
                researchTeam.ToString());
            Console.WriteLine("----------------------------------------------------------");

            /*
            2. Создать объект ResearchTeamCollection<string>. 
            Добавить в коллекцию несколько разных элементов ResearchTeam и вывести объект ResearchTeamCollection<string>.
            */

            KeySelector<string> keySelector = (ResearchTeam researchTeam1) =>
            {
                return researchTeam1.GetHashCode().ToString();
            };

            ResearchTeamCollection<string> teamCollection = new ResearchTeamCollection<string>(keySelector);

            teamCollection.AddDefaults();
            teamCollection.AddResearchTeams();
            Console.WriteLine("Вывод объекта ResearchTeamCollection<string:\n" + teamCollection.ToString());
            Console.WriteLine("----------------------------------------------------------");
            /*
            3. Вызвать методы класса ResearchTeamCollection<string>, выполняющие операции с коллекцией-словарем 
            Dictionary<TKey, ResearchTeam>, после каждой операции вывести результат операции. Выполнить:

            поиск даты последней по времени выхода публикации среди всех элементов коллекции;
            */

            Console.WriteLine("{0}: {1}", "Последняя публикация", teamCollection.LastPublicationCollection.ToString());
            Console.WriteLine("----------------------------------------------------------");

            /*
            вызвать метод TimeFrameGroup для выбора объектов ResearchTeam с заданным значением продолжительности исследований;
            */

            Console.WriteLine("Метод TimeFrameGroup для выбора объектов ResearchTeam с " +
                "заданным значением продолжительности исследований 'TwoYears'");

            foreach(var i in teamCollection.TimeFrameGroup(TimeFrame.TwoYears))
            {
                Console.WriteLine(i.Value);
            }
            Console.WriteLine("----------------------------------------------------------");

            /*
            вызвать свойство класса, выполняющее группировку элементов коллекции по значению продолжительности исследований;
            вывести все группы элементов из списка.
            */
            foreach(var i in teamCollection.KeyValuePairsSortDuration)
            {
                Console.WriteLine("Группа элементов по значению продолжительности: " + i.Key);

                foreach (var j in teamCollection.TimeFrameGroup(i.Key))
                {
                    Console.WriteLine(j.Value);
                }
            }

            Console.WriteLine("----------------------------------------------------------");
            /*
            4. Создать объект типа TestCollection<Team, ResearchTeam>. Ввести число элементов в коллекциях и 
            вызвать метод для поиска первого, центрального, последнего и элемента, не входящего в коллекции.
            Вывести значения времени поиска для всех четырех случаев.  
            */

            int numberCollection;
            while(true)
            {
                try
                {
                    Console.Write("Введите число элементов в коллекциях: ");
                    numberCollection = Convert.ToInt32(Console.ReadLine());
                    if (numberCollection < 0)
                    {
                        throw new Exception("Число должно быть больше нуля. Попробуйте снова");
                    }
                    break;
                }
                catch(Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
            
            GenerateElement<Team, ResearchTeam> generateDelegateInstance = (int delegator) =>
            {
                var key = new Team("Name", "Organization", delegator);
                var value = new ResearchTeam(
                    "Topic", delegator + 1, (TimeFrame)(delegator % 3), 
                    new DateTime(1999 + delegator, delegator % 12 + 1, delegator % 27 + 1), 
                    new Team("Name", "Organization", delegator + 1));

                return new KeyValuePair<Team, ResearchTeam>(key, value);
            };

            var searchTest = new TestCollections<Team, ResearchTeam>(numberCollection, generateDelegateInstance);

            searchTest.searchElementInTKeyList();
            searchTest.searchElementInStrList();
            searchTest.searchElementInTKeyDictionaryByKey();
            searchTest.searchElementInStrDictionaryByKey();
            searchTest.searchElementInTKeyDictionaryByValue();

        }
    }
}

