using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace flanne
{
		public class Unlockable : MonoBehaviour
	{
						public bool IsLocked
		{
			get
			{
				return this._isLocked;
			}
		}

				private void Awake()
		{
			this._originalMaterial = this.targetSprite.material;
			this.unlockCostTMP.text = (("Unlock<br>" + this.unlockCost) ?? "");
		}

				public void Lock()
		{
			this.targetSelectable.interactable = false;
			this.targetSprite.material = this.lockMaterial;
			this.lockSprite.enabled = true;
			this._isLocked = true;
		}

				public void UnlockWithPoints()
		{
			SoundEffectSO soundEffectSO = this.unlockSFX;
			if (soundEffectSO != null)
			{
				soundEffectSO.Play(null);
			}
			PointsTracker.pts -= this.unlockCost;
			this.toggle.isOn = true;
			this.Unlock();
		}

				public void Unlock()
		{
			this.HideUnlockButton();
			this.targetSelectable.interactable = true;
			this.targetSprite.material = this._originalMaterial;
			this.lockSprite.enabled = false;
			this._isLocked = false;
		}

				public void ShowUnlockButton()
		{
			if (this._isLocked)
			{
				this.unlockButton.gameObject.SetActive(true);
			}
			if (PointsTracker.pts < this.unlockCost)
			{
				this.unlockButton.interactable = false;
				return;
			}
			this.unlockButton.interactable = true;
		}

				public void HideUnlockButton()
		{
			if (this._isLocked)
			{
				this.unlockButton.gameObject.SetActive(false);
			}
		}

				[SerializeField]
		private int unlockCost;

				[SerializeField]
		private Selectable targetSelectable;

				[SerializeField]
		private Image targetSprite;

				[SerializeField]
		private Image lockSprite;

				[SerializeField]
		private Material lockMaterial;

				[SerializeField]
		private Button unlockButton;

				[SerializeField]
		private TMP_Text unlockCostTMP;

				[SerializeField]
		private Toggle toggle;

				[SerializeField]
		private SoundEffectSO unlockSFX;

				private Material _originalMaterial;

				private bool _isLocked;
	}
}
