// Enemy10
using System;
using UnityEngine;

public class Enemy10 : EnemyParent
{
	private float degrees;

	private float radians;

	private Vector3 center;

	[Tooltip("Anchura a la que se llega desde el centro, para izquierda y derecha.")]
	public float widthDist;

	[Tooltip("Anchura a la que se llega desde el centro, para arriba y abajo.")]
	public float heightDist;

	[Tooltip("Eje que sirve como traslación del objeto.")]
	public float axis;

	[Tooltip("Velocidad de oscilación.")]
	public float cicleSpeed;

	protected override void Start()
	{
		base.Start();
		center = base.transform.position;
	}

	protected override void EnemyMovement()
	{
		base.EnemyMovement();
		degrees += cicleSpeed * Time.deltaTime;
		radians = degrees * ((float)Math.PI / 180f);
		center += base.transform.forward * axis * Time.deltaTime;
		Vector3 zero = Vector3.zero;
		zero.x = Mathf.Cos(radians) * widthDist;
		zero.y = Mathf.Sin(radians) * heightDist;
		base.transform.position = center + zero;
	}
}
