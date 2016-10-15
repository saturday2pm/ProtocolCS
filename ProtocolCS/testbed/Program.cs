using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProtocolCS;
using ProtocolCS.Constants;

namespace testbed
{
    class Program
    {
        static void Main(string[] args)
        {
            var json = Serializer.ToJson(new StartGame());

            Action<MatchSuccess> a = (packet) =>
            {
                Console.WriteLine("A");
            };
            PacketHelper.AddHandler<MatchSuccess>(a);
            PacketHelper.AddHandler<MatchSuccess>((packet) =>
            {
                Console.WriteLine("B");
            });
            PacketHelper.AddHandler<MatchSuccess>((packet) =>
            {
                Console.WriteLine("C");
            });

            PacketHelper.RemoveHandler<MatchSuccess>(a);

            PacketHelper.PushPacket(new MatchSuccess());
            PacketHelper.Flush();

            Console.WriteLine(json);
        }
    }
}
