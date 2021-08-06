// BoundaryDetroy
using UnityEngine;

public class BoundaryDetroy : MonoBehaviour
{
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag != "Checkpoint")
		{
			try
			{
				Object.Destroy(other.gameObject, other.GetComponent<AudioSource>().clip.length);
			}
			catch
			{
				Object.Destroy(other.gameObject);
			}
		}
	}

	private void Update()
	{
		base.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0f);
	}
}
