using UnityEngine;

namespace Core.Player
{

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float speed = 6f;
        [SerializeField] float turnSmoothTime = 0.1f;
        float turnSmoothVelocity;
        CharacterController charController;
        [SerializeField] Transform cameraTransform;
        float gravity = 9.81f;
        float vSpeed = 0f;

        private void Start()
        {
            charController = GetComponent<CharacterController>();
        }
        private void Update()
        {
            float movementHoriz = Input.GetAxisRaw("Horizontal");
            float movementVert = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(movementHoriz, 0f, movementVert).normalized;
            if (direction.magnitude >= 0.1f)
            {
                // ROTATION
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
                // Smooth the rotation
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                charController.Move(moveDir.normalized * speed * Time.deltaTime);
                // BUGFIX:
                // HOLY FUCK. Character floating above ground due to char-controller.
                // 1. Don't have 2 colliders, c.c. already has a collider.
                // 2. ACTUALLY SET the shape of the c.c. collider correctly (above ground).
                // 3. Reduce skin width variable to prevent char from being pushed up.
            }

        }

        void ApplyGravity()
        {

        }
    }
}