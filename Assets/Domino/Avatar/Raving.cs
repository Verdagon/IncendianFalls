using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raving : MonoBehaviour {

	private bool isRaving = false;
	public Material regularMaterial;
	public Material psychedelicMaterial;

	public void ToggleRaving() {
		if (isRaving) {
			GetComponent<MeshRenderer>().material = regularMaterial;
		} else {
			GetComponent<MeshRenderer>().material = psychedelicMaterial;
		}
		isRaving = !isRaving;
	}
}
