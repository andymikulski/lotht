using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StareAt : MonoBehaviour {
	public Transform target;
	public float moveSpeed = 1f;

	private float lightSpeed = 2f;
	private float onValue;
	public bool isLightOn = true;

	private Light light;

	void Start () {
		light = GetComponent<Light>();
		onValue = light.intensity;
	}

	void Update () {
		Vector3 newDirection = target.position - transform.position;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(newDirection), moveSpeed * Time.deltaTime);

		light.intensity = Mathf.Lerp (light.intensity, isLightOn ? onValue : 0f, lightSpeed * Time.deltaTime);
	}
}
