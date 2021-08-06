// Ship3
using UnityEngine;

public class Ship3 : ShipParent
{
	protected override void Shoot()
	{
		GameObject gameObject = Object.Instantiate(bulletShot, shotPoint.position, shotPoint.rotation);
		GameObject gameObject2 = Object.Instantiate(bulletShot2, shotPoint2.position, shotPoint2.rotation);
		if (GameObject.Find("Console").GetComponent<Console>().damageUP)
		{
			gameObject.GetComponent<BulletParent>().bulletDamage = 100;
			gameObject2.GetComponent<BulletParent>().bulletDamage = 100;
		}
	}
}
