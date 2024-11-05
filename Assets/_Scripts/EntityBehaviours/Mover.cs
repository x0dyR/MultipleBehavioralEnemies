using UnityEngine;

public class Mover
{
    private const float DeadZone = .1f;

    private Transform _transform;
    private float _speed;

    public Mover(Transform transform, float speed)
    {
        _transform = transform;
        _speed = speed;
    }

    public void ProcessMove(Vector3 direction)
    {
        if (direction.sqrMagnitude < DeadZone * DeadZone)
            return;

        _transform.position += _speed * Time.deltaTime * direction.normalized;
        _transform.forward = direction;
    }
}
