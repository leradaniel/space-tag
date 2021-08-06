// BulletParent
using UnityEngine;

public class BulletParent : MonoBehaviour
{
	[Tooltip("Daño que hace la bala.")]
	public int bulletDamage;

	[Tooltip("Velocidad de la bala.")]
	public float bulletSpeed;

	[Tooltip("Tiempo que tarda en auto-destruirse la bala. Si es 0, no lo hace.")]
	public float lifeTime;

	[Tooltip("Si se activa, en vez de moverse con forward se mueve hacia la derecha del mundo.")]
	public bool moveWorldSpace;

	protected virtual void Start()
	{
		if (lifeTime != 0f)
		{
			Object.Destroy(base.gameObject, lifeTime);
		}
	}

	protected virtual void Update()
	{
		if (!GameManager.pause)
		{
			BulletMovement();
		}
	}

	protected virtual void BulletMovement()
	{
		base.transform.position += new Vector3(CameraMovement.panSpeed * Time.deltaTime, 0f, 0f);
	}

	protected virtual void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy" && (bool)other.gameObject.GetComponent<EnemyParent>())
		{
			other.gameObject.GetComponent<EnemyParent>().Damage(bulletDamage);
		}
	}
}
