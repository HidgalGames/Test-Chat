using InitializableLogic.Misc;
using System;

namespace InitializableLogic
{
    public class InitializableListener
    {
        private IInitializable _currentTarget;
        private object _subscriber;
        private Action _currentAction;

        /// <summary>
        /// Invokes action when target is initialized or if it is already initialized
        /// </summary>
        /// <param name="subscriber"></param>
        /// <param name="target"></param>
        /// <param name="onInitialized"></param>
        public InitializableListener(object subscriber, IInitializable target, Action onInitialized)
        {
            Subscribe(subscriber, target, onInitialized);
        }

        private void Subscribe(object subscriber, IInitializable target, Action onInitialized)
        {
            _subscriber = subscriber;
            _currentTarget = target;
            _currentAction = onInitialized;

            if (_currentTarget != null)
            {
                _currentTarget.Subscribers.Add(_subscriber);

                if (_currentTarget.IsInitialized)
                {
                    OnInitialized();
                }
                else
                {
                    _currentTarget.OnInitialized += OnInitialized;
                }
            }
        }

        private void OnInitialized()
        {
            _currentTarget.OnInitialized -= OnInitialized;

#pragma warning disable CS0162 // Disable unreachable code warning in editor
            if (InitializableVariables.CLEAR_SUBSCRIBERS_LIST)
            {
                _currentTarget.Subscribers.Remove(_subscriber);
            }
#pragma warning restore CS0162

            _currentAction?.Invoke();
        }
    }
}

