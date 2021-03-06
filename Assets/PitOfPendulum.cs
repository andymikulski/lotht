using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PitOfPendulum : MonoBehaviour {
	public StareAt staringLight;

	public bool hasClearedLevel = false;
	public bool hasPlayer = false;

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Player")) {
			hasPlayer = true;
			staringLight.isLightOn = true;
			staringLight.target = other.gameObject.transform;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.CompareTag ("Player")) {
			hasPlayer = false;
			staringLight.isLightOn = false;
			staringLight.target = transform;
		}
	}
}
