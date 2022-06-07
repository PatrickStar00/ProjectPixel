using System;
using UnityEngine;

public class TestingPunch : MonoBehaviour
{
		private void Start()
	{
		Debug.Log("exported curve:" + this.curveToString(this.exportCurve));
	}

		private void Update()
	{
		LeanTween.dtManual = Time.deltaTime;
		if (Input.GetKeyDown(113))
		{
			LeanTween.moveLocalX(base.gameObject, 5f, 1f).setOnComplete(delegate()
			{
				Debug.Log("on complete move local X");
			}).setOnCompleteOnStart(true);
			GameObject gameObject = GameObject.Find("DirectionalLight");
			Light lt = gameObject.GetComponent<Light>();
			LeanTween.value(lt.gameObject, lt.intensity, 0f, 1.5f).setEase(LeanTweenType.linear).setLoopPingPong().setRepeat(-1).setOnUpdate(delegate(float val)
			{
				lt.intensity = val;
			});
		}
		if (Input.GetKeyDown(115))
		{
			MonoBehaviour.print("scale punch!");
			TestingPunch.tweenStatically(base.gameObject);
			LeanTween.scale(base.gameObject, new Vector3(1.15f, 1.15f, 1.15f), 0.6f);
			LeanTween.rotateAround(base.gameObject, Vector3.forward, -360f, 0.3f).setOnComplete(delegate()
			{
				LeanTween.rotateAround(base.gameObject, Vector3.forward, -360f, 0.4f).setOnComplete(delegate()
				{
					LeanTween.scale(base.gameObject, new Vector3(1f, 1f, 1f), 0.1f);
					LeanTween.value(base.gameObject, delegate(float v)
					{
					}, 0f, 1f, 0.3f).setDelay(1f);
				});
			});
		}
		if (Input.GetKeyDown(116))
		{
			Vector3[] to = new Vector3[]
			{
				new Vector3(-1f, 0f, 0f),
				new Vector3(0f, 0f, 0f),
				new Vector3(4f, 0f, 0f),
				new Vector3(20f, 0f, 0f)
			};
			this.descr = LeanTween.move(base.gameObject, to, 15f).setOrientToPath(true).setDirection(1f).setOnComplete(delegate()
			{
				Debug.Log("move path finished");
			});
		}
		if (Input.GetKeyDown(121))
		{
			this.descr.setDirection(-this.descr.direction);
		}
		if (Input.GetKeyDown(114))
		{
			LeanTween.rotateAroundLocal(base.gameObject, base.transform.forward, -80f, 5f).setPoint(new Vector3(1.25f, 0f, 0f));
			MonoBehaviour.print("rotate punch!");
		}
		if (Input.GetKeyDown(109))
		{
			MonoBehaviour.print("move punch!");
			Time.timeScale = 0.25f;
			float start = Time.realtimeSinceStartup;
			LeanTween.moveX(base.gameObject, 1f, 1f).setOnComplete(new Action<object>(this.destroyOnComp)).setOnCompleteParam(base.gameObject).setOnComplete(delegate()
			{
				float realtimeSinceStartup = Time.realtimeSinceStartup;
				float num = realtimeSinceStartup - start;
				Debug.Log(string.Concat(new object[]
				{
					"start:",
					start,
					" end:",
					realtimeSinceStartup,
					" diff:",
					num,
					" x:",
					this.gameObject.transform.position.x
				}));
			}).setEase(LeanTweenType.easeInBack).setOvershoot(this.overShootValue).setPeriod(0.3f);
		}
		if (Input.GetKeyDown(99))
		{
			LeanTween.color(base.gameObject, new Color(1f, 0f, 0f, 0.5f), 1f);
			Color to2;
			to2..ctor(Random.Range(0f, 1f), 0f, Random.Range(0f, 1f), 0f);
			LeanTween.color(GameObject.Find("LCharacter"), to2, 4f).setLoopPingPong(1).setEase(LeanTweenType.easeOutBounce);
		}
		if (Input.GetKeyDown(101))
		{
			LeanTween.delayedCall(base.gameObject, 0.3f, new Action<object>(this.delayedMethod)).setRepeat(4).setOnCompleteOnRepeat(true).setOnCompleteParam("hi");
		}
		if (Input.GetKeyDown(118))
		{
			LeanTween.value(base.gameObject, new Action<Color>(this.updateColor), new Color(1f, 0f, 0f, 1f), Color.blue, 4f);
		}
		if (Input.GetKeyDown(112))
		{
			LeanTween.delayedCall(0.05f, new Action<object>(this.enterMiniGameStart)).setOnCompleteParam(new object[]
			{
				string.Concat(5)
			});
		}
		if (Input.GetKeyDown(117))
		{
			LeanTween.value(base.gameObject, delegate(Vector2 val)
			{
				base.transform.position = new Vector3(val.x, base.transform.position.y, base.transform.position.z);
			}, new Vector2(0f, 0f), new Vector2(5f, 100f), 1f).setEase(LeanTweenType.easeOutBounce);
			GameObject l = GameObject.Find("LCharacter");
			Debug.Log(string.Concat(new object[]
			{
				"x:",
				l.transform.position.x,
				" y:",
				l.transform.position.y
			}));
			LeanTween.value(l, new Vector2(l.transform.position.x, l.transform.position.y), new Vector2(l.transform.position.x, l.transform.position.y + 5f), 1f).setOnUpdate(delegate(Vector2 val)
			{
				Debug.Log("tweening vec2 val:" + val);
				l.transform.position = new Vector3(val.x, val.y, this.transform.position.z);
			}, null);
		}
	}

		private static void tweenStatically(GameObject gameObject)
	{
		Debug.Log("Starting to tween...");
		LeanTween.value(gameObject, delegate(float val)
		{
			Debug.Log("tweening val:" + val);
		}, 0f, 1f, 1f);
	}

		private void enterMiniGameStart(object val)
	{
		int num = int.Parse((string)((object[])val)[0]);
		Debug.Log("level:" + num);
	}

		private void updateColor(Color c)
	{
		GameObject.Find("LCharacter").GetComponent<Renderer>().material.color = c;
	}

		private void delayedMethod(object myVal)
	{
		string text = myVal as string;
		Debug.Log(string.Concat(new object[]
		{
			"delayed call:",
			Time.time,
			" myVal:",
			text
		}));
	}

		private void destroyOnComp(object p)
	{
		Object.Destroy((GameObject)p);
	}

		private string curveToString(AnimationCurve curve)
	{
		string text = "";
		for (int i = 0; i < curve.length; i++)
		{
			text = string.Concat(new object[]
			{
				text,
				"new Keyframe(",
				curve[i].time,
				"f, ",
				curve[i].value,
				"f)"
			});
			if (i < curve.length - 1)
			{
				text += ", ";
			}
		}
		return "new AnimationCurve( " + text + " )";
	}

		public AnimationCurve exportCurve;

		public float overShootValue = 1f;

		private LTDescr descr;
}
