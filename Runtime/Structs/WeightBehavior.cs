using System;
namespace TechCosmos.AIOS.Runtime.Structs
{
    [Serializable]
    public struct WeightBehavior
    {
        public Func<float> weightCalculator; // 动态计算权重
        public Action action;

        public WeightBehavior(Func<float> weight, Action action)
        {
            this.weightCalculator = weight;
            this.action = action;
        }
    }
}
