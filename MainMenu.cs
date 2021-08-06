// MainMenu
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public Image selection1;

	public Image selection2;

	public Image selection3;

	public Image selection4;

	public Image menu;

	public Image tutorial;

	public Image credits;

	public Image black;

	public AudioClip cursorSound;

	public AudioClip selectSound;

	public AudioClip cancelSound;

	public AudioClip startSound;

	private KeyCode accept = KeyCode.Space;

	private KeyCode acceptJoy = KeyCode.Joystick1Button0;

	private KeyCode cancel = KeyCode.Escape;

	private KeyCode cancelJoy = KeyCode.Joystick1Button1;

	private bool holdStick;

	private Color opacity;

	private float alpha;

	private float fadeTime = 0.75f;

	private int selectionID;

	private bool selectionMade;

	private bool enableActions;

	private void Awake()
	{
		selection1.enabled = true;
		selection2.enabled = true;
		selection3.enabled = true;
		selection4.enabled = true;
		menu.enabled = true;
		tutorial.enabled = true;
		credits.enabled = true;
		black.enabled = true;
	}

	private void Start()
	{
		selectionID = 1;
		opacity = selection1.color;
		alpha = 1f;
		enableActions = false;
		menu.color = new Color(1f, 1f, 1f, 1f);
		tutorial.color = new Color(1f, 1f, 1f, 0f);
		credits.color = new Color(1f, 1f, 1f, 0f);
	}

	private void Update()
	{
		Inputs();
		UpdateAlpha();
		UpdateGraphics();
	}

	private void Inputs()
	{
		if (!selectionMade && enableActions)
		{
			if ((double)Input.GetAxis("Horizontal") <= 0.2 && (double)Input.GetAxis("Horizontal") >= -0.2 && (double)Input.GetAxis("Vertical") <= 0.2 && (double)Input.GetAxis("Vertical") >= -0.2)
			{
				holdStick = false;
			}
			if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow) || (double)Input.GetAxis("Horizontal") > 0.2 || (double)Input.GetAxis("Vertical") < -0.2)
			{
				if (!holdStick)
				{
					GetComponent<AudioSource>().clip = cursorSound;
					GetComponent<AudioSource>().Play();
					selectionID++;
				}
				holdStick = true;
			}
			else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || (double)Input.GetAxis("Horizontal") < -0.2 || (double)Input.GetAxis("Vertical") > 0.2)
			{
				if (!holdStick)
				{
					GetComponent<AudioSource>().clip = cursorSound;
					GetComponent<AudioSource>().Play();
					selectionID--;
				}
				holdStick = true;
			}
			else if (Input.GetKeyDown(accept) || Input.GetKeyDown(acceptJoy))
			{
				if (selectionID == 1)
				{
					GetComponent<AudioSource>().clip = startSound;
					Camera.main.GetComponent<AudioSource>().Stop();
					fadeTime = 0.5f;
				}
				else
				{
					GetComponent<AudioSource>().clip = selectSound;
				}
				GetComponent<AudioSource>().Play();
				selectionMade = true;
				enableActions = false;
				alpha = 1f;
			}
		}
		else if (enableActions && (Input.GetKeyDown(cancel) || Input.GetKeyDown(cancelJoy)))
		{
			GetComponent<AudioSource>().clip = cancelSound;
			GetComponent<AudioSource>().Play();
			alpha = 1f;
			selectionMade = false;
			enableActions = false;
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
			selection3.color = opacity;
			selection4.color = opacity;
			switch (selectionID)
			{
			case 1:
				black.color = new Color(0f, 0f, 0f, 1f - alpha);
				if (alpha <= 0f)
				{
					GetComponent<ChangeScene>().LoadScene("characterSelect");
				}
				break;
			case 2:
				menu.color = new Color(1f, 1f, 1f, alpha);
				tutorial.color = new Color(1f, 1f, 1f, 1f - alpha);
				break;
			case 3:
				menu.color = new Color(1f, 1f, 1f, alpha);
				credits.color = new Color(1f, 1f, 1f, 1f - alpha);
				break;
			case 4:
				black.color = new Color(0f, 0f, 0f, 1f - alpha);
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
		selection3.color = opacity;
		selection4.color = opacity;
		switch (selectionID)
		{
		case 1:
			black.color = new Color(0f, 0f, 0f, alpha);
			if (enableActions)
			{
				opacity.a = 1f;
				selection1.color = opacity;
			}
			break;
		case 2:
			if (enableActions)
			{
				opacity.a = 1f;
				selection2.color = opacity;
			}
			menu.color = new Color(1f, 1f, 1f, 1f - alpha);
			tutorial.color = new Color(1f, 1f, 1f, alpha);
			credits.color = new Color(1f, 1f, 1f, 0f);
			break;
		case 3:
			if (enableActions)
			{
				opacity.a = 1f;
				selection3.color = opacity;
			}
			menu.color = new Color(1f, 1f, 1f, 1f - alpha);
			tutorial.color = new Color(1f, 1f, 1f, 0f);
			credits.color = new Color(1f, 1f, 1f, alpha);
			break;
		case 4:
			if (enableActions)
			{
				opacity.a = 1f;
				selection4.color = opacity;
			}
			break;
		default:
			if (selectionID <= 0 && alpha == 0f)
			{
				selectionID = 4;
			}
			else if (selectionID >= 5 && alpha == 0f)
			{
				selectionID = 1;
			}
			break;
		}
	}
}
