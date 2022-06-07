using System;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
				public virtual State CurrentState
	{
		get
		{
			return this._currentState;
		}
		set
		{
			this.Transition(value);
		}
	}

		public virtual T GetState<T>() where T : State
	{
		T t = base.GetComponent<T>();
		if (t == null)
		{
			t = base.gameObject.AddComponent<T>();
		}
		return t;
	}

		public virtual void ChangeState<T>() where T : State
	{
		this.CurrentState = this.GetState<T>();
	}

		protected virtual void Transition(State value)
	{
		if (this._currentState == value || this._inTransition)
		{
			return;
		}
		this._inTransition = true;
		if (this._currentState != null)
		{
			this._currentState.Exit();
		}
		this._currentState = value;
		if (this._currentState != null)
		{
			this._currentState.Enter();
		}
		this._inTransition = false;
	}

		protected State _currentState;

		protected bool _inTransition;
}
