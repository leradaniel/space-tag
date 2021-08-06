// GameManager
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static bool pause;

	private KeyCode pauseKey;

	private KeyCode pauseKeyJoy;

	public GameObject consoleContent;

	public static int stageCheckpoint;

	private float transitionTimeNow;

	private float transitionTime = 3f;

	public static int lifes = 4;

	public List<ShipParent> shipScripts = new List<ShipParent>();

	public Image mainHealthBar;

	public Image mainHealthBarRed;

	public Image mainHealthBarInactive;

	public Image altHealthBar;

	public Image altHealthBarRed;

	public Image altHealthBarInactive;

	public Image tagBar;

	public Image life1;

	public Image life2;

	public Image life3;

	public Image life4;

	public Image front;

	private float alpha;

	private float fadeTime = 0.5f;

	private bool fading;

	private float mainHealthScale;

	private float altHealthScale;

	[Header("Variables de Instancias:")]
	[Tooltip("Si se activa este booleano, se usan los valores temporales del inspector en vez de los del character select.\n(Usado para testear el nivel directamente)")]
	public bool useTestShips;

	[Tooltip("Primer nave temporal elegida.")]
	public int testMainShip = 1;

	[Tooltip("Segunda nave tempporal elegida.")]
	public int testAltShip = 2;

	public GameObject ship1prefab;

	public GameObject ship2prefab;

	public GameObject ship3prefab;

	public GameObject ship4prefab;

	private GameObject ship1inst;

	private GameObject ship2inst;

	private GameObject ship3inst;

	private GameObject ship4inst;

	private float timeRegenNow;

	[Header("Variables de Regeneración y Vida:")]
	public float regenerationInterval = 2f;

	public int regenerationPercentage = 3;

	private bool shipDead;

	private bool altshipDead;

	private bool immunityTime;

	private int hp;

	private float immunityDeadTimeNow;

	public float immunityDeadTime = 2f;

	[Header("Variables de Inputs y etc:")]
	public KeyCode tagKey = KeyCode.Tab;

	public KeyCode tagKeyJoy = KeyCode.Joystick1Button5;

	public KeyCode tagKeyJoyAlt = KeyCode.Joystick1Button4;

	private float tagCooldown = 10f;

	private float tagCooldownNow;

	public GameObject ship;

	public GameObject altship;

	private bool cantTag;

	public ChangeScene stagePosition;

	public List<Checkpoint> checkpoints;

	private EnemyBoss bossReference;

	private void Awake()
	{
		front.enabled = true;
		if (useTestShips)
		{
			CharacterSelect.choosenShips[1] = testAltShip;
			CharacterSelect.choosenShips[0] = testMainShip;
		}
	}

	private void Start()
	{
		DeactivateShip(CharacterSelect.choosenShips[0], CharacterSelect.choosenShips[1]);
		altship.SetActive(value: false);
		InitStage();
		UpdateLifes();
		pauseKey = shipScripts[0].pauseKey;
		pauseKeyJoy = shipScripts[0].pauseKeyJoy;
		alpha = 1f;
		bossReference = GameObject.Find("EnemyBoss").GetComponent<EnemyBoss>();
	}

	private void Update()
	{
		if (!pause)
		{
			TagShip();
			RegenerateShipHP();
			UpdateBars();
			LifeLost();
			ChangeFallenShip();
			UpdateAlpha();
			FinishedStage();
		}
		EqualShipsPositions();
		CheckPause();
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
		if (fading && lifes > 0)
		{
			front.color = new Color(1f, 1f, 1f, 1f - alpha);
		}
		else if (fading && lifes <= 0)
		{
			front.color = new Color(0f, 0f, 0f, 1f - alpha);
		}
		else
		{
			front.color = new Color(1f, 1f, 1f, alpha);
		}
	}

	private void InitStage()
	{
		float x = Camera.main.transform.position.x;
		float num = 40f;
		x = stageCheckpoint switch
		{
			1 => checkpoints[0].transform.position.x, 
			2 => checkpoints[1].transform.position.x, 
			3 => checkpoints[2].transform.position.x, 
			4 => checkpoints[3].transform.position.x, 
			5 => checkpoints[4].transform.position.x, 
			6 => checkpoints[5].transform.position.x, 
			_ => 0f, 
		};
		Camera.main.transform.position = new Vector3(x, Camera.main.transform.position.y, Camera.main.transform.position.z);
		ship.transform.position = new Vector3(x - num, Camera.main.transform.position.y, 0f);
		ship.transform.eulerAngles = new Vector3(0f, 90f, 0f);
		altship.transform.position = new Vector3(x - num, Camera.main.transform.position.y, 0f);
		altship.transform.eulerAngles = new Vector3(0f, 90f, 0f);
		GameObject[] array = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			if (gameObject.transform.position.x < ship.transform.position.x)
			{
				Object.Destroy(gameObject);
			}
		}
	}

	public void UpdateLifes()
	{
		Color color;
		switch (lifes)
		{
		case 0:
			color = life4.color;
			color.a = 0f;
			life4.color = color;
			life3.color = color;
			life2.color = color;
			life1.color = color;
			break;
		case 1:
			color = life4.color;
			color.a = 0f;
			life4.color = color;
			life3.color = color;
			life2.color = color;
			color.a = 1f;
			life1.color = color;
			break;
		case 2:
			color = life4.color;
			color.a = 0f;
			life4.color = color;
			life3.color = color;
			color.a = 1f;
			life2.color = color;
			life1.color = color;
			break;
		case 3:
			color = life4.color;
			color.a = 0f;
			life4.color = color;
			color.a = 1f;
			life3.color = color;
			life2.color = color;
			life1.color = color;
			break;
		case 4:
			color = life4.color;
			color.a = 1f;
			life4.color = color;
			life3.color = color;
			life2.color = color;
			life1.color = color;
			break;
		}
	}

	private void DeactivateShip(int shipMain, int shipAlt)
	{
		switch (shipMain)
		{
		case 1:
			ship = Object.Instantiate(ship1prefab);
			shipScripts.Add(ship.gameObject.GetComponent<Ship1>());
			break;
		case 2:
			ship = Object.Instantiate(ship2prefab);
			shipScripts.Add(ship.gameObject.GetComponent<Ship2>());
			break;
		case 3:
			ship = Object.Instantiate(ship3prefab);
			shipScripts.Add(ship.gameObject.GetComponent<Ship3>());
			break;
		case 4:
			ship = Object.Instantiate(ship4prefab);
			shipScripts.Add(ship.gameObject.GetComponent<Ship4>());
			break;
		}
		switch (shipAlt)
		{
		case 1:
			altship = Object.Instantiate(ship1prefab);
			shipScripts.Add(altship.gameObject.GetComponent<Ship1>());
			break;
		case 2:
			altship = Object.Instantiate(ship2prefab);
			shipScripts.Add(altship.gameObject.GetComponent<Ship2>());
			break;
		case 3:
			altship = Object.Instantiate(ship3prefab);
			shipScripts.Add(altship.gameObject.GetComponent<Ship3>());
			break;
		case 4:
			altship = Object.Instantiate(ship4prefab);
			shipScripts.Add(altship.gameObject.GetComponent<Ship4>());
			break;
		}
	}

	private void EqualShipsPositions()
	{
		if (shipScripts[0].isAlive && shipScripts[1].isAlive)
		{
			if (ship.activeSelf)
			{
				altship.transform.position = ship.transform.position;
			}
			else if (altship.activeSelf)
			{
				ship.transform.position = altship.transform.position;
			}
		}
	}

	private void TagShip()
	{
		if (!shipScripts[0].isAlive && !shipDead)
		{
			cantTag = true;
		}
		else if (!shipScripts[1].isAlive && !altshipDead)
		{
			cantTag = true;
		}
		else
		{
			cantTag = false;
		}
		if (tagCooldownNow > 0f)
		{
			tagCooldownNow -= Time.deltaTime;
		}
		if ((!Input.GetKeyDown(tagKey) && !Input.GetKeyDown(tagKeyJoy) && !Input.GetKeyDown(tagKeyJoyAlt)) || !(tagCooldownNow <= 0f) || cantTag)
		{
			return;
		}
		if (ship.activeSelf)
		{
			if (shipScripts[1].isAlive)
			{
				ActivateShip(2);
				ActivateImmunity();
				ship.SetActive(value: false);
				tagCooldownNow = tagCooldown;
			}
			else
			{
				ActivateImmunity();
				tagCooldownNow = tagCooldown;
			}
		}
		else if (altship.activeSelf)
		{
			if (shipScripts[0].isAlive)
			{
				ActivateShip(1);
				ActivateImmunity();
				altship.SetActive(value: false);
				tagCooldownNow = tagCooldown;
			}
			else
			{
				ActivateImmunity();
				tagCooldownNow = tagCooldown;
			}
		}
	}

	private void ActivateShip(int shipNumber)
	{
		switch (shipNumber)
		{
		case 1:
			ship.SetActive(value: true);
			break;
		case 2:
			altship.SetActive(value: true);
			break;
		}
	}

	public void ActivateImmunity()
	{
		if (ship.activeSelf)
		{
			ship.GetComponent<Collider>().enabled = false;
			shipScripts[0].colorFlashTime = immunityDeadTime;
			shipScripts[0].colorFlash = true;
			shipScripts[0].colorCounter = 0f;
		}
		if (altship.activeSelf)
		{
			altship.GetComponent<Collider>().enabled = false;
			shipScripts[1].colorFlashTime = immunityDeadTime;
			shipScripts[1].colorFlash = true;
			shipScripts[1].colorCounter = 0f;
		}
		immunityTime = true;
		immunityDeadTimeNow = immunityDeadTime;
	}

	private void RegenerateShipHP()
	{
		timeRegenNow += Time.deltaTime;
		if (!(timeRegenNow >= regenerationInterval))
		{
			return;
		}
		if (shipScripts[0].isAlive && !ship.activeSelf)
		{
			int healthPointsNow = shipScripts[0].healthPointsNow;
			int healthPointsMax = shipScripts[0].healthPointsMax;
			int num = regenerationPercentage * healthPointsMax / 100;
			if (healthPointsNow < healthPointsMax)
			{
				int num2 = healthPointsNow + num;
				if (num2 > healthPointsMax)
				{
					shipScripts[0].healthPointsNow = shipScripts[0].healthPointsMax;
				}
				else
				{
					shipScripts[0].healthPointsNow = num2;
				}
			}
		}
		else if (shipScripts[1].isAlive && !altship.activeSelf)
		{
			int healthPointsNow2 = shipScripts[1].healthPointsNow;
			int healthPointsMax2 = shipScripts[1].healthPointsMax;
			int num3 = regenerationPercentage * healthPointsMax2 / 100;
			if (healthPointsNow2 < healthPointsMax2)
			{
				int num4 = healthPointsNow2 + num3;
				if (num4 > healthPointsMax2)
				{
					shipScripts[1].healthPointsNow = shipScripts[1].healthPointsMax;
				}
				else
				{
					shipScripts[1].healthPointsNow = num4;
				}
			}
		}
		timeRegenNow = 0f;
	}

	private void UpdateBars()
	{
		float num = tagCooldownNow / tagCooldown;
		num = 1f - num;
		tagBar.transform.localScale = new Vector3(num, tagBar.transform.localScale.y, tagBar.transform.localScale.z);
		if (num < 1f)
		{
			Color color = tagBar.color;
			color.a = 0.5f;
			tagBar.color = color;
		}
		else
		{
			Color color2 = tagBar.color;
			color2.a = 1f;
			tagBar.color = color2;
		}
		if (shipScripts[0].isAlive)
		{
			float num2 = shipScripts[0].healthPointsNow;
			float num3 = shipScripts[0].healthPointsMax;
			mainHealthScale = num2 / num3;
			if (mainHealthScale < 0f)
			{
				mainHealthScale = 0f;
			}
			mainHealthBar.transform.localScale = new Vector3(mainHealthScale, mainHealthBar.transform.localScale.y, mainHealthBar.transform.localScale.z);
			Color color3 = mainHealthBar.color;
			color3.a = 1f - (1f - mainHealthScale) * 1.5f;
			mainHealthBar.color = color3;
			mainHealthBarRed.transform.localScale = new Vector3(mainHealthScale, mainHealthBarRed.transform.localScale.y, mainHealthBarRed.transform.localScale.z);
			if (ship.activeSelf)
			{
				color3 = mainHealthBarInactive.color;
				color3.a = 0f;
				mainHealthBarInactive.color = color3;
			}
			else
			{
				color3 = mainHealthBarInactive.color;
				color3.a = 0.6f;
				mainHealthBarInactive.color = color3;
			}
		}
		else
		{
			Color color4 = mainHealthBar.color;
			color4.a = 0f;
			mainHealthBar.color = color4;
			mainHealthBarRed.transform.localScale = new Vector3(1f, mainHealthBarRed.transform.localScale.y, mainHealthBarRed.transform.localScale.z);
			color4 = new Color(0f, 0f, 0f);
			color4.a = 0.6f;
			mainHealthBarInactive.color = color4;
		}
		if (shipScripts[1].isAlive)
		{
			float num4 = shipScripts[1].healthPointsNow;
			float num5 = shipScripts[1].healthPointsMax;
			altHealthScale = num4 / num5;
			if (altHealthScale < 0f)
			{
				altHealthScale = 0f;
			}
			altHealthBar.transform.localScale = new Vector3(altHealthScale, altHealthBar.transform.localScale.y, altHealthBar.transform.localScale.z);
			Color color5 = altHealthBar.color;
			color5.a = 1f - (1f - altHealthScale) * 1.5f;
			altHealthBar.color = color5;
			altHealthBarRed.transform.localScale = new Vector3(altHealthScale, altHealthBarRed.transform.localScale.y, altHealthBarRed.transform.localScale.z);
			if (altship.activeSelf)
			{
				color5 = altHealthBarInactive.color;
				color5.a = 0f;
				altHealthBarInactive.color = color5;
			}
			else
			{
				color5 = altHealthBarInactive.color;
				color5.a = 0.6f;
				altHealthBarInactive.color = color5;
			}
		}
		else
		{
			Color color6 = altHealthBar.color;
			color6.a = 0f;
			altHealthBar.color = color6;
			altHealthBarRed.transform.localScale = new Vector3(1f, altHealthBarRed.transform.localScale.y, altHealthBarRed.transform.localScale.z);
			color6 = new Color(0f, 0f, 0f);
			color6.a = 0.6f;
			altHealthBarInactive.color = color6;
		}
	}

	public void ChangeFallenShip()
	{
		if (immunityDeadTimeNow <= 0f)
		{
			if (immunityTime)
			{
				if (shipScripts[0].isAlive)
				{
					ship.GetComponent<Collider>().enabled = true;
				}
				if (shipScripts[1].isAlive)
				{
					altship.GetComponent<Collider>().enabled = true;
				}
			}
			immunityTime = false;
			if (!shipScripts[0].isAlive && shipScripts[1].isAlive && !shipDead)
			{
				ActivateShip(2);
				ActivateImmunity();
				ship.SetActive(value: false);
				shipDead = true;
			}
			else if (!shipScripts[1].isAlive && shipScripts[0].isAlive && !altshipDead)
			{
				ActivateShip(1);
				ActivateImmunity();
				altship.SetActive(value: false);
				altshipDead = true;
			}
		}
		else
		{
			immunityTime = true;
			immunityDeadTimeNow -= Time.deltaTime;
		}
	}

	public void LifeLost()
	{
		if (shipScripts[0].isAlive || shipScripts[1].isAlive)
		{
			return;
		}
		Camera.main.GetComponent<AudioSource>().Stop();
		transitionTimeNow += Time.deltaTime;
		CameraMovement.panSpeed = 0f;
		if (transitionTimeNow >= transitionTime && !fading)
		{
			alpha = 1f;
			fading = true;
		}
		else if (alpha == 0f && fading)
		{
			if (lifes > 0)
			{
				lifes--;
				immunityTime = false;
				immunityDeadTimeNow = 0f;
				UpdateLifes();
				transitionTimeNow = 0f;
				stagePosition.LoadScene("stage1");
			}
			else
			{
				lifes--;
				stagePosition.LoadScene("endscreen");
			}
		}
	}

	private void CheckPause()
	{
		if ((Input.GetKeyDown(pauseKey) || Input.GetKeyDown(pauseKeyJoy)) && !consoleContent.activeSelf && !pause)
		{
			pause = true;
		}
		if (pause)
		{
			Camera.main.GetComponent<AudioSource>().Pause();
		}
		else
		{
			Camera.main.GetComponent<AudioSource>().UnPause();
		}
	}

	public void FinishedStage()
	{
		if (bossReference.life <= 0)
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("Enemy");
			GameObject[] array2 = array;
			foreach (GameObject obj in array2)
			{
				Object.Destroy(obj);
			}
			ship.GetComponent<Collider>().enabled = false;
			altship.GetComponent<Collider>().enabled = false;
			Camera.main.GetComponent<AudioSource>().Stop();
			transitionTimeNow += Time.deltaTime;
			if (transitionTimeNow >= transitionTime && !fading)
			{
				alpha = 1f;
				fading = true;
			}
			else if (alpha == 0f && fading)
			{
				stagePosition.LoadScene("endscreen");
			}
		}
	}
}
