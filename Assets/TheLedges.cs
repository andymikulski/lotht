using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TheLedges : MonoBehaviour {
	public VRTK_Slider Toggle;
	public Door exit;
	public StareAt staringLight;

	public bool hasClearedLevel = false;
	public bool hasPlayer = false;


	// Use this for initialization
	void Start () {
		Toggle.ValueChanged += HandleChange;
	}

	private void HandleChange(object sender, Control3DEventArgs e)
	{
		if (e.value >= 90f) {
			hasClearedLevel = true;
			exit.OpenDoor ();
		}
	}

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
