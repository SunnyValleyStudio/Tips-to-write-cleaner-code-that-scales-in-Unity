using UnityEngine;

namespace Tips.Part_2_Start
{
    public class PlayerRotationStrategy : AgentRotationStrategy
    {
        [SerializeField]
        private GameObject m_mainCamera;
        private void Awake()
        {
            // get a reference to our main camera
            if (m_mainCamera == null)
            {
                m_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
        }

        protected override float RotationStrategy(Vector3 inputDirection)
            => Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + m_mainCamera.transform.eulerAngles.y;
    }
}