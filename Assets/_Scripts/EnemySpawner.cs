using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyActiveBehaviours _activeBehaviour;
    [SerializeField] private EnemyIdleBehaviours _idleBehaviour;

    [SerializeField] private Enemy _enemyPrefab;

    [SerializeField] private List<PatrolPoint> _patrolPoints;

    private void Awake()
    {
        if (_patrolPoints.Count == 0 || _patrolPoints[0] == null)
        {
            _patrolPoints.Clear();

            PatrolPoint[] patrolPoints = FindObjectsOfType<PatrolPoint>();

            foreach (var patrolPoint in patrolPoints)
                _patrolPoints.Add(patrolPoint);
        }

        Enemy enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity, null);
        enemy.Initialize(_activeBehaviour, _idleBehaviour, _patrolPoints);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(transform.position, .5f);
    }
}
