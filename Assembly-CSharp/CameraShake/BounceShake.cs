using System;
using UnityEngine;

namespace CameraShake
{
    public class BounceShake : ICameraShake
    {
        public BounceShake(BounceShake.Params parameters, Vector3? sourcePosition = null)
        {
            this.sourcePosition = sourcePosition;
            this.pars = parameters;
            Displacement a = Displacement.InsideUnitSpheres();
            this.direction = Displacement.Scale(a, this.pars.axesMultiplier).Normalized;
        }

        public BounceShake(BounceShake.Params parameters, Displacement initialDirection, Vector3? sourcePosition = null)
        {
            this.sourcePosition = sourcePosition;
            this.pars = parameters;
            this.direction = Displacement.Scale(initialDirection, this.pars.axesMultiplier).Normalized;
        }

        public Displacement CurrentDisplacement { get; private set; }

        public bool IsFinished { get; private set; }

        public void Initialize(Vector3 cameraPosition, Quaternion cameraRotation)
        {
            this.attenuation = ((this.sourcePosition == null) ? 1f : Attenuator.Strength(this.pars.attenuation, this.sourcePosition.Value, cameraPosition));
            this.currentWaypoint = this.attenuation * this.direction.ScaledBy(this.pars.positionStrength, this.pars.rotationStrength);
        }

        public void Update(float deltaTime, Vector3 cameraPosition, Quaternion cameraRotation)
        {
            if (this.t < 1f)
            {
                this.t += deltaTime * this.pars.freq;
                if (this.pars.freq == 0f)
                {
                    this.t = 1f;
                }
                this.CurrentDisplacement = Displacement.Lerp(this.previousWaypoint, this.currentWaypoint, this.moveCurve.Evaluate(this.t));
                return;
            }
            this.t = 0f;
            this.CurrentDisplacement = this.currentWaypoint;
            this.previousWaypoint = this.currentWaypoint;
            this.bounceIndex++;
            if (this.bounceIndex > this.pars.numBounces)
            {
                this.IsFinished = true;
                return;
            }
            Displacement a = Displacement.InsideUnitSpheres();
            this.direction = -this.direction + this.pars.randomness * Displacement.Scale(a, this.pars.axesMultiplier).Normalized;
            this.direction = this.direction.Normalized;
            float num = 1f - (float)this.bounceIndex / (float)this.pars.numBounces;
            this.currentWaypoint = num * num * this.attenuation * this.direction.ScaledBy(this.pars.positionStrength, this.pars.rotationStrength);
        }

        private readonly BounceShake.Params pars;

        private readonly AnimationCurve moveCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        private readonly Vector3? sourcePosition;

        private float attenuation = 1f;

        private Displacement direction;

        private Displacement previousWaypoint;

        private Displacement currentWaypoint;

        private int bounceIndex;

        private float t;

        [Serializable]
        public class Params
        {
            [Tooltip("Strength of the shake for positional axes.")]
            public float positionStrength = 0.05f;

            [Tooltip("Strength of the shake for rotational axes.")]
            public float rotationStrength = 0.1f;

            [Tooltip("Preferred direction of shaking.")]
            public Displacement axesMultiplier = new Displacement(Vector2.one, Vector3.forward);

            [Tooltip("Frequency of shaking.")]
            public float freq = 25f;

            [Tooltip("Number of vibrations before stop.")]
            public int numBounces = 5;

            [Range(0f, 1f)]
            [Tooltip("Randomness of motion.")]
            public float randomness = 0.5f;

            [Tooltip("How strength falls with distance from the shake source.")]
            public Attenuator.StrengthAttenuationParams attenuation;
        }
    }
}
