using UnityEngine;
using System.Collections.Generic;

using Cinemachine;

namespace STIHOWE.Core
{

	public class Player : MonoBehaviour
	{
		[Header("Movement")]
		[SerializeField] float m_WalkSpeed = 6f;
		[SerializeField] float m_SprintSpeed = 9f;
		[SerializeField] float m_TurnSmoothTime = 0.1f;
		//[SerializeField] Transform m_cameraBaseTransform;
		float m_TurnSmoothVel;
		CharacterController m_Controller;

		[Header("Jumping")]
		[SerializeField] float m_JumpSpeed = 3f;
		[SerializeField] float m_Gravity = 9.81f;
		float m_VSpeed = 0f;
		private bool m_CanDoubleJump = false;
		[SerializeField] float m_DoubleJumpMultiplier = 0.75f;

		private void Start()
		{
			m_Controller = GetComponent<CharacterController>();
		}
		private void Update()
		{
            // Move
			Vector3 planarDirection = ApplyHorizontalForces();
			Vector3 moveDir = DampenRotation(planarDirection);
			moveDir = ApplyVerticalForces(moveDir);

			float currentSpeed = Input.GetButton("Sprint") ? m_SprintSpeed : m_WalkSpeed;
			m_Controller.Move(moveDir.normalized * currentSpeed * Time.deltaTime);

            // Attack
            Attack();
		}

		Vector3 ApplyHorizontalForces()
		{
			float x = Input.GetAxisRaw("MoveHorizontal");
			float z = Input.GetAxisRaw("MoveVertical");
			return new Vector3(x, 0f, z).normalized;
		}

		Vector3 ApplyVerticalForces(Vector3 dirVec)
		{
			if (m_Controller.isGrounded)
			{
				m_CanDoubleJump = true;
				m_VSpeed = 0f;
				if (Input.GetButtonDown("Jump"))
				{
					m_VSpeed = m_JumpSpeed;
				}
			}
			else
			{
				if (Input.GetButtonDown("Jump") && m_CanDoubleJump)
				{
					m_VSpeed = m_JumpSpeed * m_DoubleJumpMultiplier;
					m_CanDoubleJump = false;
				}
			}

			m_VSpeed -= m_Gravity * Time.deltaTime;
			dirVec.y = m_VSpeed;
			return dirVec;
		}

		Vector3 DampenRotation(Vector3 dirVec)
		{
			Vector3 moveDir = Vector3.zero;
			if (dirVec.magnitude >= 0.1f)
			{
				float targetAngle = Mathf.Atan2(dirVec.x, dirVec.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
				// Smooth the rotation
				float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_TurnSmoothVel, m_TurnSmoothTime);
				transform.rotation = Quaternion.Euler(0f, angle, 0f);

				moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
			}
			return moveDir;
		}

		void Attack()
		{
            //print(Input.GetAxisRaw("Primary Attack"));
			if (Input.GetAxisRaw("PrimaryAttack") > 0.1f)
			{
                print("Attacked");
			}
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