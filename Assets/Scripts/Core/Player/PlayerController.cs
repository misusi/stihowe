using System;
using UnityEngine;

namespace Core.Player
{

    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] float speed = 6f;
        [SerializeField] float turnSmoothTime = 0.1f;
        [SerializeField] Transform cameraTransform;
        float turnSmoothVelocity;
        CharacterController charController;

        [Header("Jumping")]
        [SerializeField] float jumpSpeed = 3f;
        [SerializeField] float gravity = -9.81f;
        float vSpeed = 0f;

        private void Start()
        {
            charController = GetComponent<CharacterController>();
        }
        private void Update()
        {
            Vector3 planarDirection = GetInputXZ();
            Vector3 moveDir = DampenRotation(planarDirection);
            moveDir = ApplyVerticalForces(moveDir);
            charController.Move(moveDir.normalized * speed * Time.deltaTime);
            print(charController.isGrounded);
        }

        Vector3 GetInputXZ()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            return new Vector3(x, 0f, z).normalized;

        }
        Vector3 DampenRotation(Vector3 dirVec)
        {
            Vector3 moveDir = Vector3.zero;
            if (dirVec.magnitude >= 0.1f)
            {
                // ROTATION
                float targetAngle = Mathf.Atan2(dirVec.x, dirVec.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
                // Smooth the rotation
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            }
            return moveDir;
        }

        Vector3 ApplyVerticalForces(Vector3 dirVec)
        {
            if (charController.isGrounded)
            {
                vSpeed = 0f;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    vSpeed = jumpSpeed;
                }
            }

            vSpeed -= gravity * Time.deltaTime;
            dirVec.y = vSpeed;
            return dirVec;
        }
    }
}

// BUGFIX:
// HOLY FUCK. Character floating above ground due to char-controller.
// 1. Don't have 2 colliders, c.c. already has a collider.
// 2. ACTUALLY SET the shape of the c.c. collider correctly (above ground).
// 3. Reduce skin width variable to prevent char from being pushed up.