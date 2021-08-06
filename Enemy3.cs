// Enemy3
using UnityEngine;

public class Enemy3 : EnemyParent
{
	[Tooltip("Transform o GameObject desde donde sale el disparo enemigo.")]
	public Transform shotPoint2;

	[Tooltip("Transform o GameObject desde donde sale el disparo enemigo.")]
	public Transform shotPoint3;

	[Tooltip("Transform o GameObject desde donde sale el disparo enemigo.")]
	public Transform shotPoint4;

	[Tooltip("Transform o GameObject desde donde sale el disparo enemigo.")]
	public Transform shotPoint5;

	[Tooltip("Transform o GameObject desde donde sale el disparo enemigo.")]
	public Transform shotPoint6;

	[Tooltip("Transform o GameObject desde donde sale el disparo enemigo.")]
	public Transform shotPoint7;

	[Tooltip("Transform o GameObject desde donde sale el disparo enemigo.")]
	public Transform shotPoint8;

	private float patternTimer;

	private float horSpeed = 10f;

	private float modSpeed;

	private float horAccel = 0.5f;

	private float verAccel = 0.08f;

	private float vertSpeed = 0.1f;

	protected override void Start()
	{
		base.Start();
		modSpeed = horSpeed;
	}

	protected override void EnemyMovement()
	{
		base.EnemyMovement();
		patternTimer += Time.deltaTime;
		if (patternTimer < 5f)
		{
			base.transform.position += base.transform.forward * Time.deltaTime * horSpeed;
		}
		else if (patternTimer < 7f)
		{
			modSpeed -= horAccel * Time.deltaTime * horSpeed;
			base.transform.position += base.transform.forward * Time.deltaTime * modSpeed;
			vertSpeed += verAccel;
			base.transform.position -= base.transform.up * Time.deltaTime * vertSpeed;
		}
		else if (patternTimer < 9f)
		{
			modSpeed -= horAccel * Time.deltaTime * horSpeed;
			base.transform.position += base.transform.forward * Time.deltaTime * modSpeed;
			vertSpeed -= verAccel;
			base.transform.position -= base.transform.up * Time.deltaTime * vertSpeed;
		}
		else if (patternTimer < 11f)
		{
			base.transform.position += base.transform.forward * Time.deltaTime * modSpeed;
		}
		else if (patternTimer < 13f)
		{
			modSpeed += horAccel * Time.deltaTime * horSpeed;
			base.transform.position += base.transform.forward * Time.deltaTime * modSpeed;
			vertSpeed += verAccel;
			base.transform.position -= base.transform.up * Time.deltaTime * vertSpeed;
		}
		else if (patternTimer < 15f)
		{
			modSpeed += horAccel * Time.deltaTime * horSpeed;
			base.transform.position += base.transform.forward * Time.deltaTime * modSpeed;
			vertSpeed -= verAccel;
			base.transform.position -= base.transform.up * Time.deltaTime * vertSpeed;
		}
		else if (patternTimer < 17f)
		{
			base.transform.position += base.transform.forward * Time.deltaTime * horSpeed;
		}
		else
		{
			base.transform.position += base.transform.forward * Time.deltaTime * horSpeed;
			vertSpeed -= verAccel / 2f;
			base.transform.position -= base.transform.up * Time.deltaTime * vertSpeed;
		}
	}

	protected override void EnemyShoot()
	{
		if (!activated || !shottingEnemy)
		{
			return;
		}
		if (timeToShootNow > 0f)
		{
			timeToShootNow -= Time.deltaTime;
			return;
		}
		Object.Instantiate(bulletShot, shotPoint.position, shotPoint.rotation);
		Object.Instantiate(bulletShot, shotPoint2.position, shotPoint2.rotation);
		Object.Instantiate(bulletShot, shotPoint3.position, shotPoint3.rotation);
		Object.Instantiate(bulletShot, shotPoint4.position, shotPoint4.rotation);
		Object.Instantiate(bulletShot, shotPoint5.position, shotPoint5.rotation);
		Object.Instantiate(bulletShot, shotPoint6.position, shotPoint6.rotation);
		Object.Instantiate(bulletShot, shotPoint7.position, shotPoint7.rotation);
		Object.Instantiate(bulletShot, shotPoint8.position, shotPoint8.rotation);
		if (shottingRepeat)
		{
			timeToShootNow = timeToShoot;
		}
		else
		{
			shottingEnemy = false;
		}
	}

	protected override void onHitColor()
	{
		if (!colorFlash)
		{
			return;
		}
		colorTimer += Time.deltaTime;
		if (colorTimer < colorFlashTime)
		{
			if ((bool)GetComponent<Renderer>())
			{
				GetComponent<Renderer>().material.SetColor("_Color", hitColor * 0.5f);
			}
			else if ((bool)GetComponentInChildren<Renderer>())
			{
				GetComponentInChildren<Renderer>().material.SetColor("_Color", hitColor * 0.5f);
			}
			return;
		}
		if ((bool)GetComponent<Renderer>())
		{
			GetComponent<Renderer>().material.SetColor("_Color", Color.white);
		}
		else if ((bool)GetComponentInChildren<Renderer>())
		{
			GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.white);
		}
		colorFlash = false;
		colorTimer = 0f;
	}
}
