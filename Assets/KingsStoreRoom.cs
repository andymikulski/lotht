using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class KingsStoreRoom : MonoBehaviour {
	public GameObject KeyPrefab;

	public GameObject[] Pillars;
	private int SelectedLootIndex;
	private Breakable SelectedPot;
	private int SelectedKeyIndex;

	public Door exit;
	public StareAt staringLight;

	public bool hasClearedLevel = false;
	public bool hasPlayer = false;

	void Start () {
		// selected loot/pot = the one that has the key in it
		SelectedLootIndex = Random.Range (0, Pillars.Length);
		SelectedPot = Pillars [SelectedLootIndex].GetComponentInChildren<Breakable>();
		SelectedPot.Loot = KeyPrefab;

		// selected key/hole = the one that has the key slot

		SelectedKeyIndex = Random.Range (0, Pillars.Length);

		VRTK_SnapDropZone zone = Pillars [SelectedKeyIndex].GetComponentInChildren<VRTK_SnapDropZone> ();
		zone.ObjectSnappedToDropZone += OnHoleSelect;
	}

	public void OnHoleSelect(object snappedObject, SnapDropZoneEventArgs evt) {
		hasClearedLevel = true;
		exit.OpenDoor ();
	}

	public void ToggleLevelFocus() {
		hasPlayer = !hasPlayer;
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
