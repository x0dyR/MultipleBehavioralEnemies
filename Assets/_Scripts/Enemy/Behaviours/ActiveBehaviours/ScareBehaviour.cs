using UnityEngine;

public class ScareBehaviour : IBehaviour
{
    private Transform _transform;
    private Mover _mover;

    private Character _character;

    public ScareBehaviour(Transform transform, Mover mover, Character character)
    {
        _transform = transform;
        _mover = mover;
        _character = character;
    }

    public void Update()
    {
        _mover.ProcessMove(_transform.position - _character.transform.position);
    }
}
