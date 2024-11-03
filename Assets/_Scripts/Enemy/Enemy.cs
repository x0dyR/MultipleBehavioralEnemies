using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const float Speed = 1.5f;

    [SerializeField] private EnemyActiveBehaviours _currentActiveBehaviour;
    [SerializeField] private EnemyIdleBehaviours _currentIdleBehaviour;

    private void Update()
    {
        
    }
}
