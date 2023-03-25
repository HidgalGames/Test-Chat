using TMPro;
using UnityEngine;

namespace ChatLogic.Messages.Misc
{
    public class HighlightedText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Color _highlightColor;

        #region Text Highlighting
        public void SetText(string text)
        {
            SetText(text, _highlightColor);
        }
        public void SetText(string text, Color color)
        {
            PlaceTextInHandler(text.Replace("<color>", GetColorString(color)));
        }
        public void SetText(string text, int highlightStart, int highlightEnd)
        {
            if (!IsIndexesInRange(text, highlightStart, highlightEnd)) return;

            SetText(text, highlightStart, highlightEnd, _highlightColor);
        }
        public void SetText(string text, int highlightStart, int highlightEnd, Color color)
        {
            if (!IsIndexesInRange(text, highlightStart, highlightEnd)) return;

            text = text.Insert(highlightEnd, "</color>");
            text = text.Insert(highlightStart, GetColorString(color));

            PlaceTextInHandler(text);
        }
        #endregion

        #region Misc
        private bool IsIndexesInRange(string text, int start, int end)
        {
            if (start < 0 || start >= text.Length) return false;
            if (end < 0 || end >= text.Length) return false;

            return end > start;
        }
        private void PlaceTextInHandler(string text)
        {
            if (!_text) return;
            _text.text = text;
        }

        private string GetColorString(Color color)
        {
            return $"<color=#{ColorUtility.ToHtmlStringRGBA(_highlightColor)}>";
        }
        #endregion
    }
}

