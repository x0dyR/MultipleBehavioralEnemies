using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : IBehaviour
{
    private const float MinimalDistance = .1f;

    private Transform _transform;
    private Queue<PatrolPoint> _patrolPoints;
    private Transform _currentPoint;

    private Mover _mover;

    public PatrolBehaviour(Mover mover, Transform transform, IEnumerable<PatrolPoint> patrolPoints)
    {
        _transform = transform;
        _patrolPoints = new Queue<PatrolPoint>(patrolPoints);

        _mover = mover;

        _currentPoint = NextPoint();
    }

    public void Update()
    {
        if ((_currentPoint.transform.position - _transform.position).sqrMagnitude < MinimalDistance * MinimalDistance)
            _currentPoint = NextPoint();

        _mover.ProcessMove(_currentPoint.position - _transform.position);
    }

    public void Enter() { }

    private Transform NextPoint()
    {
        PatrolPoint nextPoint = _patrolPoints.Dequeue();
        _patrolPoints.Enqueue(nextPoint);

        return nextPoint.transform;
    }

    public void Exit()
    {
        //выключается музыка,партиклы, отписки))
    }
}