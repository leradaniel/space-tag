// PauseMenu
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
	public GameObject consoleContent;

	public GameObject hud;

	public GameObject pauseUI;

	public Image selection1;

	public Image selection2;

	public Image selection3;

	public Image selection4;

	public Image mask;

	public Image border1;

	public Image border2;

	public Image background;

	private Color opacity;

	private int selectionID = 1;

	private bool enableActions;

	private bool selectionMade;

	public AudioClip pauseSound;

	public AudioClip cursorSound;

	public AudioClip selectSound;

	private bool soundPlayed;

	private KeyCode accept;

	private KeyCode acceptJoy;

	private KeyCode cancel;

	private KeyCode cancelJoy;

	private bool holdStick;

	public virtual void Start()
	{
		selectionID = 1;
		opacity = selection1.color;
		PrepareInputs();
	}

	public virtual void Update()
	{
		ActivateGraphics();
		Inputs();
		HighlightSelection();
	}

	public virtual void PrepareInputs()
	{
		ShipParent shipParent = GameObject.Find("GameManager").GetComponent<GameManager>().shipScripts[0];
		accept = shipParent.shootKey;
		acceptJoy = shipParent.shootKeyJoy;
		cancel = shipParent.pauseKey;
		cancelJoy = shipParent.pauseKeyJoy;
	}

	public virtual void ActivateGraphics()
	{
		if (GameManager.pause && !consoleContent.activeSelf)
		{
			PlaySound();
			hud.SetActive(value: false);
			pauseUI.SetActive(value: true);
			mask.enabled = true;
			border1.enabled = true;
			border2.enabled = true;
			background.enabled = true;
			if (mask.GetComponent<RectTransform>().sizeDelta.x < 770f && !selectionMade)
			{
				enableActions = false;
				mask.GetComponent<RectTransform>().sizeDelta += new Vector2(1000f * Time.deltaTime, 50f * Time.deltaTime);
				border2.transform.position += new Vector3(300f * Time.deltaTime, 200f * Time.deltaTime, 0f);
				border1.transform.position -= new Vector3(300f * Time.deltaTime, 200f * Time.deltaTime, 0f);
				opacity.a = 0.3f;
				selection1.color = opacity;
				selection2.color = opacity;
				selection3.color = opacity;
				selection4.color = opacity;
				return;
			}
			if (mask.GetComponent<RectTransform>().sizeDelta.x > 0f && selectionMade)
			{
				enableActions = false;
				mask.GetComponent<RectTransform>().sizeDelta -= new Vector2(1000f * Time.deltaTime, 50f * Time.deltaTime);
				border2.transform.position -= new Vector3(300f * Time.deltaTime, 200f * Time.deltaTime, 0f);
				border1.transform.position += new Vector3(300f * Time.deltaTime, 200f * Time.deltaTime, 0f);
				opacity.a = 0.3f;
				selection1.color = opacity;
				selection2.color = opacity;
				selection3.color = opacity;
				selection4.color = opacity;
				return;
			}
			enableActions = true;
			if (selectionMade)
			{
				selectionMade = false;
				switch (selectionID)
				{
				case 1:
					GameManager.pause = false;
					break;
				case 2:
					GameManager.stageCheckpoint = 0;
					GameManager.lifes = 4;
					GetComponent<ChangeScene>().LoadScene("stage1");
					GameManager.pause = false;
					break;
				case 3:
					GetComponent<ChangeScene>().LoadScene("menu");
					GameManager.pause = false;
					break;
				case 4:
					Application.Quit();
					break;
				}
			}
		}
		else if (GameManager.pause && consoleContent.activeSelf)
		{
			hud.SetActive(value: false);
			pauseUI.SetActive(value: false);
			selectionID = 1;
			mask.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 50f);
			border1.transform.localPosition = new Vector3(60f, 0f, 0f);
			border2.transform.localPosition = new Vector3(-60f, 0f, 0f);
			mask.enabled = false;
			border1.enabled = false;
			border2.enabled = false;
			background.enabled = false;
			soundPlayed = false;
		}
		else
		{
			hud.SetActive(value: true);
			pauseUI.SetActive(value: false);
			selectionID = 1;
			mask.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 50f);
			border1.transform.localPosition = new Vector3(60f, 0f, 0f);
			border2.transform.localPosition = new Vector3(-60f, 0f, 0f);
			mask.enabled = false;
			border1.enabled = false;
			border2.enabled = false;
			background.enabled = false;
			soundPlayed = false;
		}
	}

	public virtual void PlaySound()
	{
		if (!soundPlayed)
		{
			GetComponent<AudioSource>().clip = pauseSound;
			GetComponent<AudioSource>().Play();
			soundPlayed = true;
		}
	}

	public virtual void Inputs()
	{
		if (!GameManager.pause || consoleContent.activeSelf || !enableActions)
		{
			return;
		}
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
			GetComponent<AudioSource>().clip = selectSound;
			GetComponent<AudioSource>().Play();
			selectionMade = true;
		}
		else if (Input.GetKeyDown(cancel) || Input.GetKeyDown(cancelJoy))
		{
			GetComponent<AudioSource>().clip = selectSound;
			GetComponent<AudioSource>().Play();
			selectionID = 1;
			selectionMade = true;
		}
	}

	public virtual void HighlightSelection()
	{
		if (!GameManager.pause || consoleContent.activeSelf || !enableActions)
		{
			return;
		}
		switch (selectionID)
		{
		case 1:
			opacity.a = 0.3f;
			selection2.color = opacity;
			selection3.color = opacity;
			selection4.color = opacity;
			opacity.a = 1f;
			selection1.color = opacity;
			break;
		case 2:
			opacity.a = 0.3f;
			selection1.color = opacity;
			selection3.color = opacity;
			selection4.color = opacity;
			opacity.a = 1f;
			selection2.color = opacity;
			break;
		case 3:
			opacity.a = 0.3f;
			selection1.color = opacity;
			selection2.color = opacity;
			selection4.color = opacity;
			opacity.a = 1f;
			selection3.color = opacity;
			break;
		case 4:
			opacity.a = 0.3f;
			selection1.color = opacity;
			selection2.color = opacity;
			selection3.color = opacity;
			opacity.a = 1f;
			selection4.color = opacity;
			break;
		default:
			if (selectionID <= 0)
			{
				selectionID = 4;
			}
			else if (selectionID >= 5)
			{
				selectionID = 1;
			}
			break;
		}
	}
}
