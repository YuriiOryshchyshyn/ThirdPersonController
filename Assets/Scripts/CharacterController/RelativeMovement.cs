using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpSpeed = 5f;
    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private float _termonalVelicity = -10f;
    [SerializeField] private float _minFall = -1.5f;
    [SerializeField] private float pushForce = 3f;

    public float _verticalSpeed;

    private CharacterController _characterController;

    private float _verticalAxis;
    private float _horizontalAxis;

    private Vector3 movement;
    private ControllerColliderHit _contact;
    private Animator _animator;

    private const string IDLE = "Idle";
    private const string RUN = "Run";
    private const string JUMP = "Jump";

    private string _currentAnimation;
    private bool _jumpNow;
    public bool hitGround;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _verticalSpeed = _minFall;
        _animator = GetComponent<Animator>();
    }

    public void ChangeAnimation(string newAnimation)
    {
        if (_currentAnimation == newAnimation) return;

        _animator.Play(newAnimation);
        _currentAnimation = newAnimation;
    }

    private void Update()
    {
        movement = Vector3.zero;

        hitGround = HitGround();
        UseGravity(hitGround);

        _verticalAxis = Input.GetAxis("Vertical");
        _horizontalAxis = Input.GetAxis("Horizontal");

        if ((_verticalAxis != 0 || _horizontalAxis != 0))
        {
            movement.x = _horizontalAxis * _moveSpeed;
            movement.z = _verticalAxis * _moveSpeed;
            movement.y = 0;
            movement = Vector3.ClampMagnitude(movement, _moveSpeed);

            Quaternion tmp = _target.rotation;
            _target.eulerAngles = new Vector3(0, _target.eulerAngles.y, 0);
            movement = _target.TransformDirection(movement);
            _target.rotation = tmp;

            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, _rotationSpeed * Time.deltaTime);
            if (!_jumpNow)
            {
                ChangeAnimation(RUN);
            }
        }
        else
        {
            if (!_jumpNow)
            {
                ChangeAnimation(IDLE);
            }
        }

        movement.y = _verticalSpeed;
        movement *= Time.deltaTime;
        _characterController.Move(movement);
    }

    private bool HitGround()
    {
        bool hitGround = false;
        RaycastHit hit;

        if (_verticalSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            hitGround = hit.distance < 0.11;
        }

        return hitGround;
    }

    private void UseGravity(bool hitGround)
    {
        if (hitGround)
        {
            _jumpNow = false;

            if (Input.GetButton("Jump"))
            {
                _verticalSpeed = _jumpSpeed;
                ChangeAnimation(JUMP);
                _jumpNow = true;
            }
            else
            {
                _verticalSpeed = _minFall;
            }
        }
        else
        {
            _verticalSpeed += _gravity * 5 * Time.deltaTime;

            if (_verticalSpeed < _termonalVelicity)
            {
                _verticalSpeed = _termonalVelicity;
            }

            if (_characterController.isGrounded)
            {
                if (Vector3.Dot(movement, _contact.normal) < 0)
                {
                    movement = _contact.normal * _moveSpeed;
                }
                else
                {
                    movement += _contact.normal * _moveSpeed;
                }
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _contact = hit;

        Rigidbody rigidbody = hit.collider.attachedRigidbody;
        if (rigidbody != null && !rigidbody.isKinematic)
        {
            rigidbody.velocity = hit.moveDirection * pushForce;
        }
    }
}
