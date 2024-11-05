using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : IBehaviour
{
    private const float MinimalDistance = .1f;

    private Transform _transform;
    private Queue<Transform> _patrolPoints;
    private Transform _currentPoint;

    private Mover _mover;

    public PatrolBehaviour(Mover mover, Transform transform, IEnumerable<Transform> patrolPoints)
    {
        _transform = transform;
        _patrolPoints = new Queue<Transform>(patrolPoints);

        _mover = mover;

        _currentPoint = NextPoint();
    }

    public void Update()
    {
        if ((_currentPoint.transform.position - _transform.position).sqrMagnitude < MinimalDistance * MinimalDistance)
            _currentPoint = NextPoint();

        _mover.ProcessMove(_currentPoint.position - _transform.position);
    }

    private Transform NextPoint()
    {
        Transform nextPoint = _patrolPoints.Dequeue();
        _patrolPoints.Enqueue(nextPoint);

        return nextPoint;
    }
}