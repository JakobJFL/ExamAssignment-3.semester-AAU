using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Stregsystem.Models
{
    public delegate void UserBalanceNotification(User user, decimal balance);
    public class User : IComparable
    {
        public User(string[] firstnames, string lastname, string username, string email, int id)
        {
            ID = id;
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
        }

        public int ID { get; }
        public string[] Firstnames { get; set; }
        public string Lastname { get; set; }
        public string Username { get; }
        public string Email { get; }
        public decimal Balance { get; set; }

        public override string ToString()
        {
            string firstnames = "";
            foreach (string fName in Firstnames)
                firstnames += fName + " ";

            return "[" + Username + "], " + firstnames + Lastname + ", Saldo: " + Balance + " kr.";
        }
        private static bool IsUsernameValid(string username)
        {
            Regex rgx = new Regex(@"^[0-9&_&a-z]+$"); 
            return rgx.IsMatch(username);
        }

        //Kilde: https://stackoverflow.com/questions/1365407/c-sharp-code-to-validate-email-address
        private static bool IsValidEmail(string email) 
        {
            if (email.Trim().EndsWith(".") || email.Trim().EndsWith("-"))
                return false; 
            try
            {
                var addr = new System.Net.Mail.MailAddress(email); //throws if not valed MailAddress
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
            if (((User)obj).ID != ID)
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
