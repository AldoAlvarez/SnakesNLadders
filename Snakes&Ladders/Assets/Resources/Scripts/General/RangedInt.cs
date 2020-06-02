using UnityEngine;

namespace AGAC.General {
    [System.Serializable]
    public sealed class RangedInt 
    {
        public RangedInt(int minValue, int maxValue)
        {
            int min = minValue;
            int max = maxValue;
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

        #region VARIABLES
        public int Value { get { return GetValueInRange(); } }

        [SerializeField]
        private int LowerValue;
        [SerializeField]
        private int UpperValue;

        [SerializeField]
        private int MinValue;
        [SerializeField]
        private int MaxValue;
        #endregion

        public bool isInRange(int number) 
        {
            if (number < LowerValue) return false;
            if (number > UpperValue) return false;
            return true;
        }

        private int GetValueInRange()
        {
            int min = LowerValue;
            int max = UpperValue;
            return Random.Range(min, max);
        }
    }
}