// Enemy1
using UnityEngine;

public class Enemy1 : EnemyParent
{
	protected override void EnemyMovement()
	{
		base.EnemyMovement();
		base.transform.position += base.transform.forward * enemySpeed * Time.deltaTime;
	}
}
