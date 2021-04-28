using UnityEngine;
using STIHOWE.Constants;

namespace STIHOWE.Combat.Projectiles
{
    public class Bullet : MonoBehaviour
    {
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
    }
}