using UnityEngine;

namespace Core.Player
{

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] Transform cam;
        [SerializeField] float speed = 6f;
        [SerializeField] float turnSmoothTime = 0.1f;
        float turnSmoothVelocity;
        CharacterController controller;

        private void Start()
        {
            controller = GetComponent<CharacterController>();
            if (controller == null)
            {
                Debug.LogError("No CharacterController component found on object.");
            }
        }
        private void Update()
        {
            float movementHoriz = Input.GetAxisRaw("Horizontal");
            float movementVert = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(movementHoriz, 0f, movementVert).normalized;
            if (direction.magnitude >= 0.1f)
            {
                // Find angle character should face
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
        }
    }
}