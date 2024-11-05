using UnityEngine;

public class ScareBehaviour : IBehaviour
{
    private Transform _transform;
    private Mover _mover;

    private Transform _target;

    public ScareBehaviour(Transform transform, Mover mover, Transform target)
    {
        _transform = transform;
        _mover = mover;
        _target = target;
    }

    public void Enter()
    {
        //включается музыка, парткилы, подписки))
    }

    public void Update()
    {
        Vector3 direction = _transform.position - _target.transform.position;
        direction.y = 0;
        _mover.ProcessMove(direction);
    }

    public void Exit()
    {
        //выключается музыка,партиклы, отписки))
    }
}
