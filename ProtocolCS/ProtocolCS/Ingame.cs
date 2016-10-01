using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolCS
{
    public class IngamePacket
    {
        public int frameNo { get; set; }
    }

    public class StartGame : IngamePacket
    {
        public Player[] players { get; set; }

        public long seed { get; set; }

    }
    public class RejoinGame : StartGame
    {
        public Frame[] frames { get; set; }
    }
    
    public class Frame : IngamePacket
    {
        public IngameEvent[] events { get; set; }
    }
}
