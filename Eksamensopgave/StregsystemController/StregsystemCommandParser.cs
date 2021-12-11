using System;
using System.Collections.Generic;
using System.Linq;
using Stregsystem;
using Stregsystem.Models;
using Stregsystem.UserInterface;

namespace Stregsystem
{
    public class StregsystemCommandParser : IStregsystemCommandParser
    {

        private Dictionary<string, Action<string[]>> _adminCommands = new Dictionary<string, Action<string[]>>();
        public IStregsystem Stregsystem { get; }
        public IStregsystemUI StregsystemUI { get; }

        public StregsystemCommandParser(IStregsystem stregsystem, IStregsystemUI stregsystemUI)
        {
            Stregsystem = stregsystem;
            StregsystemUI = stregsystemUI;
            StregsystemUI.CommandEntered += ParseCommand;
            InitializeAdminCommands();
        }

        private void InitializeAdminCommands()
        {
            _adminCommands.Add(":q", a => StregsystemUI.Close());
            _adminCommands.Add(":quit", a => StregsystemUI.Close());
            _adminCommands.Add(":activate", a => SetActivateOnProduct(a[1], true));
            _adminCommands.Add(":deactivate", a => SetActivateOnProduct(a[1], false));
            _adminCommands.Add(":crediton", a => SetCreditOnProduct(a[1], true));
            _adminCommands.Add(":creditoff", a => SetCreditOnProduct(a[1], false));
            _adminCommands.Add(":addcredits", a => AddToBalance(a[1], a[2])); // SKAL være decimal ikke int
        }

        public void ParseCommand(string command)
        {
            string[] commands = command.Split(" ");
            try
            {
                if (command.StartsWith(":"))
                    HandleAdminCommand(commands);
                else if (commands.Length == 1)
                    HandleShowInfoCommand(commands[0]);
                else if (commands.Length == 2)
                    HandleBuyCommand(commands);
                else if (commands.Length == 3)
                    HandleMultiBuyCommand(commands);
                else
                    throw new TooManyArgumentsError();
            }
            catch (TooManyArgumentsError)
            {
                StregsystemUI.DisplayTooManyArgumentsError(command);
            }
            catch (UserNotFoundException)
            {
                StregsystemUI.DisplayUserNotFound(commands[0]);
            }
            catch (ProductNotFoundException)
            {
                StregsystemUI.DisplayProductNotFound(commands[1]);
            }
            catch (AdminCommandNotFound)
            {
                StregsystemUI.DisplayAdminCommandNotFoundMessage(command);
            }
            catch (IndexOutOfRangeException) // Ikke mega godt
            {
                StregsystemUI.DisplayTooManyArgumentsError(command);
            }
            finally
            {
                StregsystemUI.ListenForConsoleInput();
            }
        }

        private void HandleShowInfoCommand(string username)
        {
            User user = Stregsystem.GetUserByUsername(username);
            StregsystemUI.DisplayUserInfo(user);
        }

        private void HandleMultiBuyCommand(string[] commands)
        {
            string username = commands[0];
            User user = Stregsystem.GetUserByUsername(username);
            if (!int.TryParse(commands[1], out int productCount))
                throw new ProductNotFoundException("Could not parse to int");
            if (!int.TryParse(commands[2], out int productId))
                throw new ProductNotFoundException("Could not parse to int");
            Product product = Stregsystem.GetProductByID(productId);
            try
            {
                if (user.Balance < product.Price * productCount)
                    throw new InsufficientCreditsException();

                BuyTransaction transaction = null;
                for (int i = 0; i < productCount; i++)
                {
                    transaction = Stregsystem.BuyProduct(user, product);
                }
                StregsystemUI.DisplayUserBuysProduct(productCount, transaction);
            }
            catch (InsufficientCreditsException)
            {
                StregsystemUI.DisplayInsufficientCash(user, product);
            }
        }

        private void HandleBuyCommand(string[] commands)
        {
            string username = commands[0];
            User user = Stregsystem.GetUserByUsername(username);
            if (!int.TryParse(commands[1], out int productId))
                throw new ProductNotFoundException("Could not parse to int");
            Product product = Stregsystem.GetProductByID(productId);
            try
            {
                BuyTransaction transaction = Stregsystem.BuyProduct(user, product);
                StregsystemUI.DisplayUserBuysProduct(transaction);
            }
            catch (InsufficientCreditsException)
            {
                StregsystemUI.DisplayInsufficientCash(user, product);
            }
        }

        private void HandleAdminCommand(string[] commands)
        {
            if (_adminCommands.ContainsKey(commands[0]))
            {
                _adminCommands[commands[0]](commands);
            }
            else
                throw new AdminCommandNotFound();
        }

        private void SetActivateOnProduct(string productId, bool isActive)
        {
            if (!int.TryParse(productId, out int commandId))
                throw new TooManyArgumentsError("Could not parse to int");
            Stregsystem.GetProductByID(commandId).Active = isActive;
        }

        private void SetCreditOnProduct(string productId, bool isCredit)
        {
            if (!int.TryParse(productId, out int commandId))
                throw new TooManyArgumentsError("Could not parse to int");
            Stregsystem.GetProductByID(commandId).CanBeBoughtOnCredit = isCredit;
        }

        private void AddToBalance(string username, string amount)
        {
            if (!decimal.TryParse(amount, out decimal commandAmount))
                throw new TooManyArgumentsError("Could not parse to decimal");
            Stregsystem.GetUserByUsername(username).Balance += commandAmount;
        }
    }
}
