// MoveThis
using UnityEngine;

public class MoveThis : MonoBehaviour
{
	private void Start()
	{
		Object.Destroy(base.gameObject, 10f);
	}

	private void Update()
	{
		if (!GameManager.pause)
		{
			base.transform.position += base.transform.right * CameraMovement.panSpeed * Time.deltaTime;
		}
	}
}
