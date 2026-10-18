using TechCosmos.AIOS.Runtime.System;
using System;
using UnityEngine;
namespace TechCosmos.AIOS.Runtime
{
    public class AI : MonoBehaviour
    {
        [SerializeField] private bool _autoInitialize = true;

        BehavioralExecutSystem _behavior;

        public BehavioralExecuteSystem BehavioralExecuteSystem => _behavior;

        [Obsolete("请改用 BehavioralExecuteSystem。")]
        public BehavioralExecutSystem BehavioralExecutSystem => _behavior;

        public UtilityEvaluationSystem EvaluationSystem { get; private set; }

        public bool IsInitialized { get; private set; }

        public Action OnAIUpdate;
        public Action OnAIInitialized;

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

            _behavior = new BehavioralExecutSystem();
            EvaluationSystem = new UtilityEvaluationSystem();
            IsInitialized = true;
            OnAIInitialized?.Invoke();
            OnInitialized();
        }

        protected virtual void OnInitialized()
        {
            // 供子类重写，用于自定义初始化
        }


        public virtual void AI_Update()
        {
            if (IsInitialized)
            {
                BehavioralExecuteSystem?.Update();
                OnAIUpdate?.Invoke();
            }
        }

        // 便捷访问属性
        public bool IsExecuting => BehavioralExecuteSystem?.currentState != null;
        public string CurrentStateName => BehavioralExecuteSystem?.currentState?.GetType().Name;
    }
}
