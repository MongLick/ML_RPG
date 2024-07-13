using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField] Collider attackCollider;
	[SerializeField] int damage;

	public void EnableWeapon()
	{
		attackCollider.enabled = true;
	}

	public void DisableWepon()
	{
		attackCollider.enabled = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		IDamageable damageable = other.GetComponent<IDamageable>();
		if (damageable != null)
		{
			damageable.TakeDamage(damage);
		}
	}
}
