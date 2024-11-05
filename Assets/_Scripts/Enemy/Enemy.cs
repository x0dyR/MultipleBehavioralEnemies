using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const float Speed = 3f;

    [SerializeField] private EnemyActiveBehaviours _currentActiveBehaviour;
    [SerializeField] private EnemyIdleBehaviours _currentIdleBehaviour;

    [SerializeField] private List<Transform> _patrolPoints;

    private IBehaviour _idleBehaviour;
    private IBehaviour _activeBehaviour;

    private Mover _mover;

    private void Awake()
    {
        _mover = new Mover(transform, Speed);

        _idleBehaviour = _currentIdleBehaviour switch
        {
            EnemyIdleBehaviours.Idle => new IdleBehaviour(),
            EnemyIdleBehaviours.Patrol => new PatrolBehaviour(_mover, transform, _patrolPoints),
            EnemyIdleBehaviours.RandomPoints => new RandomMovingDirectionBehaviour(_mover, transform),
            _ => throw new ArgumentException("Undefined behaviour"),
        };
    }

    private void Update()
    {
        _idleBehaviour.Update();
    }
}
