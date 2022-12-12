using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CS_Lab3_PIN_24_Khan_Anna
{
    class DataPublicationComparer : Paper, IComparer<Paper>
    {
        public new int Compare(Paper paper1, Paper paper2)
        {
            return DateTime.Compare(paper1.DataPublication, paper2.DataPublication);
        }
    }
}
