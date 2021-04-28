using UnityEngine;

namespace STIHOWE.Core
{

    public class CameraCollision : MonoBehaviour
    {
        [SerializeField] float m_MinDistance = 1f;
        [SerializeField] float m_MaxDistance = 3f;
        [SerializeField] float m_MovementSmooth = 5f;
        Vector3 m_DollyDir;
        [SerializeField] Vector3 m_DollyDirAdjusted;
        [SerializeField] float m_Distance;

        private void Awake()
        {
            m_DollyDir = transform.localPosition.normalized;
            m_Distance = transform.localPosition.magnitude;
        }

        private void Update()
        {
            Vector3 desiredCameraPosition = transform.parent.
                TransformPoint(m_DollyDir * m_MaxDistance);

            RaycastHit hit;
            if (Physics.Linecast(transform.parent.position, desiredCameraPosition, out hit))
            {
                m_Distance = Mathf.Clamp((hit.distance * 0.87f), m_MinDistance, m_MaxDistance);
            }
            else
            {
                m_Distance = m_MaxDistance;
            }
            transform.localPosition = Vector3.Lerp(transform.localPosition, m_DollyDir * m_Distance, Time.deltaTime * m_MovementSmooth);
        }
    }
}