using System;
using System.Collections.Generic;
using TechCosmos.AIOS.Runtime.Structs;
namespace TechCosmos.AIOS.Runtime.System
{
    public class UtilityEvaluationSystem
    {
        readonly Dictionary<string, WeightBehavior> _behaviors = new Dictionary<string, WeightBehavior>();
        readonly List<WeightBehavior> _choiceBuffer = new List<WeightBehavior>();
        int _decideDepth;

        public void RegisterUtilityCommand(string name, WeightBehavior behavior)
        {
            _behaviors[name] = behavior;
        }

        public void RegisterUtilityCommand(params (string, WeightBehavior)[] values)
        {
            foreach (var value in values)
            {
                _behaviors[value.Item1] = value.Item2;
            }
        }
        public void UnRegisterUtilityCommand(string name) => _behaviors.Remove(name);
        public void UnRegisterUtilityCommand(params string[] names)
        {
            foreach (string name in names)
            {
                _behaviors.Remove(name);
            }
        }

        public void ChoiceBehavior(params string[] behaviorNames)
        {
            var buffer = Rent();
            buffer.Clear();
            if (behaviorNames != null)
            {
                for (int i = 0; i < behaviorNames.Length; i++)
                {
                    if (_behaviors.TryGetValue(behaviorNames[i], out var behavior))
                        buffer.Add(behavior);
                }
            }

            Decide(buffer);
        }

        public void ChoiceBehavior()
        {
            var buffer = Rent();
            buffer.Clear();
            foreach (var behavior in _behaviors.Values)
                buffer.Add(behavior);
            Decide(buffer);
        }

        [Obsolete("请改用 ChoiceBehavior。")]
        public void ChoiceBehavor(params string[] behaviorNames) => ChoiceBehavior(behaviorNames);

        [Obsolete("请改用 ChoiceBehavior。")]
        public void ChoiceBehavor() => ChoiceBehavior();

        public void FinalDecisionMaking(params WeightBehavior[] utilityWeights)
        {
            if (utilityWeights == null || utilityWeights.Length == 0)
                return;

            Decide(utilityWeights);
        }

        List<WeightBehavior> Rent()
        {
            if (_decideDepth > 0)
                return new List<WeightBehavior>();
            return _choiceBuffer;
        }

        void Decide<T>(T behaviors) where T : IEnumerable<WeightBehavior>
        {
            _decideDepth++;
            try
            {
                bool found = false;
                WeightBehavior best = default;
                float bestWeight = 0f;
                foreach (var behavior in behaviors)
                {
                    if (behavior.weightCalculator == null || behavior.action == null)
                        continue;

                    float weight = behavior.weightCalculator();
                    if (!found || weight > bestWeight)
                    {
                        found = true;
                        best = behavior;
                        bestWeight = weight;
                    }
                }

                if (found)
                    best.action.Invoke();
            }
            finally
            {
                _decideDepth--;
            }
        }
    }
}
