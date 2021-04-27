using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnDestroy()
    {
        print(gameObject.name + " was destroyed.");
    }
}