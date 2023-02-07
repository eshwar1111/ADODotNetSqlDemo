using System;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleSqlApp
{

    public static class Operations
    {
        public static string cs = "data source=.; database=Demo; integrated security=SSPI";
        public static void GetEmployees()
        {
            try
            {
                using(SqlConnection cn=new SqlConnection(cs))
                {
                    cn.Open();
                    SqlDataAdapter dataAdapter= new SqlDataAdapter("Select * from employees", cn);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);
                    Console.WriteLine("empID name  age department");
                    foreach (DataRow dr in dt.Rows)
                    {
                        Console.WriteLine("  " + dr[0] + "   " + dr[1] + " " + dr[2] + "   " + dr[3]);
                    }
                }
            }    
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        public static void AddEmployee()
        {
            try
            {
                using(SqlConnection cn=new SqlConnection(cs))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("Insert into employees(empName,age,department) values(@name,@age,@department)", cn);
                    Console.WriteLine("enter name:");
                    cmd.Parameters.AddWithValue("@name",Console.ReadLine());
                    Console.WriteLine("enter age:");
                    cmd.Parameters.AddWithValue("@age",Convert.ToInt32(Console.ReadLine()));
                    Console.WriteLine("enter department:");
                    cmd.Parameters.AddWithValue("@department", Console.ReadLine());
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("added successfully");
                    GetEmployees();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        public static void DeleteEmployee()
        {
            try
            {
                using(SqlConnection cn=new SqlConnection(cs))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("delete from employees where empId=@curId",cn);
                    Console.WriteLine("enter the empId to delete:");
                    
                    cmd.Parameters.AddWithValue("@curId",Convert.ToInt32(Console.ReadLine()));
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("deleted successfully");
                    GetEmployees();
                }
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }


        }
        public static void EditEmployee()
        {
            try
            {
                using(SqlConnection cn=new SqlConnection(cs))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("Update employees set empName=@newName where empId=@curId",cn);
                    Console.WriteLine("enter empId to edit:");
                    cmd.Parameters.AddWithValue("@curId",Convert.ToInt32(Console.ReadLine()));
                    Console.WriteLine("enter edited name:");
                    cmd.Parameters.AddWithValue("@newName", Console.ReadLine());
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("successfully edited");
                    GetEmployees();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }



    public class Program
    {
        static void Main(string[] args)
        {
            bool terminate = true;
            do
            {
                Console.WriteLine();
                Console.WriteLine("1.Add Employee");
                Console.WriteLine("2.Edit Employee name");
                Console.WriteLine("3.Show Employees");
                Console.WriteLine("4.Delete Employee");
                Console.WriteLine();
                int op = Convert.ToInt32(Console.ReadLine());
                switch (op)
                {
                    case 1:
                        Operations.AddEmployee();
                        break;
                    case 2:
                        Operations.EditEmployee();
                        break;
                    case 3:
                        Operations.GetEmployees();
                        break;
                    case 4:
                        Operations.DeleteEmployee();
                        break;
                    case 5:
                        terminate=false;
                        break;
                }
            }
            while(terminate);


        }
    }
}