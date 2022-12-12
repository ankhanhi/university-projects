using System;
using System.Collections.Generic;
using System.Text;

namespace CS_Lab5_PIN_24_Khan_Anna
{
    class TeamsJournal<TKey>
    {
        List<TeamsJournalEntry> listChanges = new List<TeamsJournalEntry>();

        public void AddEntry(object source, ResearchTeamsChangedEventArgs<TKey> args)
        {
            listChanges.Add(new TeamsJournalEntry(args.CollectionName, args.TypeEvent, args.PropertyName, args.NumberChanged));
        }


        public override string ToString()
        {
            string result = "";
            foreach (TeamsJournalEntry entry in listChanges)
            {
                result += entry.ToString();
            }
            return result;
        }
    }
}
