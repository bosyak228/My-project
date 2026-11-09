using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 4f;
    [SerializeField] private PlayerInputReader _inputReader;

    private CharacterController _controller;

    private void Awake() => _controller = GetComponent<CharacterController>();

    private void Update()
    {
        Vector2 moveInput = _inputReader.Move;
        if (moveInput == Vector2.zero) return;

        Vector3 direction = transform.right * moveInput.x + transform.forward * moveInput.y;
        _controller.Move(direction * (_moveSpeed * Time.deltaTime));
    }
}