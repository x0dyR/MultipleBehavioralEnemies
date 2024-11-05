using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float _speed;

    private InputSystem _input;

    private Mover _mover;

    private Vector3 _inputDirection;

    private void Awake()
    {
        _input = new InputSystem();

        _mover = new Mover(transform, _speed);
    }

    private void Update()
    {
        _inputDirection = _input.ReadInputDirection();

        _mover.ProcessMove(_inputDirection);
    }
}
