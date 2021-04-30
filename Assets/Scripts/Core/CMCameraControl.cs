using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CMCameraControl : MonoBehaviour
{
	CinemachineFreeLook cam;
    float m_Height;
    float m_Radius;
	void Start()
	{
		cam = GetComponent<Cinemachine.CinemachineFreeLook>();
	}

	void Update()
	{
	}
}
