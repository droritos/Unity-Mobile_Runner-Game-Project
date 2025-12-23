using DG.Tweening;
using UnityEngine;

namespace Effects
{
    public class RotationAnimation : MonoBehaviour
    {
        [SerializeField] AxisType axisToRotate = AxisType.Y;
        [SerializeField] float rotationSpeed = 90f; // degrees per second

        private Tween rotationTween;

        private void Start()
        {
            Rotate();
        }

        private void Rotate()
        {
            Vector3 rotationAxis = GetRotationAxis();

            // Kill previous tween if exists (safety)
            rotationTween?.Kill();

            rotationTween = transform
                .DORotate(
                    rotationAxis * 360f,        // full rotation
                    360f / rotationSpeed,       // duration based on speed
                    RotateMode.LocalAxisAdd     // important!
                )
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
        }

        private Vector3 GetRotationAxis()
        {
            switch (axisToRotate)
            {
                case AxisType.X:
                    return Vector3.right;
                case AxisType.Y:
                    return Vector3.up;
                case AxisType.Z:
                    return Vector3.forward;
                case AxisType.All:
                    return new Vector3(1, 1, 1).normalized;
                default:
                    return Vector3.up;
            }
        }

        private void OnDestroy()
        {
            rotationTween?.Kill();
        }
    }

    public enum AxisType
    {
        X,
        Y,
        Z,
        All
    }
}