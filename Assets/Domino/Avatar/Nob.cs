using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nob : MonoBehaviour {

	public bool isTargeting {
		get {
			return GetComponent<Animator>().GetBool("isTargeting");
		}
		set {
			GetComponent<Animator>().SetBool("isTargeting", value);
		}
	}

	public void hack() {
		GetComponent<Animator>().SetTrigger("Hack");
	}
}
