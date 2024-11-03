using UnityEngine;

public class Character : MonoBehaviour
{
    private InputSystem _input;

    private Vector3 _inputDirection;

    private void Awake()
    {
        _input = new InputSystem();
    }

    private void Update()
    {
        _inputDirection = _input.ReadInputDirection();
    }
}
