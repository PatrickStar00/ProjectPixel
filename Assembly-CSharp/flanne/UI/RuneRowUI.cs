using System;
using flanne.RuneSystem;
using UnityEngine;
using UnityEngine.Events;

namespace flanne.UI
{
		public class RuneRowUI : MonoBehaviour
	{
								public int toggleIndex
		{
			get
			{
				for (int i = 0; i < this.runes.Length; i++)
				{
					if (this.runes[i].toggleOn)
					{
						return i;
					}
				}
				return -1;
			}
			set
			{
				if (value == -1)
				{
					RuneUnlocker[] array = this.runes;
					for (int i = 0; i < array.Length; i++)
					{
						array[i].toggleOn = false;
					}
					return;
				}
				this.runes[value].toggleOn = true;
			}
		}

						public RuneData toggledRune
		{
			get
			{
				int toggleIndex = this.toggleIndex;
				if (toggleIndex != -1)
				{
					return this.runes[toggleIndex].rune.data;
				}
				return null;
			}
		}

						public int totalLevels
		{
			get
			{
				int num = 0;
				foreach (RuneUnlocker runeUnlocker in this.runes)
				{
					num += runeUnlocker.level;
				}
				return num;
			}
		}

				private void OnLevelChange()
		{
			UnityEvent unityEvent = this.onLevelChanged;
			if (unityEvent == null)
			{
				return;
			}
			unityEvent.Invoke();
		}

				private void Awake()
		{
			this.onLevelChanged = new UnityEvent();
		}

				private void Start()
		{
			RuneUnlocker[] array = this.runes;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].onLevel.AddListener(new UnityAction(this.OnLevelChange));
			}
		}

				private void OnDestroy()
		{
			RuneUnlocker[] array = this.runes;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].onLevel.RemoveListener(new UnityAction(this.OnLevelChange));
			}
		}

				public void Lock()
		{
			RuneUnlocker[] array = this.runes;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].locked = true;
			}
		}

				public void UnLock()
		{
			RuneUnlocker[] array = this.runes;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].locked = false;
			}
		}

				[NonSerialized]
		public UnityEvent onLevelChanged;

				public int levelRequirement;

				[SerializeField]
		private RuneUnlocker[] runes;
	}
}
