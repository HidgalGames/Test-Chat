using System;
using FishNet.Broadcast;
using UnityEngine;

namespace ChatLogic.Messages
{
    [System.Serializable]
    public struct Message : IBroadcast
    {
        private const int TIME_STRING_LENGTH = 5;
        private const int USERNAME_START_INDEX = 8;

        public string UserName;
        public string MessageText;
        public string Time;

        public Message(string username, string text)
        {
            UserName = username;
            MessageText = text;
            Time = DateTime.Now.TimeOfDay.ToHHmmString();
        }

        public Message(string username, string text, string time)
        {
            UserName = username;
            MessageText = text;
            Time = time;
        }

        /// <summary>
        /// Converts Message from given string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns>New Message with data or Null if string is wrong</returns>
        public static Message FromString(string str)
        {
            var time = GetTimeFromString(str);
            var name = GetUserNameFromString(str, out var messageStartIndex);

            if(messageStartIndex > 0)
            {
                var msgText = GetMessageTextFromString(str);

                return new(name, msgText, time);
            }

            return new();
        }

        private static string GetTimeFromString(string str)
        {
            return str.Substring(1, TIME_STRING_LENGTH - 1);
        }

        private static string GetUserNameFromString(string str)
        {
            var separatorIndex = str.IndexOf(':', USERNAME_START_INDEX);
            return str.Substring(USERNAME_START_INDEX, separatorIndex - USERNAME_START_INDEX);
        }
        private static string GetUserNameFromString(string str, out int separatorIndex)
        {
            separatorIndex = str.IndexOf(':', USERNAME_START_INDEX);
            return str.Substring(USERNAME_START_INDEX, separatorIndex - USERNAME_START_INDEX);
        }

        private static string GetMessageTextFromString(string str, int messageStartIndex = -1)
        {
            if(messageStartIndex < 0)
            {
                messageStartIndex = str.IndexOf(':', USERNAME_START_INDEX);
            }

            if (messageStartIndex < 0) return string.Empty;

            return str.Substring(messageStartIndex + 1, str.Length - messageStartIndex - 1);
        }
    }

    public static class MessageExtensions
    {
        public static string ToString(this Message msg)
        {
            return $"[{msg.Time}] {msg.UserName}:{msg.MessageText}";
        }

        public static string ToHHmmString(this TimeSpan time)
        {
            return time.ToString(@"hh\:mm");
        }
    }
}

