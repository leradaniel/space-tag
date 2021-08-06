// Bullet2
using UnityEngine;

public class Bullet2 : BulletParent
{
	[Tooltip("El tamaño máximo que va a tener el disparo.")]
	public float maxScale = 35f;

	protected override void BulletMovement()
	{
		base.BulletMovement();
		base.transform.localScale += new Vector3(1f, 1f, 1f) * bulletSpeed * Time.deltaTime;
		if (base.transform.localScale.x > maxScale)
		{
			Object.Destroy(base.gameObject);
		}
	}
}
