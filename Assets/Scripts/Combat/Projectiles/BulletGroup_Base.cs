using System.Collections.Generic;
using UnityEngine;

namespace STIHOWE.Combat.Projectiles
{
    public class BulletGroup_Base : MonoBehaviour
    {
        [SerializeReference] protected Bullet m_bulletPrefab;
        [SerializeReference] protected float m_speed = 10f;
        [SerializeReference] protected uint m_numBullets;
        [SerializeReference] protected float m_damagePerBullet;

        protected virtual void Start()
        // Instantiate bullets; Set parent to this pattern
        {
            for (int i = 0; i < m_numBullets; i++)
            {
                Instantiate(m_bulletPrefab, transform).transform.SetParent(transform);
            }
            print("Base: Start");
        }

        protected virtual void Stop()
        // Destroying parent also destroys all children
        {
            Destroy(gameObject);
            print("Base: Stop");
        }
    }
}