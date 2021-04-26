using System;
using System.Collections.Generic;
using UnityEngine;

namespace Combat.Projectiles
{
    public class SpawnProjectiles : MonoBehaviour
    {
        [SerializeField] GameObject _firePoint;
        [SerializeField] List<GameObject> _vfx = new List<GameObject>();
        GameObject _effectToSpawn;
        private void Start()
        {
            _effectToSpawn = _vfx[0];
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SpawnFX();
            }
        }

        private void SpawnFX()
        {
            GameObject effect ;
            if (_firePoint != null)
            {
                effect = Instantiate(
                    _effectToSpawn, 
                    _firePoint.transform.position, 
                    Quaternion.identity);
            }
            else{
                Debug.LogError("firePoint is null");
            }
        }
    }
}