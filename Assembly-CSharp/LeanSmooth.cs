using System;
using UnityEngine;

public class LeanSmooth
{
		public static float damp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed = -1f, float deltaTime = -1f)
	{
		if (deltaTime < 0f)
		{
			deltaTime = Time.deltaTime;
		}
		smoothTime = Mathf.Max(0.0001f, smoothTime);
		float num = 2f / smoothTime;
		float num2 = num * deltaTime;
		float num3 = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
		float num4 = current - target;
		float num5 = target;
		if (maxSpeed > 0f)
		{
			float num6 = maxSpeed * smoothTime;
			num4 = Mathf.Clamp(num4, -num6, num6);
		}
		target = current - num4;
		float num7 = (currentVelocity + num * num4) * deltaTime;
		currentVelocity = (currentVelocity - num * num7) * num3;
		float num8 = target + (num4 + num7) * num3;
		if (num5 - current > 0f == num8 > num5)
		{
			num8 = num5;
			currentVelocity = (num8 - num5) / deltaTime;
		}
		return num8;
	}

		public static Vector3 damp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, float maxSpeed = -1f, float deltaTime = -1f)
	{
		float num = LeanSmooth.damp(current.x, target.x, ref currentVelocity.x, smoothTime, maxSpeed, deltaTime);
		float num2 = LeanSmooth.damp(current.y, target.y, ref currentVelocity.y, smoothTime, maxSpeed, deltaTime);
		float num3 = LeanSmooth.damp(current.z, target.z, ref currentVelocity.z, smoothTime, maxSpeed, deltaTime);
		return new Vector3(num, num2, num3);
	}

		public static Color damp(Color current, Color target, ref Color currentVelocity, float smoothTime, float maxSpeed = -1f, float deltaTime = -1f)
	{
		float num = LeanSmooth.damp(current.r, target.r, ref currentVelocity.r, smoothTime, maxSpeed, deltaTime);
		float num2 = LeanSmooth.damp(current.g, target.g, ref currentVelocity.g, smoothTime, maxSpeed, deltaTime);
		float num3 = LeanSmooth.damp(current.b, target.b, ref currentVelocity.b, smoothTime, maxSpeed, deltaTime);
		float num4 = LeanSmooth.damp(current.a, target.a, ref currentVelocity.a, smoothTime, maxSpeed, deltaTime);
		return new Color(num, num2, num3, num4);
	}

		public static float spring(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed = -1f, float deltaTime = -1f, float friction = 2f, float accelRate = 0.5f)
	{
		if (deltaTime < 0f)
		{
			deltaTime = Time.deltaTime;
		}
		float num = target - current;
		currentVelocity += deltaTime / smoothTime * accelRate * num;
		currentVelocity *= 1f - deltaTime * friction;
		if (maxSpeed > 0f && maxSpeed < Mathf.Abs(currentVelocity))
		{
			currentVelocity = maxSpeed * Mathf.Sign(currentVelocity);
		}
		return current + currentVelocity;
	}

		public static Vector3 spring(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, float maxSpeed = -1f, float deltaTime = -1f, float friction = 2f, float accelRate = 0.5f)
	{
		float num = LeanSmooth.spring(current.x, target.x, ref currentVelocity.x, smoothTime, maxSpeed, deltaTime, friction, accelRate);
		float num2 = LeanSmooth.spring(current.y, target.y, ref currentVelocity.y, smoothTime, maxSpeed, deltaTime, friction, accelRate);
		float num3 = LeanSmooth.spring(current.z, target.z, ref currentVelocity.z, smoothTime, maxSpeed, deltaTime, friction, accelRate);
		return new Vector3(num, num2, num3);
	}

		public static Color spring(Color current, Color target, ref Color currentVelocity, float smoothTime, float maxSpeed = -1f, float deltaTime = -1f, float friction = 2f, float accelRate = 0.5f)
	{
		float num = LeanSmooth.spring(current.r, target.r, ref currentVelocity.r, smoothTime, maxSpeed, deltaTime, friction, accelRate);
		float num2 = LeanSmooth.spring(current.g, target.g, ref currentVelocity.g, smoothTime, maxSpeed, deltaTime, friction, accelRate);
		float num3 = LeanSmooth.spring(current.b, target.b, ref currentVelocity.b, smoothTime, maxSpeed, deltaTime, friction, accelRate);
		float num4 = LeanSmooth.spring(current.a, target.a, ref currentVelocity.a, smoothTime, maxSpeed, deltaTime, friction, accelRate);
		return new Color(num, num2, num3, num4);
	}

		public static float linear(float current, float target, float moveSpeed, float deltaTime = -1f)
	{
		if (deltaTime < 0f)
		{
			deltaTime = Time.deltaTime;
		}
		bool flag = target > current;
		float num = deltaTime * moveSpeed * (flag ? 1f : -1f);
		float num2 = current + num;
		float num3 = num2 - target;
		if ((flag && num3 > 0f) || (!flag && num3 < 0f))
		{
			return target;
		}
		return num2;
	}

		public static Vector3 linear(Vector3 current, Vector3 target, float moveSpeed, float deltaTime = -1f)
	{
		float num = LeanSmooth.linear(current.x, target.x, moveSpeed, deltaTime);
		float num2 = LeanSmooth.linear(current.y, target.y, moveSpeed, deltaTime);
		float num3 = LeanSmooth.linear(current.z, target.z, moveSpeed, deltaTime);
		return new Vector3(num, num2, num3);
	}

		public static Color linear(Color current, Color target, float moveSpeed)
	{
		float num = LeanSmooth.linear(current.r, target.r, moveSpeed, -1f);
		float num2 = LeanSmooth.linear(current.g, target.g, moveSpeed, -1f);
		float num3 = LeanSmooth.linear(current.b, target.b, moveSpeed, -1f);
		float num4 = LeanSmooth.linear(current.a, target.a, moveSpeed, -1f);
		return new Color(num, num2, num3, num4);
	}

		public static float bounceOut(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed = -1f, float deltaTime = -1f, float friction = 2f, float accelRate = 0.5f, float hitDamping = 0.9f)
	{
		if (deltaTime < 0f)
		{
			deltaTime = Time.deltaTime;
		}
		float num = target - current;
		currentVelocity += deltaTime / smoothTime * accelRate * num;
		currentVelocity *= 1f - deltaTime * friction;
		if (maxSpeed > 0f && maxSpeed < Mathf.Abs(currentVelocity))
		{
			currentVelocity = maxSpeed * Mathf.Sign(currentVelocity);
		}
		float num2 = current + currentVelocity;
		bool flag = target > current;
		float num3 = num2 - target;
		if ((flag && num3 > 0f) || (!flag && num3 < 0f))
		{
			currentVelocity = -currentVelocity * hitDamping;
			num2 = current + currentVelocity;
		}
		return num2;
	}

		public static Vector3 bounceOut(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, float maxSpeed = -1f, float deltaTime = -1f, float friction = 2f, float accelRate = 0.5f, float hitDamping = 0.9f)
	{
		float num = LeanSmooth.bounceOut(current.x, target.x, ref currentVelocity.x, smoothTime, maxSpeed, deltaTime, friction, accelRate, hitDamping);
		float num2 = LeanSmooth.bounceOut(current.y, target.y, ref currentVelocity.y, smoothTime, maxSpeed, deltaTime, friction, accelRate, hitDamping);
		float num3 = LeanSmooth.bounceOut(current.z, target.z, ref currentVelocity.z, smoothTime, maxSpeed, deltaTime, friction, accelRate, hitDamping);
		return new Vector3(num, num2, num3);
	}

		public static Color bounceOut(Color current, Color target, ref Color currentVelocity, float smoothTime, float maxSpeed = -1f, float deltaTime = -1f, float friction = 2f, float accelRate = 0.5f, float hitDamping = 0.9f)
	{
		float num = LeanSmooth.bounceOut(current.r, target.r, ref currentVelocity.r, smoothTime, maxSpeed, deltaTime, friction, accelRate, hitDamping);
		float num2 = LeanSmooth.bounceOut(current.g, target.g, ref currentVelocity.g, smoothTime, maxSpeed, deltaTime, friction, accelRate, hitDamping);
		float num3 = LeanSmooth.bounceOut(current.b, target.b, ref currentVelocity.b, smoothTime, maxSpeed, deltaTime, friction, accelRate, hitDamping);
		float num4 = LeanSmooth.bounceOut(current.a, target.a, ref currentVelocity.a, smoothTime, maxSpeed, deltaTime, friction, accelRate, hitDamping);
		return new Color(num, num2, num3, num4);
	}
}
