// EnemyBoss
using UnityEngine;

public class EnemyBoss : EnemyParent
{
	[Tooltip("Transform o GameObject en el cual spawnean los minions.")]
	public Transform spawnPoint;

	public GameObject spawnedMinon;

	[Tooltip("Cada cuánto tiempo spawnea minions.")]
	public float spawnTime = 3f;

	private float spawnTimeNow;

	private int step;

	protected override void Update()
	{
		base.Update();
		if (!GameManager.pause)
		{
			EnemySpawn();
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
		if (base.transform.position.x <= vector.x)
		{
			activated = true;
			GetComponent<Collider>().enabled = true;
		}
	}

	protected override void EnemyShoot()
	{
	}

	protected override void EnemyMovement()
	{
	}

	private void EnemySpawn()
	{
		if (!activated)
		{
			return;
		}
		spawnTimeNow -= Time.deltaTime;
		if (spawnTimeNow <= 0f)
		{
			spawnTimeNow = spawnTime;
			EnemyCast();
		}
		if (life < 500 && step == 0)
		{
			for (int i = 0; i < 10; i++)
			{
				EnemyCast();
			}
			step = 1;
		}
		if (life < 300 && step == 1)
		{
			for (int j = 0; j < 20; j++)
			{
				EnemyCast();
			}
			step = 2;
		}
		if (life < 100 && step == 2)
		{
			for (int k = 0; k < 30; k++)
			{
				EnemyCast();
			}
			step = 3;
		}
	}

	private void EnemyCast()
	{
		spawnPoint.transform.position = base.transform.position;
		spawnPoint.transform.position -= new Vector3(Random.Range(5f, 90f), Random.Range(21f, -25f), 0f);
		Object.Instantiate(spawnedMinon, spawnPoint.position, spawnPoint.rotation);
		spawnedMinon.GetComponent<DestroyThis>().spawnEnemy = true;
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
