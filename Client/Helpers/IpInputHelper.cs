using Microsoft.VisualBasic;

namespace Client.Helpers
{
    internal class IpInputHelper
    {
        public static string IpDialog()
        {
            return Interaction.InputBox("IP adress", "IP", "127.0.0.1");
        }
    }
}