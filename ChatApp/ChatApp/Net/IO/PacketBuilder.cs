using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Net.IO
{
    class PacketBuilder
    {
        MemoryStream _ms;
        public PacketBuilder()
        {
            _ms = new MemoryStream();
        }

        public void WriteOpCode(byte opcode)
        {
            _ms.WriteByte(opcode);
        }

        public void WriteString(string msg)
        {
            var msgBytes = Encoding.ASCII.GetBytes(msg);
            var msgLength = msgBytes.Length;

            _ms.Write(BitConverter.GetBytes(msgLength), 0, sizeof(int));
            _ms.Write(msgBytes, 0, msgBytes.Length);
        }

        public byte[] GetPacketBytes()
        {
            return _ms.ToArray();
        }
    }
}