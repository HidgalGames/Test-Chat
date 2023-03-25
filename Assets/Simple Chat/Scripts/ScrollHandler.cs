using System.Collections;
using ChatLogic.Messages;
using UnityEngine;
using UnityEngine.UI;

namespace ChatLogic
{
    public class ScrollHandler : MonoBehaviour
    {
        [SerializeField] private ChatHandler _chat;

        private Coroutine _updateRoutine;
        private WaitForEndOfFrame _wait;

        private ScrollRect _scroll => _chat ? _chat.ScrollView : null;
        private MessagesHandler _msgHandler => _chat ? _chat.MessagesHandler : null;

        private void OnEnable()
        {
            _wait = new();

            if (_msgHandler)
            {
                _msgHandler.OnMessageReceived += OnMessageReceived;
            }
        }

        private void OnDisable()
        {
            if (_msgHandler)
            {
                _msgHandler.OnMessageReceived -= OnMessageReceived;
            }
        }

        private void OnMessageReceived(MessageInstance msg)
        {
            StopUpdate();

            _updateRoutine = StartCoroutine(UpdateRoutine());
        }

        private void StopUpdate()
        {
            if(_updateRoutine != null)
            {
                StopCoroutine(_updateRoutine);
                _updateRoutine = null;
            }
        }

        private IEnumerator UpdateRoutine()
        {
            yield return _wait;

            if (_scroll)
            {
                _scroll.verticalNormalizedPosition = 0f;
            }

            _updateRoutine = null;
        }
    }
}

