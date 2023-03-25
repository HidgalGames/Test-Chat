using System;
using ChatLogic.Messages.Misc;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ChatLogic.Messages
{
    public class MessageInstance : MonoBehaviour
    {
        [SerializeField] private MessageSettings _settingsAsset;

        [Space]
        [SerializeField] private TMP_Text _timeText;
        [SerializeField] private UsernameInstance _nameInstance;
        [SerializeField] private HighlightedText _messageText;

        private RectTransform _rect;
        private string _userName => Chat ? Chat.UserName : ChatHandler.DEFAULT_USERNAME;

        public Message Message { get; private set; }
        
        public ChatHandler Chat { get; set; }
        public MessagesPool Pool { get; set; }

        public event Action<MessageInstance, PointerEventData.InputButton> OnClicked;
        public event Action<MessageInstance> OnBeforeDestroy;

        private void OnEnable()
        {
            if (!_rect)
            {
                _rect = transform as RectTransform;
            }

            if (_nameInstance)
            {
                _nameInstance.OnClick += OnClick;
            }

            if (_settingsAsset)
            {
                Invoke(nameof(ReturnToPool), _settingsAsset.MessageLifeTimeInSec);
            }
            else
            {
                Debug.LogError("No SettingsAsset for MessageInstance!");
            }
        }

        private void OnDisable()
        {
            if (_nameInstance)
            {
                _nameInstance.OnClick -= OnClick;
            }
        }

        public void Setup(Message msg)
        {
            Message = msg;

            if (_nameInstance)
            {
                _nameInstance.SetName(msg.UserName);
            }
            else
            {
                Debug.LogError("No Username Instance for Message Instance");
            }

            if (_messageText)
            {
                if (msg.MessageText.Contains(_userName))
                {
                    int startIndex = msg.MessageText.IndexOf(_userName);
                    _messageText.SetText(msg.MessageText, startIndex, startIndex + _userName.Length + 1);
                }
                else
                {
                    _messageText.SetText(msg.MessageText);                    
                }
            }
            else
            {
                Debug.LogError("No MessageText Instance for Message Instance");
            }

            if (_timeText && _settingsAsset)
            {
                _timeText.gameObject.SetActive(_settingsAsset.ShowTimeStamp);
                _timeText.text = $"[{msg.Time}]";
            }
            else
            {
                Debug.LogError("No DateText Instance for Message Instance");
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(_rect);
        }

        public void Refresh()
        {
            Setup(Message);
        }

        private void ReturnToPool()
        {
            OnBeforeDestroy?.Invoke(this);

            if (Pool)
            {
                Pool.ReturnToPool(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnClick(PointerEventData.InputButton btn)
        {
            OnClicked?.Invoke(this, btn);
        }
    }
}

