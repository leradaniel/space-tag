// Enemy7
using UnityEngine;

public class Enemy7 : EnemyParent
{
	[Tooltip("Cuánto tarda el enemigo en dirigirse al jugador.")]
	public float chargeTime = 3f;

	private Transform shipTransform;

	private bool speedIncreased;

	protected override void EnemyMovement()
	{
		if (gameManagerReference.ship.activeSelf)
		{
			shipTransform = gameManagerReference.ship.GetComponent<Transform>();
		}
		else if (gameManagerReference.altship.activeSelf)
		{
			shipTransform = gameManagerReference.altship.GetComponent<Transform>();
		}
		if (chargeTime > 0f)
		{
			chargeTime -= Time.deltaTime;
			if (shipTransform != null)
			{
				base.transform.LookAt(shipTransform);
			}
		}
		else
		{
			if (!speedIncreased)
			{
				IncreaseSpeed();
			}
			base.transform.position += base.transform.forward * Time.deltaTime * enemySpeed;
		}
	}

	private void IncreaseSpeed()
	{
		GetComponentInChildren<RotateThis>().rotationSPeed += 600f;
		speedIncreased = true;
	}
}
