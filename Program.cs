////////////////////////////////////////////////////////////////////////
///Change history
///Date                 Developer           Description
///2021-26-08           Kyle Lam            Created this version of the file, Went all the way to Contact info
///2021-27-08           Kyle Lam            Did the CRUD steps, made finishing comments and changes 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StudentDB
{
    class 
        Program
    {
        static void Main(string[] args)
        {
            // create a single instance of the data base application 
            StudentApp app = new StudentApp();

            // read in the data from the input file
            app.ReadDataFromInputFile();
            // operate the database - carry out the user's CRUD operations
            app.RunDatabaseApp();

        }
           
    }
}
