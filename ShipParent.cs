// ShipParent
using UnityEngine;

public class ShipParent : MonoBehaviour
{
	public GameObject explosionType;

	[Header("Variables de Stats:")]
	public float shipSpeed;

	public int healthPointsMax;

	public int healthPointsNow;

	private float shipPanSpeed;

	[Header("Variables de Disparos:")]
	public float bulletCooldown;

	public Transform shotPoint;

	public Transform shotPoint2;

	public Transform shotPoint3;

	public Transform shotPoint4;

	public Transform shotPoint5;

	public Transform shotPoint6;

	public GameObject bulletShot;

	public GameObject bulletShot2;

	private float bulletCooldownNow;

	private float distance;

	private Vector3 shipSize;

	private Vector3 minBorder;

	private Vector3 maxBorder;

	[Header("Variables de Inputs:")]
	public KeyCode shootKey = KeyCode.Space;

	public KeyCode shootKeyJoy = KeyCode.Joystick1Button0;

	public KeyCode pauseKey = KeyCode.Escape;

	public KeyCode pauseKeyJoy = KeyCode.Joystick1Button7;

	[Header("Variables de Daño:")]
	public Color hitColor = Color.red;

	private Color regularColor;

	public bool colorFlash;

	public float colorCounter;

	public float colorFlashTime;

	public GameObject hitAnimation;

	public bool isAlive = true;

	public GameObject consoleContent;

	protected virtual void Start()
	{
		regularColor = GetComponent<Renderer>().material.color;
	}

	protected virtual void Update()
	{
		if (!GameManager.pause)
		{
			Movement();
			MovementBoundaries();
			ReadyShoot();
			OnHitColor();
		}
	}

	public virtual void OnHitColor()
	{
		if (!colorFlash)
		{
			return;
		}
		colorCounter += Time.deltaTime;
		if (colorCounter < colorFlashTime)
		{
			if ((bool)GetComponent<Renderer>())
			{
				GetComponent<Renderer>().material.color = hitColor * 0.5f;
			}
			else if ((bool)GetComponentInChildren<Renderer>())
			{
				GetComponentInChildren<Renderer>().material.color = hitColor * 0.5f;
			}
			return;
		}
		if ((bool)GetComponent<Renderer>())
		{
			GetComponent<Renderer>().material.color = regularColor;
		}
		else if ((bool)GetComponentInChildren<Renderer>())
		{
			GetComponentInChildren<Renderer>().material.color = regularColor;
		}
		colorFlash = false;
		colorCounter = 0f;
	}

	protected virtual void Movement()
	{
		if (Input.GetKey(KeyCode.RightArrow) || (double)Input.GetAxis("Horizontal") > 0.2)
		{
			shipPanSpeed = shipSpeed + CameraMovement.panSpeed;
		}
		else if (Input.GetKey(KeyCode.LeftArrow) || (double)Input.GetAxis("Horizontal") < -0.2)
		{
			shipPanSpeed = 0f - shipSpeed + CameraMovement.panSpeed;
		}
		else
		{
			shipPanSpeed = CameraMovement.panSpeed;
		}
		base.transform.position += base.transform.forward * shipPanSpeed * Time.deltaTime;
		if (Input.GetKey(KeyCode.UpArrow) || (double)Input.GetAxis("Vertical") > 0.2)
		{
			base.transform.position += base.transform.up * shipSpeed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.DownArrow) || (double)Input.GetAxis("Vertical") < -0.2)
		{
			base.transform.position -= base.transform.up * shipSpeed * Time.deltaTime;
		}
	}

	protected virtual void ReadyShoot()
	{
		if (isAlive)
		{
			if (bulletCooldownNow > 0f)
			{
				bulletCooldownNow -= Time.deltaTime * 1000f;
			}
			if ((Input.GetKey(shootKey) || Input.GetKey(shootKeyJoy)) && bulletCooldownNow <= 0f)
			{
				Shoot();
				bulletCooldownNow = bulletCooldown;
			}
		}
	}

	protected virtual void Shoot()
	{
	}

	protected virtual void MovementBoundaries()
	{
		shipSize = GetComponent<Renderer>().bounds.size;
		float z = (base.transform.position - Camera.main.transform.position).z;
		minBorder = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, z));
		maxBorder = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, z));
		base.transform.position = new Vector3(Mathf.Clamp(base.transform.position.x, minBorder.x + shipSize.x / 2f, maxBorder.x - shipSize.x / 2f), Mathf.Clamp(base.transform.position.y, minBorder.y + shipSize.y / 2f, maxBorder.y - shipSize.y / 2f), 0f);
	}

	public virtual void Damage(int damageAmount)
	{
		healthPointsNow -= damageAmount;
		GameObject.Find("GameManager").GetComponent<GameManager>().ActivateImmunity();
		colorFlash = true;
		colorCounter = 0f;
		if (healthPointsNow > 0)
		{
			Object.Instantiate(hitAnimation, base.transform.position, base.transform.rotation);
		}
		else
		{
			DestroyThisShip();
		}
	}

	public virtual void DestroyThisShip()
	{
		Object.Instantiate(explosionType, base.transform.position, base.transform.rotation);
		GetComponent<Collider>().enabled = false;
		GetComponent<Renderer>().enabled = false;
		isAlive = false;
	}

	public virtual void ResurrectThisShip()
	{
		healthPointsNow = healthPointsMax;
		GetComponent<Collider>().enabled = true;
		GetComponent<Renderer>().enabled = true;
		isAlive = true;
	}
}
