using System;
using UnityEngine;

namespace flanne
{
		public class BurnOnWalk : MonoBehaviour
	{
				private void Start()
		{
			this.BS = BurnSystem.SharedInstance;
			this._lastPos = base.transform.position;
			this._currPos = base.transform.position;
		}

				private void Update()
		{
			this._lastPos = this._currPos;
			this._currPos = base.transform.position;
			Vector2 vector = this._lastPos - this._currPos;
			this._distanceCtr += vector.magnitude;
			if (this._distanceCtr >= this.distanceToCast)
			{
				this._distanceCtr -= this.distanceToCast;
				this.particles.Play();
				this.soundFX.Play(null);
				Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, this.range);
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].gameObject.tag == "Enemy")
					{
						this.BS.Burn(array[i].gameObject, this.burnDamage);
						FlashSprite componentInChildren = array[i].gameObject.GetComponentInChildren<FlashSprite>();
						if (componentInChildren != null)
						{
							componentInChildren.Flash();
						}
					}
				}
			}
		}

				[SerializeField]
		private ParticleSystem particles;

				[SerializeField]
		private SoundEffectSO soundFX;

				[SerializeField]
		private float range;

				[SerializeField]
		private float distanceToCast;

				[SerializeField]
		private int burnDamage;

				private BurnSystem BS;

				private float _distanceCtr;

				private Vector2 _lastPos;

				private Vector2 _currPos;
	}
}
