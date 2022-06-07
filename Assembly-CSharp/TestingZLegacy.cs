using System;
using System.Collections;
using UnityEngine;

public class TestingZLegacy : MonoBehaviour
{
		private void Awake()
	{
	}

		private void Start()
	{
		this.ltLogo = GameObject.Find("LeanTweenLogo");
		LeanTween.delayedCall(1f, new Action(this.cycleThroughExamples));
		this.origin = this.ltLogo.transform.position;
	}

		private void pauseNow()
	{
		Time.timeScale = 0f;
		Debug.Log("pausing");
	}

		private void OnGUI()
	{
		string text = this.useEstimatedTime ? "useEstimatedTime" : ("timeScale:" + Time.timeScale);
		GUI.Label(new Rect(0.03f * (float)Screen.width, 0.03f * (float)Screen.height, 0.5f * (float)Screen.width, 0.3f * (float)Screen.height), text);
	}

		private void endlessCallback()
	{
		Debug.Log("endless");
	}

		private void cycleThroughExamples()
	{
		if (this.exampleIter == 0)
		{
			int num = (int)(this.timingType + 1);
			if (num > 4)
			{
				num = 0;
			}
			this.timingType = (TestingZLegacy.TimingType)num;
			this.useEstimatedTime = (this.timingType == TestingZLegacy.TimingType.IgnoreTimeScale);
			Time.timeScale = (this.useEstimatedTime ? 0f : 1f);
			if (this.timingType == TestingZLegacy.TimingType.HalfTimeScale)
			{
				Time.timeScale = 0.5f;
			}
			if (this.timingType == TestingZLegacy.TimingType.VariableTimeScale)
			{
				this.descrTimeScaleChangeId = LeanTween.value(base.gameObject, 0.01f, 10f, 3f).setOnUpdate(delegate(float val)
				{
					Time.timeScale = val;
				}).setEase(LeanTweenType.easeInQuad).setUseEstimatedTime(true).setRepeat(-1).id;
			}
			else
			{
				Debug.Log("cancel variable time");
				LeanTween.cancel(this.descrTimeScaleChangeId);
			}
		}
		base.gameObject.BroadcastMessage(this.exampleFunctions[this.exampleIter]);
		float delayTime = 1.1f;
		LeanTween.delayedCall(base.gameObject, delayTime, new Action(this.cycleThroughExamples)).setUseEstimatedTime(this.useEstimatedTime);
		this.exampleIter = ((this.exampleIter + 1 >= this.exampleFunctions.Length) ? 0 : (this.exampleIter + 1));
	}

		public void updateValue3Example()
	{
		Debug.Log("updateValue3Example Time:" + Time.time);
		LeanTween.value(base.gameObject, new Action<Vector3>(this.updateValue3ExampleCallback), new Vector3(0f, 270f, 0f), new Vector3(30f, 270f, 180f), 0.5f).setEase(LeanTweenType.easeInBounce).setRepeat(2).setLoopPingPong().setOnUpdateVector3(new Action<Vector3>(this.updateValue3ExampleUpdate)).setUseEstimatedTime(this.useEstimatedTime);
	}

		public void updateValue3ExampleUpdate(Vector3 val)
	{
	}

		public void updateValue3ExampleCallback(Vector3 val)
	{
		this.ltLogo.transform.eulerAngles = val;
	}

		public void loopTestClamp()
	{
		Debug.Log("loopTestClamp Time:" + Time.time);
		GameObject gameObject = GameObject.Find("Cube1");
		gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		LeanTween.scaleZ(gameObject, 4f, 1f).setEase(LeanTweenType.easeOutElastic).setRepeat(7).setLoopClamp().setUseEstimatedTime(this.useEstimatedTime);
	}

		public void loopTestPingPong()
	{
		Debug.Log("loopTestPingPong Time:" + Time.time);
		GameObject gameObject = GameObject.Find("Cube2");
		gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		LeanTween.scaleY(gameObject, 4f, 1f).setEase(LeanTweenType.easeOutQuad).setLoopPingPong(4).setUseEstimatedTime(this.useEstimatedTime);
	}

		public void colorExample()
	{
		LeanTween.color(GameObject.Find("LCharacter"), new Color(1f, 0f, 0f, 0.5f), 0.5f).setEase(LeanTweenType.easeOutBounce).setRepeat(2).setLoopPingPong().setUseEstimatedTime(this.useEstimatedTime);
	}

		public void moveOnACurveExample()
	{
		Debug.Log("moveOnACurveExample Time:" + Time.time);
		Vector3[] to = new Vector3[]
		{
			this.origin,
			this.pt1.position,
			this.pt2.position,
			this.pt3.position,
			this.pt3.position,
			this.pt4.position,
			this.pt5.position,
			this.origin
		};
		LeanTween.move(this.ltLogo, to, 1f).setEase(LeanTweenType.easeOutQuad).setOrientToPath(true).setUseEstimatedTime(this.useEstimatedTime);
	}

		public void customTweenExample()
	{
		Debug.Log(string.Concat(new object[]
		{
			"customTweenExample starting pos:",
			this.ltLogo.transform.position,
			" origin:",
			this.origin
		}));
		LeanTween.moveX(this.ltLogo, -10f, 0.5f).setEase(this.customAnimationCurve).setUseEstimatedTime(this.useEstimatedTime);
		LeanTween.moveX(this.ltLogo, 0f, 0.5f).setEase(this.customAnimationCurve).setDelay(0.5f).setUseEstimatedTime(this.useEstimatedTime);
	}

		public void moveExample()
	{
		Debug.Log("moveExample");
		LeanTween.move(this.ltLogo, new Vector3(-2f, -1f, 0f), 0.5f).setUseEstimatedTime(this.useEstimatedTime);
		LeanTween.move(this.ltLogo, this.origin, 0.5f).setDelay(0.5f).setUseEstimatedTime(this.useEstimatedTime);
	}

		public void rotateExample()
	{
		Debug.Log("rotateExample");
		Hashtable hashtable = new Hashtable();
		hashtable.Add("yo", 5.0);
		LeanTween.rotate(this.ltLogo, new Vector3(0f, 360f, 0f), 1f).setEase(LeanTweenType.easeOutQuad).setOnComplete(new Action<object>(this.rotateFinished)).setOnCompleteParam(hashtable).setOnUpdate(new Action<float>(this.rotateOnUpdate)).setUseEstimatedTime(this.useEstimatedTime);
	}

		public void rotateOnUpdate(float val)
	{
	}

		public void rotateFinished(object hash)
	{
		Hashtable hashtable = hash as Hashtable;
		Debug.Log("rotateFinished hash:" + hashtable["yo"]);
	}

		public void scaleExample()
	{
		Debug.Log("scaleExample");
		Vector3 localScale = this.ltLogo.transform.localScale;
		LeanTween.scale(this.ltLogo, new Vector3(localScale.x + 0.2f, localScale.y + 0.2f, localScale.z + 0.2f), 1f).setEase(LeanTweenType.easeOutBounce).setUseEstimatedTime(this.useEstimatedTime);
	}

		public void updateValueExample()
	{
		Debug.Log("updateValueExample");
		Hashtable hashtable = new Hashtable();
		hashtable.Add("message", "hi");
		LeanTween.value(base.gameObject, new Action<float, object>(this.updateValueExampleCallback), this.ltLogo.transform.eulerAngles.y, 270f, 1f).setEase(LeanTweenType.easeOutElastic).setOnUpdateParam(hashtable).setUseEstimatedTime(this.useEstimatedTime);
	}

		public void updateValueExampleCallback(float val, object hash)
	{
		Vector3 eulerAngles = this.ltLogo.transform.eulerAngles;
		eulerAngles.y = val;
		this.ltLogo.transform.eulerAngles = eulerAngles;
	}

		public void delayedCallExample()
	{
		Debug.Log("delayedCallExample");
		LeanTween.delayedCall(0.5f, new Action(this.delayedCallExampleCallback)).setUseEstimatedTime(this.useEstimatedTime);
	}

		public void delayedCallExampleCallback()
	{
		Debug.Log("Delayed function was called");
		Vector3 localScale = this.ltLogo.transform.localScale;
		LeanTween.scale(this.ltLogo, new Vector3(localScale.x - 0.2f, localScale.y - 0.2f, localScale.z - 0.2f), 0.5f).setEase(LeanTweenType.easeInOutCirc).setUseEstimatedTime(this.useEstimatedTime);
	}

		public void alphaExample()
	{
		Debug.Log("alphaExample");
		GameObject gameObject = GameObject.Find("LCharacter");
		LeanTween.alpha(gameObject, 0f, 0.5f).setUseEstimatedTime(this.useEstimatedTime);
		LeanTween.alpha(gameObject, 1f, 0.5f).setDelay(0.5f).setUseEstimatedTime(this.useEstimatedTime);
	}

		public void moveLocalExample()
	{
		Debug.Log("moveLocalExample");
		GameObject gameObject = GameObject.Find("LCharacter");
		Vector3 localPosition = gameObject.transform.localPosition;
		LeanTween.moveLocal(gameObject, new Vector3(0f, 2f, 0f), 0.5f).setUseEstimatedTime(this.useEstimatedTime);
		LeanTween.moveLocal(gameObject, localPosition, 0.5f).setDelay(0.5f).setUseEstimatedTime(this.useEstimatedTime);
	}

		public void rotateAroundExample()
	{
		Debug.Log("rotateAroundExample");
		LeanTween.rotateAround(GameObject.Find("LCharacter"), Vector3.up, 360f, 1f).setUseEstimatedTime(this.useEstimatedTime);
	}

		public void loopPause()
	{
		LeanTween.pause(GameObject.Find("Cube1"));
	}

		public void loopResume()
	{
		LeanTween.resume(GameObject.Find("Cube1"));
	}

		public void punchTest()
	{
		LeanTween.moveX(this.ltLogo, 7f, 1f).setEase(LeanTweenType.punch).setUseEstimatedTime(this.useEstimatedTime);
	}

		public AnimationCurve customAnimationCurve;

		public Transform pt1;

		public Transform pt2;

		public Transform pt3;

		public Transform pt4;

		public Transform pt5;

		private int exampleIter;

		private string[] exampleFunctions = new string[]
	{
		"updateValue3Example",
		"loopTestClamp",
		"loopTestPingPong",
		"moveOnACurveExample",
		"customTweenExample",
		"moveExample",
		"rotateExample",
		"scaleExample",
		"updateValueExample",
		"delayedCallExample",
		"alphaExample",
		"moveLocalExample",
		"rotateAroundExample",
		"colorExample"
	};

		public bool useEstimatedTime = true;

		private GameObject ltLogo;

		private TestingZLegacy.TimingType timingType;

		private int descrTimeScaleChangeId;

		private Vector3 origin;

		// (Invoke) Token: 0x0600095E RID: 2398
	public delegate void NextFunc();

		public enum TimingType
	{
				SteadyNormalTime,
				IgnoreTimeScale,
				HalfTimeScale,
				VariableTimeScale,
				Length
	}
}
