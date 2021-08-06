// Wall
using UnityEngine;

public class Wall : MonoBehaviour
{
	[Tooltip("Daño que otorga el enemigo en contacto.")]
	public int damageGiven;

	protected GameManager gameManagerReference;

	private void Start()
	{
		gameManagerReference = GameObject.Find("GameManager").GetComponent<GameManager>();
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
}
