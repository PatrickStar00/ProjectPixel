using System;
using System.Collections.Generic;
using UnityEngine;

namespace flanne
{
		public class MoveSystem2D : MonoBehaviour
	{
				private void Awake()
		{
			MoveSystem2D.moveComponents = new List<MoveComponent2D>();
		}

				private void Start()
		{
			this.OP = ObjectPooler.SharedInstance;
		}

				private void FixedUpdate()
		{
			for (int i = 0; i < MoveSystem2D.moveComponents.Count; i++)
			{
				if (MoveSystem2D.moveComponents[i].gameObject.activeSelf)
				{
					float num = MoveSystem2D.moveComponents[i].vector.magnitude;
					num *= Mathf.Exp(-MoveSystem2D.moveComponents[i].drag * Time.fixedDeltaTime);
					MoveSystem2D.moveComponents[i].vector = Mathf.Max(0f, num) * MoveSystem2D.moveComponents[i].vector.normalized;
					MoveSystem2D.moveComponents[i].Rb.MovePosition(MoveSystem2D.moveComponents[i].Rb.position + MoveSystem2D.moveComponents[i].vector * Time.fixedDeltaTime);
					if (MoveSystem2D.moveComponents[i].rotateTowardsMove)
					{
						Quaternion rotation = Quaternion.AngleAxis(Mathf.Atan2(MoveSystem2D.moveComponents[i].vector.y, MoveSystem2D.moveComponents[i].vector.x) * 57.29578f, Vector3.forward);
						MoveSystem2D.moveComponents[i].transform.rotation = rotation;
						if (MoveSystem2D.moveComponents[i].vector.x < 0f)
						{
							MoveSystem2D.moveComponents[i].transform.localScale = new Vector3(1f, -1f, 1f);
						}
						else
						{
							MoveSystem2D.moveComponents[i].transform.localScale = new Vector3(1f, 1f, 1f);
						}
					}
				}
			}
		}

				public static void Register(MoveComponent2D m)
		{
			MoveSystem2D.moveComponents.Add(m);
		}

				public static List<MoveComponent2D> moveComponents;

				private ObjectPooler OP;
	}
}
