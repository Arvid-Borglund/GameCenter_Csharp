using System;
using System.Data.SqlClient;
using System.Configuration;

namespace AIFutureDBConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AIFutureDBConnectionString"].ConnectionString;

            while (true)
            {
                Console.WriteLine("Choose an action (C)reate, (R)ead, (U)pdate, (D)elete, (E)xit:");
                string action = Console.ReadLine();

                if (action.Equals("E", StringComparison.OrdinalIgnoreCase) || action.Equals("Exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    switch (action.ToUpper())
                    {
                        case "C":
                            Console.Write("Enter first name: ");
                            string firstName = Console.ReadLine();
                            Console.Write("Enter last name: ");
                            string lastName = Console.ReadLine();
                            Console.Write("Enter birth date (yyyy-mm-dd): ");
                            string birthDate = Console.ReadLine();
                            Console.Write("Enter email: ");
                            string email = Console.ReadLine();

                            string insertSql = "INSERT INTO Person (FirstName, LastName, BirthDate, Email) VALUES (@FirstName, @LastName, @BirthDate, @Email)";
                            SqlCommand insertCommand = new SqlCommand(insertSql, connection);
                            insertCommand.Parameters.AddWithValue("@FirstName", firstName);
                            insertCommand.Parameters.AddWithValue("@LastName", lastName);
                            insertCommand.Parameters.AddWithValue("@BirthDate", birthDate);
                            insertCommand.Parameters.AddWithValue("@Email", email);
                            insertCommand.ExecuteNonQuery();

                            Console.WriteLine("Person added successfully.");
                            break;

                        case "R":
                            SqlCommand readCommand = new SqlCommand("SELECT * FROM Person", connection);
                            SqlDataReader reader = readCommand.ExecuteReader();

                            Console.WriteLine("List of persons in the database:");
                            while (reader.Read())
                            {
                                Console.WriteLine($"ID: {reader["PersonID"]}, Name: {reader["FirstName"]} {reader["LastName"]}, BirthDate: {reader["BirthDate"]}, Email: {reader["Email"]}");
                            }

                            reader.Close();
                            break;

                        case "U":
                            Console.Write("Enter person ID to update: ");
                            string updateId = Console.ReadLine();
                            Console.Write("Enter new first name: ");
                            string updateFirstName = Console.ReadLine();
                            Console.Write("Enter new last name: ");
                            string updateLastName = Console.ReadLine();
                            Console.Write("Enter new birth date (yyyy-mm-dd): ");
                            string updateBirthDate = Console.ReadLine();
                            Console.Write("Enter new email: ");
                            string updateEmail = Console.ReadLine();

                            string updateSql = "UPDATE Person SET FirstName = @FirstName, LastName = @LastName, BirthDate = @BirthDate, Email = @Email WHERE PersonID = @PersonID";
                            SqlCommand updateCommand = new SqlCommand(updateSql, connection);
                            updateCommand.Parameters.AddWithValue("@PersonID", updateId);
                            updateCommand.Parameters.AddWithValue("@FirstName", updateFirstName);
                            updateCommand.Parameters.AddWithValue("@LastName", updateLastName);
                            updateCommand.Parameters.AddWithValue("@BirthDate", updateBirthDate);
                            updateCommand.Parameters.AddWithValue("@Email", updateEmail);
                            updateCommand.ExecuteNonQuery();

                            Console.WriteLine("Person updated successfully.");
                            break;

                        case "D":
                            Console.Write("Enter person ID to delete: ");
                            string deleteId = Console.ReadLine();

                            string deleteSql = "DELETE FROM Person WHERE PersonID = @PersonID";
                            SqlCommand deleteCommand = new SqlCommand(deleteSql, connection);
                            deleteCommand.Parameters.AddWithValue("@PersonID", deleteId);
                            deleteCommand.ExecuteNonQuery();

                            Console.WriteLine("Person deleted successfully.");
                            break;

                        default:
                            Console.WriteLine("Invalid action. Please choose a valid action: (C)reate, (R)ead, (U)pdate, (D)elete, (E)xit");
                            break;
                    }
                }
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
