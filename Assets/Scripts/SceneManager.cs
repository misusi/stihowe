using UnityEngine;
using STIHOWE.Bullets;

namespace STIHOWE.Core
{
    public class SceneManager : MonoBehaviour
    {
        public Player Player;
        public BulletManager BulletManager;
        public static SceneManager Instance { get; private set; }

        void Awake()
        {
            if (Instance == null) { Instance = this; }
            else { Destroy(gameObject); }

            // Cache references to all desired variables
            Player = FindObjectOfType<Player>();
            BulletManager = GetComponent<BulletManager>();
        }
    }
}