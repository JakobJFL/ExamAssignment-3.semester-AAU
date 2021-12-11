using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stregsystem.Models;

namespace Stregsystem.UserInterface
{
    public interface IStregsystemUI
    {
        public event StregsystemCLI.StregsystemEvent CommandEntered;
        public void Start();
        public void DisplayUserInfo(User user);
        public void DisplayUserBuysProduct(BuyTransaction transaction);
        public void DisplayUserBuysProduct(int count, BuyTransaction transaction);
        public void Close();
        public void DisplayInsufficientCash(User user, Product product);
        public void DisplayUserNotFound(string username);
        public void DisplayProductNotFound(string product);
        public void DisplayAdminCommandNotFoundMessage(string adminCommand);
        public void DisplayTooManyArgumentsError(string command);
        public void DisplayGeneralError(string errorString);
        public void DisplayUserBalanceWarning(User user, decimal balance);
        public void ListenForConsoleInput();

    }
}
