using System;
using System.Collections.Generic;
using UnityEngine;

namespace InitializableLogic
{
    public abstract class MonoInitializable : MonoBehaviour, IInitializable
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

