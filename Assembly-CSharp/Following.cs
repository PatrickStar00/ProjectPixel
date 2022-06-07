using System;
using UnityEngine;

public class Following : MonoBehaviour
{
		private void Start()
	{
		this.followArrow.gameObject.LeanDelayedCall(3f, new Action(this.moveArrow)).setOnStart(new Action(this.moveArrow)).setRepeat(-1);
		LeanTween.followDamp(this.dude1, this.followArrow, LeanProp.localY, 1.1f, -1f);
		LeanTween.followSpring(this.dude2, this.followArrow, LeanProp.localY, 1.1f, -1f, 2f, 0.5f);
		LeanTween.followBounceOut(this.dude3, this.followArrow, LeanProp.localY, 1.1f, -1f, 2f, 0.5f, 0.9f);
		LeanTween.followSpring(this.dude4, this.followArrow, LeanProp.localY, 1.1f, -1f, 1.5f, 0.8f);
		LeanTween.followLinear(this.dude5, this.followArrow, LeanProp.localY, 50f);
		LeanTween.followDamp(this.dude1, this.followArrow, LeanProp.color, 1.1f, -1f);
		LeanTween.followSpring(this.dude2, this.followArrow, LeanProp.color, 1.1f, -1f, 2f, 0.5f);
		LeanTween.followBounceOut(this.dude3, this.followArrow, LeanProp.color, 1.1f, -1f, 2f, 0.5f, 0.9f);
		LeanTween.followSpring(this.dude4, this.followArrow, LeanProp.color, 1.1f, -1f, 1.5f, 0.8f);
		LeanTween.followLinear(this.dude5, this.followArrow, LeanProp.color, 0.5f);
		LeanTween.followDamp(this.dude1, this.followArrow, LeanProp.scale, 1.1f, -1f);
		LeanTween.followSpring(this.dude2, this.followArrow, LeanProp.scale, 1.1f, -1f, 2f, 0.5f);
		LeanTween.followBounceOut(this.dude3, this.followArrow, LeanProp.scale, 1.1f, -1f, 2f, 0.5f, 0.9f);
		LeanTween.followSpring(this.dude4, this.followArrow, LeanProp.scale, 1.1f, -1f, 1.5f, 0.8f);
		LeanTween.followLinear(this.dude5, this.followArrow, LeanProp.scale, 5f);
		Vector3 offset;
		offset..ctor(0f, -20f, -18f);
		LeanTween.followDamp(this.dude1Title, this.dude1, LeanProp.localPosition, 0.6f, -1f).setOffset(offset);
		LeanTween.followSpring(this.dude2Title, this.dude2, LeanProp.localPosition, 0.6f, -1f, 2f, 0.5f).setOffset(offset);
		LeanTween.followBounceOut(this.dude3Title, this.dude3, LeanProp.localPosition, 0.6f, -1f, 2f, 0.5f, 0.9f).setOffset(offset);
		LeanTween.followSpring(this.dude4Title, this.dude4, LeanProp.localPosition, 0.6f, -1f, 1.5f, 0.8f).setOffset(offset);
		LeanTween.followLinear(this.dude5Title, this.dude5, LeanProp.localPosition, 30f).setOffset(offset);
		Vector3 point = Camera.main.transform.InverseTransformPoint(this.planet.transform.position);
		LeanTween.rotateAround(Camera.main.gameObject, Vector3.left, 360f, 300f).setPoint(point).setRepeat(-1);
	}

		private void Update()
	{
		this.fromY = LeanSmooth.spring(this.fromY, this.followArrow.localPosition.y, ref this.velocityY, 1.1f, -1f, -1f, 2f, 0.5f);
		this.fromVec3 = LeanSmooth.spring(this.fromVec3, this.dude5Title.localPosition, ref this.velocityVec3, 1.1f, -1f, -1f, 2f, 0.5f);
		this.fromColor = LeanSmooth.spring(this.fromColor, this.dude1.GetComponent<Renderer>().material.color, ref this.velocityColor, 1.1f, -1f, -1f, 2f, 0.5f);
		Debug.Log(string.Concat(new object[]
		{
			"Smoothed y:",
			this.fromY,
			" vec3:",
			this.fromVec3,
			" color:",
			this.fromColor
		}));
	}

		private void moveArrow()
	{
		LeanTween.moveLocalY(this.followArrow.gameObject, Random.Range(-100f, 100f), 0f);
		Color to;
		to..ctor(Random.value, Random.value, Random.value);
		LeanTween.color(this.followArrow.gameObject, to, 0f);
		float num = Random.Range(5f, 10f);
		this.followArrow.localScale = Vector3.one * num;
	}

		public Transform planet;

		public Transform followArrow;

		public Transform dude1;

		public Transform dude2;

		public Transform dude3;

		public Transform dude4;

		public Transform dude5;

		public Transform dude1Title;

		public Transform dude2Title;

		public Transform dude3Title;

		public Transform dude4Title;

		public Transform dude5Title;

		private Color dude1ColorVelocity;

		private Vector3 velocityPos;

		private float fromY;

		private float velocityY;

		private Vector3 fromVec3;

		private Vector3 velocityVec3;

		private Color fromColor;

		private Color velocityColor;
}
