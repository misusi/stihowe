using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STIHOWE.Core
{
    public class SkyCam : MonoBehaviour
    {
        [SerializeField] float m_Distance = 10f;
        [SerializeField] float m_SmoothSpeed = 0.125f;
        Transform m_PlayerTrans;
        void Start()
        {
            m_PlayerTrans = SceneManager.Instance.Player.transform;
        }

        void Update() {
            // Keep camera m_distance away from player
            Vector3 playerToCameraDir = new Vector3(m_Distance, m_Distance, m_Distance).normalized;
            Vector3 destiredPos = m_PlayerTrans.position + m_Distance * playerToCameraDir;
            Vector3 smoothedPos = Vector3.Lerp(transform.position, destiredPos, m_SmoothSpeed * Time.deltaTime);
            transform.position = smoothedPos;
            transform.LookAt(m_PlayerTrans);
        }
    }
}
