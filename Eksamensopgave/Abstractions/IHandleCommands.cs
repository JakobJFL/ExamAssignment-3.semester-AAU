namespace StregsystemController.Abstractions
{
    public interface IHandleCommands
    {
        void AddToBalance(string username, string amount);
        void HandleAdminCommand(string[] commands);
        void HandleBuyCommand(string[] commands);
        void HandleMultiBuyCommand(string[] commands);
        void HandleShowInfoCommand(string username);
        void SetActivateOnProduct(string productId, bool isActive);
        void SetCreditOnProduct(string productId, bool isCredit);
    }
}