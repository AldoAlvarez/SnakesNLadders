using UnityEngine;

namespace AGAC.General
{
    [System.Serializable]
    public abstract class RangedFloat
    {
        public RangedFloat(float minValue, float maxValue)
        {
            float  min = minValue;
            float  max = maxValue;
            if (min > max)
            {
                max = minValue;
                min = maxValue;
            }
            MinValue = min;
            MaxValue = max;

            LowerValue = MinValue;
            UpperValue = MaxValue;
        }

        public float Value { get { return GetValueInRange(); } }

        [SerializeField]
        private float LowerValue;
        [SerializeField]
        private float UpperValue;

        [SerializeField]
        private float MinValue;
        [SerializeField]
        private float MaxValue;

        private float GetValueInRange() 
        {
            float min = LowerValue;
            float max = UpperValue;
            return Random.Range(min, max);
        }
    }
}