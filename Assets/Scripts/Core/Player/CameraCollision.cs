using UnityEngine;

namespace Core.Player
{

    public class CameraCollision : MonoBehaviour
    {
        [SerializeField] float m_minDistance = 1f;
        [SerializeField] float m_maxDistance = 3f;
        [SerializeField] float m_movementSmooth = 5f;
        Vector3 m_dollyDir;
        [SerializeField] Vector3 m_dollyDirAdjusted;
        [SerializeField] float m_distance;

        private void Awake()
        {
            m_dollyDir = transform.localPosition.normalized;
            m_distance = transform.localPosition.magnitude;
        }

        private void Update()
        {
            Vector3 desiredCameraPosition = transform.parent.
                TransformPoint(m_dollyDir * m_maxDistance);

            RaycastHit hit;
            if (Physics.Linecast(transform.parent.position, desiredCameraPosition, out hit))
            {
                m_distance = Mathf.Clamp((hit.distance * 0.87f), m_minDistance, m_maxDistance);
            }
            else
            {
                m_distance = m_maxDistance;
            }
            transform.localPosition = Vector3.Lerp(transform.localPosition, m_dollyDir * m_distance, Time.deltaTime * m_movementSmooth);
        }
    }
}