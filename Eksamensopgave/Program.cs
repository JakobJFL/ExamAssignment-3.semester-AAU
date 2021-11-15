using System;
using System.Collections.Generic;

namespace Eksamensopgave
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] str = { "kat" };
            User user = new User(str, "kat", "dsf_", "eksempel2@-mit_domain.dk");
            Console.WriteLine(user.ToString());

        }
    }
}
