using System;
using UnityEngine;

namespace CameraShake
{
		public class PerlinShake : ICameraShake
	{
				public PerlinShake(PerlinShake.Params parameters, float maxAmplitude = 1f, Vector3? sourcePosition = null, bool manualStrengthControl = false)
		{
			this.pars = parameters;
			this.envelope = new Envelope(this.pars.envelope, maxAmplitude, manualStrengthControl ? Envelope.EnvelopeControlMode.Manual : Envelope.EnvelopeControlMode.Auto);
			this.AmplitudeController = this.envelope;
			this.sourcePosition = sourcePosition;
		}

								public Displacement CurrentDisplacement { get; private set; }

								public bool IsFinished { get; private set; }

				public void Initialize(Vector3 cameraPosition, Quaternion cameraRotation)
		{
			this.seeds = new Vector2[this.pars.noiseModes.Length];
			this.norm = 0f;
			for (int i = 0; i < this.seeds.Length; i++)
			{
				this.seeds[i] = Random.insideUnitCircle * 20f;
				this.norm += this.pars.noiseModes[i].amplitude;
			}
		}

				public void Update(float deltaTime, Vector3 cameraPosition, Quaternion cameraRotation)
		{
			if (this.envelope.IsFinished)
			{
				this.IsFinished = true;
				return;
			}
			this.time += deltaTime;
			this.envelope.Update(deltaTime);
			Displacement a = Displacement.Zero;
			for (int i = 0; i < this.pars.noiseModes.Length; i++)
			{
				a += this.pars.noiseModes[i].amplitude / this.norm * this.SampleNoise(this.seeds[i], this.pars.noiseModes[i].freq);
			}
			this.CurrentDisplacement = this.envelope.Intensity * Displacement.Scale(a, this.pars.strength);
			if (this.sourcePosition != null)
			{
				this.CurrentDisplacement *= Attenuator.Strength(this.pars.attenuation, this.sourcePosition.Value, cameraPosition);
			}
		}

				private Displacement SampleNoise(Vector2 seed, float freq)
		{
			Vector3 position = new Vector3(Mathf.PerlinNoise(seed.x + this.time * freq, seed.y), Mathf.PerlinNoise(seed.x, seed.y + this.time * freq), Mathf.PerlinNoise(seed.x + this.time * freq, seed.y + this.time * freq)) - Vector3.one * 0.5f;
			Vector3 vector;
			vector..ctor(Mathf.PerlinNoise(-seed.x - this.time * freq, -seed.y), Mathf.PerlinNoise(-seed.x, -seed.y - this.time * freq), Mathf.PerlinNoise(-seed.x - this.time * freq, -seed.y - this.time * freq));
			vector -= Vector3.one * 0.5f;
			return new Displacement(position, vector);
		}

				private readonly PerlinShake.Params pars;

				private readonly Envelope envelope;

				public IAmplitudeController AmplitudeController;

				private Vector2[] seeds;

				private float time;

				private Vector3? sourcePosition;

				private float norm;

				[Serializable]
		public class Params
		{
						[Tooltip("Strength of the shake for each axis.")]
			public Displacement strength = new Displacement(Vector3.zero, new Vector3(2f, 2f, 0.8f));

						[Tooltip("Layers of perlin noise with different frequencies.")]
			public PerlinShake.NoiseMode[] noiseModes = new PerlinShake.NoiseMode[]
			{
				new PerlinShake.NoiseMode(12f, 1f)
			};

						[Tooltip("Strength of the shake over time.")]
			public Envelope.EnvelopeParams envelope;

						[Tooltip("How strength falls with distance from the shake source.")]
			public Attenuator.StrengthAttenuationParams attenuation;
		}

				[Serializable]
		public struct NoiseMode
		{
						public NoiseMode(float freq, float amplitude)
			{
				this.freq = freq;
				this.amplitude = amplitude;
			}

						[Tooltip("Frequency multiplier for the noise.")]
			public float freq;

						[Tooltip("Amplitude of the mode.")]
			[Range(0f, 1f)]
			public float amplitude;
		}
	}
}
