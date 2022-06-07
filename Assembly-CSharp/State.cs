using System;
using UnityEngine;

public abstract class State : MonoBehaviour
{
		public virtual void Enter()
	{
		this.AddListeners();
	}

		public virtual void Exit()
	{
		this.RemoveListeners();
	}

		protected virtual void OnDestroy()
	{
		this.RemoveListeners();
	}

		protected virtual void AddListeners()
	{
	}

		protected virtual void RemoveListeners()
	{
	}
}
