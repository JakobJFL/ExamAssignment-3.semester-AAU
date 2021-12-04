using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Eksamensopgave.Models
{

    public class User : IComparable
    {
        public delegate void UserBalanceNotification(User user, decimal balance);
        public event UserBalanceNotification UserBalanceNotificationEvent;
        //public event Action UserBalanceNotification;

        private static int _id = 1;

        public User(string[] firstnames, string lastname, string username, string email)
        {
            Firstnames = firstnames;
            Lastname = lastname;
            if (IsUsernameValid(username))
                Username = username;
            else
                throw new ValidationException("Does not live up to requirements");
            if (IsValidEmail(email))
                Email = email;
            else
                throw new ValidationException("Does not live up to requirements");
            UserBalanceNotificationEvent += NotifyUser;
            _id++;
        }

        public int ID { get; } = _id;
        public string[] Firstnames { get; set; }
        public string Lastname { get; set; }
        public string Username { get; private set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }

        public override string ToString()
        {
            string firstnames = "";
            foreach (string fName in Firstnames)
                firstnames += fName + " ";

            return firstnames + Lastname + " (" + Email + ")";
        }

        private void NotifyUser(User user, decimal balance)
        {

        }

        private bool IsUsernameValid(string username)
        {
            Regex rgx = new Regex(@"^[0-9&_&a-z]+$");
            return rgx.IsMatch(username);
        }

        //Kilde: https://stackoverflow.com/questions/1365407/c-sharp-code-to-validate-email-address
        private bool IsValidEmail(string email) 
        {
            if (email.Trim().EndsWith(".") || email.Trim().EndsWith("-"))
                return false; 
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public int CompareTo(object obj)
        {
            return ID.CompareTo(obj);
        }

        public override bool Equals(object obj)
        {
            if (((User)obj).ID != ID) //(obj == null) || !this.GetType().Equals(obj.GetType()) || this.Equals(obj)
            {
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }
}
