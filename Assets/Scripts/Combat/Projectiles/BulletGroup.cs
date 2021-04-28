using System.Collections.Generic;
using System;
using UnityEngine;
using STIHOWE.Core;
using STIHOWE.Constants;

namespace STIHOWE.Combat.Projectiles
{
    public enum BulletGroupType
    {
        Linear, Fork, Corkscrew, Sine
    }
    public class BulletGroup : MonoBehaviour
    {
        [SerializeField] Bullet m_bulletPrefab;
        [SerializeField] BulletGroupType m_groupType;
        [SerializeField] float m_speed = 10f;
        [SerializeField] public uint m_numBullets;
        [SerializeField] float m_damagePerBullet;
        Vector3 m_direction;
        Vector3 m_origin;

        private void Start()
        {
            for (int i = 0; i < m_numBullets; i++)
            {
                Bullet newBullet = Instantiate(
                    m_bulletPrefab, 
                    SceneManager.Instance.player.m_firePoint.position, 
                    SceneManager.Instance.player.transform.rotation);
                newBullet.gameObject.layer = Layers.Bullets;
                newBullet.transform.SetParent(transform);
            }
        }

        private void Update()
        {
            if (transform.childCount >= 1)
            {
            // TODO: Improve this switch... Maybe. Maybe inheritance/interface, functional parameters... Maybe not.
                switch(m_groupType)
                {
                    case (BulletGroupType.Linear):
                        MoveLinear();
                        break;
                    case (BulletGroupType.Fork):
                        MoveFork();
                        break;
                    case (BulletGroupType.Corkscrew):
                        MoveCorkscrew();
                        break;
                    case (BulletGroupType.Sine):
                        MoveSine();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void MoveLinear()
        {
            for (int i = 0; i < m_numBullets; i++)
            {

            }
        }
        void MoveFork()
        {
        }
        void MoveCorkscrew()
        {
        }
        void MoveSine()
        {
        }

    }
}