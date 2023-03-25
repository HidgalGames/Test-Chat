using System;
using System.Collections.Generic;

namespace InitializableLogic
{
    public interface IInitializable
    {
        public List<object> Subscribers { get; set; }

        public bool IsInitialized { get; }
        public event Action OnInitialized;
    }
}

