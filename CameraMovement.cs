// CameraMovement
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	[Tooltip("Distancia entre el jugador y la cámara.")]
	public float camDistance = -50f;

	public static float panSpeed;

	[Tooltip("Velocidad de paneo en público para poder modificarlo más fácilmente desde el inspector.")]
	public float panSpeedPublic = 10f;

	private bool bossTime;

	public AudioClip bossBGM;

	private void Start()
	{
		panSpeed = panSpeedPublic;
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, camDistance);
	}

	private void LateUpdate()
	{
		if (!GameManager.pause)
		{
			base.transform.position += new Vector3(panSpeed * Time.deltaTime, 0f, 0f);
			RenderSettings.skybox.SetFloat("_Rotation", Time.time * 1f);
		}
		if (base.transform.position.x > 8100f && !bossTime)
		{
			GetComponent<AudioSource>().Stop();
			GetComponent<AudioSource>().clip = bossBGM;
			GetComponent<AudioSource>().Play();
			bossTime = true;
		}
	}
}
