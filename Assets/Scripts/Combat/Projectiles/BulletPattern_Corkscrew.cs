using System.Collections.Generic;
using UnityEngine;

namespace Combat.Particles
{
    public class BulletPattern_Corkscrew : BulletPattern_Base
    {
        float m_radius = 1.5f;
        float m_outwardSpeed = 0.5f;
        float m_rotationSpeed = 2.5f;
        bool m_atMaxRadius;

        void Update()
        {
            if (transform.childCount == 0) return;

            MoveForward();
            for (int i = 0; i < transform.childCount; i++)
            {
                RotateAboutOrigin();
                MoveOutward();
            }
        }

        void MoveForward()
        {
            transform.position += Vector3.forward * m_speed * Time.deltaTime;
        }

        void MoveOutward()
        {
            // if (!m_atMaxRadius && )
            foreach (Bullet bullet in GetComponentInChildren<Transform>())
            {
                bullet.transform.position += bullet.transform.position.normalized * m_outwardSpeed * Time.deltaTime;
            }
            // m_atMaxRadius =  Vector3.Magnitude(GetComponentInChildren<Transform> ) >= m_radius;
        }
        void RotateAboutOrigin()
        {

        }
    }
}