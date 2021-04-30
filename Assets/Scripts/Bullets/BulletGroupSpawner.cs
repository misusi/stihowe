using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using STIHOWE.Constants;

namespace STIHOWE.Combat.Projectiles
{
    public class BulletGroupSpawner : MonoBehaviour
    {
        [SerializeField] BulletGroup m_BulletGroup;
        void Update()
        {
            //if (Input.GetMouseButtonDown(0))
            //{
            //    Instantiate(m_BulletGroup);
            //}
        }

    }
}