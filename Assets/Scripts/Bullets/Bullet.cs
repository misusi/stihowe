using UnityEngine;
using STIHOWE.Constants;

namespace STIHOWE.Bullets
{
    //Bullets:
    // Owner - who shot this
    //      A type? TAG?
    //      Or just something like an isPlayerBullet bool?
    // is active (unity already does it)
    // move function
    // damage
    // origin
    // trajectory
    public class Bullet : MonoBehaviour
    {
        bool m_IsDead;
        public string m_OwnerTag;
        public int m_PoolIndex;

        public bool IsDead() { return m_IsDead; }
        public string GetOwnerTag() { return m_OwnerTag; }
        public int GetPoolIndex() { return m_PoolIndex; }


        public Bullet(string tag, int poolIndex)
        {
            m_OwnerTag = tag;
            m_IsDead = false;
            m_PoolIndex = poolIndex;
        }


        private void Start()
        {
            gameObject.layer = Layers.Bullets;
        }
        private void OnCollisionEnter(Collision other)
        {
            // TODO: Play destroy bullet animation
            print(name + " collided with " + other.transform.name);

            Destroy(gameObject);
        }

        private void Update()
        {
            // should do the movement!
        }
    }
}