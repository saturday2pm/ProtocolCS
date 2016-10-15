using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProtocolCS
{
    public class PacketHelper
    {
        private static Dictionary<Type, object> handlers { get; set; }
        private static Queue<PacketBase> q { get; set; }
        private static object handlersLock;
        private static object qLock;
        
        static PacketHelper()
        {
            handlers = new Dictionary<Type, object>();
            q = new Queue<PacketBase>();

            handlersLock = new object();
            qLock = new object();
        }

        public static void Flush()
        {
            var packetList = new List<PacketBase>();

            lock (qLock)
            {
                while(q.Count > 0)
                    packetList.Add(q.Dequeue());
            }

            lock (handlersLock)
            {
                foreach (var packet in packetList)
                {
                    var packetType = packet.GetType();

                    if (handlers.ContainsKey(packetType) == false)
                        continue;

                    var handler = handlers[packetType];
                    if (handler == null)
                        continue;

                    handler.GetType()
                        .GetMethod("Invoke")
                        .Invoke(handler, new object[] { packet });
                }
            }
        }

        public static void AddHandler<T>(Action<T> handler)
            where T : PacketBase
        {
            lock (handlersLock)
            {
                if (handlers.ContainsKey(typeof(T)) == false)
                    handlers[typeof(T)] = handler;
                else
                {
                    handlers[typeof(T)].GetType()
                        .GetMethod("op_Add")
                        .Invoke(handlers[typeof(T)], new object[] { handler });
                }
            }
        }
        public static void RemoveHandler<T>(Action<T> handler)
            where T : PacketBase
        {
            lock (handlersLock)
            {
                if (handlers.ContainsKey(typeof(T)) == false)
                    handlers[typeof(T)] = handler;
                else
                {
                    handlers[typeof(T)].GetType()
                        .GetMethod("op_Sub")
                        .Invoke(handlers[typeof(T)], new object[] { handler });
                }
            }
        }

        public static void PushPacket(string json)
        {
            var packet = Serializer.ToObject(json);

            PushPacket((PacketBase)packet);
        }
        public static void PushPacket(PacketBase packet)
        {
            lock (qLock)
            {
                q.Enqueue(packet);
            }
        }
    }
}
