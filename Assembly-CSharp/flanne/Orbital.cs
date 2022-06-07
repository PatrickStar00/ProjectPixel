using System;
using UnityEngine;

namespace flanne
{
		public class Orbital : MonoBehaviour
	{
				private void Start()
		{
			if (this.center == null)
			{
				this.center = base.transform.parent;
			}
			base.transform.position = (base.transform.position - this.center.position).normalized * this.radius + this.center.position;
		}

				private void Update()
		{
			base.transform.RotateAround(this.center.position, this.axis, this.rotationSpeed * Time.deltaTime);
			Vector3 vector = (base.transform.position - this.center.position).normalized * this.radius + this.center.position;
			base.transform.position = Vector3.MoveTowards(base.transform.position, vector, Time.deltaTime * this.radiusSpeed);
			if (this.dontRotate)
			{
				base.transform.rotation = Quaternion.identity;
			}
		}

				public Transform center;

				public Vector3 axis = Vector3.up;

				public float radius = 2f;

				public float radiusSpeed = 0.5f;

				public float rotationSpeed = 80f;

				public bool dontRotate;
	}
}
