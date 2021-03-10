using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDB
{
    // it is internal since the other class is also internal 
    internal class GradStudent : Student // shows that GradStudent is under student
    {
        public decimal TuitionCredit { get; set; }
        public string FacultyAdvisor { get; set; }

        // the ctor
        // with things that make up a reg student
        // but with the things that make the grad student special
        public GradStudent(string first, string last, double gpa,
            string email, DateTime enrolled,
            decimal credit, string advisor)
            // needs to lay out the base class first
            : base (new ContactInfo(first, last, email), gpa, enrolled)
        {
            TuitionCredit = credit;
            FacultyAdvisor = advisor;
        }

        // lambda expression - "=>" reads: "goes to"
        public override string ToString() => base.ToString() + $"    Credit: {TuitionCredit}\n       Fac: {FacultyAdvisor}\n";
        

        // to send to the output file
        public override string ToStringForOutputFile()
        {
            StringBuilder str = new StringBuilder(this.GetType() + "\n"); // shows type of student
            str.Append(base.ToStringForOutputFile());
            str.Append($"{TuitionCredit}\n");
            str.Append($"{FacultyAdvisor}");

            // returning a string, making the string builder into a string
            return str.ToString();
        }
    }
}
