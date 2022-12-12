using System;
using System.Collections.Generic;
using System.Text;

namespace CS_Lab3_PIN_24_Khan_Anna
{
    class SurnameAuthorComparer : Paper, IComparer<Paper>
    {
        public new int Compare(Paper paper1, Paper paper2)
        {
            return String.Compare(paper1.Author.Surname, paper2.Author.Surname);
        }
    }
}
