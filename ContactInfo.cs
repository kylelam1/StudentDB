///////////////////////
///Change historyu
///Date         Develioer           Description
///2021-02-27   Kyle Lam            Create contact info class



namespace StudentDB
{
    public class ContactInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        private string emailAddress;

        public ContactInfo(string first, string last, string email)
        {
            FirstName = first;
            LastName = last;
            EmailAddress = email;
        }

        public string EmailAddress
        {
            get
            {
                return emailAddress;
            }
            set
            {
                // passed in email must have at least 3 chars and must be "@"
                if (value.Contains("@") && value.Length > 3)
                {
                    emailAddress = value;
                }
                else
                {
                    // TODO: not sure how we want to handle this - look into possible regex
                    emailAddress = "ERROR: Invalid email.";
                }
            }

        }

        // lambda expression for user friendly printing of contact info
        public override string ToString() => $"{FirstName}\n{LastName}\n{EmailAddress}\n";
        public string ToStringLegal() => $"{LastName}, {FirstName}\n{EmailAddress}\n";

    }
}