using UnityEngine;

namespace Tips.Part_2_Start
{
    public class NPCRotationStrategy : AgentRotationStrategy
    {
        protected override float RotationStrategy(Vector3 inputDirection)
            => Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
    }
}