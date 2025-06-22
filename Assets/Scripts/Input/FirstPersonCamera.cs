using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] TimeOfDayManager timeOfDayManager;
    [Tooltip("Rotation speed of the character")]
    [SerializeField] float rotationSpeed = 1.0f;
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    [SerializeField] GameObject cameraTarget;
    [Tooltip("How far in degrees can you move the camera up")]
    [SerializeField] float topClamp = 90.0f;
    [Tooltip("How far in degrees can you move the camera down")]
    [SerializeField] float bottomClamp = -90.0f;

    float _cinemachineTargetPitch;
    const float _minLookThreshold = 0.01f;
    float _rotationVelocity;

    PlayerInput _playerInput;
    PlayerInputHandler _inputHandler;

    bool IsCurrentDeviceMouse { get { return _playerInput.currentControlScheme == "KeyboardMouse"; } }

    private void Awake()
    {
        _playerInput = GetComponentInParent<PlayerInput>();
        _inputHandler = GetComponentInParent<PlayerInputHandler>();
    }

    private void LateUpdate()
    {
        if (timeOfDayManager.IsPaused) return;
        // if there is an input
        if (_inputHandler.LookInput.sqrMagnitude >= _minLookThreshold)
        {
            //Don't multiply mouse input by Time.deltaTime
            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

            _cinemachineTargetPitch += _inputHandler.LookInput.y * rotationSpeed * deltaTimeMultiplier;
            _rotationVelocity = _inputHandler.LookInput.x * rotationSpeed * deltaTimeMultiplier;

            // clamp our pitch rotation
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, bottomClamp, topClamp);

            // Update Cinemachine camera target pitch
            cameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

            // rotate the player left and right
            transform.Rotate(Vector3.up * _rotationVelocity);
        }
    }

    static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}
