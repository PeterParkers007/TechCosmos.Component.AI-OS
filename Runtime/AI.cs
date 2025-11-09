using TechCosmos.AIOS.Runtime.System;
using UnityEngine;
namespace TechCosmos.AIOS.Runtime
{
    public class AI : MonoBehaviour
    {
        [SerializeField] private bool _autoInitialize = true;

        public BehavioralExecutSystem BehavioralExecutSystem { get; private set; }
        public UtilityEvaluationSystem EvaluationSystem { get; private set; }

        public bool IsInitialized { get; private set; }

        private void Awake()
        {
            if (_autoInitialize && isActiveAndEnabled)
                Initialize();
        }

        private void Start()
        {
            if (_autoInitialize && !IsInitialized && isActiveAndEnabled)
                Initialize();
        }

        public void Initialize()
        {
            if (IsInitialized) return;

            BehavioralExecutSystem = new BehavioralExecutSystem();
            EvaluationSystem = new UtilityEvaluationSystem();
            IsInitialized = true;

            OnInitialized();
        }

        protected virtual void OnInitialized()
        {
            // 供子类重写，用于自定义初始化
        }

        public virtual void AI_Update()
        {
            if (IsInitialized)
                BehavioralExecutSystem?.Update();
        }

        // 便捷访问属性
        public bool IsExecuting => BehavioralExecutSystem?.currentState != null;
        public string CurrentStateName => BehavioralExecutSystem?.currentState?.GetType().Name;
    }
}
