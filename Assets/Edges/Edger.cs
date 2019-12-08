using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edger : MonoBehaviour {
  public float outlineRadius;
  public GameObject outlineObjectPrefab;
  private GameObject outlineObject;

  // Start is called before the first frame update
  void Start() {
    Mesh newMesh =
        new Edgifier().makeEdgeMesh(
            GetComponent<MeshFilter>().mesh, .001f, 30);
    outlineObject =
        Instantiate<GameObject>(outlineObjectPrefab, transform);
    outlineObject.GetComponent<MeshFilter>().mesh = newMesh;
    outlineObject.SetActive(true);
  }
}
