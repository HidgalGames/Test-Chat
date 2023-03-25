using DG.Tweening;
using UnityEngine;

namespace TweenComponents.Base
{
    public class TweenBase : Executable
    {
        [Header("Base Settings")]
        public TweenBaseSettings BaseSettings;

        [Space]
        [Header("Loop Settings")]
        public TweenLoopSettings LoopSettings;


        [Space]
        [Header("Execution Settings")]
        public TweenExecutionSettings ExecutionSettings;

        [Space]
        [Header("Value Settings")]
        public bool OverrideValueOnAwake;

        #region Properties

        public float Delay
        {
            get => BaseSettings.StartDelay;
            set => BaseSettings.StartDelay = value >= 0 ? value : 0;
        }

        public float Duration
        {
            get => BaseSettings.Duration;
            set => BaseSettings.Duration = value >= 0 ? value : 0;
        }

        public Ease EaseType
        {
            get => BaseSettings.EaseType;
            set => BaseSettings.EaseType = value;
        }


        public int LoopCount
        {
            get => LoopSettings.LoopCount;
            set => LoopSettings.LoopCount = value >= -1 ? value : 0;
        }

        public LoopType LoopType
        {
            get => LoopSettings.LoopType;
            set => LoopSettings.LoopType = value;
        }


        public bool CanBeInterrupted
        {
            get => ExecutionSettings.CanBeInterrupted;
            set => ExecutionSettings.CanBeInterrupted = value;
        }

        public bool ExecuteOnEnable
        {
            get => ExecutionSettings.ExecuteOnEnable;
            set => ExecutionSettings.ExecuteOnEnable = value;
        }

        public bool StraightOnEnable
        {
            get => ExecutionSettings.StraightOnEnable;
            set => ExecutionSettings.StraightOnEnable = value;
        }

        public bool ResetValueOnExecute
        {
            get => ExecutionSettings.ResetValueOnExecute;
            set => ExecutionSettings.ResetValueOnExecute = value;
        }

        public bool IgnoreTimeScale
        {
            get => ExecutionSettings.IgnoreTimeScale;
            set => ExecutionSettings.IgnoreTimeScale = value;
        }

        #endregion

        protected Tween _tween;

        protected virtual void Awake()
        {
            CheckValueOverrideOnAwake();
        }

        protected virtual void CheckValueOverrideOnAwake()
        {
            if (OverrideValueOnAwake)
            {
                ResetValue();
            }
            else
            {
                ApplyCurrentValueAsStartValue();
            }
        }

        protected virtual void ApplyCurrentValueAsStartValue() { }

        protected virtual void OnEnable()
        {
            if (ExecuteOnEnable)
            {
                Execute(StraightOnEnable);
            }
        }

        public virtual bool CanBeExecuted()
        {
            if (_tween != null && _tween.IsPlaying())
            {
                return CanBeInterrupted;
            }

            return true;
        }

        public override void Execute(bool straight = true)
        {
            if (ResetValueOnExecute)
            {
                Stop();
                ResetValue(straight);
            }

            base.Execute();
        }

        [ContextMenu("Stop")]
        public override void Stop()
        {
            base.Stop();

            if (_tween != null)
            {
                _tween.Rewind();
            }
        }

        public virtual void Kill()
        {
            if (_tween != null)
            {
                _tween.Kill();
            }
        }

        public virtual void Pause()
        {
            if (_tween != null)
            {
                _tween.Pause();
            }
        }

        public override void OnExecutionCompleted()
        {
            if(_tween != null)
                _tween.onComplete -= OnExecutionCompleted;

            _tween = null;

            base.OnExecutionCompleted();
        }

        public virtual void ResetValue(bool applyStartValue = true) { }

        protected virtual void LogError(string errorText)
        {
            Debug.LogError(errorText + $" for tween <color=#FD811E>{this.GetType().Name}</color> on object <color=#FFFFFF>{gameObject.name}</color> !");
        }
    }
}
