﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LeanTween : MonoBehaviour
{
		public static void init()
	{
		LeanTween.init(LeanTween.maxTweens);
	}

			public static int maxSearch
	{
		get
		{
			return LeanTween.tweenMaxSearch;
		}
	}

			public static int maxSimulataneousTweens
	{
		get
		{
			return LeanTween.maxTweens;
		}
	}

			public static int tweensRunning
	{
		get
		{
			int num = 0;
			for (int i = 0; i <= LeanTween.tweenMaxSearch; i++)
			{
				if (LeanTween.tweens[i].toggle)
				{
					num++;
				}
			}
			return num;
		}
	}

		public static void init(int maxSimultaneousTweens)
	{
		LeanTween.init(maxSimultaneousTweens, LeanTween.maxSequences);
	}

		public static void init(int maxSimultaneousTweens, int maxSimultaneousSequences)
	{
		if (LeanTween.tweens == null)
		{
			LeanTween.maxTweens = maxSimultaneousTweens;
			LeanTween.tweens = new LTDescr[LeanTween.maxTweens];
			LeanTween.tweensFinished = new int[LeanTween.maxTweens];
			LeanTween.tweensFinishedIds = new int[LeanTween.maxTweens];
			LeanTween._tweenEmpty = new GameObject();
			LeanTween._tweenEmpty.name = "~LeanTween";
			LeanTween._tweenEmpty.AddComponent(typeof(LeanTween));
			LeanTween._tweenEmpty.isStatic = true;
			LeanTween._tweenEmpty.hideFlags = 61;
			Object.DontDestroyOnLoad(LeanTween._tweenEmpty);
			for (int i = 0; i < LeanTween.maxTweens; i++)
			{
				LeanTween.tweens[i] = new LTDescr();
			}
			SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>(LeanTween.onLevelWasLoaded54);
			LeanTween.sequences = new LTSeq[maxSimultaneousSequences];
			for (int j = 0; j < maxSimultaneousSequences; j++)
			{
				LeanTween.sequences[j] = new LTSeq();
			}
		}
	}

		public static void reset()
	{
		if (LeanTween.tweens != null)
		{
			for (int i = 0; i <= LeanTween.tweenMaxSearch; i++)
			{
				if (LeanTween.tweens[i] != null)
				{
					LeanTween.tweens[i].toggle = false;
				}
			}
		}
		LeanTween.tweens = null;
		Object.Destroy(LeanTween._tweenEmpty);
	}

		public void Update()
	{
		LeanTween.update();
	}

		private static void onLevelWasLoaded54(Scene scene, LoadSceneMode mode)
	{
		LeanTween.internalOnLevelWasLoaded(scene.buildIndex);
	}

		private static void internalOnLevelWasLoaded(int lvl)
	{
		LTGUI.reset();
	}

		public static void update()
	{
		if (LeanTween.frameRendered != Time.frameCount)
		{
			LeanTween.init();
			LeanTween.dtEstimated = ((LeanTween.dtEstimated < 0f) ? 0f : (LeanTween.dtEstimated = Time.unscaledDeltaTime));
			LeanTween.dtActual = Time.deltaTime;
			LeanTween.maxTweenReached = 0;
			LeanTween.finishedCnt = 0;
			int num = 0;
			while (num <= LeanTween.tweenMaxSearch && num < LeanTween.maxTweens)
			{
				LeanTween.tween = LeanTween.tweens[num];
				if (LeanTween.tween.toggle)
				{
					LeanTween.maxTweenReached = num;
					if (LeanTween.tween.updateInternal())
					{
						LeanTween.tweensFinished[LeanTween.finishedCnt] = num;
						LeanTween.tweensFinishedIds[LeanTween.finishedCnt] = LeanTween.tweens[num].id;
						LeanTween.finishedCnt++;
					}
				}
				num++;
			}
			LeanTween.tweenMaxSearch = LeanTween.maxTweenReached;
			LeanTween.frameRendered = Time.frameCount;
			for (int i = 0; i < LeanTween.finishedCnt; i++)
			{
				LeanTween.j = LeanTween.tweensFinished[i];
				LeanTween.tween = LeanTween.tweens[LeanTween.j];
				if (LeanTween.tween.id == LeanTween.tweensFinishedIds[i])
				{
					LeanTween.removeTween(LeanTween.j);
					if (LeanTween.tween.hasExtraOnCompletes && LeanTween.tween.trans != null)
					{
						LeanTween.tween.callOnCompletes();
					}
				}
			}
		}
	}

		public static void removeTween(int i, int uniqueId)
	{
		if (LeanTween.tweens[i].uniqueId == uniqueId)
		{
			LeanTween.removeTween(i);
		}
	}

		public static void removeTween(int i)
	{
		if (LeanTween.tweens[i].toggle)
		{
			LeanTween.tweens[i].toggle = false;
			LeanTween.tweens[i].counter = uint.MaxValue;
			if (LeanTween.tweens[i].destroyOnComplete)
			{
				if (LeanTween.tweens[i]._optional.ltRect != null)
				{
					LTGUI.destroy(LeanTween.tweens[i]._optional.ltRect.id);
				}
				else if (LeanTween.tweens[i].trans != null && LeanTween.tweens[i].trans.gameObject != LeanTween._tweenEmpty)
				{
					Object.Destroy(LeanTween.tweens[i].trans.gameObject);
				}
			}
			LeanTween.startSearch = i;
			if (i + 1 >= LeanTween.tweenMaxSearch)
			{
				LeanTween.startSearch = 0;
			}
		}
	}

		public static Vector3[] add(Vector3[] a, Vector3 b)
	{
		Vector3[] array = new Vector3[a.Length];
		LeanTween.i = 0;
		while (LeanTween.i < a.Length)
		{
			array[LeanTween.i] = a[LeanTween.i] + b;
			LeanTween.i++;
		}
		return array;
	}

		public static float closestRot(float from, float to)
	{
		float num = 0f - (360f - to);
		float num2 = 360f + to;
		float num3 = Mathf.Abs(to - from);
		float num4 = Mathf.Abs(num - from);
		float num5 = Mathf.Abs(num2 - from);
		if (num3 < num4 && num3 < num5)
		{
			return to;
		}
		if (num4 < num5)
		{
			return num;
		}
		return num2;
	}

		public static void cancelAll()
	{
		LeanTween.cancelAll(false);
	}

		public static void cancelAll(bool callComplete)
	{
		LeanTween.init();
		for (int i = 0; i <= LeanTween.tweenMaxSearch; i++)
		{
			if (LeanTween.tweens[i].trans != null)
			{
				if (callComplete && LeanTween.tweens[i].optional.onComplete != null)
				{
					LeanTween.tweens[i].optional.onComplete();
				}
				LeanTween.removeTween(i);
			}
		}
	}

		public static void cancel(GameObject gameObject)
	{
		LeanTween.cancel(gameObject, false);
	}

		public static void cancel(GameObject gameObject, bool callOnComplete)
	{
		LeanTween.init();
		Transform transform = gameObject.transform;
		for (int i = 0; i <= LeanTween.tweenMaxSearch; i++)
		{
			LTDescr ltdescr = LeanTween.tweens[i];
			if (ltdescr != null && ltdescr.toggle && ltdescr.trans == transform)
			{
				if (callOnComplete && ltdescr.optional.onComplete != null)
				{
					ltdescr.optional.onComplete();
				}
				LeanTween.removeTween(i);
			}
		}
	}

		public static void cancel(RectTransform rect)
	{
		LeanTween.cancel(rect.gameObject, false);
	}

		public static void cancel(GameObject gameObject, int uniqueId, bool callOnComplete = false)
	{
		if (uniqueId >= 0)
		{
			LeanTween.init();
			int num = uniqueId & 65535;
			int num2 = uniqueId >> 16;
			if (LeanTween.tweens[num].trans == null || (LeanTween.tweens[num].trans.gameObject == gameObject && (ulong)LeanTween.tweens[num].counter == (ulong)((long)num2)))
			{
				if (callOnComplete && LeanTween.tweens[num].optional.onComplete != null)
				{
					LeanTween.tweens[num].optional.onComplete();
				}
				LeanTween.removeTween(num);
			}
		}
	}

		public static void cancel(LTRect ltRect, int uniqueId)
	{
		if (uniqueId >= 0)
		{
			LeanTween.init();
			int num = uniqueId & 65535;
			int num2 = uniqueId >> 16;
			if (LeanTween.tweens[num]._optional.ltRect == ltRect && (ulong)LeanTween.tweens[num].counter == (ulong)((long)num2))
			{
				LeanTween.removeTween(num);
			}
		}
	}

		public static void cancel(int uniqueId)
	{
		LeanTween.cancel(uniqueId, false);
	}

		public static void cancel(int uniqueId, bool callOnComplete)
	{
		if (uniqueId >= 0)
		{
			LeanTween.init();
			int num = uniqueId & 65535;
			int num2 = uniqueId >> 16;
			if (num > LeanTween.tweens.Length - 1)
			{
				int num3 = num - LeanTween.tweens.Length;
				LTSeq ltseq = LeanTween.sequences[num3];
				for (int i = 0; i < LeanTween.maxSequences; i++)
				{
					if (ltseq.current.tween != null)
					{
						LeanTween.removeTween(ltseq.current.tween.uniqueId & 65535);
					}
					if (ltseq.current.previous == null)
					{
						return;
					}
					ltseq.current = ltseq.current.previous;
				}
				return;
			}
			if ((ulong)LeanTween.tweens[num].counter == (ulong)((long)num2))
			{
				if (callOnComplete && LeanTween.tweens[num].optional.onComplete != null)
				{
					LeanTween.tweens[num].optional.onComplete();
				}
				LeanTween.removeTween(num);
			}
		}
	}

		public static LTDescr descr(int uniqueId)
	{
		LeanTween.init();
		int num = uniqueId & 65535;
		int num2 = uniqueId >> 16;
		if (LeanTween.tweens[num] != null && LeanTween.tweens[num].uniqueId == uniqueId && (ulong)LeanTween.tweens[num].counter == (ulong)((long)num2))
		{
			return LeanTween.tweens[num];
		}
		for (int i = 0; i <= LeanTween.tweenMaxSearch; i++)
		{
			if (LeanTween.tweens[i].uniqueId == uniqueId && (ulong)LeanTween.tweens[i].counter == (ulong)((long)num2))
			{
				return LeanTween.tweens[i];
			}
		}
		return null;
	}

		public static LTDescr description(int uniqueId)
	{
		return LeanTween.descr(uniqueId);
	}

		public static LTDescr[] descriptions(GameObject gameObject = null)
	{
		if (gameObject == null)
		{
			return null;
		}
		List<LTDescr> list = new List<LTDescr>();
		Transform transform = gameObject.transform;
		for (int i = 0; i <= LeanTween.tweenMaxSearch; i++)
		{
			if (LeanTween.tweens[i].toggle && LeanTween.tweens[i].trans == transform)
			{
				list.Add(LeanTween.tweens[i]);
			}
		}
		return list.ToArray();
	}

		[Obsolete("Use 'pause( id )' instead")]
	public static void pause(GameObject gameObject, int uniqueId)
	{
		LeanTween.pause(uniqueId);
	}

		public static void pause(int uniqueId)
	{
		int num = uniqueId & 65535;
		int num2 = uniqueId >> 16;
		if ((ulong)LeanTween.tweens[num].counter == (ulong)((long)num2))
		{
			LeanTween.tweens[num].pause();
		}
	}

		public static void pause(GameObject gameObject)
	{
		Transform transform = gameObject.transform;
		for (int i = 0; i <= LeanTween.tweenMaxSearch; i++)
		{
			if (LeanTween.tweens[i].trans == transform)
			{
				LeanTween.tweens[i].pause();
			}
		}
	}

		public static void pauseAll()
	{
		LeanTween.init();
		for (int i = 0; i <= LeanTween.tweenMaxSearch; i++)
		{
			LeanTween.tweens[i].pause();
		}
	}

		public static void resumeAll()
	{
		LeanTween.init();
		for (int i = 0; i <= LeanTween.tweenMaxSearch; i++)
		{
			LeanTween.tweens[i].resume();
		}
	}

		[Obsolete("Use 'resume( id )' instead")]
	public static void resume(GameObject gameObject, int uniqueId)
	{
		LeanTween.resume(uniqueId);
	}

		public static void resume(int uniqueId)
	{
		int num = uniqueId & 65535;
		int num2 = uniqueId >> 16;
		if ((ulong)LeanTween.tweens[num].counter == (ulong)((long)num2))
		{
			LeanTween.tweens[num].resume();
		}
	}

		public static void resume(GameObject gameObject)
	{
		Transform transform = gameObject.transform;
		for (int i = 0; i <= LeanTween.tweenMaxSearch; i++)
		{
			if (LeanTween.tweens[i].trans == transform)
			{
				LeanTween.tweens[i].resume();
			}
		}
	}

		public static bool isPaused(GameObject gameObject = null)
	{
		if (gameObject == null)
		{
			for (int i = 0; i <= LeanTween.tweenMaxSearch; i++)
			{
				if (object.Equals(LeanTween.tweens[i].direction, 0f))
				{
					return true;
				}
			}
			return false;
		}
		Transform transform = gameObject.transform;
		for (int j = 0; j <= LeanTween.tweenMaxSearch; j++)
		{
			if (object.Equals(LeanTween.tweens[j].direction, 0f) && LeanTween.tweens[j].trans == transform)
			{
				return true;
			}
		}
		return false;
	}

		public static bool isPaused(RectTransform rect)
	{
		return LeanTween.isTweening(rect.gameObject);
	}

		public static bool isPaused(int uniqueId)
	{
		int num = uniqueId & 65535;
		int num2 = uniqueId >> 16;
		return num >= 0 && num < LeanTween.maxTweens && ((ulong)LeanTween.tweens[num].counter == (ulong)((long)num2) && object.Equals(LeanTween.tweens[LeanTween.i].direction, 0f));
	}

		public static bool isTweening(GameObject gameObject = null)
	{
		if (gameObject == null)
		{
			for (int i = 0; i <= LeanTween.tweenMaxSearch; i++)
			{
				if (LeanTween.tweens[i].toggle)
				{
					return true;
				}
			}
			return false;
		}
		Transform transform = gameObject.transform;
		for (int j = 0; j <= LeanTween.tweenMaxSearch; j++)
		{
			if (LeanTween.tweens[j].toggle && LeanTween.tweens[j].trans == transform)
			{
				return true;
			}
		}
		return false;
	}

		public static bool isTweening(RectTransform rect)
	{
		return LeanTween.isTweening(rect.gameObject);
	}

		public static bool isTweening(int uniqueId)
	{
		int num = uniqueId & 65535;
		int num2 = uniqueId >> 16;
		return num >= 0 && num < LeanTween.maxTweens && ((ulong)LeanTween.tweens[num].counter == (ulong)((long)num2) && LeanTween.tweens[num].toggle);
	}

		public static bool isTweening(LTRect ltRect)
	{
		for (int i = 0; i <= LeanTween.tweenMaxSearch; i++)
		{
			if (LeanTween.tweens[i].toggle && LeanTween.tweens[i]._optional.ltRect == ltRect)
			{
				return true;
			}
		}
		return false;
	}

		public static void drawBezierPath(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float arrowSize = 0f, Transform arrowTransform = null)
	{
		Vector3 vector = a;
		Vector3 vector2 = -a + 3f * (b - c) + d;
		Vector3 vector3 = 3f * (a + c) - 6f * b;
		Vector3 vector4 = 3f * (b - a);
		if (arrowSize > 0f)
		{
			Vector3 position = arrowTransform.position;
			Quaternion rotation = arrowTransform.rotation;
			float num = 0f;
			for (float num2 = 1f; num2 <= 120f; num2 += 1f)
			{
				float num3 = num2 / 120f;
				Vector3 vector5 = ((vector2 * num3 + vector3) * num3 + vector4) * num3 + a;
				Gizmos.DrawLine(vector, vector5);
				num += (vector5 - vector).magnitude;
				if (num > 1f)
				{
					num -= 1f;
					arrowTransform.position = vector5;
					arrowTransform.LookAt(vector, Vector3.forward);
					Vector3 vector6 = arrowTransform.TransformDirection(Vector3.right);
					Vector3 normalized = (vector - vector5).normalized;
					Gizmos.DrawLine(vector5, vector5 + (vector6 + normalized) * arrowSize);
					vector6 = arrowTransform.TransformDirection(-Vector3.right);
					Gizmos.DrawLine(vector5, vector5 + (vector6 + normalized) * arrowSize);
				}
				vector = vector5;
			}
			arrowTransform.position = position;
			arrowTransform.rotation = rotation;
			return;
		}
		for (float num4 = 1f; num4 <= 30f; num4 += 1f)
		{
			float num3 = num4 / 30f;
			Vector3 vector5 = ((vector2 * num3 + vector3) * num3 + vector4) * num3 + a;
			Gizmos.DrawLine(vector, vector5);
			vector = vector5;
		}
	}

		public static object logError(string error)
	{
		if (LeanTween.throwErrors)
		{
			Debug.LogError(error);
		}
		else
		{
			Debug.Log(error);
		}
		return null;
	}

		public static LTDescr options(LTDescr seed)
	{
		Debug.LogError("error this function is no longer used");
		return null;
	}

		public static LTDescr options()
	{
		LeanTween.init();
		bool flag = false;
		LeanTween.j = 0;
		LeanTween.i = LeanTween.startSearch;
		while (LeanTween.j <= LeanTween.maxTweens)
		{
			if (LeanTween.j >= LeanTween.maxTweens)
			{
				return LeanTween.logError("LeanTween - You have run out of available spaces for tweening. To avoid this error increase the number of spaces to available for tweening when you initialize the LeanTween class ex: LeanTween.init( " + LeanTween.maxTweens * 2 + " );") as LTDescr;
			}
			if (LeanTween.i >= LeanTween.maxTweens)
			{
				LeanTween.i = 0;
			}
			if (!LeanTween.tweens[LeanTween.i].toggle)
			{
				if (LeanTween.i + 1 > LeanTween.tweenMaxSearch && LeanTween.i + 1 < LeanTween.maxTweens)
				{
					LeanTween.tweenMaxSearch = LeanTween.i + 1;
				}
				LeanTween.startSearch = LeanTween.i + 1;
				flag = true;
				break;
			}
			LeanTween.j++;
			LeanTween.i++;
		}
		if (!flag)
		{
			LeanTween.logError("no available tween found!");
		}
		LeanTween.tweens[LeanTween.i].reset();
		LeanTween.global_counter += 1U;
		if (LeanTween.global_counter > 32768U)
		{
			LeanTween.global_counter = 0U;
		}
		LeanTween.tweens[LeanTween.i].setId((uint)LeanTween.i, LeanTween.global_counter);
		return LeanTween.tweens[LeanTween.i];
	}

			public static GameObject tweenEmpty
	{
		get
		{
			LeanTween.init(LeanTween.maxTweens);
			return LeanTween._tweenEmpty;
		}
	}

		private static LTDescr pushNewTween(GameObject gameObject, Vector3 to, float time, LTDescr tween)
	{
		LeanTween.init(LeanTween.maxTweens);
		if (gameObject == null || tween == null)
		{
			return null;
		}
		tween.trans = gameObject.transform;
		tween.to = to;
		tween.time = time;
		if (tween.time <= 0f)
		{
			tween.updateInternal();
		}
		return tween;
	}

		public static LTDescr play(RectTransform rectTransform, Sprite[] sprites)
	{
		float time = 0.25f * (float)sprites.Length;
		return LeanTween.pushNewTween(rectTransform.gameObject, new Vector3((float)sprites.Length - 1f, 0f, 0f), time, LeanTween.options().setCanvasPlaySprite().setSprites(sprites).setRepeat(-1));
	}

		public static LTSeq sequence(bool initSequence = true)
	{
		LeanTween.init(LeanTween.maxTweens);
		for (int i = 0; i < LeanTween.sequences.Length; i++)
		{
			if ((LeanTween.sequences[i].tween == null || !LeanTween.sequences[i].tween.toggle) && !LeanTween.sequences[i].toggle)
			{
				LTSeq ltseq = LeanTween.sequences[i];
				if (initSequence)
				{
					ltseq.init((uint)(i + LeanTween.tweens.Length), LeanTween.global_counter);
					LeanTween.global_counter += 1U;
					if (LeanTween.global_counter > 32768U)
					{
						LeanTween.global_counter = 0U;
					}
				}
				else
				{
					ltseq.reset();
				}
				return ltseq;
			}
		}
		return null;
	}

		public static LTDescr alpha(GameObject gameObject, float to, float time)
	{
		LTDescr ltdescr = LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, LeanTween.options().setAlpha());
		SpriteRenderer component = gameObject.GetComponent<SpriteRenderer>();
		ltdescr.spriteRen = component;
		return ltdescr;
	}

		public static LTDescr alpha(LTRect ltRect, float to, float time)
	{
		ltRect.alphaEnabled = true;
		return LeanTween.pushNewTween(LeanTween.tweenEmpty, new Vector3(to, 0f, 0f), time, LeanTween.options().setGUIAlpha().setRect(ltRect));
	}

		public static LTDescr textAlpha(RectTransform rectTransform, float to, float time)
	{
		return LeanTween.pushNewTween(rectTransform.gameObject, new Vector3(to, 0f, 0f), time, LeanTween.options().setTextAlpha());
	}

		public static LTDescr alphaText(RectTransform rectTransform, float to, float time)
	{
		return LeanTween.pushNewTween(rectTransform.gameObject, new Vector3(to, 0f, 0f), time, LeanTween.options().setTextAlpha());
	}

		public static LTDescr alphaCanvas(CanvasGroup canvasGroup, float to, float time)
	{
		return LeanTween.pushNewTween(canvasGroup.gameObject, new Vector3(to, 0f, 0f), time, LeanTween.options().setCanvasGroupAlpha());
	}

		public static LTDescr alphaVertex(GameObject gameObject, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, LeanTween.options().setAlphaVertex());
	}

		public static LTDescr color(GameObject gameObject, Color to, float time)
	{
		LTDescr ltdescr = LeanTween.pushNewTween(gameObject, new Vector3(1f, to.a, 0f), time, LeanTween.options().setColor().setPoint(new Vector3(to.r, to.g, to.b)));
		SpriteRenderer component = gameObject.GetComponent<SpriteRenderer>();
		ltdescr.spriteRen = component;
		return ltdescr;
	}

		public static LTDescr textColor(RectTransform rectTransform, Color to, float time)
	{
		return LeanTween.pushNewTween(rectTransform.gameObject, new Vector3(1f, to.a, 0f), time, LeanTween.options().setTextColor().setPoint(new Vector3(to.r, to.g, to.b)));
	}

		public static LTDescr colorText(RectTransform rectTransform, Color to, float time)
	{
		return LeanTween.pushNewTween(rectTransform.gameObject, new Vector3(1f, to.a, 0f), time, LeanTween.options().setTextColor().setPoint(new Vector3(to.r, to.g, to.b)));
	}

		public static LTDescr delayedCall(float delayTime, Action callback)
	{
		return LeanTween.pushNewTween(LeanTween.tweenEmpty, Vector3.zero, delayTime, LeanTween.options().setCallback().setOnComplete(callback));
	}

		public static LTDescr delayedCall(float delayTime, Action<object> callback)
	{
		return LeanTween.pushNewTween(LeanTween.tweenEmpty, Vector3.zero, delayTime, LeanTween.options().setCallback().setOnComplete(callback));
	}

		public static LTDescr delayedCall(GameObject gameObject, float delayTime, Action callback)
	{
		return LeanTween.pushNewTween(gameObject, Vector3.zero, delayTime, LeanTween.options().setCallback().setOnComplete(callback));
	}

		public static LTDescr delayedCall(GameObject gameObject, float delayTime, Action<object> callback)
	{
		return LeanTween.pushNewTween(gameObject, Vector3.zero, delayTime, LeanTween.options().setCallback().setOnComplete(callback));
	}

		public static LTDescr destroyAfter(LTRect rect, float delayTime)
	{
		return LeanTween.pushNewTween(LeanTween.tweenEmpty, Vector3.zero, delayTime, LeanTween.options().setCallback().setRect(rect).setDestroyOnComplete(true));
	}

		public static LTDescr move(GameObject gameObject, Vector3 to, float time)
	{
		return LeanTween.pushNewTween(gameObject, to, time, LeanTween.options().setMove());
	}

		public static LTDescr move(GameObject gameObject, Vector2 to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to.x, to.y, gameObject.transform.position.z), time, LeanTween.options().setMove());
	}

		public static LTDescr move(GameObject gameObject, Vector3[] to, float time)
	{
		LeanTween.d = LeanTween.options().setMoveCurved();
		if (LeanTween.d.optional.path == null)
		{
			LeanTween.d.optional.path = new LTBezierPath(to);
		}
		else
		{
			LeanTween.d.optional.path.setPoints(to);
		}
		return LeanTween.pushNewTween(gameObject, new Vector3(1f, 0f, 0f), time, LeanTween.d);
	}

		public static LTDescr move(GameObject gameObject, LTBezierPath to, float time)
	{
		LeanTween.d = LeanTween.options().setMoveCurved();
		LeanTween.d.optional.path = to;
		return LeanTween.pushNewTween(gameObject, new Vector3(1f, 0f, 0f), time, LeanTween.d);
	}

		public static LTDescr move(GameObject gameObject, LTSpline to, float time)
	{
		LeanTween.d = LeanTween.options().setMoveSpline();
		LeanTween.d.optional.spline = to;
		return LeanTween.pushNewTween(gameObject, new Vector3(1f, 0f, 0f), time, LeanTween.d);
	}

		public static LTDescr moveSpline(GameObject gameObject, Vector3[] to, float time)
	{
		LeanTween.d = LeanTween.options().setMoveSpline();
		LeanTween.d.optional.spline = new LTSpline(to);
		return LeanTween.pushNewTween(gameObject, new Vector3(1f, 0f, 0f), time, LeanTween.d);
	}

		public static LTDescr moveSpline(GameObject gameObject, LTSpline to, float time)
	{
		LeanTween.d = LeanTween.options().setMoveSpline();
		LeanTween.d.optional.spline = to;
		return LeanTween.pushNewTween(gameObject, new Vector3(1f, 0f, 0f), time, LeanTween.d);
	}

		public static LTDescr moveSplineLocal(GameObject gameObject, Vector3[] to, float time)
	{
		LeanTween.d = LeanTween.options().setMoveSplineLocal();
		LeanTween.d.optional.spline = new LTSpline(to);
		return LeanTween.pushNewTween(gameObject, new Vector3(1f, 0f, 0f), time, LeanTween.d);
	}

		public static LTDescr move(LTRect ltRect, Vector2 to, float time)
	{
		return LeanTween.pushNewTween(LeanTween.tweenEmpty, to, time, LeanTween.options().setGUIMove().setRect(ltRect));
	}

		public static LTDescr moveMargin(LTRect ltRect, Vector2 to, float time)
	{
		return LeanTween.pushNewTween(LeanTween.tweenEmpty, to, time, LeanTween.options().setGUIMoveMargin().setRect(ltRect));
	}

		public static LTDescr moveX(GameObject gameObject, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, LeanTween.options().setMoveX());
	}

		public static LTDescr moveY(GameObject gameObject, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, LeanTween.options().setMoveY());
	}

		public static LTDescr moveZ(GameObject gameObject, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, LeanTween.options().setMoveZ());
	}

		public static LTDescr moveLocal(GameObject gameObject, Vector3 to, float time)
	{
		return LeanTween.pushNewTween(gameObject, to, time, LeanTween.options().setMoveLocal());
	}

		public static LTDescr moveLocal(GameObject gameObject, Vector3[] to, float time)
	{
		LeanTween.d = LeanTween.options().setMoveCurvedLocal();
		if (LeanTween.d.optional.path == null)
		{
			LeanTween.d.optional.path = new LTBezierPath(to);
		}
		else
		{
			LeanTween.d.optional.path.setPoints(to);
		}
		return LeanTween.pushNewTween(gameObject, new Vector3(1f, 0f, 0f), time, LeanTween.d);
	}

		public static LTDescr moveLocalX(GameObject gameObject, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, LeanTween.options().setMoveLocalX());
	}

		public static LTDescr moveLocalY(GameObject gameObject, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, LeanTween.options().setMoveLocalY());
	}

		public static LTDescr moveLocalZ(GameObject gameObject, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, LeanTween.options().setMoveLocalZ());
	}

		public static LTDescr moveLocal(GameObject gameObject, LTBezierPath to, float time)
	{
		LeanTween.d = LeanTween.options().setMoveCurvedLocal();
		LeanTween.d.optional.path = to;
		return LeanTween.pushNewTween(gameObject, new Vector3(1f, 0f, 0f), time, LeanTween.d);
	}

		public static LTDescr moveLocal(GameObject gameObject, LTSpline to, float time)
	{
		LeanTween.d = LeanTween.options().setMoveSplineLocal();
		LeanTween.d.optional.spline = to;
		return LeanTween.pushNewTween(gameObject, new Vector3(1f, 0f, 0f), time, LeanTween.d);
	}

		public static LTDescr move(GameObject gameObject, Transform to, float time)
	{
		return LeanTween.pushNewTween(gameObject, Vector3.zero, time, LeanTween.options().setTo(to).setMoveToTransform());
	}

		public static LTDescr rotate(GameObject gameObject, Vector3 to, float time)
	{
		return LeanTween.pushNewTween(gameObject, to, time, LeanTween.options().setRotate());
	}

		public static LTDescr rotate(LTRect ltRect, float to, float time)
	{
		return LeanTween.pushNewTween(LeanTween.tweenEmpty, new Vector3(to, 0f, 0f), time, LeanTween.options().setGUIRotate().setRect(ltRect));
	}

		public static LTDescr rotateLocal(GameObject gameObject, Vector3 to, float time)
	{
		return LeanTween.pushNewTween(gameObject, to, time, LeanTween.options().setRotateLocal());
	}

		public static LTDescr rotateX(GameObject gameObject, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, LeanTween.options().setRotateX());
	}

		public static LTDescr rotateY(GameObject gameObject, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, LeanTween.options().setRotateY());
	}

		public static LTDescr rotateZ(GameObject gameObject, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, LeanTween.options().setRotateZ());
	}

		public static LTDescr rotateAround(GameObject gameObject, Vector3 axis, float add, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(add, 0f, 0f), time, LeanTween.options().setAxis(axis).setRotateAround());
	}

		public static LTDescr rotateAroundLocal(GameObject gameObject, Vector3 axis, float add, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(add, 0f, 0f), time, LeanTween.options().setRotateAroundLocal().setAxis(axis));
	}

		public static LTDescr scale(GameObject gameObject, Vector3 to, float time)
	{
		return LeanTween.pushNewTween(gameObject, to, time, LeanTween.options().setScale());
	}

		public static LTDescr scale(LTRect ltRect, Vector2 to, float time)
	{
		return LeanTween.pushNewTween(LeanTween.tweenEmpty, to, time, LeanTween.options().setGUIScale().setRect(ltRect));
	}

		public static LTDescr scaleX(GameObject gameObject, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, LeanTween.options().setScaleX());
	}

		public static LTDescr scaleY(GameObject gameObject, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, LeanTween.options().setScaleY());
	}

		public static LTDescr scaleZ(GameObject gameObject, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, LeanTween.options().setScaleZ());
	}

		public static LTDescr value(GameObject gameObject, float from, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, LeanTween.options().setCallback().setFrom(new Vector3(from, 0f, 0f)));
	}

		public static LTDescr value(float from, float to, float time)
	{
		return LeanTween.pushNewTween(LeanTween.tweenEmpty, new Vector3(to, 0f, 0f), time, LeanTween.options().setCallback().setFrom(new Vector3(from, 0f, 0f)));
	}

		public static LTDescr value(GameObject gameObject, Vector2 from, Vector2 to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to.x, to.y, 0f), time, LeanTween.options().setValue3().setTo(new Vector3(to.x, to.y, 0f)).setFrom(new Vector3(from.x, from.y, 0f)));
	}

		public static LTDescr value(GameObject gameObject, Vector3 from, Vector3 to, float time)
	{
		return LeanTween.pushNewTween(gameObject, to, time, LeanTween.options().setValue3().setFrom(from));
	}

		public static LTDescr value(GameObject gameObject, Color from, Color to, float time)
	{
		LTDescr ltdescr = LeanTween.pushNewTween(gameObject, new Vector3(1f, to.a, 0f), time, LeanTween.options().setCallbackColor().setPoint(new Vector3(to.r, to.g, to.b)).setFromColor(from).setHasInitialized(false));
		SpriteRenderer component = gameObject.GetComponent<SpriteRenderer>();
		ltdescr.spriteRen = component;
		return ltdescr;
	}

		public static LTDescr value(GameObject gameObject, Action<float> callOnUpdate, float from, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, LeanTween.options().setCallback().setTo(new Vector3(to, 0f, 0f)).setFrom(new Vector3(from, 0f, 0f)).setOnUpdate(callOnUpdate));
	}

		public static LTDescr value(GameObject gameObject, Action<float, float> callOnUpdateRatio, float from, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, LeanTween.options().setCallback().setTo(new Vector3(to, 0f, 0f)).setFrom(new Vector3(from, 0f, 0f)).setOnUpdateRatio(callOnUpdateRatio));
	}

		public static LTDescr value(GameObject gameObject, Action<Color> callOnUpdate, Color from, Color to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(1f, to.a, 0f), time, LeanTween.options().setCallbackColor().setPoint(new Vector3(to.r, to.g, to.b)).setAxis(new Vector3(from.r, from.g, from.b)).setFrom(new Vector3(0f, from.a, 0f)).setHasInitialized(false).setOnUpdateColor(callOnUpdate));
	}

		public static LTDescr value(GameObject gameObject, Action<Color, object> callOnUpdate, Color from, Color to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(1f, to.a, 0f), time, LeanTween.options().setCallbackColor().setPoint(new Vector3(to.r, to.g, to.b)).setAxis(new Vector3(from.r, from.g, from.b)).setFrom(new Vector3(0f, from.a, 0f)).setHasInitialized(false).setOnUpdateColor(callOnUpdate));
	}

		public static LTDescr value(GameObject gameObject, Action<Vector2> callOnUpdate, Vector2 from, Vector2 to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to.x, to.y, 0f), time, LeanTween.options().setValue3().setTo(new Vector3(to.x, to.y, 0f)).setFrom(new Vector3(from.x, from.y, 0f)).setOnUpdateVector2(callOnUpdate));
	}

		public static LTDescr value(GameObject gameObject, Action<Vector3> callOnUpdate, Vector3 from, Vector3 to, float time)
	{
		return LeanTween.pushNewTween(gameObject, to, time, LeanTween.options().setValue3().setTo(to).setFrom(from).setOnUpdateVector3(callOnUpdate));
	}

		public static LTDescr value(GameObject gameObject, Action<float, object> callOnUpdate, float from, float to, float time)
	{
		return LeanTween.pushNewTween(gameObject, new Vector3(to, 0f, 0f), time, LeanTween.options().setCallback().setTo(new Vector3(to, 0f, 0f)).setFrom(new Vector3(from, 0f, 0f)).setOnUpdate(callOnUpdate, gameObject));
	}

		public static LTDescr delayedSound(AudioClip audio, Vector3 pos, float volume)
	{
		return LeanTween.pushNewTween(LeanTween.tweenEmpty, pos, 0f, LeanTween.options().setDelayedSound().setTo(pos).setFrom(new Vector3(volume, 0f, 0f)).setAudio(audio));
	}

		public static LTDescr delayedSound(GameObject gameObject, AudioClip audio, Vector3 pos, float volume)
	{
		return LeanTween.pushNewTween(gameObject, pos, 0f, LeanTween.options().setDelayedSound().setTo(pos).setFrom(new Vector3(volume, 0f, 0f)).setAudio(audio));
	}

		public static LTDescr move(RectTransform rectTrans, Vector3 to, float time)
	{
		return LeanTween.pushNewTween(rectTrans.gameObject, to, time, LeanTween.options().setCanvasMove().setRect(rectTrans));
	}

		public static LTDescr moveX(RectTransform rectTrans, float to, float time)
	{
		return LeanTween.pushNewTween(rectTrans.gameObject, new Vector3(to, 0f, 0f), time, LeanTween.options().setCanvasMoveX().setRect(rectTrans));
	}

		public static LTDescr moveY(RectTransform rectTrans, float to, float time)
	{
		return LeanTween.pushNewTween(rectTrans.gameObject, new Vector3(to, 0f, 0f), time, LeanTween.options().setCanvasMoveY().setRect(rectTrans));
	}

		public static LTDescr moveZ(RectTransform rectTrans, float to, float time)
	{
		return LeanTween.pushNewTween(rectTrans.gameObject, new Vector3(to, 0f, 0f), time, LeanTween.options().setCanvasMoveZ().setRect(rectTrans));
	}

		public static LTDescr rotate(RectTransform rectTrans, float to, float time)
	{
		return LeanTween.pushNewTween(rectTrans.gameObject, new Vector3(to, 0f, 0f), time, LeanTween.options().setCanvasRotateAround().setRect(rectTrans).setAxis(Vector3.forward));
	}

		public static LTDescr rotate(RectTransform rectTrans, Vector3 to, float time)
	{
		return LeanTween.pushNewTween(rectTrans.gameObject, to, time, LeanTween.options().setCanvasRotateAround().setRect(rectTrans).setAxis(Vector3.forward));
	}

		public static LTDescr rotateAround(RectTransform rectTrans, Vector3 axis, float to, float time)
	{
		return LeanTween.pushNewTween(rectTrans.gameObject, new Vector3(to, 0f, 0f), time, LeanTween.options().setCanvasRotateAround().setRect(rectTrans).setAxis(axis));
	}

		public static LTDescr rotateAroundLocal(RectTransform rectTrans, Vector3 axis, float to, float time)
	{
		return LeanTween.pushNewTween(rectTrans.gameObject, new Vector3(to, 0f, 0f), time, LeanTween.options().setCanvasRotateAroundLocal().setRect(rectTrans).setAxis(axis));
	}

		public static LTDescr scale(RectTransform rectTrans, Vector3 to, float time)
	{
		return LeanTween.pushNewTween(rectTrans.gameObject, to, time, LeanTween.options().setCanvasScale().setRect(rectTrans));
	}

		public static LTDescr size(RectTransform rectTrans, Vector2 to, float time)
	{
		return LeanTween.pushNewTween(rectTrans.gameObject, to, time, LeanTween.options().setCanvasSizeDelta().setRect(rectTrans));
	}

		public static LTDescr alpha(RectTransform rectTrans, float to, float time)
	{
		return LeanTween.pushNewTween(rectTrans.gameObject, new Vector3(to, 0f, 0f), time, LeanTween.options().setCanvasAlpha().setRect(rectTrans));
	}

		public static LTDescr color(RectTransform rectTrans, Color to, float time)
	{
		return LeanTween.pushNewTween(rectTrans.gameObject, new Vector3(1f, to.a, 0f), time, LeanTween.options().setCanvasColor().setRect(rectTrans).setPoint(new Vector3(to.r, to.g, to.b)));
	}

		public static float tweenOnCurve(LTDescr tweenDescr, float ratioPassed)
	{
		return tweenDescr.from.x + tweenDescr.diff.x * tweenDescr.optional.animationCurve.Evaluate(ratioPassed);
	}

		public static Vector3 tweenOnCurveVector(LTDescr tweenDescr, float ratioPassed)
	{
		return new Vector3(tweenDescr.from.x + tweenDescr.diff.x * tweenDescr.optional.animationCurve.Evaluate(ratioPassed), tweenDescr.from.y + tweenDescr.diff.y * tweenDescr.optional.animationCurve.Evaluate(ratioPassed), tweenDescr.from.z + tweenDescr.diff.z * tweenDescr.optional.animationCurve.Evaluate(ratioPassed));
	}

		public static float easeOutQuadOpt(float start, float diff, float ratioPassed)
	{
		return -diff * ratioPassed * (ratioPassed - 2f) + start;
	}

		public static float easeInQuadOpt(float start, float diff, float ratioPassed)
	{
		return diff * ratioPassed * ratioPassed + start;
	}

		public static float easeInOutQuadOpt(float start, float diff, float ratioPassed)
	{
		ratioPassed /= 0.5f;
		if (ratioPassed < 1f)
		{
			return diff / 2f * ratioPassed * ratioPassed + start;
		}
		ratioPassed -= 1f;
		return -diff / 2f * (ratioPassed * (ratioPassed - 2f) - 1f) + start;
	}

		public static Vector3 easeInOutQuadOpt(Vector3 start, Vector3 diff, float ratioPassed)
	{
		ratioPassed /= 0.5f;
		if (ratioPassed < 1f)
		{
			return diff / 2f * ratioPassed * ratioPassed + start;
		}
		ratioPassed -= 1f;
		return -diff / 2f * (ratioPassed * (ratioPassed - 2f) - 1f) + start;
	}

		public static float linear(float start, float end, float val)
	{
		return Mathf.Lerp(start, end, val);
	}

		public static float clerp(float start, float end, float val)
	{
		float num = 0f;
		float num2 = 360f;
		float num3 = Mathf.Abs((num2 - num) / 2f);
		float result;
		if (end - start < -num3)
		{
			float num4 = (num2 - start + end) * val;
			result = start + num4;
		}
		else if (end - start > num3)
		{
			float num4 = -(num2 - end + start) * val;
			result = start + num4;
		}
		else
		{
			result = start + (end - start) * val;
		}
		return result;
	}

		public static float spring(float start, float end, float val)
	{
		val = Mathf.Clamp01(val);
		val = (Mathf.Sin(val * 3.1415927f * (0.2f + 2.5f * val * val * val)) * Mathf.Pow(1f - val, 2.2f) + val) * (1f + 1.2f * (1f - val));
		return start + (end - start) * val;
	}

		public static float easeInQuad(float start, float end, float val)
	{
		end -= start;
		return end * val * val + start;
	}

		public static float easeOutQuad(float start, float end, float val)
	{
		end -= start;
		return -end * val * (val - 2f) + start;
	}

		public static float easeInOutQuad(float start, float end, float val)
	{
		val /= 0.5f;
		end -= start;
		if (val < 1f)
		{
			return end / 2f * val * val + start;
		}
		val -= 1f;
		return -end / 2f * (val * (val - 2f) - 1f) + start;
	}

		public static float easeInOutQuadOpt2(float start, float diffBy2, float val, float val2)
	{
		val /= 0.5f;
		if (val < 1f)
		{
			return diffBy2 * val2 + start;
		}
		val -= 1f;
		return -diffBy2 * (val2 - 2f - 1f) + start;
	}

		public static float easeInCubic(float start, float end, float val)
	{
		end -= start;
		return end * val * val * val + start;
	}

		public static float easeOutCubic(float start, float end, float val)
	{
		val -= 1f;
		end -= start;
		return end * (val * val * val + 1f) + start;
	}

		public static float easeInOutCubic(float start, float end, float val)
	{
		val /= 0.5f;
		end -= start;
		if (val < 1f)
		{
			return end / 2f * val * val * val + start;
		}
		val -= 2f;
		return end / 2f * (val * val * val + 2f) + start;
	}

		public static float easeInQuart(float start, float end, float val)
	{
		end -= start;
		return end * val * val * val * val + start;
	}

		public static float easeOutQuart(float start, float end, float val)
	{
		val -= 1f;
		end -= start;
		return -end * (val * val * val * val - 1f) + start;
	}

		public static float easeInOutQuart(float start, float end, float val)
	{
		val /= 0.5f;
		end -= start;
		if (val < 1f)
		{
			return end / 2f * val * val * val * val + start;
		}
		val -= 2f;
		return -end / 2f * (val * val * val * val - 2f) + start;
	}

		public static float easeInQuint(float start, float end, float val)
	{
		end -= start;
		return end * val * val * val * val * val + start;
	}

		public static float easeOutQuint(float start, float end, float val)
	{
		val -= 1f;
		end -= start;
		return end * (val * val * val * val * val + 1f) + start;
	}

		public static float easeInOutQuint(float start, float end, float val)
	{
		val /= 0.5f;
		end -= start;
		if (val < 1f)
		{
			return end / 2f * val * val * val * val * val + start;
		}
		val -= 2f;
		return end / 2f * (val * val * val * val * val + 2f) + start;
	}

		public static float easeInSine(float start, float end, float val)
	{
		end -= start;
		return -end * Mathf.Cos(val / 1f * 1.5707964f) + end + start;
	}

		public static float easeOutSine(float start, float end, float val)
	{
		end -= start;
		return end * Mathf.Sin(val / 1f * 1.5707964f) + start;
	}

		public static float easeInOutSine(float start, float end, float val)
	{
		end -= start;
		return -end / 2f * (Mathf.Cos(3.1415927f * val / 1f) - 1f) + start;
	}

		public static float easeInExpo(float start, float end, float val)
	{
		end -= start;
		return end * Mathf.Pow(2f, 10f * (val / 1f - 1f)) + start;
	}

		public static float easeOutExpo(float start, float end, float val)
	{
		end -= start;
		return end * (-Mathf.Pow(2f, -10f * val / 1f) + 1f) + start;
	}

		public static float easeInOutExpo(float start, float end, float val)
	{
		val /= 0.5f;
		end -= start;
		if (val < 1f)
		{
			return end / 2f * Mathf.Pow(2f, 10f * (val - 1f)) + start;
		}
		val -= 1f;
		return end / 2f * (-Mathf.Pow(2f, -10f * val) + 2f) + start;
	}

		public static float easeInCirc(float start, float end, float val)
	{
		end -= start;
		return -end * (Mathf.Sqrt(1f - val * val) - 1f) + start;
	}

		public static float easeOutCirc(float start, float end, float val)
	{
		val -= 1f;
		end -= start;
		return end * Mathf.Sqrt(1f - val * val) + start;
	}

		public static float easeInOutCirc(float start, float end, float val)
	{
		val /= 0.5f;
		end -= start;
		if (val < 1f)
		{
			return -end / 2f * (Mathf.Sqrt(1f - val * val) - 1f) + start;
		}
		val -= 2f;
		return end / 2f * (Mathf.Sqrt(1f - val * val) + 1f) + start;
	}

		public static float easeInBounce(float start, float end, float val)
	{
		end -= start;
		float num = 1f;
		return end - LeanTween.easeOutBounce(0f, end, num - val) + start;
	}

		public static float easeOutBounce(float start, float end, float val)
	{
		val /= 1f;
		end -= start;
		if (val < 0.36363637f)
		{
			return end * (7.5625f * val * val) + start;
		}
		if (val < 0.72727275f)
		{
			val -= 0.54545456f;
			return end * (7.5625f * val * val + 0.75f) + start;
		}
		if ((double)val < 0.9090909090909091)
		{
			val -= 0.8181818f;
			return end * (7.5625f * val * val + 0.9375f) + start;
		}
		val -= 0.95454544f;
		return end * (7.5625f * val * val + 0.984375f) + start;
	}

		public static float easeInOutBounce(float start, float end, float val)
	{
		end -= start;
		float num = 1f;
		if (val < num / 2f)
		{
			return LeanTween.easeInBounce(0f, end, val * 2f) * 0.5f + start;
		}
		return LeanTween.easeOutBounce(0f, end, val * 2f - num) * 0.5f + end * 0.5f + start;
	}

		public static float easeInBack(float start, float end, float val, float overshoot = 1f)
	{
		end -= start;
		val /= 1f;
		float num = 1.70158f * overshoot;
		return end * val * val * ((num + 1f) * val - num) + start;
	}

		public static float easeOutBack(float start, float end, float val, float overshoot = 1f)
	{
		float num = 1.70158f * overshoot;
		end -= start;
		val = val / 1f - 1f;
		return end * (val * val * ((num + 1f) * val + num) + 1f) + start;
	}

		public static float easeInOutBack(float start, float end, float val, float overshoot = 1f)
	{
		float num = 1.70158f * overshoot;
		end -= start;
		val /= 0.5f;
		if (val < 1f)
		{
			num *= 1.525f * overshoot;
			return end / 2f * (val * val * ((num + 1f) * val - num)) + start;
		}
		val -= 2f;
		num *= 1.525f * overshoot;
		return end / 2f * (val * val * ((num + 1f) * val + num) + 2f) + start;
	}

		public static float easeInElastic(float start, float end, float val, float overshoot = 1f, float period = 0.3f)
	{
		end -= start;
		float num = 0f;
		if (val == 0f)
		{
			return start;
		}
		if (val == 1f)
		{
			return start + end;
		}
		float num2;
		if (num == 0f || num < Mathf.Abs(end))
		{
			num = end;
			num2 = period / 4f;
		}
		else
		{
			num2 = period / 6.2831855f * Mathf.Asin(end / num);
		}
		if (overshoot > 1f && val > 0.6f)
		{
			overshoot = 1f + (1f - val) / 0.4f * (overshoot - 1f);
		}
		val -= 1f;
		return start - num * Mathf.Pow(2f, 10f * val) * Mathf.Sin((val - num2) * 6.2831855f / period) * overshoot;
	}

		public static float easeOutElastic(float start, float end, float val, float overshoot = 1f, float period = 0.3f)
	{
		end -= start;
		float num = 0f;
		if (val == 0f)
		{
			return start;
		}
		if (val == 1f)
		{
			return start + end;
		}
		float num2;
		if (num == 0f || num < Mathf.Abs(end))
		{
			num = end;
			num2 = period / 4f;
		}
		else
		{
			num2 = period / 6.2831855f * Mathf.Asin(end / num);
		}
		if (overshoot > 1f && val < 0.4f)
		{
			overshoot = 1f + val / 0.4f * (overshoot - 1f);
		}
		return start + end + num * Mathf.Pow(2f, -10f * val) * Mathf.Sin((val - num2) * 6.2831855f / period) * overshoot;
	}

		public static float easeInOutElastic(float start, float end, float val, float overshoot = 1f, float period = 0.3f)
	{
		end -= start;
		float num = 0f;
		if (val == 0f)
		{
			return start;
		}
		val /= 0.5f;
		if (val == 2f)
		{
			return start + end;
		}
		float num2;
		if (num == 0f || num < Mathf.Abs(end))
		{
			num = end;
			num2 = period / 4f;
		}
		else
		{
			num2 = period / 6.2831855f * Mathf.Asin(end / num);
		}
		if (overshoot > 1f)
		{
			if (val < 0.2f)
			{
				overshoot = 1f + val / 0.2f * (overshoot - 1f);
			}
			else if (val > 0.8f)
			{
				overshoot = 1f + (1f - val) / 0.2f * (overshoot - 1f);
			}
		}
		if (val < 1f)
		{
			val -= 1f;
			return start - 0.5f * (num * Mathf.Pow(2f, 10f * val) * Mathf.Sin((val - num2) * 6.2831855f / period)) * overshoot;
		}
		val -= 1f;
		return end + start + num * Mathf.Pow(2f, -10f * val) * Mathf.Sin((val - num2) * 6.2831855f / period) * 0.5f * overshoot;
	}

		public static LTDescr followDamp(Transform trans, Transform target, LeanProp prop, float smoothTime, float maxSpeed = -1f)
	{
		LTDescr d = LeanTween.pushNewTween(trans.gameObject, Vector3.zero, float.MaxValue, LeanTween.options().setFollow().setTarget(target));
		switch (prop)
		{
		case LeanProp.position:
			d.diff = d.trans.position;
			d.easeInternal = delegate()
			{
				d.optional.axis = LeanSmooth.damp(d.optional.axis, d.toTrans.position, ref d.fromInternal, smoothTime, maxSpeed, Time.deltaTime);
				d.trans.position = d.optional.axis + d.toInternal;
			};
			break;
		case LeanProp.localPosition:
			d.optional.axis = d.trans.localPosition;
			d.easeInternal = delegate()
			{
				d.optional.axis = LeanSmooth.damp(d.optional.axis, d.toTrans.localPosition, ref d.fromInternal, smoothTime, maxSpeed, Time.deltaTime);
				d.trans.localPosition = d.optional.axis + d.toInternal;
			};
			break;
		case LeanProp.x:
			d.easeInternal = delegate()
			{
				d.trans.LeanSetPosX(LeanSmooth.damp(d.trans.position.x, d.toTrans.position.x, ref d.fromInternal.x, smoothTime, maxSpeed, Time.deltaTime));
			};
			break;
		case LeanProp.y:
			d.easeInternal = delegate()
			{
				d.trans.LeanSetPosY(LeanSmooth.damp(d.trans.position.y, d.toTrans.position.y, ref d.fromInternal.y, smoothTime, maxSpeed, Time.deltaTime));
			};
			break;
		case LeanProp.z:
			d.easeInternal = delegate()
			{
				d.trans.LeanSetPosZ(LeanSmooth.damp(d.trans.position.z, d.toTrans.position.z, ref d.fromInternal.z, smoothTime, maxSpeed, Time.deltaTime));
			};
			break;
		case LeanProp.localX:
			d.easeInternal = delegate()
			{
				d.trans.LeanSetLocalPosX(LeanSmooth.damp(d.trans.localPosition.x, d.toTrans.localPosition.x, ref d.fromInternal.x, smoothTime, maxSpeed, Time.deltaTime));
			};
			break;
		case LeanProp.localY:
			d.easeInternal = delegate()
			{
				d.trans.LeanSetLocalPosY(LeanSmooth.damp(d.trans.localPosition.y, d.toTrans.localPosition.y, ref d.fromInternal.y, smoothTime, maxSpeed, Time.deltaTime));
			};
			break;
		case LeanProp.localZ:
			d.easeInternal = delegate()
			{
				d.trans.LeanSetLocalPosZ(LeanSmooth.damp(d.trans.localPosition.z, d.toTrans.localPosition.z, ref d.fromInternal.z, smoothTime, maxSpeed, Time.deltaTime));
			};
			break;
		case LeanProp.scale:
			d.easeInternal = delegate()
			{
				d.trans.localScale = LeanSmooth.damp(d.trans.localScale, d.toTrans.localScale, ref d.fromInternal, smoothTime, maxSpeed, Time.deltaTime);
			};
			break;
		case LeanProp.color:
			d.easeInternal = delegate()
			{
				Color color = LeanSmooth.damp(d.trans.LeanColor(), d.toTrans.LeanColor(), ref d.optional.color, smoothTime, maxSpeed, Time.deltaTime);
				d.trans.GetComponent<Renderer>().material.color = color;
			};
			break;
		}
		return d;
	}

		public static LTDescr followSpring(Transform trans, Transform target, LeanProp prop, float smoothTime, float maxSpeed = -1f, float friction = 2f, float accelRate = 0.5f)
	{
		LTDescr d = LeanTween.pushNewTween(trans.gameObject, Vector3.zero, float.MaxValue, LeanTween.options().setFollow().setTarget(target));
		switch (prop)
		{
		case LeanProp.position:
			d.diff = d.trans.position;
			d.easeInternal = delegate()
			{
				d.diff = LeanSmooth.spring(d.diff, d.toTrans.position, ref d.fromInternal, smoothTime, maxSpeed, Time.deltaTime, friction, accelRate);
				d.trans.position = d.diff;
			};
			break;
		case LeanProp.localPosition:
			d.optional.axis = d.trans.localPosition;
			d.easeInternal = delegate()
			{
				d.optional.axis = LeanSmooth.spring(d.optional.axis, d.toTrans.localPosition, ref d.fromInternal, smoothTime, maxSpeed, Time.deltaTime, friction, accelRate);
				d.trans.localPosition = d.optional.axis + d.toInternal;
			};
			break;
		case LeanProp.x:
			d.easeInternal = delegate()
			{
				d.trans.LeanSetPosX(LeanSmooth.spring(d.trans.position.x, d.toTrans.position.x, ref d.fromInternal.x, smoothTime, maxSpeed, Time.deltaTime, friction, accelRate));
			};
			break;
		case LeanProp.y:
			d.easeInternal = delegate()
			{
				d.trans.LeanSetPosY(LeanSmooth.spring(d.trans.position.y, d.toTrans.position.y, ref d.fromInternal.y, smoothTime, maxSpeed, Time.deltaTime, friction, accelRate));
			};
			break;
		case LeanProp.z:
			d.easeInternal = delegate()
			{
				d.trans.LeanSetPosZ(LeanSmooth.spring(d.trans.position.z, d.toTrans.position.z, ref d.fromInternal.z, smoothTime, maxSpeed, Time.deltaTime, friction, accelRate));
			};
			break;
		case LeanProp.localX:
			d.easeInternal = delegate()
			{
				d.trans.LeanSetLocalPosX(LeanSmooth.spring(d.trans.localPosition.x, d.toTrans.localPosition.x, ref d.fromInternal.x, smoothTime, maxSpeed, Time.deltaTime, friction, accelRate));
			};
			break;
		case LeanProp.localY:
			d.easeInternal = delegate()
			{
				d.trans.LeanSetLocalPosY(LeanSmooth.spring(d.trans.localPosition.y, d.toTrans.localPosition.y, ref d.fromInternal.y, smoothTime, maxSpeed, Time.deltaTime, friction, accelRate));
			};
			break;
		case LeanProp.localZ:
			d.easeInternal = delegate()
			{
				d.trans.LeanSetLocalPosZ(LeanSmooth.spring(d.trans.localPosition.z, d.toTrans.localPosition.z, ref d.fromInternal.z, smoothTime, maxSpeed, Time.deltaTime, friction, accelRate));
			};
			break;
		case LeanProp.scale:
			d.easeInternal = delegate()
			{
				d.trans.localScale = LeanSmooth.spring(d.trans.localScale, d.toTrans.localScale, ref d.fromInternal, smoothTime, maxSpeed, Time.deltaTime, friction, accelRate);
			};
			break;
		case LeanProp.color:
			d.easeInternal = delegate()
			{
				Color color = LeanSmooth.spring(d.trans.LeanColor(), d.toTrans.LeanColor(), ref d.optional.color, smoothTime, maxSpeed, Time.deltaTime, friction, accelRate);
				d.trans.GetComponent<Renderer>().material.color = color;
			};
			break;
		}
		return d;
	}

		public static LTDescr followBounceOut(Transform trans, Transform target, LeanProp prop, float smoothTime, float maxSpeed = -1f, float friction = 2f, float accelRate = 0.5f, float hitDamping = 0.9f)
	{
		LTDescr d = LeanTween.pushNewTween(trans.gameObject, Vector3.zero, float.MaxValue, LeanTween.options().setFollow().setTarget(target));
		switch (prop)
		{
		case LeanProp.position:
			d.easeInternal = delegate()
			{
				d.optional.axis = LeanSmooth.bounceOut(d.optional.axis, d.toTrans.position, ref d.fromInternal, smoothTime, maxSpeed, Time.deltaTime, friction, accelRate, hitDamping);
				d.trans.position = d.optional.axis + d.toInternal;
			};
			break;
		case LeanProp.localPosition:
			d.optional.axis = d.trans.localPosition;
			d.easeInternal = delegate()
			{
				d.optional.axis = LeanSmooth.bounceOut(d.optional.axis, d.toTrans.localPosition, ref d.fromInternal, smoothTime, maxSpeed, Time.deltaTime, friction, accelRate, hitDamping);
				d.trans.localPosition = d.optional.axis + d.toInternal;
			};
			break;
		case LeanProp.x:
			d.easeInternal = delegate()
			{
				d.trans.LeanSetPosX(LeanSmooth.bounceOut(d.trans.position.x, d.toTrans.position.x, ref d.fromInternal.x, smoothTime, maxSpeed, Time.deltaTime, friction, accelRate, hitDamping));
			};
			break;
		case LeanProp.y:
			d.easeInternal = delegate()
			{
				d.trans.LeanSetPosY(LeanSmooth.bounceOut(d.trans.position.y, d.toTrans.position.y, ref d.fromInternal.y, smoothTime, maxSpeed, Time.deltaTime, friction, accelRate, hitDamping));
			};
			break;
		case LeanProp.z:
			d.easeInternal = delegate()
			{
				d.trans.LeanSetPosZ(LeanSmooth.bounceOut(d.trans.position.z, d.toTrans.position.z, ref d.fromInternal.z, smoothTime, maxSpeed, Time.deltaTime, friction, accelRate, hitDamping));
			};
			break;
		case LeanProp.localX:
			d.easeInternal = delegate()
			{
				d.trans.LeanSetLocalPosX(LeanSmooth.bounceOut(d.trans.localPosition.x, d.toTrans.localPosition.x, ref d.fromInternal.x, smoothTime, maxSpeed, Time.deltaTime, friction, accelRate, hitDamping));
			};
			break;
		case LeanProp.localY:
			d.easeInternal = delegate()
			{
				d.trans.LeanSetLocalPosY(LeanSmooth.bounceOut(d.trans.localPosition.y, d.toTrans.localPosition.y, ref d.fromInternal.y, smoothTime, maxSpeed, Time.deltaTime, friction, accelRate, hitDamping));
			};
			break;
		case LeanProp.localZ:
			d.easeInternal = delegate()
			{
				d.trans.LeanSetLocalPosZ(LeanSmooth.bounceOut(d.trans.localPosition.z, d.toTrans.localPosition.z, ref d.fromInternal.z, smoothTime, maxSpeed, Time.deltaTime, friction, accelRate, hitDamping));
			};
			break;
		case LeanProp.scale:
			d.easeInternal = delegate()
			{
				d.trans.localScale = LeanSmooth.bounceOut(d.trans.localScale, d.toTrans.localScale, ref d.fromInternal, smoothTime, maxSpeed, Time.deltaTime, friction, accelRate, hitDamping);
			};
			break;
		case LeanProp.color:
			d.easeInternal = delegate()
			{
				Color color = LeanSmooth.bounceOut(d.trans.LeanColor(), d.toTrans.LeanColor(), ref d.optional.color, smoothTime, maxSpeed, Time.deltaTime, friction, accelRate, hitDamping);
				d.trans.GetComponent<Renderer>().material.color = color;
			};
			break;
		}
		return d;
	}

		public static LTDescr followLinear(Transform trans, Transform target, LeanProp prop, float moveSpeed)
	{
		LTDescr d = LeanTween.pushNewTween(trans.gameObject, Vector3.zero, float.MaxValue, LeanTween.options().setFollow().setTarget(target));
		switch (prop)
		{
		case LeanProp.position:
			d.easeInternal = delegate()
			{
				d.trans.position = LeanSmooth.linear(d.trans.position, d.toTrans.position, moveSpeed, -1f);
			};
			break;
		case LeanProp.localPosition:
			d.optional.axis = d.trans.localPosition;
			d.easeInternal = delegate()
			{
				d.optional.axis = LeanSmooth.linear(d.optional.axis, d.toTrans.localPosition, moveSpeed, -1f);
				d.trans.localPosition = d.optional.axis + d.toInternal;
			};
			break;
		case LeanProp.x:
			d.easeInternal = delegate()
			{
				d.trans.LeanSetPosX(LeanSmooth.linear(d.trans.position.x, d.toTrans.position.x, moveSpeed, -1f));
			};
			break;
		case LeanProp.y:
			d.easeInternal = delegate()
			{
				d.trans.LeanSetPosY(LeanSmooth.linear(d.trans.position.y, d.toTrans.position.y, moveSpeed, -1f));
			};
			break;
		case LeanProp.z:
			d.easeInternal = delegate()
			{
				d.trans.LeanSetPosZ(LeanSmooth.linear(d.trans.position.z, d.toTrans.position.z, moveSpeed, -1f));
			};
			break;
		case LeanProp.localX:
			d.easeInternal = delegate()
			{
				d.trans.LeanSetLocalPosX(LeanSmooth.linear(d.trans.localPosition.x, d.toTrans.localPosition.x, moveSpeed, -1f));
			};
			break;
		case LeanProp.localY:
			d.easeInternal = delegate()
			{
				d.trans.LeanSetLocalPosY(LeanSmooth.linear(d.trans.localPosition.y, d.toTrans.localPosition.y, moveSpeed, -1f));
			};
			break;
		case LeanProp.localZ:
			d.easeInternal = delegate()
			{
				d.trans.LeanSetLocalPosZ(LeanSmooth.linear(d.trans.localPosition.z, d.toTrans.localPosition.z, moveSpeed, -1f));
			};
			break;
		case LeanProp.scale:
			d.easeInternal = delegate()
			{
				d.trans.localScale = LeanSmooth.linear(d.trans.localScale, d.toTrans.localScale, moveSpeed, -1f);
			};
			break;
		case LeanProp.color:
			d.easeInternal = delegate()
			{
				Color color = LeanSmooth.linear(d.trans.LeanColor(), d.toTrans.LeanColor(), moveSpeed);
				d.trans.GetComponent<Renderer>().material.color = color;
			};
			break;
		}
		return d;
	}

		public static void addListener(int eventId, Action<LTEvent> callback)
	{
		LeanTween.addListener(LeanTween.tweenEmpty, eventId, callback);
	}

		public static void addListener(GameObject caller, int eventId, Action<LTEvent> callback)
	{
		if (LeanTween.eventListeners == null)
		{
			LeanTween.INIT_LISTENERS_MAX = LeanTween.LISTENERS_MAX;
			LeanTween.eventListeners = new Action<LTEvent>[LeanTween.EVENTS_MAX * LeanTween.LISTENERS_MAX];
			LeanTween.goListeners = new GameObject[LeanTween.EVENTS_MAX * LeanTween.LISTENERS_MAX];
		}
		LeanTween.i = 0;
		while (LeanTween.i < LeanTween.INIT_LISTENERS_MAX)
		{
			int num = eventId * LeanTween.INIT_LISTENERS_MAX + LeanTween.i;
			if (LeanTween.goListeners[num] == null || LeanTween.eventListeners[num] == null)
			{
				LeanTween.eventListeners[num] = callback;
				LeanTween.goListeners[num] = caller;
				if (LeanTween.i >= LeanTween.eventsMaxSearch)
				{
					LeanTween.eventsMaxSearch = LeanTween.i + 1;
				}
				return;
			}
			if (LeanTween.goListeners[num] == caller && object.Equals(LeanTween.eventListeners[num], callback))
			{
				return;
			}
			LeanTween.i++;
		}
		Debug.LogError("You ran out of areas to add listeners, consider increasing LISTENERS_MAX, ex: LeanTween.LISTENERS_MAX = " + LeanTween.LISTENERS_MAX * 2);
	}

		public static bool removeListener(int eventId, Action<LTEvent> callback)
	{
		return LeanTween.removeListener(LeanTween.tweenEmpty, eventId, callback);
	}

		public static bool removeListener(int eventId)
	{
		int num = eventId * LeanTween.INIT_LISTENERS_MAX + LeanTween.i;
		LeanTween.eventListeners[num] = null;
		LeanTween.goListeners[num] = null;
		return true;
	}

		public static bool removeListener(GameObject caller, int eventId, Action<LTEvent> callback)
	{
		LeanTween.i = 0;
		while (LeanTween.i < LeanTween.eventsMaxSearch)
		{
			int num = eventId * LeanTween.INIT_LISTENERS_MAX + LeanTween.i;
			if (LeanTween.goListeners[num] == caller && object.Equals(LeanTween.eventListeners[num], callback))
			{
				LeanTween.eventListeners[num] = null;
				LeanTween.goListeners[num] = null;
				return true;
			}
			LeanTween.i++;
		}
		return false;
	}

		public static void dispatchEvent(int eventId)
	{
		LeanTween.dispatchEvent(eventId, null);
	}

		public static void dispatchEvent(int eventId, object data)
	{
		for (int i = 0; i < LeanTween.eventsMaxSearch; i++)
		{
			int num = eventId * LeanTween.INIT_LISTENERS_MAX + i;
			if (LeanTween.eventListeners[num] != null)
			{
				if (LeanTween.goListeners[num])
				{
					LeanTween.eventListeners[num](new LTEvent(eventId, data));
				}
				else
				{
					LeanTween.eventListeners[num] = null;
				}
			}
		}
	}

		public static bool throwErrors = true;

		public static float tau = 6.2831855f;

		public static float PI_DIV2 = 1.5707964f;

		private static LTSeq[] sequences;

		private static LTDescr[] tweens;

		private static int[] tweensFinished;

		private static int[] tweensFinishedIds;

		private static LTDescr tween;

		private static int tweenMaxSearch = -1;

		private static int maxTweens = 400;

		private static int maxSequences = 400;

		private static int frameRendered = -1;

		private static GameObject _tweenEmpty;

		public static float dtEstimated = -1f;

		public static float dtManual;

		public static float dtActual;

		private static uint global_counter = 0U;

		private static int i;

		private static int j;

		private static int finishedCnt;

		public static AnimationCurve punch = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f),
		new Keyframe(0.112586f, 0.9976035f),
		new Keyframe(0.3120486f, -0.1720615f),
		new Keyframe(0.4316337f, 0.07030682f),
		new Keyframe(0.5524869f, -0.03141804f),
		new Keyframe(0.6549395f, 0.003909959f),
		new Keyframe(0.770987f, -0.009817753f),
		new Keyframe(0.8838775f, 0.001939224f),
		new Keyframe(1f, 0f)
	});

		public static AnimationCurve shake = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f),
		new Keyframe(0.25f, 1f),
		new Keyframe(0.75f, -1f),
		new Keyframe(1f, 0f)
	});

		private static int maxTweenReached;

		public static int startSearch = 0;

		public static LTDescr d;

		private static Action<LTEvent>[] eventListeners;

		private static GameObject[] goListeners;

		private static int eventsMaxSearch = 0;

		public static int EVENTS_MAX = 10;

		public static int LISTENERS_MAX = 10;

		private static int INIT_LISTENERS_MAX = LeanTween.LISTENERS_MAX;
}
