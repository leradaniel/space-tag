// SpeedUpCamera
using UnityEngine;

public class SpeedUpCamera : MonoBehaviour
{
	public int camPanSpeed = 10;

	public bool finalPoint;

	private void Update()
	{
		if (base.transform.position.x < Camera.main.transform.position.x)
		{
			if (finalPoint)
			{
				CameraMovement.panSpeed = 0f;
				Camera.main.transform.position = new Vector3(base.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
			}
			else if ((float)camPanSpeed > CameraMovement.panSpeed)
			{
				CameraMovement.panSpeed = camPanSpeed;
			}
			Object.Destroy(base.gameObject);
		}
	}
}
