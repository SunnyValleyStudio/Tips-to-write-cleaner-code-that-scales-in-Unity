using UnityEngine;

namespace Tips.Part_2_Start
{
    public class BasicCharacterControllerMover : MonoBehaviour
    {
        [Header("Movement Parameters")]
        [SerializeField] private float m_rotationSmoothTime = 0.12f;
        [SerializeField] private float m_speedChangeRate = 10.0f;

        private CharacterController m_controller;
        private float m_speed;
        private float m_targetRotation = 0.0f;
        private float m_rotationVelocity;

        [SerializeField]
        private AgentRotationStrategy m_rotationStrategy;

        private void Awake()
        {
            m_controller = GetComponent<CharacterController>();
            // get a reference to our main camera
            if (m_rotationStrategy == null)
            {
                m_rotationStrategy = GetComponent<AgentRotationStrategy>();
            }
        }


        public void Move(Vector3 input, float speed)
        {
            Vector2 horizontalInput = new Vector2(input.x, input.z);
            CharacterMovementCalculation(horizontalInput, speed);

            m_targetRotation = m_rotationStrategy.RotationCalculation(new Vector2(input.x, input.z), transform, ref m_rotationVelocity, m_rotationSmoothTime, m_targetRotation);

            Vector3 targetDirection = Quaternion.Euler(0.0f, m_targetRotation, 0.0f) * Vector3.forward;

            //move the character controller
            m_controller.Move(targetDirection.normalized * (m_speed * Time.deltaTime) +
                             new Vector3(0.0f, input.y, 0.0f) * Time.deltaTime);
        }

        private void CharacterMovementCalculation(Vector2 horizontalInput, float targetSpeed)
        {
            if (horizontalInput == Vector2.zero)
                targetSpeed = 0.0f;

            // a reference to the players current horizontal velocity
            float currentHorizontalSpeed = new Vector3(m_controller.velocity.x, 0.0f, m_controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = horizontalInput.magnitude;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                m_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                    Time.deltaTime * m_speedChangeRate);

                // round speed to 3 decimal places
                m_speed = Mathf.Round(m_speed * 1000f) / 1000f;
            }
            else
            {
                m_speed = targetSpeed;
            }

        }
    }
}