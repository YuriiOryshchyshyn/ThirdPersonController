
using System;

namespace Assets.Scripts.Quaternions
{
    public class Vector3Lib
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public Vector3Lib(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void Normalize()
        {
            float length = vectorLength();
            float inverseLength = 1 / length;
            x *= inverseLength;
            y *= inverseLength;
            z *= inverseLength;
        }

        private float vectorLength()
        {
            return (float)Math.Sqrt(x * x + y * y + z * z);
        }

        public override string ToString()
        {
            return $"({x})({y})({z})";
        }
    }
}
