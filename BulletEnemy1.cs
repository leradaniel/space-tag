// BulletEnemy1
using UnityEngine;

public class BulletEnemy1 : BulletParent
{
	private GameManager gameManagerReference;

	protected override void Start()
	{
		base.Start();
		gameManagerReference = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	protected override void BulletMovement()
	{
		base.BulletMovement();
		base.transform.position += base.transform.forward * bulletSpeed * Time.deltaTime;
	}

	protected override void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 8)
		{
			GetComponent<Renderer>().enabled = false;
			GetComponent<Collider>().enabled = false;
			Object.Destroy(base.gameObject, GetComponent<AudioSource>().clip.length);
			if (other.gameObject == gameManagerReference.ship)
			{
				gameManagerReference.shipScripts[0].Damage(bulletDamage);
			}
			else if (other.gameObject == gameManagerReference.altship)
			{
				gameManagerReference.shipScripts[1].Damage(bulletDamage);
			}
		}
	}
}
