using TechCosmos.AIOS.Runtime.Interfaces;
namespace TechCosmos.AIOS.Runtime.System
{
    public class BehavioralExecutSystem
    {
        public IState currentState;
        public void Update()
        {
            currentState?.OnUpdate();
            Choice();
        }
        public void Choice()
        {
            if (currentState == null) return;
            currentState.UpdateChoice();
        }
        public void HandOffState(IState state)
        {
            if (state == null) return;
            if(currentState != null) currentState.OnExit();
            currentState = state;
            currentState.OnEnter();
        }
    }
}

