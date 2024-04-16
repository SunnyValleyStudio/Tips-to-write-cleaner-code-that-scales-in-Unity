using UnityEngine;

public class NPCRotationStrategy : AgentRoatationStrategy
{
    protected override float RotationStrategy(Vector3 inputDirection)
        => Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
}
