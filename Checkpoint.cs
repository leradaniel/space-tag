// Checkpoint
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	public int positionCheckpoint;

	private GameManager gmReference;

	private void Start()
	{
		gmReference = GameObject.Find("GameManager").GetComponent<GameManager>();
		GetComponent<Renderer>().enabled = false;
	}

	private void Update()
	{
		if (gmReference.ship.activeSelf && gmReference.ship.transform.position.x > base.transform.position.x)
		{
			NewCheckpoint();
		}
		else if (gmReference.altship.activeSelf && gmReference.altship.transform.position.x > base.transform.position.x)
		{
			NewCheckpoint();
		}
	}

	private void NewCheckpoint()
	{
		if (positionCheckpoint > GameManager.stageCheckpoint)
		{
			GameManager.stageCheckpoint = positionCheckpoint;
		}
		Object.Destroy(base.gameObject);
	}
}
