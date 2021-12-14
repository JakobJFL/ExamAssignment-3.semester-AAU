using System.IO;
using Stregsystem;
using Stregsystem.Abstractions;
using StregsystemController.Abstractions;
using UserInterface;
using UserInterface.Abstractions;

namespace StregsystemController
{
    public class Program
    {
        private static readonly string _productsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "InputData", "products.csv");
        private static readonly string _usersFilePath = Path.Combine(Directory.GetCurrentDirectory(), "InputData", "users.csv");
        private static readonly string _logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "log.csv");

        private static void Main(string[] args)
        {
            IStregsystemHandler stregsystemHandler = new StregsystemHandler(_productsFilePath, _usersFilePath, _logFilePath);
            IStregsystemUI ui = new StregsystemCLI(stregsystemHandler);
            IStregsystemCommandParser sc = new StregsystemCommandParser(stregsystemHandler, ui);
            ui.Start();
        }
    }
}
