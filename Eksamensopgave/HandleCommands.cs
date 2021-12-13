using System;
using System.Collections.Generic;
using Stregsystem;
using Stregsystem.Abstractions;
using Stregsystem.Exceptions;
using Stregsystem.Models;
using Stregsystem.UserInterface;

namespace StregsystemController
{
    public class HandleCommands
    {
        private Dictionary<string, Action<string[]>> _adminCommands = new Dictionary<string, Action<string[]>>();

        public IStregsystemHandler Stregsystem { get; }
        public IStregsystemUI StregsystemUI { get; }
        public HandleCommands(IStregsystemHandler stregsystem, IStregsystemUI stregsystemUI, Dictionary<string, Action<string[]>> adminCommands)
        {
            Stregsystem = stregsystem;
            StregsystemUI = stregsystemUI;
            _adminCommands = adminCommands;
        }

        public void HandleShowInfoCommand(string username)
        {
            User user = Stregsystem.GetUserByUsername(username);
            StregsystemUI.DisplayUserInfo(user);
        }

        public void HandleMultiBuyCommand(string[] commands)
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

        public void HandleBuyCommand(string[] commands)
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

        public void HandleAdminCommand(string[] commands)
        {
            if (_adminCommands.ContainsKey(commands[0]))
            {
                _adminCommands[commands[0]](commands);
            }
            else
                throw new AdminCommandNotFound();
        }

        public void SetActivateOnProduct(string productId, bool isActive)
        {
            if (!int.TryParse(productId, out int commandId))
                throw new TooManyArgumentsError("Could not parse to int");
            Stregsystem.GetProductByID(commandId).Active = isActive;
        }

        public void SetCreditOnProduct(string productId, bool isCredit)
        {
            if (!int.TryParse(productId, out int commandId))
                throw new TooManyArgumentsError("Could not parse to int");
            Stregsystem.GetProductByID(commandId).CanBeBoughtOnCredit = isCredit;
        }

        public void AddToBalance(string username, string amount)
        {
            if (!decimal.TryParse(amount, out decimal commandAmount))
                throw new TooManyArgumentsError("Could not parse to decimal");
            Stregsystem.GetUserByUsername(username).Balance += commandAmount;
        }
    }
}
