using System;
using UnityEngine;

namespace odt.util
{
    public class AccelerationCurve : MonoBehaviour
    {
        [SerializeField]
        private float startSpeed;
        [SerializeField]
        private float endSpeed;
        [SerializeField]
        private float curveOverLifetime;
        [SerializeField]
        private AnimationCurve curve;

        private float curveDeltaTime;

        public void Reset()
        {
            curveDeltaTime = 0;
        }

        public float CurrentSpeed(float deltaTime)
        {
            curveDeltaTime += deltaTime / curveOverLifetime;
            curveDeltaTime = Mathf.Clamp(curveDeltaTime, 0f, 1f);
            float t = curve.Evaluate(curveDeltaTime);
            return Mathf.Lerp(startSpeed, endSpeed, t);
        }
    }
}

