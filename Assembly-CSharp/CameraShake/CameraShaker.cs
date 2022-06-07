using System;
using System.Collections.Generic;
using UnityEngine;

namespace CameraShake
{
		public class CameraShaker : MonoBehaviour
	{
				public static void Shake(ICameraShake shake)
		{
			if (CameraShaker.IsInstanceNull())
			{
				return;
			}
			CameraShaker.Instance.RegisterShake(shake);
		}

				public void RegisterShake(ICameraShake shake)
		{
			shake.Initialize(this.cameraTransform.position, this.cameraTransform.rotation);
			this.activeShakes.Add(shake);
		}

				public void SetCameraTransform(Transform cameraTransform)
		{
			cameraTransform.localPosition = Vector3.zero;
			cameraTransform.localEulerAngles = Vector3.zero;
			this.cameraTransform = cameraTransform;
		}

				private void Awake()
		{
			CameraShaker.Instance = this;
			this.ShakePresets = new CameraShakePresets(this);
			CameraShaker.Presets = this.ShakePresets;
			if (this.cameraTransform == null)
			{
				this.cameraTransform = base.transform;
			}
		}

				private void Update()
		{
			if (this.cameraTransform == null || !CameraShaker.ShakeOn)
			{
				return;
			}
			Displacement displacement = Displacement.Zero;
			for (int i = this.activeShakes.Count - 1; i >= 0; i--)
			{
				if (this.activeShakes[i].IsFinished)
				{
					this.activeShakes.RemoveAt(i);
				}
				else
				{
					this.activeShakes[i].Update(Time.deltaTime, this.cameraTransform.position, this.cameraTransform.rotation);
					displacement += this.activeShakes[i].CurrentDisplacement;
				}
			}
			this.cameraTransform.localPosition = this.StrengthMultiplier * displacement.position;
			this.cameraTransform.localRotation = Quaternion.Euler(this.StrengthMultiplier * displacement.eulerAngles);
		}

				private static bool IsInstanceNull()
		{
			if (CameraShaker.Instance == null)
			{
				Debug.LogError("CameraShaker Instance is missing!");
				return true;
			}
			return false;
		}

				public static CameraShaker Instance;

				public static CameraShakePresets Presets;

				public static bool ShakeOn;

				private readonly List<ICameraShake> activeShakes = new List<ICameraShake>();

				[Tooltip("Transform which will be affected by the shakes.\n\nCameraShaker will set this transform's local position and rotation.")]
		[SerializeField]
		private Transform cameraTransform;

				[Tooltip("Scales the strength of all shakes.")]
		[Range(0f, 1f)]
		[SerializeField]
		public float StrengthMultiplier = 1f;

				public CameraShakePresets ShakePresets;
	}
}
