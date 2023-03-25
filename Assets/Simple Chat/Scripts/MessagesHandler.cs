using System;
using ChatLogic.Messages;
using ChatLogic.Messages.Misc;
using FishNet;
using FishNet.Connection;
using UnityEngine;

namespace ChatLogic
{
    public class MessagesHandler : MonoBehaviour
    {
        [SerializeField] private ChatHandler _chat;

        private MessagesPool _pool => _chat ? _chat.Pool : null;

        public event Action<MessageInstance> OnMessageReceived;

        private void OnEnable()
        {
            InstanceFinder.ClientManager.RegisterBroadcast<Message>(Receive);
            InstanceFinder.ServerManager.RegisterBroadcast<Message>(OnMessageReceivedFromClient);
        }

        private void OnDisable()
        {
            if(InstanceFinder.ClientManager)
                InstanceFinder.ClientManager.UnregisterBroadcast<Message>(Receive);

            if(InstanceFinder.ServerManager)
                InstanceFinder.ServerManager.UnregisterBroadcast<Message>(OnMessageReceivedFromClient);
        }

        public void Send(Message msg)
        {
            if (InstanceFinder.IsClient)
                InstanceFinder.ClientManager.Broadcast(msg);
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

        private void OnMessageReceivedFromClient(NetworkConnection connection, Message msg)
        {
            InstanceFinder.ServerManager.Broadcast(msg);
        }
    }
}

