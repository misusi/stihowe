using UnityEngine;
using STIHOWE.Core;
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
            BulletManager = new BulletManager(BulletManager.MAX_BULLETS);
        }
    }
}