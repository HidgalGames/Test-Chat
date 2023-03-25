using ChatLogic.Messages.Misc;
using FishNet;
using UnityEngine;
using UnityEngine.UI;

namespace ChatLogic
{
    public class ChatHandler : MonoBehaviour
    {
        public static readonly string DEFAULT_USERNAME = "UserName";

        public string UserName = DEFAULT_USERNAME;

        [field: Space]
        [field: SerializeField] public MessagesPool Pool { get; private set; }
        [field: SerializeField] public MessagesHandler MessagesHandler { get; private set; }
        [field: SerializeField] public ChatInput Input { get; private set; }
        [field: SerializeField] public ScrollRect ScrollView { get; private set; }
    }
}

