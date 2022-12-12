using System;
using System.Collections.Generic;
using System.Text;

namespace CS_Lab4_PIN_24_Khan_Anna
{
    class TeamsJournalEntry
    {
        public string CollectionName { get; set; }
        public Revision TypeEvent { get; set; }
        public string NameProperty { get; set; }
        public int NumberChanged { get; set; }

        public TeamsJournalEntry(string nameCol, Revision typeEvent, string nameProperty, int numberChanged)
        {
            CollectionName = nameCol;
            TypeEvent = typeEvent;
            NameProperty = nameProperty;
            NumberChanged = numberChanged;
        }

        public override string ToString()
        {
            return "\nИзменение объекта ResearchTeamCollection<TKey>\nНазвание коллекции: " + CollectionName 
                + "\nТип события: " + TypeEvent.ToString() 
                + "\nИзмененное свойство класса ResearchTeam: " + NameProperty 
                + "\nНомер регистрации объекта ResearchTeam измененного элемента: " + NumberChanged.ToString() + "\n";
        }
    }
}
