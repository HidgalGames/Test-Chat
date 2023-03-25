using DG.Tweening;
using TweenComponents.Base;
using UnityEngine;

namespace TweenComponents
{
    public class JumpToPositionTween : TweenBase
    {
        public bool ChangeLocalPosition;
        public Vector3 StartPosition;

        [Space]
        public bool SetCurrentPositionAsFinishValue;
        public Vector3 FinishPosition;

        [Space]
        public float JumpForce;
        public int JumpsCount;

        [Space]
        public Transform TransformToChange;

        private Vector3 _positionToExecute;

        protected override void Awake()
        {
            if (!TransformToChange)
            {
                TransformToChange = transform;
            }

            base.Awake();
        }

        protected override void CheckValueOverrideOnAwake()
        {
            if (SetCurrentPositionAsFinishValue)
            {
                FinishPosition = TransformToChange.localPosition;
            }

            base.CheckValueOverrideOnAwake();
        }

        protected override void ApplyCurrentValueAsStartValue()
        {
            StartPosition = TransformToChange.localPosition;
        }

        public override bool CanBeExecuted()
        {
            return base.CanBeExecuted() && TransformToChange;
        }

        public override void Execute(bool straight = true)
        {
            if (!CanBeExecuted()) return;

            base.Execute(straight);

            if (ResetValueOnExecute)
            {
                ResetValue(straight);
            }

            if (straight)
            {
                _positionToExecute = FinishPosition;
            }
            else
            {
                _positionToExecute = StartPosition;
            }

            if (ChangeLocalPosition)
            {
                _tween = TransformToChange.DOLocalJump(_positionToExecute, JumpForce, JumpsCount, Duration).ApplyBaseSettingsFrom(this);
            }
            else
            {
                _tween = TransformToChange.DOJump(_positionToExecute, JumpForce, JumpsCount, Duration).ApplyBaseSettingsFrom(this);
            }
        }

        public override void ResetValue(bool applyStartValue = true)
        {
            if (applyStartValue)
            {
                if (ChangeLocalPosition)
                {
                    TransformToChange.localPosition = StartPosition;
                }
                else
                {
                    TransformToChange.position = StartPosition;
                }
            }
            else
            {
                if (ChangeLocalPosition)
                {
                    TransformToChange.localPosition = FinishPosition;
                }
                else
                {
                    TransformToChange.position = FinishPosition;
                }
            }
        }
    }
}