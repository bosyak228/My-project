using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float _lookSensitivity = 0.15f;
    [SerializeField] private float _maxPitch = 70f;
    [SerializeField] private Transform _cameraHolder;
    [SerializeField] private PlayerInputReader _inputReader;

    private float _pitch;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector2 lookInput = _inputReader.Look;
        if (lookInput == Vector2.zero) return;

        transform.Rotate(Vector3.up * (lookInput.x * _lookSensitivity));

        _pitch -= lookInput.y * _lookSensitivity;
        _pitch = Mathf.Clamp(_pitch, -_maxPitch, _maxPitch);
        _cameraHolder.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
    }
}