// Enemy5
using UnityEngine;

public class Enemy5 : EnemyParent
{
	[Tooltip("Velocidad en Y. Ponerla negativa para que suba en vez de bajar.")]
	public float upSpeed = 0.2f;

	private float turnTimer;

	private float forwardSpeed = 30f;

	private float realForwardSpeed = 30f;

	private float forwardAccel = 1.5f;

	private float upAccel;

	protected override void Start()
	{
		base.Start();
		upAccel = upSpeed;
	}

	protected override void EnemyMovement()
	{
		base.EnemyMovement();
		turnTimer += Time.deltaTime;
		if (turnTimer < 2.5f)
		{
			base.transform.position += base.transform.forward * Time.deltaTime * forwardSpeed;
		}
		else if (turnTimer < 3.2f)
		{
			realForwardSpeed -= forwardAccel * Time.deltaTime * forwardSpeed;
			base.transform.position += base.transform.forward * Time.deltaTime * realForwardSpeed;
			upSpeed += upAccel;
			base.transform.position -= base.transform.up * Time.deltaTime * upSpeed;
		}
		else if (turnTimer < 3.9f)
		{
			realForwardSpeed -= forwardAccel * Time.deltaTime * forwardSpeed;
			base.transform.position += base.transform.forward * Time.deltaTime * realForwardSpeed;
			upSpeed -= upAccel;
			base.transform.position -= base.transform.up * Time.deltaTime * upSpeed;
		}
		else
		{
			base.transform.position -= base.transform.forward * Time.deltaTime * forwardSpeed;
		}
	}
}
