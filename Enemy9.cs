// Enemy9
using UnityEngine;

public class Enemy9 : EnemyParent
{
	protected override void EnemyMovement()
	{
	}

	protected override void onHitColor()
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
				GetComponent<Renderer>().material.SetColor("_Color", hitColor * 0.5f);
			}
			else if ((bool)GetComponentInChildren<Renderer>())
			{
				GetComponentInChildren<Renderer>().material.SetColor("_Color", hitColor * 0.5f);
			}
			return;
		}
		if ((bool)GetComponent<Renderer>())
		{
			GetComponent<Renderer>().material.SetColor("_Color", Color.white);
		}
		else if ((bool)GetComponentInChildren<Renderer>())
		{
			GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.white);
		}
		colorFlash = false;
		colorTimer = 0f;
	}
}
