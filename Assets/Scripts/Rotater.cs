using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
	private void Update()
	{
		transform.Rotate(Vector3.up * 180 * Time.deltaTime, Space.World);
	}
}
