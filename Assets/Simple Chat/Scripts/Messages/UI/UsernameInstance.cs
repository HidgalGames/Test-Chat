using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ChatLogic.Messages
{
    [RequireComponent(typeof(TMP_Text))]
    public class UsernameInstance : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TMP_Text _nameText;

        public event Action<PointerEventData.InputButton> OnClick;

        private void Awake()
        {
            if (!_nameText)
            {
                _nameText = GetComponent<TMP_Text>();
            }
        }

        private void OnEnable()
        {
            SetHighlightState(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(eventData.button);
        }

        //TODO: append networkInstance to username
        public void SetName(string username)
        {
            if (!_nameText)
            {
                Debug.LogError("No TextField for UsernameInstance!");
                return;
            }

            _nameText.text = username;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_nameText) return;

            SetHighlightState(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_nameText) return;

            SetHighlightState(false);
        }

        private void SetHighlightState(bool isEnabled)
        {
            if (isEnabled)
            {
                _nameText.fontStyle = FontStyles.Bold | FontStyles.Underline;
            }
            else
            {
                _nameText.fontStyle = FontStyles.Bold;
            }
        }
    }
}

