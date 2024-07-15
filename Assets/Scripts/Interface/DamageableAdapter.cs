using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageableAdapter : MonoBehaviour, IDamageable
{
	public UnityEvent<int> OnTakeDamaged;

	public void TakeDamage(int damage)
	{
		OnTakeDamaged?.Invoke(damage);
	}
}
