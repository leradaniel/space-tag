// Enemy2
using UnityEngine;

public class Enemy2 : EnemyParent
{
	[Header("Variables de Movimiento:")]
	[Tooltip("Fuerza con la que se 'lanza' al enemigo.")]
	public float strengthSpeed = 50f;

	private float actualSpeed;

	[Tooltip("Cuánto tarda en llegar al punto máximo en X.")]
	public float decreaseSpeed = 1.5f;

	[Tooltip("Desplazamiento vertical.")]
	public float verticalSpeed = 20f;

	protected override void Start()
	{
		base.Start();
		actualSpeed = strengthSpeed;
		decreaseSpeed = 1f / decreaseSpeed;
	}

	protected override void EnemyMovement()
	{
		base.EnemyMovement();
		actualSpeed -= decreaseSpeed * Time.deltaTime * strengthSpeed;
		base.transform.position += base.transform.forward * Time.deltaTime * actualSpeed;
		base.transform.position += base.transform.up * Time.deltaTime * verticalSpeed;
	}
}
