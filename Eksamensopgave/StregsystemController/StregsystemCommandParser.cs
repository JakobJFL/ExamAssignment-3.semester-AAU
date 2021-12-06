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

        private Dictionary<string, Action<string, int>> _adminCommands = new Dictionary<string, Action<string, int>>();
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
            _adminCommands.Add(":q", (a, b) => StregsystemUI.Close());
            _adminCommands.Add(":quit", (a, b) => StregsystemUI.Close());
            _adminCommands.Add(":activate", (a, id) => SetActivateOnProduct(id, true));
            _adminCommands.Add(":deactivate", (a, id) => SetActivateOnProduct(id, false));
            _adminCommands.Add(":crediton", (a, id) => SetCreditOnProduct(id, true));
            _adminCommands.Add(":creditoff", (a, id) => SetCreditOnProduct(id, false));
            _adminCommands.Add(":addcredits", (name, amount) => AddToBalance(name, amount)); // SKAL være decimal ikke int
        }

        public void ParseCommand(string command)
        {
            if (command.StartsWith(":"))
            {
                AdminCommand(command);
                return;
            }
            string[] commands = command.Split(" ");
            User user = null;
            Product product = null;
            try
            {
                if (!(commands.Length == 2))
                    throw new TooManyArgumentsError();
                string userName = commands[0];

                user = Stregsystem.GetUserByUsername(userName);
                if (!int.TryParse(commands[1], out int productId))
                    throw new ProductNotFoundException("Could not parse to int");
                product = Stregsystem.GetProductByID(productId);

                BuyTransaction transaction = Stregsystem.BuyProduct(user, product);

                StregsystemUI.DisplayUserBuysProduct(transaction);
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
            catch (InsufficientCreditsException)
            {
                StregsystemUI.DisplayInsufficientCash(user, product);
            }
            finally
            {
                StregsystemUI.ListenForConsoleInput();
            }
        }

        private void AdminCommand(string command)
        {
            string[] commands = command.Split(" ");
            int productId = 0;
            string username = null;
            try
            {
                if (commands.Length == 2) // :crediton, :creditoff, :activate, :deactivate
                {
                    if (!int.TryParse(commands[1], out int commandId))
                        throw new TooManyArgumentsError("Could not parse to int");
                    productId = commandId;
                }
                else if (commands.Length == 3) // addcredits
                {
                    username = commands[1];
                    if (!int.TryParse(commands[2], out int commandId))
                        throw new TooManyArgumentsError("Could not parse to int");
                    productId = commandId;
                }

                if (_adminCommands.ContainsKey(commands[0]))
                {
                    _adminCommands[commands[0]](username, productId);
                    StregsystemUI.Start();
                }
                else
                    throw new AdminCommandNotFound();
            }
            catch (AdminCommandNotFound)
            {
                StregsystemUI.DisplayAdminCommandNotFoundMessage(command);
            }
            catch (TooManyArgumentsError)
            {
                StregsystemUI.DisplayTooManyArgumentsError(command);
            }
            catch (UserNotFoundException)
            {
                if (commands.Length == 3)
                    StregsystemUI.DisplayUserNotFound(commands[2]);
                else
                    StregsystemUI.DisplayUserNotFound("");
            }
            catch (ProductNotFoundException)
            {
                if (commands.Length == 2)
                    StregsystemUI.DisplayProductNotFound(commands[1]);
                else
                    StregsystemUI.DisplayProductNotFound("");
            }
            finally
            {
                StregsystemUI.ListenForConsoleInput();
            }
        }

        private void SetActivateOnProduct(int productId, bool isActive)
        {
            Stregsystem.GetProductByID(productId).Active = isActive;
        }

        private void SetCreditOnProduct(int productId, bool isCredit)
        {
            Stregsystem.GetProductByID(productId).CanBeBoughtOnCredit = isCredit;
        }

        private void AddToBalance(string username, decimal amount)
        {
            Stregsystem.GetUserByUsername(username).Balance += amount;
        }
    }
}
