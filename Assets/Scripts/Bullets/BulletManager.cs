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
        // TODO: Maybe make this array dynamic
        public Bullet[] bulletArray;
        public List<int> availableBulletIndexes;
        public int currentCapacity;
        public int count = 0;
        public BulletPool(int size = 2000)
        {
            currentCapacity = size;
            bulletArray = new Bullet[size];
            availableBulletIndexes = new List<int>();
        }
        public bool IsFull()
        {
            return availableBulletIndexes.Count == 0;
        }
        public void Clear()
        {
            foreach (int index in availableBulletIndexes)
            {
                bulletArray[index].gameObject.SetActive(false);
            }
        }
        public void Expand() { }
        public void Shrink() { }
        // Game object Bullet disable itself on collision, etc.
        // No need to do that here w/ Add/Remove
        public void Add(int indexToAdd) { count++; }
        public void Remove(int indexToRemove) { count--; }
    }

    public class BulletManager : MonoBehaviour
    {
        public readonly int MAX_BULLETS;
        public static BulletManager Instance { get; private set; }
        public BulletPool BulletPool;
        public int ActiveCount
        {
            get { return BulletPool.count; }
        }

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

        // TODO: Async this?
        // TODO: Thread this?
        public int GetNextAvailableBulletIndex()
        {
            if (BulletPool.IsFull())
            {
                // All bullets in use: Must start pruning
                // Or just wait? Or just increase the size?
                // mufucka mufasa
                return -1;
            }
            else
            {
                int n = -1;
                while (true)
                // TODO: Another break condition where array is expanded
                // after a certain number of attempts.
                {
                    n = Random.Range(0, MAX_BULLETS);
                    if (BulletPool.bulletArray[n].m_IsDead)
                    {
                        break;
                    }
                }
                return n;
            }

        }
    }
}