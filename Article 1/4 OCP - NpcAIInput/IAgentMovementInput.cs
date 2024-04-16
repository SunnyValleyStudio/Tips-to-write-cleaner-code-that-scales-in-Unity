using UnityEngine;

public interface IAgentMovementInput
{
    public Vector2 MovementInput { get; }
    public bool SprintInput { get; }
}
