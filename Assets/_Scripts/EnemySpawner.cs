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

    private void Awake()
    {
        IsPatrolPointsNull();

        _enemyTarget = FindObjectOfType<Character>().transform;

        Enemy enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity, null);

        _enemyMover = new Mover(enemy.transform, enemy.Speed);

        _activeBehaviour = GetActiveBehaviour(enemy);
        _idleBehaviour = GetIdleBehaviour(enemy);

        //для удобства опознавания поведений врага
        enemy.name = enemy.GetType().ToString() + _activeBehaviourEnum.ToString() + _idleBehaviourEnum.ToString(); 

        enemy.Initialize(_activeBehaviour, _idleBehaviour);
    }

    private IBehaviour GetActiveBehaviour(Enemy enemy)
    {
        _activeBehaviour ??= _activeBehaviourEnum switch
        {
            EnemyActiveBehaviours.Scared => new ScareBehaviour(enemy.transform, _enemyMover, _enemyTarget),

            EnemyActiveBehaviours.Chasing => new ChaseBehaviour(enemy.transform, _enemyMover, _enemyTarget),

            EnemyActiveBehaviours.Dying => new DyingBehaviour(enemy, _enemyPrefab.DyinEffect),

            _ => throw new ArgumentException("Undefined behaviour"),
        };

        return _activeBehaviour;
    }

    private IBehaviour GetIdleBehaviour(Enemy enemy)
    {
        _idleBehaviour ??= _idleBehaviourEnum switch
        {
            EnemyIdleBehaviours.Idle => new IdleBehaviour(),

            EnemyIdleBehaviours.Patrol => new PatrolBehaviour(_enemyMover, enemy.transform, _patrolPoints),

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
