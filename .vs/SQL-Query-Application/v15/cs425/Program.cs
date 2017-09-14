/* Author: Petros Besieris
 * Creation Date: 9/14/2017
 * Modification Date: 9/15/2017
 * Purpose: To allow user to enter in correct SQL commands and display queried results onto screen
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;
using System.Data.SqlClient;

namespace cs425
{
    class Program
    {
        static void Main(string[] args)
        {
            bool choice = true;

            DataTable table = new DataTable("Cars");

            foreach(DataRow data in table.Rows)
            {
                foreach(var item in data.ItemArray)
                {
                    Console.WriteLine(item);
                }
            }
           

            while (choice == true)
            {
                Console.WriteLine("Enter in desired SQL Command:");
                var input = "";

                input = Console.ReadLine(); //read user's input (assuming SQL syntax is correct)

                string con_string = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Petros\Documents\visual studio 2017\Projects\cs425\cs425\Database1.mdf; Integrated Security = True;";


                exec_SQL(con_string, input); // execute user SQL command   

                Console.WriteLine("Would you like to execute another SQL command?");
               input = Console.ReadLine();
                if (input == "yes" || input == "YES" || input == "Yes")
                {
                    choice = true;
                }
                else if (input == "NO" || input == "no" || input == "No")
                {
                    choice = false;
                }

            }

            
        }

        static void exec_SQL(string connect_string, string sql_cmd)
        {
            SqlConnection connect = new SqlConnection(connect_string); // establish new connection with database 


            SqlCommand comm = new SqlCommand(sql_cmd, connect); // initialize new SQL command with respect to the connected database

            connect.Open(); // open connection of database 
            SqlDataReader reader = comm.ExecuteReader(); //execute the SQL command

            print_results(reader); // print results 


            reader.Close(); // close connection once finished for security reasons
        }


        static void print_results(SqlDataReader r)
        {
            while (r.Read())
            {
                int counter = r.FieldCount; // set counter to the queried results 
                for (int i = 0; i < counter; i++)
                {
                   // Console.WriteLine("|");
                    Console.WriteLine(r.GetValue(i)); // print out queried results 
                    Console.WriteLine("-----------------------------------------------");
                }
            }

        }

        int tableWidth = 77;

        void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }
}


