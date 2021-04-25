using System;
using UnityEngine;

namespace Core.Player
{

    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] float _walkSpeed = 6f;
        [SerializeField] float _sprintSpeed = 9f;
        [SerializeField] float _turnSmoothTime = 0.1f;
        [SerializeField] Transform _cameraBaseTransform;
        float _turnSmoothVel;
        CharacterController _controller;

        [Header("Jumping")]
        [SerializeField] float _jumpSpeed = 3f;
        [SerializeField] float _gravity = 9.81f;
        float _vSpeed = 0f;
        private bool _canDoubleJump = false;
        [SerializeField] float _doubleJumpMultiplier = 0.75f;

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
        }
        private void Update()
        {
            Vector3 planarDirection = ApplyHorizontalForces();
            Vector3 moveDir = DampenRotation(planarDirection);
            moveDir = ApplyVerticalForces(moveDir);

            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? _sprintSpeed : _walkSpeed;
            _controller.Move(moveDir.normalized * currentSpeed * Time.deltaTime);
        }

        Vector3 ApplyHorizontalForces()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            return new Vector3(x, 0f, z).normalized;
        }

        Vector3 ApplyVerticalForces(Vector3 dirVec)
        {
            if (_controller.isGrounded)
            {
                _canDoubleJump = true;
                _vSpeed = 0f;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _vSpeed = _jumpSpeed;
                }
            }
            else
            {
                if(Input.GetKeyDown(KeyCode.Space) && _canDoubleJump)
                {
                    _vSpeed = _jumpSpeed * _doubleJumpMultiplier;
                    _canDoubleJump = false;
                }
            }

            _vSpeed -= _gravity * Time.deltaTime;
            dirVec.y = _vSpeed;
            return dirVec;
        }

        Vector3 DampenRotation(Vector3 dirVec)
        {
            Vector3 moveDir = Vector3.zero;
            if (dirVec.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(dirVec.x, dirVec.z) * Mathf.Rad2Deg + _cameraBaseTransform.eulerAngles.y;
                // Smooth the rotation
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVel, _turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            }
            return moveDir;
        }
    }
}

// BUGFIX:
// 1. HOLY FUCK. Character floating above ground due to char-controller.
//      1. Don't have 2 colliders, c.c. already has a collider.
//      2. ACTUALLY SET the shape of the c.c. collider correctly (above ground).
//      3. Reduce skin width variable to prevent char from being pushed up.
// 2. Character blasts off into sky like jimmy nuttrin
//      1. Pay attention to sign on gravity in inspector.