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

    public class BulletPool
    {
        public Bullet[] bulletArray;
        public List<int> activeBulletIndexes;
        public readonly int capacity;
        public BulletPool(int size = 2000)
        {
            capacity = size;
            bulletArray = new Bullet[size];
            activeBulletIndexes = new List<int>();
        }
        bool IsFull()
        {
            return activeBulletIndexes.Count >= capacity;
        }
        void Clear()
        {
            foreach (int index in activeBulletIndexes)
            {
                bulletArray[index].gameObject.SetActive(false);
            }
        }
        void Prune()
        {
        }
        void Add()
        {
        }
    }

    public class BulletManager : MonoBehaviour
    {
        public readonly int MAX_BULLETS;
        public static BulletManager Instance { get; private set; }
        public BulletPool BulletPool;

        public BulletManager(int size)
        {
            MAX_BULLETS = size;
            this.BulletPool = new BulletPool(size);
        }

        void Awake()
        {
            if (Instance == null) { Instance = this; }
            else { Destroy(gameObject); }
            // // Cache references to all desired variables
        }
        void Start()
        {
        }

        public int GetAssignedBulletIndex()
        {
            // All bullets in use: Must start pruning
            if (BulletPool.activeBulletIndexes.Count == MAX_BULLETS)
            {

            }

            // mufucka mufasa
            return -1;
        }
    }
}