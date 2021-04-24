using UnityEngine;

namespace Core.Player
{

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float speed = 6f;
        [SerializeField] float turnSmoothTime = 0.1f;
        float turnSmoothVelocity;
        CharacterController controller;
        Transform cameraTransform;

        private void Start()
        {
            controller = GetComponent<CharacterController>();
            if (controller == null)
            {
                Debug.LogError("No CharacterController component found on object.");
            }
            cameraTransform = Camera.main.transform;
        }
        private void Update()
        {


            float movementHoriz = Input.GetAxisRaw("Horizontal");
            float movementVert = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(movementHoriz, 0f, movementVert).normalized;
            if (direction.magnitude >= 0.1f)
            {
                // Find angle character should face
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                // print("Before gravity: " + moveDir);
                // moveDir += GetGravityEffect();
                // print("After gravity: " + moveDir);
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
        }

        Vector3 GetGravityEffect()
        {
            return controller.isGrounded ? Vector3.zero : Physics.gravity;
        }
    }
}