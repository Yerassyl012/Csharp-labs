using System;

namespace Tulane
{
    class User
    {
        private string Name;
        private string Location;
        private int Age;
        public void GetUserDetails()
        {
            Console.WriteLine("Name: {0}", Name);
            Console.WriteLine("Location: {0}", Location);
            Console.WriteLine("Age: {0}", Age);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            User u = new User();
            // Compiler Error
            // These are inaccessible due to private specifier
            u.GetUserDetails();
            Console.WriteLine("\nPress Enter Key to Exit..");
            Console.ReadLine();
        }
    }
}