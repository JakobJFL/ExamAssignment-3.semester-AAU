using System;
using System.Collections.Generic;
using Stregsystem.Abstractions;
using Stregsystem.Exceptions;
using StregsystemController.Abstractions;
using UserInterface.Abstractions;

namespace StregsystemController
{
    public class StregsystemCommandParser : IStregsystemCommandParser
    {
        private readonly Dictionary<string, Action<string[]>> _adminCommands = new Dictionary<string, Action<string[]>>();
        public IStregsystemHandler Stregsystem { get; }
        public IStregsystemUI StregsystemUI { get; }
        private HandleCommands Handle { get; }

        public StregsystemCommandParser(IStregsystemHandler stregsystem, IStregsystemUI stregsystemUI)
        {
            Stregsystem = stregsystem;
            StregsystemUI = stregsystemUI;
            StregsystemUI.CommandEntered += ParseCommand;
            Handle = new HandleCommands(stregsystem, stregsystemUI, _adminCommands);
            InitializeAdminCommands();
        }

        private void InitializeAdminCommands()
        {
            _adminCommands.Add(":q", a => StregsystemUI.Close());
            _adminCommands.Add(":quit", a => StregsystemUI.Close());
            _adminCommands.Add(":activate", a => Handle.SetActivateOnProduct(a[1], true));
            _adminCommands.Add(":deactivate", a => Handle.SetActivateOnProduct(a[1], false));
            _adminCommands.Add(":crediton", a => Handle.SetCreditOnProduct(a[1], true));
            _adminCommands.Add(":creditoff", a => Handle.SetCreditOnProduct(a[1], false));
            _adminCommands.Add(":addcredits", a => Handle.AddToBalance(a[1], a[2]));
        }

        public void ParseCommand(string command)
        {
            string[] commands = command.Split(" ");
            try
            {
                if (command.StartsWith(":"))
                    Handle.HandleAdminCommand(commands);
                else if (commands[0] == "list")
                    StregsystemUI.DisplayProductList();
                else if (commands.Length == 1)
                    Handle.HandleShowInfoCommand(commands[0]);
                else if (commands.Length == 2)
                    Handle.HandleBuyCommand(commands);
                else if (commands.Length == 3)
                    Handle.HandleMultiBuyCommand(commands);
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
                if (commands.Length == 2)
                    StregsystemUI.DisplayProductNotFound(commands[1]);
                else if (commands.Length == 3)
                    StregsystemUI.DisplayProductNotFound(commands[2]);
            }
            catch (CommandNotFound)
            {
                StregsystemUI.DisplayCommandNotFoundError(command);
            }
            catch (FormatException)
            {
                StregsystemUI.DisplayFormatError(command);
            }
            catch (IndexOutOfRangeException) 
            {
                StregsystemUI.DisplayTooManyArgumentsError(command);
            }
            catch (InvalidOperationException)
            {
                StregsystemUI.DisplayProductNotFound(commands[1]);
            }
            finally
            {
                StregsystemUI.ListenForConsoleInput();
            }
        }
    }
}
