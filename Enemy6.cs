// Enemy6
using UnityEngine;

public class Enemy6 : EnemyParent
{
	[Tooltip("Waypoints por donde pasa la nave. Se autodestruyen después de 10 segundos en su script.")]
	public GameObject waypoints;

	private bool wpCreated;

	protected override void Start()
	{
		base.Start();
		base.transform.position = new Vector3(base.transform.position.x, 23f, base.transform.position.z);
	}

	protected override void Update()
	{
		base.Update();
		if (!GameManager.pause)
		{
			InstanciateWaypoints();
		}
	}

	private void InstanciateWaypoints()
	{
		if (activated && !wpCreated)
		{
			waypoints.transform.position = new Vector3(Camera.main.transform.position.x - 20f, Camera.main.transform.position.y + 10f, 0f);
			Object.Instantiate(waypoints);
			wpCreated = true;
		}
	}

	protected override void EnemyMovement()
	{
		base.EnemyMovement();
		base.transform.position += base.transform.forward * Time.deltaTime * enemySpeed;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Waypoint")
		{
			base.transform.forward = other.transform.forward;
		}
	}
}
