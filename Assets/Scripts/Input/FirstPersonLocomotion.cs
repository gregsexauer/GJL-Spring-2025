using UnityEngine;
using System.Collections;

public class FirstPersonLocomotion : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Move speed of the character in m/s")]
    [SerializeField] float moveSpeed = 4.0f;
    [Tooltip("Sprint speed of the character in m/s")]
    [SerializeField] float sprintSpeed = 6.0f;
    [Tooltip("Acceleration and deceleration")]
    [SerializeField] float speedChangeRate = 10.0f;

    [Header("Jumping and Falling")]
    [Tooltip("The initial upwards velocity of the jump")]
    [SerializeField] float jumpVelocity = 5.0f;
    [Tooltip("The character uses its own gravity value.")]
    [SerializeField] float gravity = -15.0f;
    [Tooltip("Time required to pass before being able to jump again.")]
    [SerializeField] float jumpTimeout = 0.1f;
    [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
    [SerializeField] float fallTimeout = 0.15f;

    [Header("Player Grounded")]
    [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
    [SerializeField] bool grounded = true;
    [Tooltip("Useful for rough ground")]
    [SerializeField] float groundedOffset = -0.14f;
    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    [SerializeField] float groundedRadius = 0.5f;
    [Tooltip("What layers the character uses as ground")]
    [SerializeField] LayerMask groundLayers;

    float _speed;
    float _verticalVelocity;
    float _terminalVelocity = 53.0f;

    // timeout deltatime
    bool _justJumped = false;
    float _jumpTimeoutDelta;
    float _fallTimeoutDelta;
    CharacterController _controller;
    PlayerInputHandler _inputHandler;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _inputHandler = GetComponentInParent<PlayerInputHandler>();
    }

    private void Start()
    {
        // reset our timeouts on start
        _jumpTimeoutDelta = 0;
        _fallTimeoutDelta = fallTimeout;
    }

    private void Update()
    {
        JumpAndGravity();
        GroundedCheck();
        Move();
    }

    private void JumpAndGravity()
    {
        if (grounded)
        {
            // reset the fall timeout timer
            _fallTimeoutDelta = fallTimeout;

            // stop our velocity dropping infinitely when grounded
            if (_verticalVelocity < 0.0f)
                _verticalVelocity = -2f;

            // Jump
            if (_inputHandler.JumpInput && !_justJumped)
            {
                _verticalVelocity = jumpVelocity;
                StopCoroutine(JumpTimeout());
                StartCoroutine(JumpTimeout());
            }
        }
        else
        {
            // fall timeout
            if (_fallTimeoutDelta >= 0.0f)
                _fallTimeoutDelta -= Time.deltaTime;
        }

        // apply gravity over time if under terminal
        if (_verticalVelocity < _terminalVelocity)
            _verticalVelocity += gravity * Time.deltaTime;
    }

    IEnumerator JumpTimeout()
    {
        _justJumped = true;
        _jumpTimeoutDelta = jumpTimeout;
        while (_jumpTimeoutDelta >= 0.0f)
        {
            _jumpTimeoutDelta -= Time.deltaTime;
            yield return null;
        }
        _justJumped = false;
    }

    private void GroundedCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
        grounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
    }

    private void Move()
    {
        // set target speed based on move speed, sprint speed and if sprint is pressed
        float targetSpeed = _inputHandler.SprintInput ? sprintSpeed : moveSpeed;

        // if there is no input, set the target speed to 0
        if (_inputHandler.MoveInput == Vector2.zero) targetSpeed = 0.0f;

        // a reference to the players current horizontal velocity
        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

        float speedOffset = 0.1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * speedChangeRate);

            // round speed to 3 decimal places
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }

        // normalise input direction
        Vector3 inputDirection = new Vector3(_inputHandler.MoveInput.x, 0.0f, _inputHandler.MoveInput.y).normalized;

        // if there is a move input rotate player when the player is moving
        if (_inputHandler.MoveInput != Vector2.zero)
            inputDirection = transform.right * _inputHandler.MoveInput.x + transform.forward * _inputHandler.MoveInput.y;

        // move the player
        _controller.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }
    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (grounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z), groundedRadius);
    }
}
