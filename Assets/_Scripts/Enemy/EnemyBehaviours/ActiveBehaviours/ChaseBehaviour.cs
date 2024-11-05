using UnityEngine;

public class ChaseBehaviour : IBehaviour
{
    private const float MinimalDistance = .1f;

    private Transform _transform;
    private Mover _mover;

    private Transform _target;

    public ChaseBehaviour(Transform transform, Mover mover, Transform target)
    {
        _transform = transform;
        _mover = mover;
        _target = target;
    }

    public void Enter()
    {
        //включается музыка, парткилы, подписки))
    }

    public void Exit()
    {
        //выключается музыка,партиклы, отписки))
    }

    public void Update()
    {
        if ((_target.transform.position - _transform.position).sqrMagnitude < MinimalDistance * MinimalDistance)
            return;

        Vector3 direction = _target.transform.position - _transform.position;
        direction.y = 0;
        _mover.ProcessMove(direction);
    }
}
