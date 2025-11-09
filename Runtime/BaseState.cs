using TechCosmos.AIOS.Runtime.System;
using TechCosmos.AIOS.Runtime.Interfaces;
namespace TechCosmos.AIOS.Runtime
{
    public abstract class BaseState : IState
    {

        public string[] behaviorNames { get; set; }

        public UtilityEvaluationSystem _evaluationSystem { get; }
        public BaseState(string[] behaviorNames, UtilityEvaluationSystem evaluationSystem)
        {
            this.behaviorNames = behaviorNames;
            this._evaluationSystem = evaluationSystem;
        }

        public virtual void UpdateChoice(params string[] behaviors)
        {
            _evaluationSystem.ChoiceBehavor(behaviors);
        }

        public virtual void OnEnter()
        {
            
        }

        public virtual void OnExit()
        {
            
        }

        public virtual void OnUpdate()
        {
            
        }
    }
}

