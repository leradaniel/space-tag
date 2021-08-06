// Ship2
using UnityEngine;

public class Ship2 : ShipParent
{
	protected override void Shoot()
	{
		GameObject gameObject = Object.Instantiate(bulletShot, shotPoint.position, shotPoint.rotation);
		GameObject gameObject2 = Object.Instantiate(bulletShot2, shotPoint3.position, shotPoint3.rotation);
		GameObject gameObject3 = Object.Instantiate(bulletShot2, shotPoint4.position, shotPoint4.rotation);
		GameObject gameObject4 = Object.Instantiate(bulletShot2, shotPoint5.position, shotPoint5.rotation);
		GameObject gameObject5 = Object.Instantiate(bulletShot2, shotPoint6.position, shotPoint6.rotation);
		if (GameObject.Find("Console").GetComponent<Console>().damageUP)
		{
			gameObject.GetComponent<BulletParent>().bulletDamage = 100;
			gameObject2.GetComponent<BulletParent>().bulletDamage = 100;
			gameObject3.GetComponent<BulletParent>().bulletDamage = 100;
			gameObject4.GetComponent<BulletParent>().bulletDamage = 100;
			gameObject5.GetComponent<BulletParent>().bulletDamage = 100;
		}
	}
}
