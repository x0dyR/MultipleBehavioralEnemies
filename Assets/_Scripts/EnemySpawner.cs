using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyActiveBehaviours _activeBehaviourEnum;
    [SerializeField] private EnemyIdleBehaviours _idleBehaviourEnum;

    [SerializeField] private Enemy _enemyPrefab;

    [SerializeField] private Transform _enemyTarget;

    [SerializeField] private List<PatrolPoint> _patrolPoints;

    private Mover _enemyMover;

    private IBehaviour _activeBehaviour;
    private IBehaviour _idleBehaviour;

    private Enemy _enemy;

    private void Awake()
    {
        IsPatrolPointsNull();

        _enemyTarget = FindObjectOfType<Character>().transform;

        _enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity, null);

        _enemyMover = new Mover(_enemy.transform, _enemy.Speed);

        _activeBehaviour = GetActiveBehaviour();
        _idleBehaviour = GetIdleBehaviour();

        //для удобства опознавания поведений врага
        _enemy.name = _enemy.GetType().ToString() + _activeBehaviourEnum.ToString() + _idleBehaviourEnum.ToString(); 

        _enemy.Initialize(_activeBehaviour, _idleBehaviour);
    }

    private IBehaviour GetActiveBehaviour()
    {
        _activeBehaviour ??= _activeBehaviourEnum switch
        {
            EnemyActiveBehaviours.Scared => new ScareBehaviour(_enemy.transform, _enemyMover, _enemyTarget),

            EnemyActiveBehaviours.Chasing => new ChaseBehaviour(_enemy.transform, _enemyMover, _enemyTarget),

            EnemyActiveBehaviours.Dying => new DyingBehaviour(_enemy, _enemyPrefab.DyinEffect),

            _ => throw new ArgumentException("Undefined behaviour"),
        };

        return _activeBehaviour;
    }

    private IBehaviour GetIdleBehaviour()
    {
        _idleBehaviour ??= _idleBehaviourEnum switch
        {
            EnemyIdleBehaviours.Idle => new IdleBehaviour(),

            EnemyIdleBehaviours.Patrol => new PatrolBehaviour(_enemyMover, _enemy.transform, _patrolPoints),

            EnemyIdleBehaviours.RandomPoints => new RandomMovingDirectionBehaviour(_enemyMover),

            _ => throw new ArgumentException("Undefined behaviour"),
        };

        return _idleBehaviour;
    }


    private void IsPatrolPointsNull()
    {
        if (_patrolPoints.Count == 0 || _patrolPoints[0] == null)
        {
            _patrolPoints.Clear();

            //понимаю что тяжелая операция,просто чтобы не пихать для каждой точки
            PatrolPoint[] patrolPoints = FindObjectsOfType<PatrolPoint>();

            foreach (var patrolPoint in patrolPoints)
                _patrolPoints.Add(patrolPoint);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(transform.position, .5f);
    }
}
