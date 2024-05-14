using System.Collections.Generic;
using UnityEngine;

namespace Tips.Part_2_Start
{
    public class AgentAnimations : MonoBehaviour
    {
        private Animator m_animator;

        [Header("Animations")]
        [SerializeField] private string m_animationSpeedFloat;
        [SerializeField] private string m_animationGroundedBool;
        [SerializeField] private string m_animationFallTrigger;
        [SerializeField] private string m_animationJumpTrigger;
        [SerializeField] private string m_animationLandTrigger;

        // Mapping enums to animator parameter names
        private Dictionary<AnimationFloatType, string> floatParameters;
        private Dictionary<AnimationBoolType, string> boolParameters;
        private Dictionary<AnimationTriggerType, string> triggerParameters;

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
            InitializeParameterMappings();
        }

        private void InitializeParameterMappings()
        {
            floatParameters = new Dictionary<AnimationFloatType, string> {
            { AnimationFloatType.Speed, m_animationSpeedFloat },
            // Add more mappings here
        };
            boolParameters = new Dictionary<AnimationBoolType, string> {
            { AnimationBoolType.Grounded, m_animationGroundedBool },
            // Add more mappings here
        };
            triggerParameters = new Dictionary<AnimationTriggerType, string> {
            { AnimationTriggerType.Jump, m_animationJumpTrigger },
            { AnimationTriggerType.Fall, m_animationFallTrigger },
            { AnimationTriggerType.Land, m_animationLandTrigger }
            // Add more mappings here
        };
        }

        public void SetFloat(AnimationFloatType floatType, float value)
        {
            if (floatParameters.TryGetValue(floatType, out string paramName))
            {
                m_animator.SetFloat(paramName, value);
                return;
            }
            Debug.LogError($"Float parameter {floatType} not configured.");
        }

        public void SetBool(AnimationBoolType boolType, bool value)
        {
            if (boolParameters.TryGetValue(boolType, out string paramName))
            {
                m_animator.SetBool(paramName, value);
                return;
            }
            Debug.LogError($"Bool parameter {boolType} not configured.");
        }

        public void SetTrigger(AnimationTriggerType triggerType)
        {
            if (triggerParameters.TryGetValue(triggerType, out string paramName))
            {
                m_animator.SetTrigger(paramName);
                return;
            }
            Debug.LogError($"Trigger parameter {triggerType} not configured.");
        }

        public void ResetTrigger(AnimationTriggerType triggerType)
        {
            if (triggerParameters.TryGetValue(triggerType, out string paramName))
            {
                m_animator.ResetTrigger(paramName);
                return;
            }
            Debug.LogError($"Trigger parameter {triggerType} not configured.");
        }
    }

    public enum AnimationTriggerType
    {
        None,
        Jump,
        Fall,
        Land
    }

    public enum AnimationFloatType
    {
        None,
        Speed
    }

    public enum AnimationBoolType
    {
        None,
        Grounded
    }
}