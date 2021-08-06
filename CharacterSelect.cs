// CharacterSelect
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
	public GameObject roster;

	public Image front;

	public Image ship1;

	public Image ship2;

	public Image ship3;

	public Image ship4;

	public Image forbidden;

	public Image black;

	private KeyCode accept = KeyCode.Space;

	private KeyCode acceptJoy = KeyCode.Joystick1Button0;

	private KeyCode cancel = KeyCode.Escape;

	private KeyCode cancelJoy = KeyCode.Joystick1Button1;

	private bool holdStick;

	public AudioClip cursorSound;

	public AudioClip selectSound;

	public AudioClip cancelSound;

	public AudioClip startSound;

	private Vector3 startPosition;

	private Quaternion startRotation;

	private float alpha;

	private float fadeTime = 0.5f;

	private int selectionID;

	private bool selectionMade;

	private bool enableActions;

	private bool introMade;

	private bool outroMade;

	private int direction;

	private float rotationSpeed = 400f;

	public static int[] choosenShips = new int[2];

	private void Awake()
	{
		black.enabled = true;
		front.enabled = true;
		forbidden.enabled = true;
		ship1.enabled = true;
		ship2.enabled = true;
		ship3.enabled = true;
		ship4.enabled = true;
	}

	private void Start()
	{
		front.color = new Color(1f, 1f, 1f, 0f);
		ship1.color = new Color(1f, 1f, 1f, 0f);
		ship2.color = new Color(1f, 1f, 1f, 0f);
		ship3.color = new Color(1f, 1f, 1f, 0f);
		ship4.color = new Color(1f, 1f, 1f, 0f);
		forbidden.color = new Color(1f, 1f, 1f, 0f);
		choosenShips[0] = 0;
		choosenShips[1] = 0;
		startPosition = roster.transform.position;
		startRotation = roster.transform.rotation;
		roster.transform.position += new Vector3(0f, 40f, 0f);
		enableActions = false;
		alpha = 1f;
		selectionID = 1;
	}

	private void Update()
	{
		RenderSettings.skybox.SetFloat("_Rotation", Time.time * -20f);
		Inputs();
		UpdateGraphics();
		UpdateAlpha();
		if (roster.transform.position.y > startPosition.y)
		{
			roster.transform.position += new Vector3(0f, -22f * Time.deltaTime, 0f);
			roster.transform.localEulerAngles += new Vector3(0f, 360f * Time.deltaTime, 0f);
		}
		else if (!introMade)
		{
			roster.transform.position = startPosition;
			roster.transform.rotation = startRotation;
		}
		if (!introMade)
		{
			if (alpha <= 0f)
			{
				introMade = true;
				alpha = 1f;
				fadeTime = 0.75f;
			}
			else
			{
				black.color = new Color(0f, 0f, 0f, alpha);
			}
		}
		else if (!enableActions && !outroMade)
		{
			front.color = new Color(1f, 1f, 1f, 1f - alpha);
		}
		else if (!enableActions)
		{
			black.color = new Color(1f, 1f, 1f, 1f - alpha);
		}
	}

	private void UpdateAlpha()
	{
		if (enableActions)
		{
			return;
		}
		if (alpha > 0f)
		{
			alpha -= fadeTime * Time.deltaTime;
			return;
		}
		alpha = 0f;
		enableActions = true;
		if (outroMade)
		{
			GameManager.lifes = 4;
			GameManager.stageCheckpoint = 0;
			GetComponent<ChangeScene>().LoadScene("stage1");
		}
	}

	private void Inputs()
	{
		if (!selectionMade && enableActions)
		{
			if ((double)Input.GetAxis("Horizontal") <= 0.2 && (double)Input.GetAxis("Horizontal") >= -0.2)
			{
				holdStick = false;
			}
			if (Input.GetKeyDown(KeyCode.RightArrow) || (double)Input.GetAxis("Horizontal") > 0.2)
			{
				if (!holdStick)
				{
					GetComponent<AudioSource>().pitch = 1f;
					GetComponent<AudioSource>().clip = cursorSound;
					GetComponent<AudioSource>().Play();
					selectionID++;
					direction = 2;
				}
				holdStick = true;
			}
			else if (Input.GetKeyDown(KeyCode.LeftArrow) || (double)Input.GetAxis("Horizontal") < -0.2)
			{
				if (!holdStick)
				{
					GetComponent<AudioSource>().pitch = 1f;
					GetComponent<AudioSource>().clip = cursorSound;
					GetComponent<AudioSource>().Play();
					selectionID--;
					direction = 1;
				}
				holdStick = true;
			}
			else if (Input.GetKeyDown(accept) || Input.GetKeyDown(acceptJoy))
			{
				if (selectionID == 1)
				{
					if (choosenShips[0] == 0)
					{
						GetComponent<AudioSource>().pitch = 3f;
						GetComponent<AudioSource>().clip = startSound;
						choosenShips[0] = 1;
					}
					else if (choosenShips[0] != 1)
					{
						choosenShips[1] = 1;
					}
					else
					{
						GetComponent<AudioSource>().pitch = 4f;
						GetComponent<AudioSource>().clip = cancelSound;
					}
					GetComponent<AudioSource>().Play();
				}
				else if (selectionID == 2)
				{
					if (choosenShips[0] == 0)
					{
						GetComponent<AudioSource>().pitch = 3f;
						GetComponent<AudioSource>().clip = startSound;
						choosenShips[0] = 2;
					}
					else if (choosenShips[0] != 2)
					{
						choosenShips[1] = 2;
					}
					else
					{
						GetComponent<AudioSource>().pitch = 4f;
						GetComponent<AudioSource>().clip = cancelSound;
					}
					GetComponent<AudioSource>().Play();
				}
				else if (selectionID == 3)
				{
					if (choosenShips[0] == 0)
					{
						GetComponent<AudioSource>().pitch = 3f;
						GetComponent<AudioSource>().clip = startSound;
						choosenShips[0] = 3;
					}
					else if (choosenShips[0] != 3)
					{
						choosenShips[1] = 3;
					}
					else
					{
						GetComponent<AudioSource>().pitch = 4f;
						GetComponent<AudioSource>().clip = cancelSound;
					}
					GetComponent<AudioSource>().Play();
				}
				else if (selectionID == 4)
				{
					if (choosenShips[0] == 0)
					{
						GetComponent<AudioSource>().pitch = 3f;
						GetComponent<AudioSource>().clip = startSound;
						choosenShips[0] = 4;
					}
					else if (choosenShips[0] != 4)
					{
						choosenShips[1] = 4;
					}
					else
					{
						GetComponent<AudioSource>().pitch = 4f;
						GetComponent<AudioSource>().clip = cancelSound;
					}
					GetComponent<AudioSource>().Play();
				}
				if (choosenShips[1] != 0)
				{
					selectionMade = true;
					enableActions = false;
					outroMade = true;
					alpha = 1f;
					fadeTime = 0.5f;
					Camera.main.GetComponent<AudioSource>().Stop();
					GetComponent<AudioSource>().pitch = 1f;
					GetComponent<AudioSource>().clip = startSound;
					GetComponent<AudioSource>().Play();
				}
			}
		}
		if (enableActions && choosenShips[0] != 0 && (Input.GetKeyDown(cancel) || Input.GetKeyDown(cancelJoy)))
		{
			GetComponent<AudioSource>().pitch = 2f;
			GetComponent<AudioSource>().clip = cancelSound;
			GetComponent<AudioSource>().Play();
			choosenShips[0] = 0;
			choosenShips[1] = 0;
			forbidden.color = new Color(1f, 1f, 1f, 0f);
			selectionMade = false;
			enableActions = false;
		}
	}

	private void UpdateGraphics()
	{
		if (!enableActions)
		{
			return;
		}
		ship1.color = new Color(1f, 1f, 1f, 0f);
		ship2.color = new Color(1f, 1f, 1f, 0f);
		ship3.color = new Color(1f, 1f, 1f, 0f);
		ship4.color = new Color(1f, 1f, 1f, 0f);
		if (direction == 1)
		{
			roster.transform.localEulerAngles += new Vector3(0f, (0f - rotationSpeed) * Time.deltaTime, 0f);
			forbidden.color = new Color(1f, 1f, 1f, 0f);
		}
		else if (direction == 2)
		{
			roster.transform.localEulerAngles += new Vector3(0f, rotationSpeed * Time.deltaTime, 0f);
			forbidden.color = new Color(1f, 1f, 1f, 0f);
		}
		else if (choosenShips[0] == selectionID || choosenShips[1] == selectionID)
		{
			forbidden.color = new Color(1f, 1f, 1f, 1f);
		}
		switch (selectionID)
		{
		case 1:
			ship1.color = new Color(1f, 1f, 1f, 1f);
			if (roster.transform.localEulerAngles.y < rotationSpeed * Time.deltaTime || roster.transform.localEulerAngles.y > 360f - rotationSpeed * Time.deltaTime)
			{
				direction = 0;
				roster.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			}
			break;
		case 2:
			ship2.color = new Color(1f, 1f, 1f, 1f);
			if (roster.transform.localEulerAngles.y > 90f - rotationSpeed * Time.deltaTime && roster.transform.localEulerAngles.y < 90f + rotationSpeed * Time.deltaTime)
			{
				direction = 0;
				roster.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
			}
			break;
		case 3:
			ship3.color = new Color(1f, 1f, 1f, 1f);
			if (roster.transform.localEulerAngles.y > 180f - rotationSpeed * Time.deltaTime && roster.transform.localEulerAngles.y < 180f + rotationSpeed * Time.deltaTime)
			{
				direction = 0;
				roster.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
			}
			break;
		case 4:
			ship4.color = new Color(1f, 1f, 1f, 1f);
			if (roster.transform.localEulerAngles.y > 270f - rotationSpeed * Time.deltaTime && roster.transform.localEulerAngles.y < 270f + rotationSpeed * Time.deltaTime)
			{
				direction = 0;
				roster.transform.localEulerAngles = new Vector3(0f, 270f, 0f);
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

	public void ChoosenCharacter(int number)
	{
		choosenShips[0] = number;
	}

	public void ChoosenCharacter2(int number)
	{
		choosenShips[1] = number;
	}
}
