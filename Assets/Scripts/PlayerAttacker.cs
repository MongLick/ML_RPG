using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float range;
    [SerializeField] LayerMask layerMask;
    [SerializeField] int damage;
    [SerializeField] float angle;

    private float cosRange;

	private void Awake()
	{
		cosRange = Mathf.Cos(angle * Mathf.Deg2Rad);
	}

	private void OnAttack(InputValue value)
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        Attack();
	}

    private void Attack()
    {
        int rand = Random.Range(0, 2);
        if(rand == 0)
        {
			animator.SetTrigger("Attack1");
		}
        else
        {
			animator.SetTrigger("Attack2");
		}
	}

    Collider[] colliders = new Collider[20];
    private void AttackTiming()
    {
        int size = Physics.OverlapSphereNonAlloc(transform.position, range, colliders, layerMask);
        for(int i = 0; i < size; i++)
        {
            Vector3 dirToTarget = (colliders[i].transform.position - transform.position).normalized;

            if(Vector3.Dot(transform.forward, dirToTarget) < cosRange)
            {
				continue;
			}

            IDamageable damageable = colliders[i].GetComponent<IDamageable>();

            if(damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }
    }

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
	}
}
