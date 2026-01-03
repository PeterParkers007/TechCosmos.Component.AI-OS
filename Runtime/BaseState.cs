using TechCosmos.AIOS.Runtime.System;
using TechCosmos.AIOS.Runtime.Interfaces;
namespace TechCosmos.AIOS.Runtime
{
    public class State : IState
    {

        public string[] behaviorNames { get; set; }

        public UtilityEvaluationSystem _evaluationSystem { get; }
        public State(UtilityEvaluationSystem evaluationSystem,params string[] behaviorNames)
        {
            this.behaviorNames = behaviorNames;
            this._evaluationSystem = evaluationSystem;
        }

        public virtual void UpdateChoice()
        {
            _evaluationSystem.ChoiceBehavor(behaviorNames);
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

