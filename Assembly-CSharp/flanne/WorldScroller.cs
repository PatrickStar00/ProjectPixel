using System;
using System.Collections.Generic;
using UnityEngine;

namespace flanne
{
		public class WorldScroller : MonoBehaviour
	{
				private void Start()
		{
			this._currentTile = this.GetCurrentTile();
		}

				private void FixedUpdate()
		{
			if (!this.IsPlayerOnTile(this._currentTile))
			{
				this._currentTile = this.GetCurrentTile();
			}
			WorldScroller.Quadrant quadrant = this.GetQuadrant(this._currentTile);
			if (this._currentQuadrant != quadrant)
			{
				this._currentQuadrant = quadrant;
				List<Transform> list = new List<Transform>(this.tiles);
				list.Remove(this._currentTile);
				switch (quadrant)
				{
				case WorldScroller.Quadrant.TopLeft:
					list[0].position = new Vector3(this._currentTile.position.x - this.tileSize, this._currentTile.position.y, 0f);
					list[1].position = new Vector3(this._currentTile.position.x, this._currentTile.position.y + this.tileSize, 0f);
					list[2].position = new Vector3(this._currentTile.position.x - this.tileSize, this._currentTile.position.y + this.tileSize, 0f);
					return;
				case WorldScroller.Quadrant.TopRight:
					list[0].position = new Vector3(this._currentTile.position.x + this.tileSize, this._currentTile.position.y, 0f);
					list[1].position = new Vector3(this._currentTile.position.x, this._currentTile.position.y + this.tileSize, 0f);
					list[2].position = new Vector3(this._currentTile.position.x + this.tileSize, this._currentTile.position.y + this.tileSize, 0f);
					return;
				case WorldScroller.Quadrant.BotLeft:
					list[0].position = new Vector3(this._currentTile.position.x - this.tileSize, this._currentTile.position.y, 0f);
					list[1].position = new Vector3(this._currentTile.position.x, this._currentTile.position.y - this.tileSize, 0f);
					list[2].position = new Vector3(this._currentTile.position.x - this.tileSize, this._currentTile.position.y - this.tileSize, 0f);
					return;
				case WorldScroller.Quadrant.BotRight:
					list[0].position = new Vector3(this._currentTile.position.x + this.tileSize, this._currentTile.position.y, 0f);
					list[1].position = new Vector3(this._currentTile.position.x, this._currentTile.position.y - this.tileSize, 0f);
					list[2].position = new Vector3(this._currentTile.position.x + this.tileSize, this._currentTile.position.y - this.tileSize, 0f);
					break;
				default:
					return;
				}
			}
		}

				private WorldScroller.Quadrant GetQuadrant(Transform tile)
		{
			Vector3 position = this.player.position;
			Vector3 position2 = tile.position;
			if (position.x < position2.x && position.y < position2.y)
			{
				return WorldScroller.Quadrant.BotLeft;
			}
			if (position.x < position2.x && position.y >= position2.y)
			{
				return WorldScroller.Quadrant.TopLeft;
			}
			if (position.x >= position2.x && position.y < position2.y)
			{
				return WorldScroller.Quadrant.BotRight;
			}
			return WorldScroller.Quadrant.TopRight;
		}

				private bool IsPlayerOnTile(Transform tile)
		{
			Vector3 position = this.player.position;
			Vector3 position2 = tile.position;
			return position2.x - this.tileSize / 2f < position.x && position2.x + this.tileSize / 2f > position.x && position2.y - this.tileSize / 2f < position.y && position2.y + this.tileSize / 2f > position.y;
		}

				private Transform GetCurrentTile()
		{
			for (int i = 0; i < this.tiles.Count; i++)
			{
				if (this.IsPlayerOnTile(this.tiles[i]))
				{
					return this.tiles[i];
				}
			}
			Debug.LogError("Player is not on any current tile");
			return this.tiles[0];
		}

				[SerializeField]
		private float tileSize = 32f;

				[SerializeField]
		private Transform player;

				[SerializeField]
		private List<Transform> tiles = new List<Transform>(4);

				private WorldScroller.Quadrant _currentQuadrant;

				private Transform _currentTile;

				private enum Quadrant
		{
						TopLeft,
						TopRight,
						BotLeft,
						BotRight
		}
	}
}
