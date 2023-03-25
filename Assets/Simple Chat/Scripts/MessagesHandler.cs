using System;
using ChatLogic.Messages;
using ChatLogic.Messages.Misc;
using UnityEngine;

namespace ChatLogic
{
    public class MessagesHandler : MonoBehaviour
    {
        [SerializeField] private ChatHandler _chat;

        private MessagesPool _pool => _chat ? _chat.Pool : null;

        public event Action<MessageInstance> OnMessageReceived;

        public void Send(Message msg)
        {
            Receive(msg);

            //TODO: send message logic
        }

        public void Receive(string msg)
        {
            var message = _pool.CreateMessage(msg);
            OnMessageReceived?.Invoke(message);
        }
        public void Receive(Message msg)
        {
            var message = _pool.CreateMessage(msg);
            OnMessageReceived?.Invoke(message);
        }
    }
}

