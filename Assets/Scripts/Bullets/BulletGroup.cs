using System.Collections.Generic;
using System;
using UnityEngine;
using STIHOWE.Core;
using STIHOWE.Constants;
using STIHOWE.Bullets;



/*
		UNITY COORDINATE SYSTEM TOP DOWN
**All Unity Mathf Functions Use Radians, Not Degrees
					   Z
				       0*
                       |
                       |
                       |
                       |    /
                       |30 /
                       |--/
                       | /
                       |/
270* ---------------------------------------- 90* X
                       |
                       |
                       |
                       |
                       |
                       |
                       |
                       |
				      180*
*/

namespace STIHOWE.Combat.Projectiles
{

    /*
	 BULLET GROUP TYPE DESCRIPTIONS:
		Linear:
			- Straightforward, 1 bullet
		Fork:
			- 3 or 5 prong spray like 2hu
		Corkscrew:
			- 3 bullets corkscrew around origin
		Sine:
			- 2 bullets in opposing sine waves
		Drop:
			- Maybe make this a bomb instead
			- Inspiration: accidentally left the move direction as (0,0,0), so bullets are simply left
			  behind where player was.
			- "Drop" bullets where you are standing, no trajectory, they later explode.
	 */
    public enum BulletGroupType
    {
        Linear, Fork, Corkscrew, Drop, SineWave, TriangleWave, SquareWave
    }
    public class BulletGroup : MonoBehaviour
    {
        [SerializeField] Bullet m_BulletPrefab;
        [SerializeField] BulletGroupType m_GroupType;
        [SerializeField] float m_Speed = 10f;
        [SerializeField] public int m_NumBullets;
        [SerializeField] float m_DamagePerBullet;
        Vector3 m_Direction;
        //Vector3 m_Origin;
        List<int> m_AssignedBulletIndexes;

        private void Start()
        {
            m_AssignedBulletIndexes = new List<int>();
            //m_origin = transform.position;
            m_Direction = SceneManager.Instance.Player.transform.forward;
            for (int i = 0; i < m_NumBullets; i++)
            {
                Bullet newBullet = Instantiate(
                    m_BulletPrefab,
                    SceneManager.Instance.Player.m_FirePoint.position,
                    SceneManager.Instance.Player.transform.rotation);
                newBullet.gameObject.layer = Layers.Bullets;
                newBullet.transform.SetParent(transform);
            }
        }

        private void Update()
        {
            if (transform.childCount >= 1)
            {
                // TODO: Improve this switch... Maybe. Maybe inheritance/interface, functional parameters... Maybe not.
                switch (m_GroupType)
                {
                    case (BulletGroupType.Linear):
                        MoveLinear();
                        break;
                    case (BulletGroupType.Fork):
                        MoveFork();
                        break;
                    case (BulletGroupType.Corkscrew):
                        MoveCorkscrew();
                        break;
                    case (BulletGroupType.SineWave):
                        MoveSine();
                        break;
                    case (BulletGroupType.TriangleWave):
                        MoveTriangle();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void MoveLinear()
        {
            transform.position += m_Direction * m_Speed * Time.deltaTime;
        }

        [Header("Fork")]
        [SerializeField] float m_forkAngle;
        void MoveFork()
        {
        }
        [Header("Corkscrew")]
        [SerializeField] float m_screwRotateSpeed;
        void MoveCorkscrew()
        {
        }
        [Header("Sine")]
        [SerializeField] float m_SineAmplitude;
        void MoveSine()
        {
        }

        [Header("TriangleWave")]
        [SerializeField] float m_triangleLength = 2f;
        [SerializeField] float m_triangleAngle = 30f;
        float m_currentLength = 0f;
        void MoveTriangle()
        {
            if (m_currentLength >= m_triangleLength)
            {
				m_triangleAngle *= -1f;
            }
			// Modify m_direction
			float zMag = m_Speed * Time.deltaTime * Mathf.Cos(m_triangleAngle * Mathf.Deg2Rad);
			float xMag = m_Speed * Time.deltaTime * Mathf.Cos(m_triangleAngle * Mathf.Deg2Rad);

			transform.position = new Vector3(
				transform.position.x + xMag, 
				0, 
				transform.position.z + zMag);

			m_currentLength += m_Speed * Time.deltaTime;
        }

    }
}