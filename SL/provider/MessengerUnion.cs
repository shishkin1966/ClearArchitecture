using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ClearArchitecture.SL
{
    public class MessengerUnion : AbsSmallUnion, IMessengerUnion
    {
        public const string NAME = "MessengerUnion";

        private readonly ConcurrentDictionary<int, IMessage> _messages = new ConcurrentDictionary<int, IMessage>();
        private readonly Secretary<List<string>> _messagingList = new Secretary<List<string>>();
        private int _id = 0;

        public MessengerUnion(string name) : base(name)
        {
        }

        private List<string> GetAddresses(string address)  
        {
            List<string> addresses = new List<string>();

            if (string.IsNullOrEmpty(address)) return addresses;

            if (_messagingList.ContainsKey(address)) {
                List<string> list = _messagingList.GetValue(address);
                if (list != null) 
                {
                    foreach (string adr in list) 
                    {
                        addresses.AddRange(GetAddresses(adr));
                    }
                }
            } 
            else
            {
                addresses.Add(address);
            }
            return addresses;
        }

        private void RemoveDublicate(IMessage message)
        {
            if (message == null) return;

            foreach (IMessage tmpMessage in _messages.Values)
            {
                if (message.GetSubj() == tmpMessage.GetSubj() && message.GetAddress() == tmpMessage.GetAddress())
                {
                    RemoveMessage(tmpMessage);
                }
            }
        }

        private void CheckAndReadMessagesSubscriber(string address)
        {
            if (string.IsNullOrEmpty(address)) return;

            IMessengerSubscriber subscriber = (IMessengerSubscriber)GetSubscriber(address);
            if (subscriber != null && address == subscriber.GetName())
            {
                ReadMessages(subscriber);
            }
        }

        private IMessengerSubscriber CheckSubscriber(string address) 
        {
            if (string.IsNullOrEmpty(address)) return default;

            IMessengerSubscriber subscriber = (IMessengerSubscriber)GetSubscriber(address);
            if (subscriber != null && address == subscriber.GetName()) 
            {
                int state = subscriber.GetState();
                if (state == Lifecycle.ON_READY || state == Lifecycle.ON_START) {
                    return subscriber;
                }
            }
            return default;
        }


        public void AddMessage(IMessage message)
        {
            if (message == null) return;

            List<string> list = new List<string>();
            list.AddRange(message.GetCopyTo());
            if (!string.IsNullOrEmpty(message.GetAddress()))
            {
                list.Add(message.GetAddress());
            }
            List<string> addresses = new List<string>();
            foreach (string address in list)
            {
                addresses.AddRange(GetAddresses(address));
            }
            foreach (string address in addresses)
            {
                int id = Interlocked.Increment(ref _id);
                IMessage newMessage = message.Copy();
                newMessage.SetMessageId(id);
                newMessage.SetAddress(address);
                newMessage.SetCopyTo(new List<string>());
    
                if (!message.IsCheckDublicate())
                {
                    _messages[id] = newMessage;
                }
                else
                {
                    RemoveDublicate(newMessage);
                    _messages[id] = newMessage;
                }

                CheckAndReadMessagesSubscriber(address);
            }
        }

        public void AddMessagingList(string name, List<string> addresses)
        {
            if (string.IsNullOrEmpty(name) || addresses == null) return;

            _messagingList.Put(name, addresses);
        }

        public void AddNotMandatoryMessage(IMessage message)
        {
            if (message == null) return;

            List<string> list = new List<string>();
            list.AddRange(message.GetCopyTo());
            if (!string.IsNullOrEmpty(message.GetAddress()))
            {
                list.Add(message.GetAddress());
            }
            List<string> addresses = new List<string>();
            foreach (string address in list)
            {
                addresses.AddRange(GetAddresses(address));
            }
            foreach (string address in addresses)
            {
                IMessengerSubscriber subscriber = CheckSubscriber(address);
                if (subscriber != null)
                {
                    message.Read(subscriber);
                }
            }
        }

        public void ClearMessages()
        {
            _messages.Clear();
        }

        public void ClearMessages(string subscriber)
        {
            if (string.IsNullOrEmpty(subscriber)) return;

            List<IMessage> list = new List<IMessage>();
            list.AddRange(collection: from IMessage message in _messages.Values
                          where message.ContainsAddress(subscriber)
                          select message);
            foreach (IMessage message in list)
            {
                _messages.TryRemove(message.GetMessageId(), out IMessage value);
            }
        }

        public override int CompareTo(IProvider other)
        {
            if (other is IMessengerUnion)
            { return 0; }
            else 
            { return 1; }
        }

        public List<IMessage> GetMessages(IMessengerSubscriber subscriber)
        {
            if (subscriber == null) return new List<IMessage>();
            if (_messages.IsEmpty) return new List<IMessage>();

            // удаляем старые письма
            string name = subscriber.GetName();
            long currentTime = DateTime.Now.Ticks;
            List<IMessage> list = new List<IMessage>();
            foreach (IMessage message in _messages.Values)
            {
                if (message.ContainsAddress(name) && message.GetEndTime() != -1L && message.GetEndTime() < currentTime)
                {
                    list.Add(message);
                }
            }
            foreach (IMessage message in list)
            {
                _messages.TryRemove(message.GetMessageId(), out IMessage value);
            }
            List<IMessage> sortedList =  list.OrderBy(message=>message.GetMessageId()).ToList();
            return sortedList;
        }

        public List<string> GetMessagingList(string name)
        {
            if (string.IsNullOrEmpty(name)) return new List<string>();

            if (_messagingList.ContainsKey(name))
            {
                return _messagingList.GetValue(name);
            }
            else
            {
                return new List<string>();
            }
        }

        public void ReadMessages(IMessengerSubscriber subscriber)
        {
            if (subscriber == null) return;

            List<IMessage> list = GetMessages(subscriber);
            foreach (IMessage message in list)
            {
                int state = subscriber.GetState();
                if (state == Lifecycle.ON_READY)
                {
                    message.Read(subscriber);
                    RemoveMessage(message);
                }
            }
        }

        public void RemoveMessage(IMessage message)
        {
            if (message == null) return;

            _messages.TryRemove(message.GetMessageId(), out IMessage value);
        }

        public void RemoveMessagingList(string name)
        {
            if (string.IsNullOrEmpty(name)) return;

            _messagingList.Remove(name);
        }

        public override void Stop()
        {
            _messages.Clear();
            _messagingList.Clear();
            Console.WriteLine(DateTime.Now.ToString("G") + ": " + "Очистка списка сообщений и списков рассылки");

            base.Stop();
        }
    }
}
