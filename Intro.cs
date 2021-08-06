// Intro
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
	public Image front;

	private float alpha;

	private float fadeTime = 0.7f;

	private float timer = 2f;

	private float timerNow;

	private bool fadeOut;

	private void Start()
	{
		alpha = 1f;
	}

	private void Update()
	{
		UpdateAlpha();
		UpdateGraphics();
	}

	private void UpdateAlpha()
	{
		if (alpha > 0f)
		{
			alpha -= fadeTime * Time.deltaTime;
		}
		else
		{
			alpha = 0f;
		}
	}

	private void UpdateGraphics()
	{
		if (fadeOut)
		{
			front.color = new Color(0f, 0f, 0f, 1f - alpha);
			if (alpha <= 0f)
			{
				GetComponent<ChangeScene>().LoadScene("menu");
			}
		}
		else
		{
			front.color = new Color(0f, 0f, 0f, alpha);
		}
		if (alpha == 0f && !fadeOut)
		{
			timerNow += Time.deltaTime;
			if (timerNow > timer)
			{
				alpha = 1f;
				fadeOut = true;
			}
		}
	}
}
