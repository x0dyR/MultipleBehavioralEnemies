using UnityEngine;

public class RandomMovingDirectionBehaviour : IBehaviour
{
    private const float MinimalDistance = .1f;

    private Vector3 _currentDirection;

    private float _switchDirectionTime = 1;
    private float _currentMoveTime;

    private Mover _mover;

    public RandomMovingDirectionBehaviour(Mover mover)
    {
        _mover = mover;

        _currentDirection = GetRandomDirection();
    }

    public void Update()
    {
        _currentMoveTime += Time.deltaTime;

        if (IsShouldChangeDirection())
            _currentDirection = GetRandomDirection();

        _mover.ProcessMove(_currentDirection);
    }

    public void Enter() { }

    private bool IsShouldChangeDirection()
    {
        if (_currentMoveTime >= _switchDirectionTime)
        {
            _currentMoveTime = 0;
            return true;
        }

        return false;
    }

    private Vector3 GetRandomDirection()
        => new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
}
