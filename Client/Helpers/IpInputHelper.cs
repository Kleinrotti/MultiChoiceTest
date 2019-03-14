using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Helpers
{
    class IpInputHelper
    {
        public static string IpDialog()
        {
            return Interaction.InputBox("IP adress", "IP", "127.0.0.1");
        }
    }
}
