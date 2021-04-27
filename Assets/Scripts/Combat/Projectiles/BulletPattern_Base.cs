using System.Collections.Generic;
using UnityEngine;

namespace Combat.Particles
{
    public class BulletPattern_Base : MonoBehaviour
    {
        [SerializeReference] protected Bullet m_bulletPrefab;
        [SerializeReference] protected float m_speed = 10f;
        [SerializeReference] protected uint m_numBullets;
        [SerializeReference] protected float m_damagePerBullet;

        void Start()
        // Instantiate bullets; Set parent to this pattern
        {
            for (int i = 0; i < m_numBullets; i++)
            {
                Instantiate(m_bulletPrefab, transform).transform.SetParent(transform);
            }
        }

        void Destroy()
        // Destroying parent also destroys all children
        {
            Destroy(gameObject);
        }
    }
}