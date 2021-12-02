using System;
using UnityEditor.UIElements;
using UnityEngine;

namespace MyMath
{
    public struct Vec2 : IFormattable
    {
        public float x;
        public float y;

        public Vec2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public float Length()
        {
            return Mathf.Sqrt(x * x + y * y);
        }

        public Vec2 Normalize()
        {
            return this / Length();
        }

        public static float Dot(Vec2 a, Vec2 b)
        {
            return a.x * b.x + a.y * b.y;
        }

        public static Vec2 operator -(Vec2 vector)
        {
            return new Vec2(-vector.x, -vector.y);
        }

        public static Vec2 operator *(float scalar, Vec2 vector)
        {
            return vector * scalar;
        }

        public static Vec2 operator *(Vec2 vector, float scalar)
        {
            return new Vec2(vector.x * scalar, vector.y * scalar);
        }

        public static Vec2 operator /(Vec2 vector, float scalar)
        {
            float inv = 1f / scalar;
            return new Vec2(vector.x * inv, vector.y * inv);
        }

        public static Vec2 operator +(Vec2 vectorA, Vec2 vectorB)
        {
            return new Vec2(vectorA.x + vectorB.x, vectorA.y + vectorB.y);
        }

        public static Vec2 operator -(Vec2 vectorA, Vec2 vectorB)
        {
            return new Vec2(vectorA.x - vectorB.x, vectorA.y - vectorB.y);
        }

        public static Vec2 Zero => new Vec2(0, 0);

        public static Vec2 Up => new Vec2(0, 1);

        public static Vec2 Down => new Vec2(0, -1);

        public static Vec2 Left => new Vec2(-1, 0);

        public static Vec2 Right => new Vec2(1, 0);

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return $"({x},{y})";
        }
    }
}