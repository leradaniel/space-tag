// RotateThis
using UnityEngine;

public class RotateThis : MonoBehaviour
{
	public float rotationSPeed;

	private void Update()
	{
		if (!GameManager.pause)
		{
			base.transform.Rotate(rotationSPeed * Time.deltaTime, 0f, 0f);
		}
	}
}
