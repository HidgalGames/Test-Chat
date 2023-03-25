using ChatLogic.Messages;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ChatLogic
{
    public class ChatClickHandler : MonoBehaviour
    {
        [SerializeField] private ChatHandler _chat;

        private MessagesHandler _messageHandler => _chat ? _chat.MessagesHandler : null;
        private ChatInput _input => _chat ? _chat.Input : null;

        private void Awake()
        {
            if (!_messageHandler)
            {
                Debug.LogError("No MessagesHandler for ChatClickHandler!");
                return;
            }

            _messageHandler.OnMessageReceived += OnMessageReceived;
        }

        private void OnDestroy()
        {
            if (_messageHandler)
            {
                _messageHandler.OnMessageReceived -= OnMessageReceived;
            }
        }

        private void OnMessageReceived(MessageInstance msg)
        {
            if (!msg) return;

            msg.OnClicked += OnMessageClicked;
            msg.OnBeforeDestroy += OnMessageDestroyed;
        }

        private void OnMessageClicked(MessageInstance msg, PointerEventData.InputButton btn)
        {
            if (!msg) return;

            switch (btn)
            {
                case PointerEventData.InputButton.Left:
                    AddNameToInput(msg);
                    break;

                case PointerEventData.InputButton.Right:
                    break;

                case PointerEventData.InputButton.Middle:
                    break;

                default:
                    break;
            }
        }

        private void OnMessageDestroyed(MessageInstance msg)
        {
            msg.OnClicked -= OnMessageClicked;
            msg.OnBeforeDestroy -= OnMessageDestroyed;
        }

        private void AddNameToInput(MessageInstance msg)
        {
            if (_input)
            {
                _input.SetSelectedState(true);
                _input.AddText(msg.Message.UserName + ", ");
            }
        }
    }
}

