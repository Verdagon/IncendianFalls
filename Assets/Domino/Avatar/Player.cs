using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private class Movement {
		public float startTime;
		public float endTime;
		public float a, b, c;
		public Vector3 start;
		public Vector3 end;
	}

	public GameObject nob;
	public GameObject capeCloth;
	private Movement movement;

	public void moveTo(int p, int q, int elevation) {
//		GetComponent<Rigidbody>().isKinematic = true;
//		GetComponent<Rigidbody>().detectCollisions = false;
//
		float gravity = -9.8f;
		float duration = 0.5f;

		Vector3 start = transform.position;
		Vector3 end = new Vector3(p * 1.5f, elevation * 0.5f, q * 1.5f);

		float a = -9.8f;
		float c = start.y;
		float b = end.y - start.y - gravity;

		movement = new Movement();
		movement.startTime = Time.time;
		movement.endTime = movement.startTime + duration;
		movement.a = a;
		movement.b = b;
		movement.c = c;
		movement.start = start;
		movement.end = end;

//		transform.position = destination;
//		capeCloth.GetComponent<Cloth>().ClearTransformMotion();
	}

	void FixedUpdate() {
		if (movement != null) {
			float t = (Time.time - movement.startTime) / (movement.endTime - movement.startTime);
			if (t < 1) {
				float x = Mathf.Lerp(movement.start.x, movement.end.x, t);
				float z = Mathf.Lerp(movement.start.z, movement.end.z, t);
				float y = movement.a * t * t + movement.b * t + movement.c;
				transform.position = new Vector3(x, y, z);
			} else {
				transform.position = movement.end;
				movement = null;
//				GetComponent<Rigidbody>().isKinematic = false;
//				GetComponent<Rigidbody>().detectCollisions = true;
			}
		}
	}

  void Update() {
    //    Vector3 toCameraTarget = cameraTarget.transform.position - transform.position;
    //    cameraTarget.transform.position -= toCameraTarget * cameraFollowSpeed;

    if (Input.GetKeyDown(KeyCode.C)) {
      GetComponentInChildren<Raving>().ToggleRaving();
    }
    if (Input.GetKeyDown(KeyCode.T)) {
      nob.GetComponent<Nob>().isTargeting = !nob.GetComponent<Nob>().isTargeting;
    }
    if (Input.GetKeyDown(KeyCode.H)) {
      nob.GetComponent<Nob>().hack();
    }
  }
}
