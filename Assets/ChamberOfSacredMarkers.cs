using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ChamberOfSacredMarkers : MonoBehaviour {
	public Door exit;
	public StareAt staringLight;

	public bool hasClearedLevel = false;
	public bool hasPlayer = false;
	public bool setupLeftSide = false;

	public GameObject[] Markers = new GameObject[4];

	public GameObject[] LeftZone = new GameObject[4];
	public GameObject[] RightZone = new GameObject[4];

	private bool[] HasCorrect = new bool[4];

	public static T[] ShuffleArray<T>(T[] arr) {
		T[] newArray = (T[])arr.Clone ();

		for (int i = arr.Length - 1; i > 0; i--) {
			int r = Random.Range(0, i);
			T tmp = newArray[i];
			newArray[i] = newArray[r];
			newArray[r] = tmp;
		}

		return newArray;
	}

	Component CopyComponent(Component original, GameObject destination)
	{
		System.Type type = original.GetType();
		Component copy = destination.AddComponent(type);
		// Copied fields can be restricted with BindingFlags
		System.Reflection.FieldInfo[] fields = type.GetFields(); 
		foreach (System.Reflection.FieldInfo field in fields)
		{
			field.SetValue(copy, field.GetValue(original));
		}
		return copy;
	}

	void Awake() {
		// setupLeftSide = Random.value >= 0.5;

		GameObject[] PlacedMarkers = ShuffleArray (Markers);

		/*
		for (int i = LeftZone.Length - 1; i > 0; i--) {
			VRTK_InteractableObject marker = PlacedMarkers [i];
			VRTK_SnapDropZone zone = LeftZone [i];
			GameObject markerGo = Instantiate (marker.gameObject, zone.transform) as GameObject;
			markerGo.tag = "Keys";
			zone.defaultSnappedObject = markerGo;
		}
		*/
			
		for (int i = 0; i < PlacedMarkers.Length; i++) {
			GameObject marker = PlacedMarkers [i];
			GameObject zone = RightZone [i];
			GameObject markerGo = Instantiate (marker) as GameObject;
			markerGo.tag = "Snapped";
			zone.GetComponentInChildren<VRTK_SnapDropZone>().defaultSnappedObject = markerGo;

			GameObject TargetZone = LeftZone [i];
			VRTK_SnapDropZone TargetDropZone = TargetZone.GetComponentInChildren<VRTK_SnapDropZone> ();
			int currentIndex = i;
			TargetDropZone.ObjectSnappedToDropZone += (object snappedObject, SnapDropZoneEventArgs evt) => {
				bool isCorrectMarker = markerGo.name == TargetDropZone.GetCurrentSnappedObject().name + "(Clone)";

				Debug.Log("snapped on " + currentIndex + " - " + isCorrectMarker + " - " + markerGo.name + " - " + TargetDropZone.GetCurrentSnappedObject().name);
				UpdateCorrects(currentIndex, isCorrectMarker);
			};
		}
	}

	void UpdateCorrects(int index, bool value){
		if (hasClearedLevel)
			return;


		HasCorrect [index] = value;

		bool allCorrect = true;

		for (int marker = 0; marker < HasCorrect.Length; marker++) {
			if (!HasCorrect [marker]) {
				allCorrect = false;
				break;
			}
		}

		if (allCorrect) {
			Success ();
		}
	}

	void Success(){
		hasClearedLevel = true;
		exit.OpenDoor ();
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
