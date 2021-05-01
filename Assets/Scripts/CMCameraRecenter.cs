using UnityEngine;
using Cinemachine;

public class CMCameraRecenter : MonoBehaviour
{
	CinemachineFreeLook m_CMFreeLookCam;
	float m_RecenterTime;
	float m_THENUMBERTHATMAGICALLYMAKESTHECINEMACHINERECENTERINGTIMEEQUALMINE;
	float m_TimeSinceRecenter = 0f;
	bool m_CurrentlyRecentering = false;
	private void Start()
	{
		m_CMFreeLookCam = GetComponent<CinemachineFreeLook>();
		// TODO: Is there really no built-in way to do this through
		// Cinemachine?
		m_RecenterTime =
			m_THENUMBERTHATMAGICALLYMAKESTHECINEMACHINERECENTERINGTIMEEQUALMINE
			* m_CMFreeLookCam.m_YAxisRecentering.m_RecenteringTime;
	}
	void Update()
	{
		if (Input.GetButton("RecenterCamera")) StartCentering();

		if (m_CurrentlyRecentering)
		{
			m_TimeSinceRecenter += Time.deltaTime;
			if (m_TimeSinceRecenter >= m_RecenterTime) StopCentering();
		}
	}

	void StartCentering()
	{
		m_CurrentlyRecentering = true;
		m_CMFreeLookCam.m_RecenterToTargetHeading.m_enabled = true;
		m_CMFreeLookCam.m_YAxisRecentering.m_enabled = true;
		m_CMFreeLookCam.m_RecenterToTargetHeading.RecenterNow();
		m_CMFreeLookCam.m_YAxisRecentering.RecenterNow();
	}
	void StopCentering()
	{

		m_CMFreeLookCam.m_RecenterToTargetHeading.m_enabled = false;
		m_CMFreeLookCam.m_YAxisRecentering.m_enabled = false;
		m_CurrentlyRecentering = false;
		m_TimeSinceRecenter = 0f;
	}
}