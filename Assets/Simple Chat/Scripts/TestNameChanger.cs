using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ChatLogic
{
    public class TestNameChanger : MonoBehaviour
    {
        [SerializeField] private ChatHandler _chat;

        [Space]
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _applyButton;

        private void Awake()
        {
            _applyButton.onClick.AddListener(ApplyName);
        }

        private void OnDestroy()
        {
            _applyButton.onClick.RemoveListener(ApplyName);
        }

        private void ApplyName()
        {
            _chat.UserName = _inputField.text;
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
