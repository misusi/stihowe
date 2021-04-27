using System.Collections.Generic;
using UnityEngine;

namespace STIHOWE.Combat.Projectiles
{
    public class BulletGroup_Corkscrew : BulletGroup_Base
    {
        [SerializeField] float m_radius = 1.5f;
        [SerializeField] float m_outwardSpeed = 0.5f;
        [SerializeField] float m_rotationSpeed = 2.5f;
        bool m_atMaxRadius;
        Vector3 m_fowardDir;

        protected override void Start()
        {
            m_fowardDir = transform.parent.forward;
            transform.position = transform.parent.position;
            base.Start();
            print("Child: Start");
        }

        protected override void Stop()
        {
            base.Stop();
            Destroy(gameObject);
            print("Child: Stop");
        }

        void Update()
        {
            if (transform.childCount == 0)
            {
                Stop();
            }
            else
            {
                MoveForward();
                for (int i = 0; i < transform.childCount; i++)
                {
                    //RotateAboutOrigin();
                    //MoveOutward();
                }
            }
        }

        void MoveForward()
        {
            transform.position += m_fowardDir * m_speed * Time.deltaTime;
        }
        void MoveOutward()
        {
            // if (!m_atMaxRadius && )
            foreach (Transform t in GetComponentInChildren<Transform>())
            {
                t.transform.position += t.transform.position.normalized * m_outwardSpeed * Time.deltaTime;
            }
            // m_atMaxRadius =  Vector3.Magnitude(GetComponentInChildren<Transform> ) >= m_radius;
        }
        void RotateAboutOrigin()
        {

        }
    }
}