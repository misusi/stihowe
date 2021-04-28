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
        public string m_OwnerTag;
        public bool m_IsDead;

        // MovementType
        // enum...

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