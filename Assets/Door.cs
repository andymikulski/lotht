using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	public bool isOpen;
	private Vector3 ClosedPosition;
	private Vector3 OpenPosition;

	public float speed = 5f;

	void Start() {
		Bounds bounds = GetComponent<MeshRenderer>().bounds;
		float size = (bounds.max.z - bounds.min.z) * 1.25f;

		if (!isOpen) {
			ClosedPosition = transform.position;
			OpenPosition = transform.position + new Vector3 (0f, 0f, size);
		} else {
			OpenPosition = transform.position;
			ClosedPosition = transform.position - new Vector3 (0f, 0f, size);
		}
	}

	public void OpenDoor() {
		isOpen = true;
	}

	public void CloseDoor() {
		isOpen = false;
	}

	void Update () {
		transform.position = Vector3.Lerp (transform.position, isOpen ? OpenPosition : ClosedPosition, speed * Time.deltaTime);
	}
}
