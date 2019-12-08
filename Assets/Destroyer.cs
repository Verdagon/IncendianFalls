using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
  public void Die() {
    Debug.Log("Dying!");
    Destroy(this.gameObject);
  }
}
