using TechCosmos.AIOS.Runtime.System;
namespace TechCosmos.AIOS.Runtime.Interfaces
{
    public interface IState
    {
        UtilityEvaluationSystem _evaluationSystem { get; }
        public string[] behaviorNames { get; }
        public void UpdateChoice(params string[] behaviors);
    }
}

