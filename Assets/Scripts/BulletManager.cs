using UnityEngine;

public class BulletManager : MonoBehaviour
{
	// List of bullet prefabs
}





/*
// All entities send *request* to singleton bullet manager
// to spawn a bullet 
// Request:
//      Send in bullet as param
//      So 2 queues: 1 = bullet l
//      
// Keep MAX bullets in container at all times
//      Reuse them
//      Turn them on/off
//
// Circular list or array - keep track of next free index

using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace STIHOWE.Bullets
{
	public class BulletManager : MonoBehaviour
	{
		public static BulletManager Instance { get; private set; }
		public List<Bullet> m_BulletPool;
		public int m_Capacity = 2000;
		int m_ActiveCount = 0;
		public int ActiveCount { get { return m_ActiveCount; } }
		bool t_ThreadRunning;
		Thread t_Thread;

		void Awake()
		{
			if (Instance == null) { Instance = this; }
			else { Destroy(gameObject); }
			// // Cache references to all desired variables

			// mike geig casts this as a game object...
			// List<Gameobject> m_bulletpool = new List<GameObject>();
			// for i in range...
			// 		Gameobject obj = (GameObject) Instantiate(bulletPrefab);
			// Why the game object cast??
			m_BulletPool = new List<Bullet>(m_Capacity);
			t_Thread = new Thread(Fill);
		}

		// TODO: Async this?
		// TODO: Thread this?
		public int GetNextAvailableBulletIndex()
		{
			if (BulletPool.IsFull())
			{
				// All bullets in use: Must start pruning
				// Or just wait? Or just increase the size?
				// mufucka mufasa
				return -1;
			}
			else
			{
				int n = -1;
				//int maxAttempts = 5;
				//int attempts = 0;
				//while (attempts < maxAttempts)
				while (true)
				// TODO: Another break condition where array is expanded
				// after a certain number of attempts.
				{
					n = Random.Range(0, MAX_BULLETS);
					if (BulletPool.bulletArray[n].IsDead)
					{
						break;
					}

				}
				return n;
			}
		}
		public void Clear()
		{
			foreach (Bullet bullet in bulletArray)
			{
				bullet.gameObject.SetActive(false);
			}
		}


		public BulletPool(int size = 2000)
		{
		}
		public void Fill()
		{
			t_ThreadRunning = true;
			bool t_WorkDone = false;
			while (t_ThreadRunning && !t_WorkDone)
			{
				for (int i = 0; i < m_Capacity; i++)
				{
					//bulletArray[i] = new Bullet();
					m_BulletPool[i] = Instantiate()
				}


				for (int i = 0; i < amountToPool; i++)
				{
					GameObject obj = (GameObject)Instantiate(objectToPool);
					obj.SetActive(false);
					pooledObjects.Add(obj);
				}


				t_WorkDone = true;
			}
		}
		void OnDisable()
		{
			// If the thread is still running, we should shut it down,
			// otherwise it can prevent the game from exiting correctly.
			if (t_ThreadRunning)
			{
				// This forces the while loop in the ThreadedWork function to abort.
				t_ThreadRunning = false;

				// This waits until the thread exits,
				// ensuring any cleanup we do after this is safe. 
				t_Thread.Join();
			}

			// Thread is guaranteed no longer running. Do other cleanup tasks.
		}




	}
}
*/