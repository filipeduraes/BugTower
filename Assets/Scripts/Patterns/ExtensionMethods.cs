namespace UnityEngine
{
    public static class ExtensionMethods
    {
        public static void SetWithoutOverwrite(this ref bool variable, bool value)
        {
            if (variable != value)
                variable = value;
        }

        public static bool CompareLayer(this LayerMask layerMask, int layer)
        {
            return (((1 << layer) & layerMask) != 0);
        }

        public static int LayerToLayerMask(this int layer) => (1 << layer);

        public static bool Approximately(this Vector2 vector, Vector2 other)
        {
            bool x = Mathf.Approximately(vector.x, other.x);
            bool y = Mathf.Approximately(vector.y, other.y);
            return x && y;
        }

        public static bool Approximately(this Vector3 vector, Vector3 other)
        {
            bool x = Mathf.Approximately(vector.x, other.x);
            bool y = Mathf.Approximately(vector.y, other.y);
            bool z = Mathf.Approximately(vector.z, other.z);

            return x && y && z;
        }

        public static bool Approximately(this Vector3 vector, Vector2 other)
        {
            Vector3 otherConverted = new Vector3(other.x, other.y, 0f);
            return vector.Approximately(otherConverted);
        }

        public static bool Different(this float variable, float value)
        {
            return !Mathf.Approximately(variable, value);
        }
    }
}