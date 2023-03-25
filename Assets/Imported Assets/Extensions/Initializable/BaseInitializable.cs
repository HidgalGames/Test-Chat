using System;
using System.Collections.Generic;

namespace InitializableLogic
{
    public class BaseInitializable : IInitializable
    {
        protected bool _isInitialized;
        public bool IsInitialized => _isInitialized;

        public List<object> Subscribers { get; set; } = new();


        public event Action OnInitialized;

        protected virtual void OnInited()
        {
            _isInitialized = true;
            OnInitialized?.Invoke();
        }
    }
}

