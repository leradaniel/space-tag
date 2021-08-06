// EndScreen
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
	public Image backgroundGameOver;

	public Image backgroundVictory;

	public Image selection1GameOver;

	public Image selection1Victory;

	public Image selection2GameOver;

	public Image selection2Victory;

	public Image frontGameOver;

	public Image frontVictory;

	public Image frontWhite;

	private Image background;

	private Image selection1;

	private Image selection2;

	private Image front;

	private KeyCode accept = KeyCode.Space;

	private KeyCode acceptJoy = KeyCode.Joystick1Button0;

	private bool holdStick;

	private int selectionID = 1;

	private bool selectionMade;

	private bool enableActions;

	public AudioClip cursorSound;

	public AudioClip selectSound;

	public AudioClip BGMGameOver;

	public AudioClip BGMVictory;

	private Color opacity;

	private float alpha;

	private float fadeTime = 0.75f;

	private void Start()
	{
		if (GameManager.lifes >= 0)
		{
			background = backgroundVictory;
			front = frontVictory;
			selection1 = selection1Victory;
			selection2 = selection2Victory;
			GetComponent<AudioSource>().clip = BGMVictory;
			GetComponent<AudioSource>().Play();
		}
		else
		{
			background = backgroundGameOver;
			front = frontGameOver;
			selection1 = selection1GameOver;
			selection2 = selection2GameOver;
			GetComponent<AudioSource>().clip = BGMGameOver;
			GetComponent<AudioSource>().Play();
		}
		background.enabled = true;
		front.enabled = true;
		selection1.enabled = true;
		selection2.enabled = true;
		frontWhite.enabled = true;
		selectionID = 1;
		opacity = selection1.color;
		alpha = 1f;
		enableActions = false;
	}

	private void Update()
	{
		Inputs();
		UpdateAlpha();
		UpdateGraphics();
	}

	public virtual void Inputs()
	{
		if (selectionMade || !enableActions)
		{
			return;
		}
		if ((double)Input.GetAxis("Horizontal") <= 0.2 && (double)Input.GetAxis("Horizontal") >= -0.2)
		{
			holdStick = false;
		}
		if (Input.GetKeyDown(KeyCode.RightArrow) || (double)Input.GetAxis("Horizontal") > 0.2)
		{
			if (!holdStick)
			{
				GetComponent<AudioSource>().PlayOneShot(cursorSound);
				selectionID++;
			}
			holdStick = true;
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow) || (double)Input.GetAxis("Horizontal") < -0.2)
		{
			if (!holdStick)
			{
				GetComponent<AudioSource>().PlayOneShot(cursorSound);
				selectionID--;
			}
			holdStick = true;
		}
		else if (Input.GetKeyDown(accept) || Input.GetKeyDown(acceptJoy))
		{
			GetComponent<AudioSource>().PlayOneShot(selectSound);
			selectionMade = true;
			enableActions = false;
			fadeTime = 0.5f;
			alpha = 1f;
		}
	}

	private void UpdateAlpha()
	{
		if (!enableActions)
		{
			if (alpha > 0f)
			{
				alpha -= fadeTime * Time.deltaTime;
				return;
			}
			alpha = 0f;
			enableActions = true;
		}
	}

	private void UpdateGraphics()
	{
		if (selectionMade)
		{
			opacity.a = alpha * 0.3f;
			selection1.color = opacity;
			selection2.color = opacity;
			switch (selectionID)
			{
			case 1:
				frontWhite.color = new Color(0f, 0f, 0f, 1f - alpha);
				if (alpha <= 0f)
				{
					GetComponent<ChangeScene>().LoadScene("menu");
				}
				break;
			case 2:
				frontWhite.color = new Color(0f, 0f, 0f, 1f - alpha);
				if (alpha <= 0f)
				{
					Application.Quit();
				}
				break;
			}
			return;
		}
		opacity.a = 0.3f - alpha * 0.3f;
		selection1.color = opacity;
		selection2.color = opacity;
		switch (selectionID)
		{
		case 1:
			if (GameManager.lifes >= 0)
			{
				frontWhite.color = new Color(1f, 1f, 1f, alpha);
			}
			else
			{
				frontWhite.color = new Color(0f, 0f, 0f, alpha);
			}
			if (enableActions)
			{
				opacity.a = 0.8f;
				selection1.color = opacity;
			}
			break;
		case 2:
			if (enableActions)
			{
				opacity.a = 0.8f;
				selection2.color = opacity;
			}
			break;
		default:
			if (selectionID <= 0 && alpha == 0f)
			{
				selectionID = 2;
			}
			else if (selectionID >= 3 && alpha == 0f)
			{
				selectionID = 1;
			}
			break;
		}
	}
}
