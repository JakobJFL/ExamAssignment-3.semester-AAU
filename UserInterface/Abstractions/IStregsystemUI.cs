using Stregsystem.Abstractions;
using Stregsystem.Models;

namespace UserInterface.Abstractions
{
    public interface IStregsystemUI
    {
        event StregsystemCLI.StregsystemEvent CommandEntered;
        void Close();
        void DisplayAdminCommandNotFoundMessage(string adminCommand);
        void DisplayCommandNotFoundError(string command);
        void DisplayFormatError(string command);
        void DisplayGeneralError(string errorString);
        void DisplayInsufficientCash(User user, Product product);
        void DisplayProductNotFound(string product);
        void DisplayTooManyArgumentsError(string command);
        void DisplayUserBalanceWarning(User user, decimal balance);
        void DisplayUserBuysProduct(BuyTransaction transaction);
        void DisplayUserBuysProduct(int count, BuyTransaction transaction);
        void DisplayUserInfo(User user);
        void DisplayUserNotFound(string username);
        void ListenForConsoleInput();
        void Start();
    }
}
