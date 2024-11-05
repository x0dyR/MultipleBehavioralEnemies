using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyActiveBehaviours _activeBehaviour;
    [SerializeField] private EnemyIdleBehaviours _idleBehaviour;

    [SerializeField] private Enemy _enemyPrefab;

    [SerializeField] private List<Transform> _patrolPoints;

    private void Awake()
    {
        if (_patrolPoints[0] == null)
            throw new ArgumentException("No patrol points");

        Enemy enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity, null);
        enemy.Initialize(_activeBehaviour, _idleBehaviour, _patrolPoints);
    }
}
