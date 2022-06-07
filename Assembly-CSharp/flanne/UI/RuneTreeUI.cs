using System;
using System.Collections.Generic;
using flanne.RuneSystem;
using UnityEngine;
using UnityEngine.Events;

namespace flanne.UI
{
		public class RuneTreeUI : MonoBehaviour
	{
				private void OnLevelChange()
		{
			this.Refresh();
		}

				private void Start()
		{
			RuneRowUI[] array = this.rows;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].onLevelChanged.AddListener(new UnityAction(this.OnLevelChange));
			}
		}

				private void OnDestroy()
		{
			RuneRowUI[] array = this.rows;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].onLevelChanged.AddListener(new UnityAction(this.OnLevelChange));
			}
		}

				public int[] GetSelections()
		{
			int[] array = new int[this.rows.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.rows[i].toggleIndex;
			}
			return array;
		}

				public void SetSelections(int[] selections)
		{
			if (selections == null)
			{
				return;
			}
			for (int i = 0; i < selections.Length; i++)
			{
				this.rows[i].toggleIndex = selections[i];
			}
		}

				public List<RuneData> GetActiveRunes()
		{
			List<RuneData> list = new List<RuneData>();
			for (int i = 0; i < this.rows.Length; i++)
			{
				RuneData toggledRune = this.rows[i].toggledRune;
				if (toggledRune != null)
				{
					list.Add(toggledRune);
				}
			}
			return list;
		}

				private void Refresh()
		{
			int num = 0;
			foreach (RuneRowUI runeRowUI in this.rows)
			{
				num += runeRowUI.totalLevels;
			}
			foreach (RuneRowUI runeRowUI2 in this.rows)
			{
				if (runeRowUI2.levelRequirement <= num)
				{
					runeRowUI2.UnLock();
				}
				else
				{
					runeRowUI2.Lock();
				}
			}
		}

				[SerializeField]
		private RuneRowUI[] rows;
	}
}
