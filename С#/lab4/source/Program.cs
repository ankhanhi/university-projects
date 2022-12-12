using System;
using System.Collections.Generic;

namespace CS_Lab4_PIN_24_Khan_Anna
{
    static class Program
    {
        static void Main(string[] args)
        {
            // Создать две коллекции ResearchTeamCollection<string>
             
            ResearchTeamCollection<string> collection1 = new ResearchTeamCollection<string>(i => i.Name) { CollectionName = "Collection1" };
            ResearchTeamCollection<string> collection2 = new ResearchTeamCollection<string>(i => i.Name) { CollectionName = "Collection2" };

            /* Создать объект TeamsJournal, подписать его на события ResearchTeamsChanged 
             * из обоих объектов ResearchTeamCollection<string>
             */

            TeamsJournal<string> journal = new TeamsJournal<string>();
            collection1.ResearchTeamsChanged += journal.AddEntry;
            collection2.ResearchTeamsChanged += journal.AddEntry;


            /*  Внести изменения в коллекции ResearchTeamCollection<string>:
             *  
             *  добавить элементы в коллекции;
             *  изменить значения разных свойств элементов, входящих в коллекцию;
             *  удалить элемент из коллекции;
             *  изменить данные в удаленном элементе;
             *  заменить один из элементов коллекции;
             *  изменить данные в элементе, который был удален из коллекции при замене элемента.
             */



            ResearchTeam researchTeam1 = new ResearchTeam("NameResearch 1", 1, "Team1", TimeFrame.Long, new DateTime(1, 2, 3), new Team());
            ResearchTeam researchTeam2 = new ResearchTeam("NameResearch 2", 2, "Team2", TimeFrame.Year, new DateTime(2001, 1, 3), new Team());
            ResearchTeam researchTeam3 = new ResearchTeam("NameResearch 3", 3, "Team3", TimeFrame.Long, new DateTime(1999, 2, 3), new Team());
 
            //добавить элементы в коллекции;
            collection1.Add(researchTeam1, researchTeam2);
            collection2.Add(researchTeam3);

            //изменить значения разных свойств элементов, входящих в коллекцию;
            researchTeam1.Duration = TimeFrame.TwoYears;
            researchTeam2.Members.Add(new Person("Maria", "Volkova", DateTime.Now));

            //удалить элемент из коллекции;
            collection1.Remove(researchTeam2);
            collection2.Remove(researchTeam3);

            //изменить данные в удаленном элементе;
            researchTeam2.Number++;

            //заменить один из элементов коллекции;
            collection1.Replace(researchTeam1, researchTeam2);

            // Вывести данные объекта TeamsJournal.

            Console.WriteLine(journal.ToString());
        }
    }
}

