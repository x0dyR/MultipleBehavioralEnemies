using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem _dyingEffect;
    [SerializeField] private string _currentBehaviourStr;

    private const float Speed = 3f;

    private EnemyActiveBehaviours _currentActiveBehaviour;
    private EnemyIdleBehaviours _currentIdleBehaviour;

    private List<Transform> _patrolPoints;

    private IBehaviour _activeBehaviour;
    private IBehaviour _idleBehaviour;

    private IBehaviour _currentBehaviour;

    private Mover _mover;

    public void Initialize(EnemyActiveBehaviours activeBehaviour, EnemyIdleBehaviours idleBehaviour, IEnumerable<Transform> patrolPoints)
    {
        _currentActiveBehaviour = activeBehaviour;
        _currentIdleBehaviour = idleBehaviour;

        _patrolPoints = new List<Transform>(patrolPoints);

        _mover = new Mover(transform, Speed);

        _idleBehaviour ??= _currentIdleBehaviour switch
        {
            EnemyIdleBehaviours.Idle => new IdleBehaviour(),

            EnemyIdleBehaviours.Patrol => new PatrolBehaviour(_mover, transform, _patrolPoints),

            EnemyIdleBehaviours.RandomPoints => new RandomMovingDirectionBehaviour(_mover, transform),

            _ => throw new ArgumentException("Undefined behaviour"),
        };

        _currentBehaviour = _idleBehaviour;

        _currentBehaviourStr = _currentIdleBehaviour.ToString();
    }

    private void Update()
    {
        _currentBehaviour.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            _activeBehaviour ??= _currentActiveBehaviour switch
            {
                EnemyActiveBehaviours.Scared => new ScareBehaviour(transform, _mover, character),

                EnemyActiveBehaviours.Chasing => new ChaseBehaviour(transform, _mover, character),

                EnemyActiveBehaviours.Dying => new DyingBehaviour(this, _dyingEffect),

                _ => throw new ArgumentException("Undefined behaviour"),
            };

            _currentBehaviour = _activeBehaviour;
            _currentBehaviour.Enter();

            _currentBehaviourStr = _currentIdleBehaviour.ToString();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Character _))
        {
            _currentBehaviour = _idleBehaviour;
            _currentBehaviour.Enter();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
}
