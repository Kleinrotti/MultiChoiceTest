using Microsoft.VisualBasic;

namespace Client.Helpers
{
    internal class IpInputHelper
    {
        /// <summary>
        /// Display <see cref="Interaction.InputBox()"/> to set the Server IP.
        /// </summary>
        /// <returns></returns>
        public static string IpDialog()
        {
            return Interaction.InputBox("Server Address:", "Set Server Address", "127.0.0.1");
        }
    }
}