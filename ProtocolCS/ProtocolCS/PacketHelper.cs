using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProtocolCS
{
    public class PacketHelper
    {
        private Dictionary<Type, object> handlers { get; set; }
        private Queue<PacketBase> q { get; set; }
        private object handlersLock;
        private object qLock;
        
        public PacketHelper()
        {
            handlers = new Dictionary<Type, object>();
            q = new Queue<PacketBase>();

            handlersLock = new object();
            qLock = new object();
        }

        public void Flush()
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

        public void AddHandler<T>(Action<T> handler)
            where T : PacketBase
        {
            lock (handlersLock)
            {
                if (handlers.ContainsKey(typeof(T)) == false)
                    handlers[typeof(T)] = handler;
                else
                {
                    handlers[typeof(T)] = Delegate.Combine((Delegate)handlers[typeof(T)], handler);
                }
            }
        }
        public void RemoveHandler<T>(Action<T> handler)
            where T : PacketBase
        {
            lock (handlersLock)
            {
                if (handlers.ContainsKey(typeof(T)) == false)
                    Console.WriteLine("no handelr");
                else
                {
                    handlers[typeof(T)] = Delegate.Remove((Delegate)handlers[typeof(T)], handler);
                }
            }
        }

        public void PushPacket(string json)
        {
            var packet = Serializer.ToObject(json);

            PushPacket((PacketBase)packet);
        }
        public void PushPacket(PacketBase packet)
        {
            lock (qLock)
            {
                q.Enqueue(packet);
            }
        }
    }
}
