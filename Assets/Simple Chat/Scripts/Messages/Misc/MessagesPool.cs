using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ChatLogic.Messages.Misc
{
    public class MessagesPool : MonoBehaviour
    {
        [SerializeField] private ChatHandler _chat;
        [SerializeField] private RectTransform _messagesParent;
        [SerializeField] private MessageInstance _instancePrefab;

        private Queue<MessageInstance> _pool;

        private void Awake()
        {
            if (!_messagesParent)
            {
                _messagesParent = transform as RectTransform;
            }

            _pool = new();
        }

        #region Creating Instance
        public MessageInstance CreateMessage()
        {
            if (!_messagesParent) return null;

            if (_pool.Count > 0)
            {
                var msg = _pool.Dequeue();
                if (msg)
                {
                    msg.gameObject.SetActive(true);
                    msg.transform.SetParent(_messagesParent);
                    return msg;
                }
            }

            if (_instancePrefab)
            {
                var instance = Instantiate(_instancePrefab, _messagesParent);
                instance.Pool = this;
                instance.Chat = _chat;
                return instance; 
            }

            return null;
        }
        public MessageInstance CreateMessage(Message msg)
        {
            var instance = CreateMessage();
            if (instance)
            {
                instance.Setup(msg);
                RebuildLayout();

                return instance;
            }

            return null;
        }
        public MessageInstance CreateMessage(string msg)
        {
            var instance = CreateMessage();
            if (instance)
            {
                instance.Setup(Message.FromString(msg));
                RebuildLayout();

                return instance;
            }

            return null;
        }
        #endregion

        #region Returning To Pool
        public void ReturnToPool(MessageInstance msg)
        {
            if (!msg) return;

            RebuildLayout();

            msg.gameObject.SetActive(false);
            msg.transform.SetParent(transform);

            _pool.Enqueue(msg);
        }
        #endregion

        #region Misc
        private void RebuildLayout()
        {
            if (_messagesParent)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(_messagesParent);
            }
        }
        #endregion
    }
}

