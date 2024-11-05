using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem _dyingEffect;

    private IBehaviour _activeBehaviour;
    private IBehaviour _idleBehaviour;

    private IBehaviour _currentBehaviour;

    private Mover _mover;

    private readonly float _speed = 3f;

    public float Speed => _speed;

    public ParticleSystem DyinEffect => _dyingEffect;

    public void Initialize(IBehaviour activeBehaviour, IBehaviour idleBehaviour)
    {
        _activeBehaviour = activeBehaviour;
        _idleBehaviour = idleBehaviour;

        _currentBehaviour = _idleBehaviour;
        _currentBehaviour.Enter();
    }

    private void Update()
    {
        _currentBehaviour.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character _))
        {
            _currentBehaviour = _activeBehaviour;
            _currentBehaviour.Enter();
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
