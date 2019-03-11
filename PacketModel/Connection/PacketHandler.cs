namespace PacketModel.Connection
{
    /// <summary>
    /// Packet handling for received object
    /// </summary>
    public class PacketHandler
    {
        private object _exam;

        /// <summary>
        /// Decide how packets will be processed
        /// </summary>
        public void ProcessPacket(object exam)
        {
            _exam = exam;
        }

        private bool IsEmpty()
        {
            return false;
        }
    }
}