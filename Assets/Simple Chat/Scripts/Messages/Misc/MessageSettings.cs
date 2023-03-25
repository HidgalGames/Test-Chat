using UnityEngine;

namespace ChatLogic.Messages.Misc
{
    [CreateAssetMenu(menuName = "Chat/Create Message Settings", fileName = "Message Settings")]
    public class MessageSettings : ScriptableObject
    {
        [Min(0)] public float MessageLifeTimeInSec = 300f;
        public bool ShowTimeStamp = true;
    }
}

