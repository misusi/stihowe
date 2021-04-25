using UnityEngine;

namespace Core.Player
{

    public class CameraCollision : MonoBehaviour
    {
        [SerializeField] float _minDistance = 1f;
        [SerializeField] float _maxDistance = 3f;
        [SerializeField] float _movementSmooth = 5f;
        Vector3 _dollyDir;
        [SerializeField] Vector3 _dollyDirAdjusted;
        [SerializeField] float _distance;

        private void Awake()
        {
            _dollyDir = transform.localPosition.normalized;
            _distance = transform.localPosition.magnitude;
        }

        private void Update()
        {
            Vector3 desiredCameraPosition = transform.parent.
                TransformPoint(_dollyDir * _maxDistance);

            RaycastHit hit;
            if (Physics.Linecast(transform.parent.position, desiredCameraPosition, out hit))
            {
                _distance = Mathf.Clamp((hit.distance * 0.87f), _minDistance, _maxDistance);
            }
            else
            {
                _distance = _maxDistance;
            }
            transform.localPosition = Vector3.Lerp(transform.localPosition, _dollyDir * _distance, Time.deltaTime * _movementSmooth);
        }
    }
}