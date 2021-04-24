using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Player
{
    enum MouseButton { Left, Right, Middle }

    public class CameraFollow : MonoBehaviour
    {

        [SerializeField] float CameraMoveSpeed = 120.0f;
        [SerializeField] GameObject CameraFollowObj;
        [SerializeField] float clampAngle = 67.0f;
        [SerializeField] float inputSensitivity = 150.0f;
        [SerializeField] bool invertHoriz;
        [SerializeField] bool invertVert;
        float rotY = 0.0f;
        float rotX = 0.0f;



        // Use this for initialization
        void Start()
        {
            Vector3 rot = transform.localRotation.eulerAngles;
            rotY = rot.y;
            rotX = rot.x;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Update is called once per frame
        void Update()
        {

            // We setup the rotation of the sticks here
            float inputX = Input.GetAxis("RightStickHorizontal");
            float inputZ = Input.GetAxis("RightStickVertical");
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            float finalInputX = inputX + mouseX;
            float finalInputZ = inputZ + mouseY;

            if (invertHoriz) finalInputX *= -1;
            if (!invertVert) finalInputZ *= -1;

            rotY += finalInputX * inputSensitivity * Time.deltaTime;
            rotX += finalInputZ * inputSensitivity * Time.deltaTime;

            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            transform.rotation = localRotation;

            // Snap camera behind player on middle click(?)
            if (Input.GetMouseButton((int)MouseButton.Middle))
            {
                // Reset camera behind player
                // NOT the rotation, just move the position
                transform.rotation = CameraFollowObj.transform.rotation;
            }

        }

        void LateUpdate()
        {
            CameraUpdater();
        }

        void CameraUpdater()
        {
            // set the target object to follow
            Transform target = CameraFollowObj.transform;

            //move towards the game object that is the target
            float step = CameraMoveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
    }

}