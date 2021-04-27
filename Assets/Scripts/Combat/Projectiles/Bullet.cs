using UnityEngine;

namespace STIHOWE.Combat.Projectiles
{
    public class Bullet : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            // TODO: Play destroy bullet animation
            print(name + " collided with " + other.transform.name);
            Destroy(gameObject);
        }
        private void OnDestroy()
        {
            //print(gameObject.name + " was destroyed.");
        }
    }
}