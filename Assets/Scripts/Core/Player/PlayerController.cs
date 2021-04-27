using System;
using UnityEngine;
using STIHOWE.Combat.Projectiles;

namespace STIHOWE.Core.Player
{

    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] float m_walkSpeed = 6f;
        [SerializeField] float m_sprintSpeed = 9f;
        [SerializeField] float m_turnSmoothTime = 0.1f;
        [SerializeField] Transform m_cameraBaseTransform;
        float m_turnSmoothVel;
        CharacterController m_controller;

        [Header("Jumping")]
        [SerializeField] float m_jumpSpeed = 3f;
        [SerializeField] float m_gravity = 9.81f;
        float m_vSpeed = 0f;
        private bool m_canDoubleJump = false;
        [SerializeField] float m_doubleJumpMultiplier = 0.75f;

        private void Start()
        {
            m_controller = GetComponent<CharacterController>();
        }
        private void Update()
        {
            Vector3 planarDirection = ApplyHorizontalForces();
            Vector3 moveDir = DampenRotation(planarDirection);
            moveDir = ApplyVerticalForces(moveDir);

            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? m_sprintSpeed : m_walkSpeed;
            m_controller.Move(moveDir.normalized * currentSpeed * Time.deltaTime);

            // TODO: Remove this.
            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<BulletSpawner>().Spawn();
            }
        }

        Vector3 ApplyHorizontalForces()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            return new Vector3(x, 0f, z).normalized;
        }

        Vector3 ApplyVerticalForces(Vector3 dirVec)
        {
            if (m_controller.isGrounded)
            {
                m_canDoubleJump = true;
                m_vSpeed = 0f;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    m_vSpeed = m_jumpSpeed;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space) && m_canDoubleJump)
                {
                    m_vSpeed = m_jumpSpeed * m_doubleJumpMultiplier;
                    m_canDoubleJump = false;
                }
            }

            m_vSpeed -= m_gravity * Time.deltaTime;
            dirVec.y = m_vSpeed;
            return dirVec;
        }

        Vector3 DampenRotation(Vector3 dirVec)
        {
            Vector3 moveDir = Vector3.zero;
            if (dirVec.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(dirVec.x, dirVec.z) * Mathf.Rad2Deg + m_cameraBaseTransform.eulerAngles.y;
                // Smooth the rotation
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_turnSmoothVel, m_turnSmoothTime);
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