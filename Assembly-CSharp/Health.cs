using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
				public int maxHP
	{
		get
		{
			return this._maxHP;
		}
		set
		{
			int num = value - this._maxHP;
			this._maxHP = value;
			this.onMaxHealthChange.Invoke(this._maxHP);
			if (num > 0)
			{
				this.HP += num;
				this.onHealthChange.Invoke(this.HP);
				return;
			}
			this.HP = Mathf.Clamp(this.HP, 0, this.maxHP);
			this.onHealthChange.Invoke(this.HP);
			if (this._maxHP <= 0)
			{
				this.onDeath.Invoke();
			}
		}
	}

			public bool isDead
	{
		get
		{
			return this.HP <= 0;
		}
	}

				public int HP { get; private set; }

		private void OnEnable()
	{
		this.HP = this.maxHP;
		this.onMaxHealthChange.Invoke(this.maxHP);
		this.onHealthChange.Invoke(this.HP);
	}

		public void HPChange(int change)
	{
		if (this.isInvincible && change < 0)
		{
			this.onDamagePrevented.Invoke();
			return;
		}
		int num = change.NotifyModifiers(Health.TweakDamageEvent, this);
		bool hp = this.HP != 0;
		this.HP = Mathf.Clamp(this.HP + num, 0, this.maxHP);
		this.onHealthChange.Invoke(this.HP);
		if (change < 0)
		{
			this.onHurt.Invoke(num);
		}
		if (hp && this.HP == 0)
		{
			this.onDeath.Invoke();
			this.PostNotification(Health.DeathEvent, null);
		}
	}

		public void AutoKill()
	{
		this.HPChange(-1 * this.HP);
	}

		public static string DeathEvent = "Health.DeathEvent";

		public static string TweakDamageEvent = "Health.TweakDamageEvent";

		[SerializeField]
	private int _maxHP;

		public bool isInvincible;

		public UnityIntEvent onHealthChange;

		public UnityIntEvent onMaxHealthChange;

		public UnityIntEvent onHurt;

		public UnityEvent onDeath;

		public UnityEvent onDamagePrevented;
}
