////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.IO;

namespace StudentDB
{
    internal class StudentApp
    {
        // create the storage for a collection of students
        private List<Student> students = new List<Student>();
        internal void ReadDataFromInputFile()
        {
            // create a stream reader to attach to the input file on disk
            StreamReader infile = new StreamReader("INPUTDATAFILE.txt");

            // use the file to read in student data
            string studentType= string.Empty;
            while ((studentType = infile.ReadLine()) != null)
            {
                // reading the student records
                string first = infile.ReadLine();
                string last = infile.ReadLine();
                double gpa = double.Parse(infile.ReadLine());
                string email = infile.ReadLine();
                DateTime date = DateTime.Parse(infile.ReadLine());

                // now we've read everything for a student - branch depending
                // on what kind of student
                if(studentType == "StudentDB.Undergrad")
                {
                    YearRank rank = (YearRank)Enum.Parse(typeof(YearRank), infile.ReadLine());
                    string major = infile.ReadLine();

                    Undergrad undergrad = new Undergrad(first, last, gpa, email, date, rank, major);
                    students.Add(undergrad); // adding the undergrad to the list

                    // Console.WriteLine(undergrad);
                }
                // if not undergrad, must mean we have a grad student
                else if (studentType == "StudentDB.GradStudent")
                {
                    decimal tuition = decimal.Parse(infile.ReadLine());
                    string facAdvisor = infile.ReadLine();

                    GradStudent grad = new GradStudent(first, last, gpa, email, date, tuition, facAdvisor);
                    students.Add(grad);
                }

                // create the new read-in student from the data and store in the list
                //Student stu = new Student(first, last, gpa, email, date);
                //students.Add(stu);
                //Console.WriteLine($"Done reading record for:\n {stu}");
            }

            // release the resource
            Console.WriteLine("Reading input file is complete...");
            infile.Close();
        }

        // searches the current list for a student record
        // returns the student object it finds, null if not found
        // email contains the search string
        private Student FindStudentRecord(out string email) // out means an output must come out
        {
            // we have to find the email from somewhere
            Console.WriteLine("\nENTER email address (primary key) to search: ");
            email = Console.ReadLine();

            // looking through the database
            foreach (var student in students)
            {
                if(email == student.Info.EmailAddress)
                {
                    // found it!
                    Console.WriteLine($"FOUND email address: {student.Info.EmailAddress}");
                    return student;
                }
            }
            // arrived here, does not find it
            Console.WriteLine($"{email} NOT FOUND.");
            return null;
        }

        internal void RunDatabaseApp()
        {
            while (true)
            {
                // display a menu for the main selection of CRUD operations
                DisplayMainMenu();

                // capturing the user's selection
                char selection = GetUserSelection();
                string email = string.Empty; // to make the compiler happy

                // doing that thing (method call) that the user indicated
                switch (selection)
                {
                    case 'P':
                    case 'p':
                        PrintAllRecords();
                        break;
                    case 'A':
                    case 'a':
                        AddStudentRecord();
                        break;
                    case 'F':
                    case 'f':
                        FindStudentRecord(out email);
                        break;
                    case 'D':
                    case 'd':
                        DeleteStudentRecord();
                        break;
                    case 'M':
                    case 'm':
                        ModifyStudentRecord();
                        break;
                    case 'E':
                    case 'e':
                        ExitApplicationWithoutSave();
                        break;
                    case 'S':
                    case 's':
                        SaveAllChangesAndQuit();
                        break;
                    default:
                        Console.WriteLine($"{selection} is not a valid choice!");
                        break;
                }
            }
        }

        // if the user wants to change a certain record
        private void ModifyStudentRecord()
        {
            // first, search the list to see if this email rec already exists
            string email = string.Empty;
            Student stu = FindStudentRecord(out email);
            if (stu != null)
            {
                // lots of code!!!
                // if we find it
                ModifyStudent(stu);
            }
            else
            {
                // record as not in the database - issuie an info message
                Console.WriteLine($"***** RECORD NOT FOUND.\nCan/t edit record for user {email}.");
            }
        }
        
        // the class that the user modifies the record
        private void ModifyStudent(Student stu)
        {
            // gets each type of student and turns to string
            string studentType = stu.GetType().ToString();
            Console.WriteLine(stu);
            Console.WriteLine($"Editing student type: {studentType.Substring(10)}");
            DisplayModifyMenu();
            char selection = GetUserSelection();
            if (studentType == "StudentDB.Undergrad")
            {
                // allows for the student to have
                // the things that a undergrad will have
                Undergrad undergrad = stu as Undergrad;
                // if student is an undergrad
                switch (selection)
                {
                    case 'Y':
                    case 'y': // year rank in school
                        Console.WriteLine("\nENTER new year/rank in school from the following choices.");
                        Console.Write("[1] Freshman, [2] Sophomore, [3] Junior, [4] Senior: ");
                        undergrad.Rank = (YearRank)int.Parse(Console.ReadLine());
                        break;
                    case 'D':
                    case 'd': // degree in school
                        Console.Write("\nENTER new degree major: ");
                        undergrad.DegreeMajor = Console.ReadLine();
                        break;
                }
            }
            else if (studentType == "StudentDB.GradStudent")
            {
                GradStudent grad = stu as GradStudent;
                // if student is an grad
                switch (selection)
                {
                    case 'T':
                    case 't': // Tuition credit
                        Console.Write("\nENTER new tuition reimbursement credit: ");
                        grad.TuitionCredit = decimal.Parse(Console.ReadLine());
                        break;
                    case 'A':
                    case 'a': // faculty advisor in school
                        Console.Write("\nENTER new faculty advisor name: ");
                        grad.FacultyAdvisor = Console.ReadLine();
                        break;
                }
            }
            // choices for all students
            switch (selection)
            {
                case 'F':
                case 'f': 
                    Console.Write("\nENTER new student first name: ");
                    stu.Info.FirstName = Console.ReadLine();
                    break;
                case 'L':
                case 'l':
                    Console.Write("\nENTER new student last name: ");
                    stu.Info.LastName = Console.ReadLine();
                    break;
                case 'G':
                case 'g':
                    Console.Write("\nENTER new student grade pt average: ");
                    stu.GradePtAvg = double.Parse(Console.ReadLine());
                    break;
                case 'E':
                case 'e':
                    Console.Write("\nENTER new student enrollment date: ");
                    stu.EnrollmentDate = DateTime.Parse(Console.ReadLine());
                    break;
            }
            // the end of the code, user presses anything to reset
            Console.WriteLine($"\nEDIT operation done. Current record info:\n{stu}\nPress any key to continue...");
            Console.ReadKey();
        }

        // creating menu for the user to see
        private void DisplayModifyMenu()
        {
            Console.WriteLine(@"
        ************************************
        ***** Edit Student Menu ************
        ************************************
        [F]irst name
        [L]ast name
        [G]rade pt average
        [E]nrollment date
        [Y]ear in school            (undergrad only)
        [D]egree major              (undergrad only)
        [T]uition teaching credit   (graduate only)
        Faculty [A]dvisor           {graduate only}
        ** Email address can never be modified. See admin.
");
            Console.Write("ENTER edit menu selection: ");
        }

        // method used for the user to add something into the student records
        private void AddStudentRecord()
        {
            // first, search the list to see if this email rec already exists
            string email = string.Empty;
            Student stu = FindStudentRecord(out email);
            if(stu == null)
            {
                // record was NOT FOUND - go ahead and add
                // first, gather all the data needed for a new student
                Console.WriteLine($"Adding new student, Email: {email}");

                // start gathering datra
                Console.Write("ENTER first name: ");
                string first = Console.ReadLine();
                Console.Write("Enter last name: ");
                string last = Console.ReadLine();
                Console.Write("ENTER grad pt. average: ");
                double gpa = double.Parse(Console.ReadLine());
                // we have the email, obviously!
                // we have to find out which kind of student, grad or undergrad?
                Console.Write("[U]undergrad or [G]rad student? ");
                string studentType = Console.ReadLine().ToUpper();

                // branch out based on what the type of student is
                if(studentType == "U")
                {
                    // reading in an enumerated type
                    Console.WriteLine("[1]Freshman, [2]Sophomore, [3]Junior, [4]Senior");
                    Console.Write("ENTER year/rank in school from above choices: ");
                    YearRank rank = (YearRank)int.Parse(Console.ReadLine());

                    // ask for the major
                    Console.Write("ENTER major degree program: ");
                    string major = Console.ReadLine();

                    // creating the new student with the information given
                    stu = new Undergrad(first, last, gpa, email, DateTime.Now, rank, major);
                    students.Add(stu); // to add to list
                }
                else if(studentType == "G")
                {
                    // gather additional grad student info
                    Console.Write("ENTER the tuition reimbursement earned (no commas): $");
                    decimal discount = decimal.Parse(Console.ReadLine());
                    Console.Write("ENTER full name of graduate faculty advisor: ");
                    string facAdvisor = Console.ReadLine();

                    // creating the new student with the information given
                    GradStudent grad = new GradStudent(first, last, gpa, 
                        email, DateTime.Now, 
                        discount, facAdvisor);
                    students.Add(grad);
                }
                else
                {
                    // if the user puts a random string
                    Console.WriteLine($"ERROR: No student {email} created.\n" +
                        $"{studentType} is not a valid type.");
                }
            }
            else
            {
                Console.WriteLine($"{email} RECORD FOUND! Can't add student {email}, \n"  +
                   $"Record already exists.");
            }
        }

        // user looks for record then deletes it if found
        private void DeleteStudentRecord()
        {
            // to look for the information we need in the database
            string email = string.Empty;
            Student stu = FindStudentRecord(out email);
            if(stu != null)
            {
                // record was found = go ahead and remove it
                students.Remove(stu);
            }
            else
            {
                // record not in the database
                Console.WriteLine($"***** RECORD NOT FOUND.\nCan't delete record for user:{email}.");
            }
        }

        // takes everything and spits it out for the user to see
        private void PrintAllRecords()
        {
            // runs through the all the students 
            // and prints out all to the user
            foreach (var student in students)
            {
                // runs through the data and prints out all of the student records
                Console.WriteLine(student);
            }
        }

        private char GetUserSelection()
        {
            // it'll take whatever key the user uses and they don't have to press enter
            ConsoleKeyInfo keyPressed = Console.ReadKey();
            return keyPressed.KeyChar;
        }

        private void SaveAllChangesAndQuit()
        {
            WriteDataToOuputFile();
            Environment.Exit(0);
        }

        private void ExitApplicationWithoutSave()
        {
            Environment.Exit(0);
        }

        // displaying to the user what they can do
        private void DisplayMainMenu()
        {
            Console.WriteLine(@"
        ******************************************
        ******* Student Database App *************
        ******************************************
        [A]dd a student record          (C in CRUD) 
        [F]ind a record                 (R in CRUD)
        [P]rint all the records         (Only for testing)
        [D]elete an existing record     (D in CRUD)
        [M]odify an existing record     (U in CRUD)
        [E]xit without saving changes
        [S]ave all changes and quit
");
            Console.Write("ENTER user selection: ");
        }

        // taking data and putting it in the output file everytime it changes
        internal void WriteDataToOuputFile()
        {
            // create the object output file
            StreamWriter outFile = new StreamWriter("OUTPUTFILE.txt");

            Console.WriteLine("Writing data to output file....");

            foreach (var student in students)
            {
                // using the output file
                outFile.WriteLine(student.ToStringForOutputFile());
                // see an echo of what is writing to the file
                Console.WriteLine(student.ToStringForOutputFile());
                //Console.WriteLine(student);
            }

            // closing the output file
            outFile.Close();
            // letting the user know that the file will be closing
            Console.WriteLine("Done writing data - file has been closed");
        }
    }
}