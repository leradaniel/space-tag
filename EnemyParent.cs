// EnemyParent
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
	[Header("Variables de Stats:")]
	public float enemySpeed;

	[Tooltip("Daño que otorga el enemigo en contacto.")]
	public int damageGiven;

	[Header("Variables de Vida:")]
	[Tooltip("Vida del enemigo.")]
	public int life;

	[Tooltip("Color del flash que parpadea en el enemigo al ser golpeado.")]
	public Color hitColor = Color.red;

	[Tooltip("Prefab de la explosión al morir.")]
	public GameObject explosionType;

	[Header("Variables de Disparo:")]
	[Tooltip("¿El enemigo dispara?")]
	public bool shottingEnemy;

	[Tooltip("¿Los disparos son constantes?")]
	public bool shottingRepeat;

	[Tooltip("¿Cada cuántos segundos dispara?")]
	public float timeToShoot = 3f;

	[Tooltip("Transform o GameObject desde donde sale el disparo enemigo.")]
	public Transform shotPoint;

	[Tooltip("Prefab de la bala disparada.")]
	public GameObject bulletShot;

	protected float timeToShootNow;

	protected bool colorFlash;

	protected float colorFlashTime = 0.05f;

	protected float colorTimer;

	protected Transform playerTransform;

	protected bool activated;

	protected Vector3 enemySize;

	protected GameManager gameManagerReference;

	protected virtual void Start()
	{
		GetComponent<Collider>().enabled = false;
		timeToShootNow = timeToShoot;
		gameManagerReference = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	protected virtual void Update()
	{
		if (!GameManager.pause)
		{
			onHitColor();
			activateEnemy();
			EnemyShoot();
			if (activated)
			{
				EnemyMovement();
			}
		}
	}

	protected virtual void EnemyMovement()
	{
		base.transform.position += new Vector3(CameraMovement.panSpeed * Time.deltaTime, 0f, 0f);
	}

	protected virtual void activateEnemy()
	{
		if ((bool)GetComponent<Renderer>())
		{
			enemySize = GetComponent<Renderer>().bounds.size;
		}
		else if ((bool)GetComponentInChildren<Renderer>())
		{
			enemySize = GetComponentInChildren<Renderer>().bounds.size;
		}
		else
		{
			enemySize = Vector3.zero;
		}
		float z = (base.transform.position - Camera.main.transform.position).z;
		Vector3 vector = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, z));
		if (base.transform.position.x <= vector.x + enemySize.x / 2f)
		{
			activated = true;
			GetComponent<Collider>().enabled = true;
		}
	}

	protected virtual void EnemyShoot()
	{
		if (!activated || !shottingEnemy)
		{
			return;
		}
		if (timeToShootNow > 0f)
		{
			timeToShootNow -= Time.deltaTime;
			if (gameManagerReference.ship.activeSelf)
			{
				playerTransform = gameManagerReference.ship.GetComponent<Transform>();
			}
			else if (gameManagerReference.altship.activeSelf)
			{
				playerTransform = gameManagerReference.altship.GetComponent<Transform>();
			}
			if (playerTransform != null)
			{
				shotPoint.transform.LookAt(playerTransform);
			}
		}
		else
		{
			Object.Instantiate(bulletShot, shotPoint.position, shotPoint.rotation);
			if (shottingRepeat)
			{
				timeToShootNow = timeToShoot;
			}
			else
			{
				shottingEnemy = false;
			}
		}
	}

	protected virtual void OnTriggerStay(Collider other)
	{
		if (other.gameObject.layer == 8)
		{
			if (other.gameObject == gameManagerReference.ship)
			{
				gameManagerReference.shipScripts[0].Damage(damageGiven);
			}
			else if (other.gameObject == gameManagerReference.altship)
			{
				gameManagerReference.shipScripts[1].Damage(damageGiven);
			}
		}
	}

	protected virtual void onHitColor()
	{
		if (!colorFlash)
		{
			return;
		}
		colorTimer += Time.deltaTime;
		if (colorTimer < colorFlashTime)
		{
			if ((bool)GetComponent<Renderer>())
			{
				GetComponent<Renderer>().material.SetColor("_EmissionColor", hitColor * 0.5f);
			}
			else if ((bool)GetComponentInChildren<Renderer>())
			{
				GetComponentInChildren<Renderer>().material.SetColor("_EmissionColor", hitColor * 0.5f);
			}
			return;
		}
		if ((bool)GetComponent<Renderer>())
		{
			GetComponent<Renderer>().material.SetColor("_EmissionColor", hitColor * 0f);
		}
		else if ((bool)GetComponentInChildren<Renderer>())
		{
			GetComponentInChildren<Renderer>().material.SetColor("_EmissionColor", hitColor * 0f);
		}
		colorFlash = false;
		colorTimer = 0f;
	}

	public virtual void Damage(int damageAmount)
	{
		if (!colorFlash)
		{
			life -= damageAmount;
			if (life <= 0)
			{
				Object.Instantiate(explosionType, base.transform.position, base.transform.rotation);
				Object.Destroy(base.gameObject);
				return;
			}
			GetComponent<AudioSource>().pitch = Random.Range(1f, 2f);
			GetComponent<AudioSource>().Play();
			colorFlash = true;
			colorTimer = 0f;
		}
	}
}
