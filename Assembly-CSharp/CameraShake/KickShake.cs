using System;
using UnityEngine;

namespace CameraShake
{
		public class KickShake : ICameraShake
	{
				public KickShake(KickShake.Params parameters, Vector3 sourcePosition, bool attenuateStrength)
		{
			this.pars = parameters;
			this.sourcePosition = new Vector3?(sourcePosition);
			this.attenuateStrength = attenuateStrength;
		}

				public KickShake(KickShake.Params parameters, Displacement direction)
		{
			this.pars = parameters;
			this.direction = direction.Normalized;
		}

								public Displacement CurrentDisplacement { get; private set; }

								public bool IsFinished { get; private set; }

				public void Initialize(Vector3 cameraPosition, Quaternion cameraRotation)
		{
			if (this.sourcePosition != null)
			{
				this.direction = Attenuator.Direction(this.sourcePosition.Value, cameraPosition, cameraRotation);
				if (this.attenuateStrength)
				{
					this.direction *= Attenuator.Strength(this.pars.attenuation, this.sourcePosition.Value, cameraPosition);
				}
			}
			this.currentWaypoint = Displacement.Scale(this.direction, this.pars.strength);
		}

				public void Update(float deltaTime, Vector3 cameraPosition, Quaternion cameraRotation)
		{
			if (this.t < 1f)
			{
				this.Move(deltaTime, this.release ? this.pars.releaseTime : this.pars.attackTime, this.release ? this.pars.releaseCurve : this.pars.attackCurve);
				return;
			}
			this.CurrentDisplacement = this.currentWaypoint;
			this.prevWaypoint = this.currentWaypoint;
			if (this.release)
			{
				this.IsFinished = true;
				return;
			}
			this.release = true;
			this.t = 0f;
			this.currentWaypoint = Displacement.Zero;
		}

				private void Move(float deltaTime, float duration, AnimationCurve curve)
		{
			if (duration > 0f)
			{
				this.t += deltaTime / duration;
			}
			else
			{
				this.t = 1f;
			}
			this.CurrentDisplacement = Displacement.Lerp(this.prevWaypoint, this.currentWaypoint, curve.Evaluate(this.t));
		}

				private readonly KickShake.Params pars;

				private readonly Vector3? sourcePosition;

				private readonly bool attenuateStrength;

				private Displacement direction;

				private Displacement prevWaypoint;

				private Displacement currentWaypoint;

				private bool release;

				private float t;

				[Serializable]
		public class Params
		{
						[Tooltip("Strength of the shake for each axis.")]
			public Displacement strength = new Displacement(Vector3.zero, Vector3.one);

						[Tooltip("How long it takes to move forward.")]
			public float attackTime = 0.05f;

						public AnimationCurve attackCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

						[Tooltip("How long it takes to move back.")]
			public float releaseTime = 0.2f;

						public AnimationCurve releaseCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

						[Tooltip("How strength falls with distance from the shake source.")]
			public Attenuator.StrengthAttenuationParams attenuation;
		}
	}
}
