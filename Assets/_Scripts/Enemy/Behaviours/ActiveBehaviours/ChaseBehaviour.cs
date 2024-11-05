using UnityEngine;

public class ChaseBehaviour : IBehaviour
{
    private const float MinimalDistance = .1f;

    private Transform _transform;
    private Mover _mover;

    private Character _character;

    public ChaseBehaviour(Transform transform, Mover mover, Character character)
    {
        _transform = transform;
        _mover = mover;
        _character = character;
    }

    public void Update()
    {
        if ((_character.transform.position - _transform.position).sqrMagnitude < MinimalDistance * MinimalDistance)
            return;

        _mover.ProcessMove(_character.transform.position - _transform.position);
    }
}
