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

	public class BulletPool
	{
		// TODO: Maybe make this array dynamic
		public Bullet[] bulletArray;
		public List<int> availableBulletIndexes;
		public int currentCapacity;
		public int count = 0;
		bool t_ThreadRunning;
		Thread t_Thread;
		public BulletPool(int size = 2000)
		{
			currentCapacity = size;
			bulletArray = new Bullet[size];
			availableBulletIndexes = new List<int>();
			t_Thread = new Thread(Fill);
		}
		public void Fill()
		{
			t_ThreadRunning = true;
			bool t_WorkDone = false;
			while (t_ThreadRunning && !t_WorkDone)
			{
				for (int i = 0; i < currentCapacity; i++)
				{
					bulletArray[i] = new Bullet();
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

		public bool IsFull()
		{
			return availableBulletIndexes.Count == 0;
		}
		public void Clear()
		{
			foreach (int index in availableBulletIndexes)
			{
				bulletArray[index].gameObject.SetActive(false);
			}
		}
		public void Expand() { }
		public void Shrink() { }
		// Game object Bullet disable itself on collision, etc.
		// No need to do that here w/ Add/Remove
		public void Add(int indexToAdd) { count++; }
		public void Remove(int indexToRemove) { count--; }
	}

	public class BulletManager : MonoBehaviour
	{
		public const int MAX_BULLETS = 2000;
		public static BulletManager Instance { get; private set; }
		public BulletPool BulletPool;
		public int ActiveCount
		{
			get { return BulletPool.count; }
		}

		void Awake()
		{
			if (Instance == null) { Instance = this; }
			else { Destroy(gameObject); }
			// // Cache references to all desired variables

			this.BulletPool = new BulletPool(MAX_BULLETS);
			this.BulletPool.Fill();
			print(this.BulletPool.bulletArray[0].IsDead);
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
	}
}