using System;
using System.Collections.Generic;

namespace Eksamensopgave
{
    class Program
    {
        private static readonly string _usersFilePath = @"C:\Users\JFL\Documents\GitHub\ExamAssignment-3.semester-AAU\Eksamensopgave\InputData\users.csv";
        private static readonly string _productsFilePath = @"C:\Users\JFL\Documents\GitHub\ExamAssignment-3.semester-AAU\Eksamensopgave\InputData\products.csv";
        static void Main(string[] args)
        {
            IFileManager fileManager = new FileManager();
            IStregsystem stregsystem = new Stregsystem(fileManager.LoadProducts(_productsFilePath));
            for (int i = 1; i < 135; i++)
            {
                Console.WriteLine(stregsystem.GetProductByID(i));
            }

        }
    }
}
