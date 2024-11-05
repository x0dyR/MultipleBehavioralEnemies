using UnityEngine;

public class DyingBehaviour : IBehaviour
{
    private ParticleSystem _dyingEffect;

    private Enemy _enemy;

    public DyingBehaviour(Enemy enemy, ParticleSystem dyingEffect)
    {
        _enemy = enemy;
        _dyingEffect = dyingEffect;
    }

    public void Enter()
    {
        Object.Instantiate(_dyingEffect, _enemy.transform.position, _enemy.transform.rotation, _enemy.transform.parent);

        Object.Destroy(_enemy.gameObject);
    }

    public void Update() { }
}
