using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STIHOWE.Core
{
    enum MouseButton { Left, Right, Middle }

    public class CameraFollow : MonoBehaviour
    {

        [SerializeField] float m_CamMoveSpeed = 120.0f;
        [SerializeField] GameObject m_CamFollowObj;
        [SerializeField] float m_ClampAngle = 67.0f;
        [SerializeField] float m_InputSensitivity = 150.0f;
        [SerializeField] bool m_InvertHoriz;
        [SerializeField] bool m_InvertVert;
        float m_RotY = 0.0f;
        float m_RotX = 0.0f;



        // Use this for initialization
        void Start()
        {
            Vector3 rot = transform.localRotation.eulerAngles;
            m_RotY = rot.y;
            m_RotX = rot.x;
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

            if (m_InvertHoriz) finalInputX *= -1;
            if (!m_InvertVert) finalInputZ *= -1;

            m_RotY += finalInputX * m_InputSensitivity * Time.deltaTime;
            m_RotX += finalInputZ * m_InputSensitivity * Time.deltaTime;

            m_RotX = Mathf.Clamp(m_RotX, -m_ClampAngle, m_ClampAngle);

            Quaternion localRotation = Quaternion.Euler(m_RotX, m_RotY, 0.0f);
            transform.rotation = localRotation;

            // Snap camera behind player on middle click(?)
            if (Input.GetMouseButton((int)MouseButton.Middle))
            {
                // Reset camera behind player
                // NOT the rotation, just move the position
                transform.rotation = m_CamFollowObj.transform.rotation;
            }

        }

        void LateUpdate()
        {
            CameraUpdater();
        }

        void CameraUpdater()
        {
            // set the target object to follow
            Transform target = m_CamFollowObj.transform;

            //move towards the game object that is the target
            float step = m_CamMoveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
    }

}