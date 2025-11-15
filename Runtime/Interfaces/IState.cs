using TechCosmos.AIOS.Runtime.System;
namespace TechCosmos.AIOS.Runtime.Interfaces
{
    public interface IState
    {
        UtilityEvaluationSystem _evaluationSystem { get; }
        public string[] behaviorNames { get; }
        // 添加生命周期
        void OnEnter();
        void OnExit();
        void OnUpdate();

        void UpdateChoice();
    }
}

