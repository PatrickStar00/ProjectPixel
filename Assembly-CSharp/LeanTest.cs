using System;
using UnityEngine;

public class LeanTest
{
		public static void debug(string name, bool didPass, string failExplaination = null)
	{
		LeanTest.expect(didPass, name, failExplaination);
	}

		public static void expect(bool didPass, string definition, string failExplaination = null)
	{
		float num = LeanTest.printOutLength(definition);
		int totalWidth = 40 - (int)(num * 1.05f);
		string text = "".PadRight(totalWidth, "_"[0]);
		string text2 = string.Concat(new string[]
		{
			LeanTest.formatB(definition),
			" ",
			text,
			" [ ",
			didPass ? LeanTest.formatC("pass", "green") : LeanTest.formatC("fail", "red"),
			" ]"
		});
		if (!didPass && failExplaination != null)
		{
			text2 = text2 + " - " + failExplaination;
		}
		Debug.Log(text2);
		if (didPass)
		{
			LeanTest.passes++;
		}
		LeanTest.tests++;
		if (LeanTest.tests == LeanTest.expected && !LeanTest.testsFinished)
		{
			LeanTest.overview();
		}
		else if (LeanTest.tests > LeanTest.expected)
		{
			Debug.Log(LeanTest.formatB("Too many tests for a final report!") + " set LeanTest.expected = " + LeanTest.tests);
		}
		if (!LeanTest.timeoutStarted)
		{
			LeanTest.timeoutStarted = true;
			GameObject gameObject = new GameObject();
			gameObject.name = "~LeanTest";
			(gameObject.AddComponent(typeof(LeanTester)) as LeanTester).timeout = LeanTest.timeout;
			gameObject.hideFlags = 61;
		}
	}

		public static string padRight(int len)
	{
		string text = "";
		for (int i = 0; i < len; i++)
		{
			text += "_";
		}
		return text;
	}

		public static float printOutLength(string str)
	{
		float num = 0f;
		for (int i = 0; i < str.Length; i++)
		{
			if (str[i] == "I"[0])
			{
				num += 0.5f;
			}
			else if (str[i] == "J"[0])
			{
				num += 0.85f;
			}
			else
			{
				num += 1f;
			}
		}
		return num;
	}

		public static string formatBC(string str, string color)
	{
		return LeanTest.formatC(LeanTest.formatB(str), color);
	}

		public static string formatB(string str)
	{
		return "<b>" + str + "</b>";
	}

		public static string formatC(string str, string color)
	{
		return string.Concat(new string[]
		{
			"<color=",
			color,
			">",
			str,
			"</color>"
		});
	}

		public static void overview()
	{
		LeanTest.testsFinished = true;
		int num = LeanTest.expected - LeanTest.passes;
		string text = (num > 0) ? LeanTest.formatBC(string.Concat(num), "red") : string.Concat(num);
		Debug.Log(string.Concat(new string[]
		{
			LeanTest.formatB("Final Report:"),
			" _____________________ PASSED: ",
			LeanTest.formatBC(string.Concat(LeanTest.passes), "green"),
			" FAILED: ",
			text,
			" "
		}));
	}

		public static int expected = 0;

		private static int tests = 0;

		private static int passes = 0;

		public static float timeout = 15f;

		public static bool timeoutStarted = false;

		public static bool testsFinished = false;
}
