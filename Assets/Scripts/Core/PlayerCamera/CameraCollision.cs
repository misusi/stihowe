using UnityEngine;

namespace Core.PlayerCamera
{

    public class CameraCollision : MonoBehaviour
    {
        [SerializeField] float minDistance = 1f;
        [SerializeField] float maxDistance = 3f;
        [SerializeField] float smooth = 5f;
        Vector3 dollyDir;
        [SerializeField] Vector3 dollyDirAdjusted;
        [SerializeField] float distance;

        private void Awake()
        {
            dollyDir = transform.localPosition.normalized;
            distance = transform.localPosition.magnitude;
        }

        private void Update()
        {
            Vector3 desiredCameraPosition = transform.parent.
                TransformPoint(dollyDir * maxDistance);

            RaycastHit hit;
            if (Physics.Linecast(transform.parent.position, desiredCameraPosition, out hit))
            {
                distance = Mathf.Clamp((hit.distance * 0.87f), minDistance, maxDistance);
            }
            else
            {
                distance = maxDistance;
            }
            transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
        }
    }
}