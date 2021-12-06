using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stregsystem.UserInterface;

namespace Stregsystem
{
    public class StregsystemController : IStregsystemController
    {
        public StregsystemController(IStregsystem stregsystem, IStregsystemUI stregsystemUI)
        {
            Stregsystem = stregsystem;
            StregsystemUI = stregsystemUI;
            new StregsystemCommandParser(stregsystem, stregsystemUI);
        }

        public IStregsystem Stregsystem { get; }
        public IStregsystemUI StregsystemUI { get; }
    }
}
