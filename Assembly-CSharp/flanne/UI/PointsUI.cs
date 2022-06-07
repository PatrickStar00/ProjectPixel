using System;
using TMPro;
using UnityEngine;

namespace flanne.UI
{
		public class PointsUI : MonoBehaviour
	{
				private void OnPointChanged(object sender, int pts)
		{
			this.Refresh(pts);
		}

				private void Start()
		{
			this.Refresh(PointsTracker.pts);
			PointsTracker.PointsChangedEvent = (EventHandler<int>)Delegate.Combine(PointsTracker.PointsChangedEvent, new EventHandler<int>(this.OnPointChanged));
		}

				private void OnDestroy()
		{
			PointsTracker.PointsChangedEvent = (EventHandler<int>)Delegate.Remove(PointsTracker.PointsChangedEvent, new EventHandler<int>(this.OnPointChanged));
		}

				private void Refresh(int pts)
		{
			this.ptsTMP.text = pts.ToString();
		}

				[SerializeField]
		private TMP_Text ptsTMP;
	}
}
