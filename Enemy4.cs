// Enemy4
using UnityEngine;

public class Enemy4 : EnemyParent
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

	[Tooltip("GameObject que apunta al jugador y mueve los shotPoint.")]
	public Transform shotPointGlobal;

	[Tooltip("Cuánto tiempo el enemigo permanece escondido.")]
	public float hideTime = 3f;

	[Tooltip("Cuánto tiempo el enemigo permanece a la vista.")]
	public float showTime = 5f;

	private bool shown = true;

	private float zSpeed = 60f;

	private float showhideTimeNow;

	protected override void EnemyMovement()
	{
		base.EnemyMovement();
		if (base.transform.position.z > 0f && shown)
		{
			base.transform.position += base.transform.forward * Time.deltaTime * zSpeed;
		}
		else if (base.transform.position.z <= 0f && shown && showhideTimeNow <= 0f)
		{
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, 0f);
			showhideTimeNow = showTime;
		}
		else if (base.transform.position.z <= 0f && shown && showhideTimeNow > 0f)
		{
			showhideTimeNow -= Time.deltaTime;
			if (showhideTimeNow <= 0f)
			{
				shown = false;
				if (shottingEnemy)
				{
					NewEnemyShoot();
				}
			}
		}
		if (base.transform.position.z < 60f && !shown)
		{
			base.transform.position -= base.transform.forward * Time.deltaTime * zSpeed;
		}
		else if (base.transform.position.z >= 60f && !shown && showhideTimeNow <= 0f)
		{
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, 60f);
			showhideTimeNow = hideTime;
		}
		else if (base.transform.position.z >= 60f && !shown && showhideTimeNow > 0f)
		{
			showhideTimeNow -= Time.deltaTime;
			if (showhideTimeNow <= 0f)
			{
				shown = true;
			}
		}
	}

	protected override void EnemyShoot()
	{
	}

	private void NewEnemyShoot()
	{
		if (activated && shottingEnemy)
		{
			if (gameManagerReference.ship.activeSelf)
			{
				playerTransform = gameManagerReference.ship.GetComponent<Transform>();
			}
			else if (gameManagerReference.altship.activeSelf)
			{
				playerTransform = gameManagerReference.altship.GetComponent<Transform>();
			}
			if (playerTransform != null)
			{
				shotPointGlobal.transform.LookAt(playerTransform);
			}
			Object.Instantiate(bulletShot, shotPoint.position, shotPoint.rotation);
			Object.Instantiate(bulletShot, shotPoint2.position, shotPoint2.rotation);
			Object.Instantiate(bulletShot, shotPoint3.position, shotPoint3.rotation);
			Object.Instantiate(bulletShot, shotPoint4.position, shotPoint4.rotation);
			Object.Instantiate(bulletShot, shotPoint5.position, shotPoint5.rotation);
			Object.Instantiate(bulletShot, shotPoint6.position, shotPoint6.rotation);
			Object.Instantiate(bulletShot, shotPoint7.position, shotPoint7.rotation);
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
