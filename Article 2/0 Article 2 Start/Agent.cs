using UnityEngine;

namespace Tips.Part_2_Start
{
    public class Agent : MonoBehaviour
    {
        [Header("Movement Parameters")]
        public float MoveSpeed = 2.0f;
        public float SprintSpeed = 5.335f;
        public float SpeedChangeRate = 10.0f;
        public float FallTimeout = 0.15f;

        private BasicCharacterControllerMover m_mover;
        private float targetMovementSpeed = 0;

        private IAgentMovementInput m_input;

        private float m_verticalVelocity;
        private float m_fallTimeoutDelta;

        public float Gravity = -15.0f;

        private GroundedDetector m_groundDetector;

        private AgentAnimations m_agentAnimations;

        private float m_animationMovementSpeed;


        private void Awake()
        {
            m_input = GetComponent<IAgentMovementInput>();

            m_groundDetector = GetComponent<GroundedDetector>();
            m_agentAnimations = GetComponent<AgentAnimations>();
            m_mover = GetComponent<BasicCharacterControllerMover>();
        }

        private void Update()
        {
            if (m_groundDetector.Grounded == false)
            {
                m_verticalVelocity += Gravity * Time.deltaTime;
                m_fallTimeoutDelta -= Time.deltaTime;
                if (m_fallTimeoutDelta <= 0 && m_groundDetector.StairsGrounded == false)
                {
                    m_agentAnimations.SetTrigger(AnimationTriggerType.Fall);
                }
            }
            else
            {
                m_verticalVelocity = 0;
                m_fallTimeoutDelta = FallTimeout;
                m_agentAnimations.ResetTrigger(AnimationTriggerType.Fall);
            }


            targetMovementSpeed = m_input.SprintInput ? SprintSpeed : MoveSpeed;
            m_mover.Move(new Vector3(m_input.MovementInput.x, m_verticalVelocity, m_input.MovementInput.y), targetMovementSpeed);

            targetMovementSpeed = m_input.MovementInput == Vector2.zero ? 0 : targetMovementSpeed;
            m_animationMovementSpeed = Mathf.Lerp(m_animationMovementSpeed, targetMovementSpeed, Time.deltaTime * SpeedChangeRate);
            if (m_animationMovementSpeed < 0.01f)
                m_animationMovementSpeed = 0f;

            //play animations
            m_agentAnimations.SetFloat(AnimationFloatType.Speed, m_animationMovementSpeed);
        }

        private void FixedUpdate()
        {
            m_groundDetector.GroundedCheck();
            m_agentAnimations.SetBool(AnimationBoolType.Grounded, m_groundDetector.Grounded);
        }

    }
}

