using UnityEngine;

namespace Assets.Scripts.Quaternions
{
    public class QuaternionLib
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public float w { get; set; }

        public static QuaternionLib Euler2Quaternion(Vector3Lib rotateVector, float rotateAngle)
        {
            QuaternionLib quaternionLib = new QuaternionLib();

            float Angle2Radians(float angle)
            {
                return angle * Mathf.PI / 180;
            }

            quaternionLib.w = Mathf.Cos(Angle2Radians(rotateAngle) / 2);
            quaternionLib.x = rotateVector.x * Mathf.Sin(Angle2Radians(rotateAngle) / 2);
            quaternionLib.y = rotateVector.y * Mathf.Sin(Angle2Radians(rotateAngle) / 2);
            quaternionLib.z = rotateVector.z * Mathf.Sin(Angle2Radians(rotateAngle) / 2);
            return quaternionLib;
        }

        private QuaternionLib QuaternionScale(QuaternionLib quaternion, float value)
        {
            quaternion.x *= value;
            quaternion.y *= value;
            quaternion.z *= value;
            return quaternion;
        }

        private float QuaternionLength(QuaternionLib quaternion)
        {
            return (float)Mathf.Sqrt(quaternion.w * quaternion.w
                + quaternion.x * quaternion.x
                + quaternion.y * quaternion.y
                + quaternion.z * quaternion.z);
        }

        private QuaternionLib QuaternionNormalize(QuaternionLib quaternion)
        {
            float length = QuaternionLength(quaternion);
            return QuaternionScale(quaternion, 1 / length);
        }

        public QuaternionLib QuaternionInvert(QuaternionLib quaternion)
        {
            quaternion.w = quaternion.w;
            quaternion.x = -quaternion.x;
            quaternion.y = -quaternion.y;
            quaternion.z = -quaternion.z;
            return QuaternionNormalize(quaternion);
        }

        public override string ToString()
        {
            return $"({w})({x})({y})({z})";
        }
    }
}
