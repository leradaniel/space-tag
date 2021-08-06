// DestroyThis
using UnityEngine;

public class DestroyThis : MonoBehaviour
{
	public float destroyTime;

	public bool spawnEnemy;

	private float counterTimeNow;

	public GameObject spawnedMinon;

	private void Start()
	{
		counterTimeNow = destroyTime;
	}

	private void Update()
	{
		if (GameManager.pause)
		{
			if (GetComponentInChildren<ParticleSystem>().isPlaying)
			{
				GetComponentInChildren<ParticleSystem>().Pause();
			}
			GetComponent<AudioSource>().Pause();
			return;
		}
		if (GetComponentInChildren<ParticleSystem>().isPaused)
		{
			GetComponentInChildren<ParticleSystem>().Play();
		}
		counterTimeNow -= Time.deltaTime;
		if (counterTimeNow <= 0f)
		{
			if (spawnEnemy)
			{
				Object.Instantiate(spawnedMinon, base.transform.position, base.transform.rotation);
				spawnedMinon.GetComponent<Enemy7>().life = 1;
			}
			Object.Destroy(base.gameObject);
		}
		GetComponent<AudioSource>().UnPause();
	}
}
