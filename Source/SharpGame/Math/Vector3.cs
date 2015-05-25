using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGame
{
    public struct Vector3
    {
        #region Predefined Vectors
        public static readonly Vector3 Zero = new Vector3(0, 0, 0);
        public static readonly Vector3 Up = new Vector3(0, -1f, 0);
        public static readonly Vector3 Right = new Vector3(1f, 0, 0);
        public static readonly Vector3 Left = new Vector3(-1f, 0, 0);
        public static readonly Vector3 Down = new Vector3(0, 1f, 0);
        #endregion

        public float x;
        public float y;
        public float z;

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public float LengthSquared
        {
            get
            {
                return x * x + y * y + z * z;
            }
        }

        public float Length
        {
            get
            {
                // TODO: provide fast float sqrt method instead of using standart one
                return (float)System.Math.Sqrt(LengthSquared);
            }
        }

        public Vector3 Normalized
        {
            get
            {
                return this / Length;
            }
        }

        public static float Dot(Vector3 a, Vector3 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public float Dot(Vector3 that)
        {
            return Dot(this, that);
        }

        public Vector3 Cross(Vector3 a, Vector3 b)
        {
            return new Vector3(
                a.y * b.z - a.z * b.y,
                a.z * b.x - a.x * b.z,
                a.x * b.y - a.y * b.x);
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(
                a.x + b.x,
                a.y + b.y,
                a.z + b.z);
        }

        public static Vector3 operator *(Vector3 vec, float scalar)
        {
            return new Vector3(
                vec.x * scalar,
                vec.y * scalar,
                vec.z * scalar);
        }

        public static Vector3 operator /(Vector3 vec, float scalar)
        {
            return vec * (1f / scalar);
        }

        public static Vector3 operator -(Vector3 vec)
        {
            return vec * -1f;
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return a + (-b);
        }

        public static bool operator ==(Vector3 a, Vector3 b)
        {
            return a.x == b.x &&
                   a.y == b.y &&
                   a.z == b.z;
        }

        public static bool operator !=(Vector3 a, Vector3 b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector3)
            {
                Vector3 that = (Vector3)obj;
                return this == that;
            }

            return false;
        }

        public bool Equals(Vector3 that)
        {
            return this == that;
        }

        public override int GetHashCode()
        {
            // TODO: provide efficient hashing algorithm instead of relying on strings
            return string.Format("{0}{1}{2}", x, y, z).GetHashCode();
        }

        #region added by kakabubu
        public bool IsInBounds(Resolution Resolution)
        {
            return this.x > 0 && this.y > 5 &&
                   this.x < Resolution.x - 1 &&
                   this.y < Resolution.y - 1;
        }

        public Vector3 Move(float x, float y, float z)
        {
            return new Vector3(this.x + x, this.y + y, this.z + z);
        }
        public Vector3 Move(Vector3 that)
        {
            return new Vector3(this.x + that.x, this.y + that.y, this.z + that.z);
        }
        public Vector3 Move(double facing, double distance)
        {
            facing = ((facing - 90) * Math.PI / 180F);
            return new Vector3(x + (float)(distance * Math.Cos(facing)),
                               y + (float)(distance * Math.Sin(facing)),
                               z);
        }
        #endregion
    }
}
