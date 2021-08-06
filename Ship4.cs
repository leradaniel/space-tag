// Ship4
using UnityEngine;

public class Ship4 : ShipParent
{
	protected override void Shoot()
	{
		GameObject gameObject = Object.Instantiate(bulletShot, shotPoint.position, shotPoint.rotation);
		if (GameObject.Find("Console").GetComponent<Console>().damageUP)
		{
			gameObject.GetComponent<BulletParent>().bulletDamage = 100;
		}
	}
}
