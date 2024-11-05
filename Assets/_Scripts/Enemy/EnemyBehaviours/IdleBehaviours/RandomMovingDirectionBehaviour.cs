using UnityEngine;

public class RandomMovingDirectionBehaviour : IBehaviour
{
    private Vector3 _currentDirection;

    private float _switchDirectionTime = 1;
    private float _currentMoveTime;

    private const int MaxRandomDirection = 1;
    private const int MinRandomDirection = -1;

    private Mover _mover;

    public RandomMovingDirectionBehaviour(Mover mover)
    {
        _mover = mover;

        _currentDirection = GetRandomDirection();
    }

    public void Enter()
    {
        //включается музыка, парткилы, подписки))
    }

    public void Update()
    {
        _currentMoveTime += Time.deltaTime;

        if (IsShouldChangeDirection())
            _currentDirection = GetRandomDirection();

        _mover.ProcessMove(_currentDirection);
    }

    public void Exit()
    {
        //выключается музыка,партиклы, отписки))
    }

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
        => new Vector3(Random.Range(MinRandomDirection, MaxRandomDirection),
            0, Random.Range(MinRandomDirection, MaxRandomDirection)).normalized;
}
