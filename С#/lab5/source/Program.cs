using System;
using System.Collections.Generic;
using System.IO;

namespace CS_Lab5_PIN_24_Khan_Anna
{
    static class Program
    {
        static void Main(string[] args)
        {
            //Создать объект типа T с непустым списком элементов, для которого предусмотрен ввод данных с консоли.
            ResearchTeam researchTeam1 = new ResearchTeam("NameResearch 1", 1, "Team1", TimeFrame.Long, new DateTime(1, 2, 3), new Team());
            researchTeam1.AddPublications(new Paper("Publication 1", new Person("Maria", "Volkova", DateTime.Now), DateTime.Now));
            researchTeam1.AddPublications(new Paper("Publication 2", new Person("Philiph", "Pulman", DateTime.Now), DateTime.Now));

            //Создать полную копию объекта с помощью метода, использующего сериализацию, и вывести исходный объект и его копию.
            ResearchTeam researchTeam2 = researchTeam1.DeepCopy<ResearchTeam>();
            Console.WriteLine(researchTeam1.ToString());
            Console.WriteLine("COPY\n" + researchTeam2.ToString());

            //Предложить пользователю ввести имя файла:
            //если файла с введенным именем нет, приложение должно сообщить об этом и создать файл;
            //если файл существует, вызвать метод Load(string filename) для инициализации объекта T данными из файла.
            ResearchTeam team = new ResearchTeam();
            Console.WriteLine("Input path of file with data of objects: ");
            string filename = Console.ReadLine();
            if (File.Exists(filename))
            {
                team.Load(filename);
            }
            else
            {
                filename = Path.GetFullPath(@"..\..\..\") + "\\resource\\base.txt";
                FileStream file = File.Create(filename);
                Console.WriteLine("File is created");
                file.Close();
            }

            //Вывести объект T
            Console.WriteLine(team.ToString());

            //Для этого же объекта T сначала вызвать метод AddFromConsole(), затем метод Save(string filename)
            team.AddFromConsole();
            team.Save(filename);

            //Вывести объект T
            Console.WriteLine(team.ToString());

            //Вызвать последовательно
            //статический метод Load(string filename, T obj), передав как параметры ссылку на тот же самый объект T и
            //введенное ранее имя файла;
            ResearchTeam.Load(filename, team);

            //метод AddFromConsole();
            team.AddFromConsole();

            //статический метод Save(string filename, T obj).
            ResearchTeam.Save(filename, team);

            //Вывести объект T
            Console.WriteLine(team.ToString());
        }
    }
}

