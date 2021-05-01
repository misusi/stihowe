using UnityEngine;
using System.Collections.Generic;
namespace STIHOWE.Core
{
	public class ObjectPooler : MonoBehaviour
	{
		// TODO: The single GameObject below can be changed into a list
		// so I can select which bullet prefab I want to use based on who
		// calls for a bullet.
		// Alternatively: Just have multiple pools with the different bullet
		// prefabs, where all the prefabs needed are instantiated 
		// at the beginning of the scene (rather than just having one large
		// pool and having to set the part.sys./trails manually).
		public GameObject m_PooledObject;
		public int m_Capacity;
		public bool m_WillGrow;
		public float m_GrowFactor = .20f;
		List<GameObject> m_Pool;

		void Start()
		{
			m_Pool = new List<GameObject>();
			for (int i = 0; i < m_Capacity; i++)
			{
				GameObject obj = (GameObject)Instantiate(m_PooledObject);
				obj.SetActive(false);
				m_Pool.Add(obj);
			}
		}

		public GameObject GetPooledObject()
		{
			// If there is an available object
			for (int i = 0; i < m_Capacity; i++)
			{
				int randIndex = Random.Range(0,m_Capacity-1);
				if (!m_Pool[randIndex].activeInHierarchy)
				{
					return m_Pool[randIndex];
				}
			}

			// If not, will the pool grow?
			if (m_WillGrow)
			{
				GameObject nextObj = Grow();

				return nextObj;
			}

			// None available
			return null;
		}

		// Returns first newly created object
		// TODO: Thread/coroutine this.
		GameObject Grow()
		{
			for (int i = 0; i < m_Capacity * (1 + m_GrowFactor); i++)
			{
				GameObject newObj = Instantiate(m_PooledObject);
				m_Pool.Add(newObj);
			}
			int firstNewObjIndex = m_Capacity;
			m_Capacity += (int)(m_Capacity * m_GrowFactor);

			return m_Pool[firstNewObjIndex];

			// Grow by one
			//GameObject newObj = Instantiate(m_PooledObject);
			//m_Pool.Add(newObj);
		}
	}
}