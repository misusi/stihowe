using UnityEngine;
using STIHOWE.Core;

namespace STIHOWE.Core
{
    public class SceneManager : MonoBehaviour
    {
        public Player player;
        public static SceneManager Instance { get; private set; }

        void Awake()
        {
            if (Instance == null) { Instance = this; }
            else { Destroy(gameObject); }
            // Cache references to all desired variables
            player = FindObjectOfType<Player>();
        }
    }
}