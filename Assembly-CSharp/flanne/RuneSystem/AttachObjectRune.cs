using System;
using System.Collections.Generic;
using UnityEngine;

namespace flanne.RuneSystem
{
		public class AttachObjectRune : Rune
	{
				protected override void Init()
		{
			for (int i = 0; i < this.amountToAttach; i++)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.prefab);
				gameObject.transform.SetParent(this.player.transform);
				gameObject.transform.localPosition = this.posOffset;
				Orbital component = gameObject.GetComponent<Orbital>();
				if (component != null)
				{
					Orbital[] componentsInChildren = this.player.GetComponentsInChildren<Orbital>();
					List<Orbital> list = new List<Orbital>();
					foreach (Orbital orbital in componentsInChildren)
					{
						if (orbital.tag == component.tag)
						{
							list.Add(orbital);
						}
					}
					Vector2 v = list[0].transform.localPosition;
					for (int k = 1; k < list.Count; k++)
					{
						list[k].transform.localPosition = v.Rotate((float)(k * (360 / list.Count)));
					}
				}
			}
		}

				[SerializeField]
		private GameObject prefab;

				[SerializeField]
		private int amountToAttach = 1;

				[SerializeField]
		private Vector3 posOffset;
	}
}
