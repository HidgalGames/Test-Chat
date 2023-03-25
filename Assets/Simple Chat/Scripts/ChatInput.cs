using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ChatLogic
{
    public class ChatInput : MonoBehaviour
    {
        [SerializeField] private ChatHandler _chat;
        
        [Space]
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _sendButton;

        [Space]
        [SerializeField] private KeyCode _hotkey = KeyCode.Return;
        [SerializeField] private int _minimalMessageLength = 1;
        [SerializeField] private bool _deselectAfterSending = true;

        private string _userName => _chat ? _chat.UserName : "UserName";
        private MessagesHandler _msgHandler => _chat ? _chat.MessagesHandler : null;

        private void Start()
        {
            if (_sendButton)
            {
                _sendButton.onClick.AddListener(SendAndDeselect);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(_hotkey))
            {
                if (!HasEventSystem()) return;

                if(EventSystem.current.currentSelectedGameObject != _inputField.gameObject)
                {
                    SetSelectedState(true);
                }
                else
                {
                    SendAndDeselect();
                }
            }
        }

        private void OnDestroy()
        {
            if (_sendButton)
            {
                _sendButton.onClick.RemoveListener(SendAndDeselect);
            }
        }

        public void SetSelectedState(bool isSelected)
        {
            if (!HasEventSystem()) return;

            if (isSelected)
            {
                EventSystem.current.SetSelectedGameObject(_inputField.gameObject);
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
        }

        public void AddText(string text)
        {
            if (_inputField)
            {
                _inputField.text += text;
                _inputField.MoveTextEnd(false);
            }
        }

        private void SendAndDeselect()
        {
            SendMessage();

            if (_deselectAfterSending)
            {
                SetSelectedState(false);
            }
        }

        private void SendMessage()
        {           
            if (!CanSendMessage()) return;            
            if (_inputField.text.Length < _minimalMessageLength)
            {
                _inputField.text = string.Empty;
                return;
            }

            _msgHandler.Send(new(_userName, _inputField.text));
            _inputField.text = string.Empty;
        }

        private bool CanSendMessage()
        {
            if (!_msgHandler)
            {
                Debug.LogError("No MessagesHandler for ChatInput");
                return false;
            }

            if (!_inputField)
            {
                Debug.LogError("No InputField for ChatInput");
                return false;
            }

            return true;
        }

        private bool HasEventSystem()
        {
            if (!EventSystem.current)
            {
                Debug.LogError("No EventSystem for UI!");
                return false;
            }

            return true;
        }
    }
}

