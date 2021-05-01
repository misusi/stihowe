using UnityEngine;
using STIHOWE.Constants;

namespace STIHOWE.Bullets
{
	//Bullets:
	// Owner - who shot this
	//      A type? TAG?
	//      Or just something like an isPlayerBullet bool?
	// is active (unity already does it)
	// move function
	// damage
	// origin
	// trajectory
	public class Bullet : MonoBehaviour
	{
		string m_OwnerTag;
		int m_PoolIndex = -1;
		ParticleSystem m_ParticleSystem;
		TrailRenderer m_TrailRenderer;
		void OnEnable()
		{

		}
		void OnDisable()
		{

		}

		public string OwnerTag
		{
			get { return m_OwnerTag; }
			set { m_OwnerTag = value; }
		}
		public int PoolIndex
		{
			get { return m_PoolIndex; }
			set { m_PoolIndex = value; }
		}

		public void WarpTo(Vector3 toPos, Quaternion toRot)
		{
			transform.position = toPos;
			transform.rotation = toRot;
		}
		public void SetParticleSystem(ParticleSystem ps)
		{

		}

		private void OnCollisionEnter(Collision other)
		{
			// TODO: Play destroy bullet animation
			print(name + " collided with " + other.transform.name);

			Destroy(gameObject);
		}

		private void Update()
		{
			// should do the movement!
		}
	}
}