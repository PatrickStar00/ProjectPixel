using System;
using System.Collections.Generic;
using UnityEngine;

namespace flanne.UI
{
		public class Menu : MonoBehaviour
	{
				// (add) Token: 0x0600089B RID: 2203 RVA: 0x00022FA0 File Offset: 0x000211A0
		// (remove) Token: 0x0600089C RID: 2204 RVA: 0x00022FD8 File Offset: 0x000211D8
		public event EventHandler<InfoEventArgs<int>> ClickEvent;

				// (add) Token: 0x0600089D RID: 2205 RVA: 0x00023010 File Offset: 0x00021210
		// (remove) Token: 0x0600089E RID: 2206 RVA: 0x00023048 File Offset: 0x00021248
		public event EventHandler<InfoEventArgs<int>> SelectEvent;

				public virtual void OnEntryClicked(int index)
		{
			if (this.ClickEvent != null)
			{
				this.ClickEvent(this, new InfoEventArgs<int>(index));
			}
		}

				public virtual void OnEntrySelected(int index)
		{
			if (this.SelectEvent != null)
			{
				this.SelectEvent(this, new InfoEventArgs<int>(index));
			}
		}

				private void Start()
		{
			for (int i = 0; i < this.entries.Count; i++)
			{
				int closureIndex = i;
				this.entries[closureIndex].onClick.AddListener(delegate()
				{
					this.OnEntryClicked(closureIndex);
				});
				this.entries[closureIndex].onSelect.AddListener(delegate()
				{
					this.OnEntrySelected(closureIndex);
				});
			}
		}

				private void OnDestroy()
		{
			for (int i = 0; i < this.entries.Count; i++)
			{
				int closureIndex = i;
				this.entries[closureIndex].onClick.RemoveListener(delegate()
				{
					this.OnEntryClicked(closureIndex);
				});
				this.entries[closureIndex].onSelect.RemoveListener(delegate()
				{
					this.OnEntrySelected(closureIndex);
				});
			}
		}

				public void SetProperties<T>(int index, T properties) where T : IUIProperties
		{
			if (index >= 0 && index < this.entries.Count)
			{
				Widget<T> component = this.entries[index].GetComponent<Widget<T>>();
				if (component != null)
				{
					component.SetProperties(properties);
					return;
				}
			}
			else
			{
				Debug.LogError("Cannot set property of entry " + index + ". Index out of bounds.");
			}
		}

				public void Select(int index)
		{
			if (index >= 0 && index < this.entries.Count)
			{
				this.entries[index].Select();
				return;
			}
			Debug.LogError("Cannot select menu entry " + index + ". Index out of bounds.");
		}

				public int SelectFirstAvailable()
		{
			for (int i = 0; i < this.entries.Count; i++)
			{
				if (this.entries[i].interactable)
				{
					this.Select(i);
					return i;
				}
			}
			return -1;
		}

				public virtual void Lock(int index)
		{
			this.entries[index].interactable = false;
		}

				public void UnLock(int index)
		{
			this.entries[index].interactable = true;
		}

				public void SetEntryActive(int index, bool active)
		{
			this.entries[index].gameObject.SetActive(active);
		}

				public void UnlockAll()
		{
			foreach (MenuEntry menuEntry in this.entries)
			{
				menuEntry.interactable = true;
			}
		}

				public Vector2 GetEntryPosition(int index)
		{
			return this.entries[index].transform.position;
		}

				public MenuEntry GetEntry(int index)
		{
			return this.entries[index];
		}

				[SerializeField]
		protected List<MenuEntry> entries;
	}
}
