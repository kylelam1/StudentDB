using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDB
{
    public enum YearRank // a simple class
    {
        Freshman = 1, // need to set a number so it doesn't start at 0
        Sophomore = 2,
        Junior = 3,
        Senior = 4
    }
    internal class Undergrad : Student // shows undergrad is under student
    {
        public YearRank Rank { get; set; }
        public string DegreeMajor { get; set; }

        // the ctor
        // with things that make up a reg student
        // but with the things that make the undergrad student special
        public Undergrad(string first, string last, double gpa,
            string email, DateTime enrolled,
            YearRank rank, string degree)
            // needs to lay out the base class first
            : base(new ContactInfo(first, last, email), gpa, enrolled)
        {
            Rank = rank;
            DegreeMajor = degree;
        }

        // does not go to the output file
        public override string ToString() => base.ToString() + $"      Year: {Rank}\n     Major: {DegreeMajor}\n";
    
        public override string ToStringForOutputFile()
        {
            string str = this.GetType() + "\n";
            str += base.ToStringForOutputFile();
            str += $" {Rank}\n";
            str += $"{DegreeMajor}";  // no CRLF here becuse it will be called in WriteLine()

            return str;
        }
    }
}
