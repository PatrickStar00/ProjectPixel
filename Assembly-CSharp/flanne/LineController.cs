using System;
using UnityEngine;

namespace flanne
{
		public class LineController : MonoBehaviour
	{
				private void Start()
		{
			this.lineRenderer.positionCount = this.targets.Length;
		}

				private void Update()
		{
			Vector3[] array = new Vector3[this.targets.Length];
			for (int i = 0; i < this.targets.Length; i++)
			{
				array[i] = this.targets[i].transform.position;
			}
			this.lineRenderer.SetPositions(array);
		}

				public Vector3[] GetPositions()
		{
			Vector3[] array = new Vector3[this.lineRenderer.positionCount];
			this.lineRenderer.GetPositions(array);
			return array;
		}

				public float GetWidth()
		{
			return this.lineRenderer.startWidth;
		}

				[SerializeField]
		private LineRenderer lineRenderer;

				[SerializeField]
		private GameObject[] targets;
	}
}
