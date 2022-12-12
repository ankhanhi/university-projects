using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CS_Lab4_PIN_24_Khan_Anna
{
    public class Paper : IComparable, IComparer<Paper>
    {

        public string Title { get; set; }

        public Person Author { get; set; }

        public DateTime DataPublication { get; set; }

        public Paper(string TitleValue, Person AuthorValue, DateTime DataPublicationValue)
        {
            Title = TitleValue;
            Author = AuthorValue;
            DataPublication = DataPublicationValue;
        }

        public Paper() : this(String.Empty, new Person(), DateTime.MinValue) { }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            return (DateTime.Compare(DataPublication, (obj as Paper).DataPublication));
        }

        public int Compare(Paper paper1, Paper paper2)
        {
            return paper1.Title.CompareTo(paper2.Title);
        }
        public override string ToString()
        {
            return ("'" + Title + "' " + Author + " " + DataPublication.ToShortDateString());
        }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Paper paper = (Paper)obj;
                return (Title == paper.Title) && (Author == paper.Author) &&
                    (DateTime.Compare(DataPublication, paper.DataPublication) == 0);
            }
        }

        public override int GetHashCode()
        {
            return (Title.GetHashCode() + (Author.GetHashCode() + DataPublication.GetHashCode()));
        }

        public static bool operator ==(Paper paper1, Paper paper2)
        {
            return (paper1.Title == paper2.Title) && (paper1.Author == paper2.Author) &&
                (DateTime.Compare(paper1.DataPublication, paper2.DataPublication) == 0);
        }

        public static bool operator !=(Paper paper1, Paper paper2)
        {
            return !(paper1 == paper2);
        }

        public virtual object DeepCopy()
        {
            Paper copy_example = new Paper(Title, Author, DataPublication);

            return copy_example;
        }


    }
}
