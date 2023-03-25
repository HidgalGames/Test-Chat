using DG.Tweening;
using TweenComponents.Base;
using UnityEngine;

namespace TweenComponents
{
    public class ShakePositionTween : TweenBase
    {
        public Vector3 Strength = Vector3.one;
        public int Vibrato = 5;
        [Range(0f, 90f)] public float Randomness;

        [Space]
        public bool Snapping = false;
        public bool FadeOut = true;

        [Space]
        public Transform TransformToChange;

        protected override void Awake()
        {
            if (!TransformToChange)
            {
                TransformToChange = transform;
            }

            base.Awake();
        }

        public override void Execute(bool straight = true)
        {
            if (!CanBeExecuted()) return;

            base.Execute(straight);

            _tween = TransformToChange.DOShakePosition(Duration, Strength, Vibrato, Randomness, Snapping, FadeOut).ApplyBaseSettingsFrom(this);
        }
    }
}
