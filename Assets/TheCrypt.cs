using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TheCrypt : MonoBehaviour {
	public LinearMapping[] Handles;
	private int SelectedIndex;
	private LinearMapping SelectedHandle;

	public Door exit;
	public StareAt staringLight;

	public bool hasClearedLevel = false;
	public bool hasPlayer = false;

	void Start () {
		SelectedIndex = Random.Range (0, Handles.Length);
		SelectedHandle = Handles [SelectedIndex];
	}

	void Update () {
		if (exit != null && !hasClearedLevel && SelectedHandle != null && SelectedHandle.value >= 1) {
			hasClearedLevel = true;
			exit.OpenDoor ();
		}
	}

	public void ToggleLevelFocus() {
		hasPlayer = !hasPlayer;
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("enter - " + other.tag);
		if (other.CompareTag ("Player")) {
			hasPlayer = true;
			staringLight.isLightOn = true;
			staringLight.target = other.gameObject.transform;
		}
	}

	void OnTriggerExit(Collider other) {
		Debug.Log ("Exit - " + other.tag);
		if (other.CompareTag ("Player")) {
			hasPlayer = false;
			staringLight.isLightOn = false;
			staringLight.target = transform;
		}
	}
}
