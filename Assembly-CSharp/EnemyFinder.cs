using System;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyFinder
{
		public static GameObject GetRandomEnemy(Vector2 center, Vector2 range)
	{
		List<GameObject> list = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
		GameObject gameObject = null;
		while (list.Count > 0 && gameObject == null)
		{
			GameObject gameObject2 = list[Random.Range(0, list.Count)];
			if (Mathf.Abs(gameObject2.transform.position.x - center.x) < range.x && Mathf.Abs(gameObject2.transform.position.y - center.y) < range.y && !gameObject2.tag.Contains("Passive"))
			{
				gameObject = gameObject2;
			}
			else
			{
				list.Remove(gameObject2);
			}
		}
		return gameObject;
	}
}
