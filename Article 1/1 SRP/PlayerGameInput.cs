using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGameInput : MonoBehaviour
    {
        private PlayerInput m_input;

        public Vector2 MovementInput { get; private set; }
        public Vector2 CameraInput { get; private set; }
        public bool SprintInput { get; private set; }

        private void Awake()
        {
            m_input = GetComponent<PlayerInput>();
        }
        private void OnEnable()
        {
            m_input.actions["Player/Move"].performed += OnMove;
            m_input.actions["Player/Move"].canceled += OnMove;
            m_input.actions["Player/Look"].performed += OnLook;
            m_input.actions["Player/Look"].canceled += OnLook;
            m_input.actions["Player/Sprint"].performed += OnInput;
        }

        private void OnDisable()
        {
            m_input.actions["Player/Move"].performed -= OnMove;
            m_input.actions["Player/Move"].canceled -= OnMove;
            m_input.actions["Player/Look"].performed -= OnLook;
            m_input.actions["Player/Look"].canceled -= OnLook;
            m_input.actions["Player/Sprint"].performed -= OnInput;
        }

        private void OnLook(InputAction.CallbackContext context)
        {
            CameraInput = context.ReadValue<Vector2>();
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            MovementInput = context.ReadValue<Vector2>();
        }

        private void OnInput(InputAction.CallbackContext context)
        {
            SprintInput = context.ReadValueAsButton();
        }
    }