using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using STIHOWE.Constants;

namespace STIHOWE.Combat.Projectiles
{
    public class BulletSpawner : MonoBehaviour
    {
        [SerializeField] BulletGroup_Base m_bulletGroup;
        [SerializeField] Transform m_spawnPointTransform;
        public void Spawn()
        {
            Instantiate(m_bulletGroup, m_spawnPointTransform)
                .transform.SetParent(m_spawnPointTransform);
        }
    }
}