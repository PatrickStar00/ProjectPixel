using System;
using UnityEngine;

namespace CameraShake
{
		public class Envelope : IAmplitudeController
	{
				public Envelope(Envelope.EnvelopeParams pars, float initialTargetAmplitude, Envelope.EnvelopeControlMode controlMode)
		{
			this.pars = pars;
			this.controlMode = controlMode;
			this.SetTarget(initialTargetAmplitude);
		}

								public float Intensity { get; private set; }

						public bool IsFinished
		{
			get
			{
				return this.finishImmediately || ((this.finishWhenAmplitudeZero || this.controlMode == Envelope.EnvelopeControlMode.Auto) && this.amplitude <= 0f && this.targetAmplitude <= 0f);
			}
		}

				public void Finish()
		{
			this.finishWhenAmplitudeZero = true;
			this.SetTarget(0f);
		}

				public void FinishImmediately()
		{
			this.finishImmediately = true;
		}

				public void Update(float deltaTime)
		{
			if (this.IsFinished)
			{
				return;
			}
			if (this.state == Envelope.EnvelopeState.Increase)
			{
				if (this.pars.attack > 0f)
				{
					this.amplitude += deltaTime * this.pars.attack;
				}
				if (this.amplitude > this.targetAmplitude || this.pars.attack <= 0f)
				{
					this.amplitude = this.targetAmplitude;
					this.state = Envelope.EnvelopeState.Sustain;
					if (this.controlMode == Envelope.EnvelopeControlMode.Auto)
					{
						this.sustainEndTime = Time.time + this.pars.sustain;
					}
				}
			}
			else if (this.state == Envelope.EnvelopeState.Decrease)
			{
				if (this.pars.decay > 0f)
				{
					this.amplitude -= deltaTime * this.pars.decay;
				}
				if (this.amplitude < this.targetAmplitude || this.pars.decay <= 0f)
				{
					this.amplitude = this.targetAmplitude;
					this.state = Envelope.EnvelopeState.Sustain;
				}
			}
			else if (this.controlMode == Envelope.EnvelopeControlMode.Auto && Time.time > this.sustainEndTime)
			{
				this.SetTarget(0f);
			}
			this.amplitude = Mathf.Clamp01(this.amplitude);
			this.Intensity = Power.Evaluate(this.amplitude, this.pars.degree);
		}

				public void SetTargetAmplitude(float value)
		{
			if (this.controlMode == Envelope.EnvelopeControlMode.Manual && !this.finishWhenAmplitudeZero)
			{
				this.SetTarget(value);
			}
		}

				private void SetTarget(float value)
		{
			this.targetAmplitude = Mathf.Clamp01(value);
			this.state = ((this.targetAmplitude > this.amplitude) ? Envelope.EnvelopeState.Increase : Envelope.EnvelopeState.Decrease);
		}

				private readonly Envelope.EnvelopeParams pars;

				private readonly Envelope.EnvelopeControlMode controlMode;

				private float amplitude;

				private float targetAmplitude;

				private float sustainEndTime;

				private bool finishWhenAmplitudeZero;

				private bool finishImmediately;

				private Envelope.EnvelopeState state;

				[Serializable]
		public class EnvelopeParams
		{
						[Tooltip("How fast the amplitude increases.")]
			public float attack = 10f;

						[Tooltip("How long in seconds the amplitude holds maximum value.")]
			public float sustain;

						[Tooltip("How fast the amplitude decreases.")]
			public float decay = 1f;

						[Tooltip("Power in which the amplitude is raised to get intensity.")]
			public Degree degree = Degree.Cubic;
		}

				public enum EnvelopeControlMode
		{
						Auto,
						Manual
		}

				public enum EnvelopeState
		{
						Sustain,
						Increase,
						Decrease
		}
	}
}
