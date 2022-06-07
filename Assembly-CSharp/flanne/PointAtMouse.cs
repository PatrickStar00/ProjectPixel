using System;
using flanne.Core;
using UnityEngine;

namespace flanne
{
		public class PointAtMouse : MonoBehaviour
	{
				private void Start()
		{
			this.SC = ShootingCursor.Instance;
		}

				private void Update()
		{
			if (!PauseController.isPaused)
			{
				Vector2 vector = Camera.main.ScreenToWorldPoint(this.SC.cursorPosition);
				Vector2 vector2 = base.transform.position;
				Vector2 vector3 = vector - vector2;
				float num = Mathf.Atan2(vector3.y, vector3.x) * 57.29578f;
				base.transform.rotation = Quaternion.AngleAxis(num, Vector3.forward);
				if (num <= 90f && num > -90f)
				{
					base.transform.localScale = new Vector3(1f, 1f, 1f);
					return;
				}
				base.transform.localScale = new Vector3(1f, -1f, 1f);
			}
		}

				private ShootingCursor SC;
	}
}
