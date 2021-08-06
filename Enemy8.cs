// Enemy8
using UnityEngine;

public class Enemy8 : EnemyParent
{
	private Transform activeshipTransform;

	private bool charge;

	protected override void EnemyMovement()
	{
		base.EnemyMovement();
		if (gameManagerReference.ship.activeSelf)
		{
			activeshipTransform = gameManagerReference.ship.GetComponent<Transform>();
		}
		else if (gameManagerReference.altship.activeSelf)
		{
			activeshipTransform = gameManagerReference.altship.GetComponent<Transform>();
		}
		if (activeshipTransform != null)
		{
			if (activeshipTransform.position.y > base.transform.position.y && !charge)
			{
				base.transform.position += base.transform.up * Time.deltaTime * enemySpeed / 2f;
				return;
			}
			charge = true;
			base.transform.position += base.transform.forward * Time.deltaTime * enemySpeed;
		}
	}

	protected override void activateEnemy()
	{
		if ((bool)GetComponent<Renderer>())
		{
			enemySize = GetComponent<Renderer>().bounds.size;
		}
		else if ((bool)GetComponentInChildren<Renderer>())
		{
			enemySize = GetComponentInChildren<Renderer>().bounds.size;
		}
		else
		{
			enemySize = Vector3.zero;
		}
		float z = (base.transform.position - Camera.main.transform.position).z;
		Vector3 vector = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, z));
		if (base.transform.position.x <= vector.x - enemySize.x)
		{
			activated = true;
			GetComponent<Collider>().enabled = true;
		}
	}
}
