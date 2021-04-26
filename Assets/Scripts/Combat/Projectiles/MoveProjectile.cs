using UnityEngine;

namespace Combat.Projectiles
{
    public class MoveProjectile : MonoBehaviour
    {
        [SerializeField] float _speed = 5f;
        [SerializeField] float _fireRate = 3f;
        [SerializeField] float _ttl = 5f;
        GameObject _player;
        Vector3 _moveDirection;
        float _timeAlive = 0f;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _moveDirection = _player.transform.forward.normalized;
        }

        void Update()
        {
            if (_timeAlive >= _ttl) Destroy(gameObject);
            else
            {
                if (_speed != 0)
                {
                    transform.position += _moveDirection * _speed * Time.deltaTime;
                }
            }
            _timeAlive += Time.deltaTime;

        }
        private void OnCollisionEnter(Collision other)
        {
            Destroy(gameObject);
        }
    }
}