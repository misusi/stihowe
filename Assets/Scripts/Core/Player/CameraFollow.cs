using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STIHOWE.Core.Player
{
    enum MouseButton { Left, Right, Middle }

    public class CameraFollow : MonoBehaviour
    {

        [SerializeField] float m_camMoveSpeed = 120.0f;
        [SerializeField] GameObject m_camFollowObj;
        [SerializeField] float m_clampAngle = 67.0f;
        [SerializeField] float m_inputSensitivity = 150.0f;
        [SerializeField] bool m_invertHoriz;
        [SerializeField] bool m_invertVert;
        float m_rotY = 0.0f;
        float m_rotX = 0.0f;



        // Use this for initialization
        void Start()
        {
            Vector3 rot = transform.localRotation.eulerAngles;
            m_rotY = rot.y;
            m_rotX = rot.x;
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

            if (m_invertHoriz) finalInputX *= -1;
            if (!m_invertVert) finalInputZ *= -1;

            m_rotY += finalInputX * m_inputSensitivity * Time.deltaTime;
            m_rotX += finalInputZ * m_inputSensitivity * Time.deltaTime;

            m_rotX = Mathf.Clamp(m_rotX, -m_clampAngle, m_clampAngle);

            Quaternion localRotation = Quaternion.Euler(m_rotX, m_rotY, 0.0f);
            transform.rotation = localRotation;

            // Snap camera behind player on middle click(?)
            if (Input.GetMouseButton((int)MouseButton.Middle))
            {
                // Reset camera behind player
                // NOT the rotation, just move the position
                transform.rotation = m_camFollowObj.transform.rotation;
            }

        }

        void LateUpdate()
        {
            CameraUpdater();
        }

        void CameraUpdater()
        {
            // set the target object to follow
            Transform target = m_camFollowObj.transform;

            //move towards the game object that is the target
            float step = m_camMoveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
    }

}