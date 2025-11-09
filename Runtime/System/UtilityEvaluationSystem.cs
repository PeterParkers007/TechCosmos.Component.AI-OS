using System.Collections.Generic;
using System.Linq;
using TechCosmos.AIOS.Runtime.Structs;
namespace TechCosmos.AIOS.Runtime.System
{
    public class UtilityEvaluationSystem
    {
        private Dictionary<string, WeightBehavior> UtilityWeightBehaviors = new();

        public void RegisterUtilityCommand(string name, WeightBehavior behavior)
        {
            UtilityWeightBehaviors[name] = behavior;
        }

        public void RegisterUtilityCommand(params (string, WeightBehavior)[] values)
        {
            foreach (var value in values)
            {
                UtilityWeightBehaviors[value.Item1] = value.Item2;
            }
        }
        public void UnRegisterUtilityCommand(string name) => UtilityWeightBehaviors.Remove(name);
        public void UnRegisterUtilityCommand(params string[] names)
        {
            foreach (string name in names)
            {
                UtilityWeightBehaviors.Remove(name);
            }
        }
        public void ChoiceBehavor(params string[] behaviorNames)
        {
            List<WeightBehavior> behaviorList = new List<WeightBehavior>();
            foreach (var behaviorName in behaviorNames)
            {
                if (UtilityWeightBehaviors.ContainsKey(behaviorName))
                {
                    behaviorList.Add(UtilityWeightBehaviors[behaviorName]);
                }
            }
            WeightBehavior[] behaviors = behaviorList.ToArray();
            FinalDecisionMaking(behaviors);
        }
        public void ChoiceBehavor() => FinalDecisionMaking(UtilityWeightBehaviors.Values.ToArray());
        public void FinalDecisionMaking(params WeightBehavior[] utilityWeights)
        {
            if (utilityWeights == null || utilityWeights.Length == 0)
                return;

            // ¿ÕÖµ¼ì²é
            var validBehaviors = utilityWeights
                .Where(b => b.weightCalculator != null && b.action != null)
                .ToArray();

            if (validBehaviors.Length == 0)
                return;

            var bestChoice = validBehaviors
                .OrderByDescending(cmd => cmd.weightCalculator())
                .First();

            bestChoice.action.Invoke();
        }

    }
}

