// Bullet4
using UnityEngine;

public class Bullet4 : BulletParent
{
	private Vector3 initialPosition;

	private Vector3 actualPosition;

	private int distanceDestroy = 17;

	protected override void Start()
	{
		base.Start();
		initialPosition = base.transform.localPosition;
	}

	protected override void BulletMovement()
	{
		base.BulletMovement();
		base.transform.position += base.transform.forward * bulletSpeed * Time.deltaTime;
		actualPosition = base.transform.localPosition;
		if (Vector3.Distance(actualPosition, initialPosition) > (float)distanceDestroy)
		{
			Object.Destroy(base.gameObject);
		}
	}

	protected override void OnTriggerEnter(Collider other)
	{
		base.OnTriggerEnter(other);
		if (other.gameObject.tag == "Enemy")
		{
			GetComponent<Renderer>().enabled = false;
			GetComponent<Collider>().enabled = false;
			try
			{
				Object.Destroy(base.gameObject, GetComponent<AudioSource>().clip.length);
			}
			catch
			{
				Object.Destroy(base.gameObject);
			}
		}
	}
}
