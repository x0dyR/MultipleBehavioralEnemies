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

    public void Update()
    {
        //Object.Destroy(_enemy.gameObject, _dyingEffect.main.duration);
        Object.Destroy(_enemy.gameObject, 1);
    }
}
