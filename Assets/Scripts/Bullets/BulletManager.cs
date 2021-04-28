// All entities send *request* to singleton bullet manager
// to spawn a bullet 
// Request:
//      Send in bullet as param
//      So 2 queues: 1 = bullet l
//      
// Keep MAX bullets in container at all times
//      Reuse them
//      Turn them on/off
//
// Circular list or array - keep track of next free index

using System.Collections.Generic;
using UnityEngine;

namespace STIHOWE.Bullets
{

    public class BulletRequest
    {
        Vector3 m_Location;
        Quaternion m_Rotation;
    }

    public class BulletManager : MonoBehaviour
    {
        public const uint MaxBullets = 2000;
        public static BulletManager Instance { get; private set; }
        public Bullet[] m_BulletPool;
        public List<uint> m_ActiveBulletIndexes;

        void Awake()
        {
            if (Instance == null) { Instance = this; }
            else { Destroy(gameObject); }
            // // Cache references to all desired variables
            // player = FindObjectOfType<Player>();
            m_BulletPool = new Bullet[MaxBullets];
            m_ActiveBulletIndexes = new List<uint>();
        }
        void Start()
        {
        }

        public void Request(Bullet reqBullet)
        {
            // InstantiateAt(...);
        }
    }
}