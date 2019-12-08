using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominoClickForwarder : MonoBehaviour, IButts {

  public List<IButts> observers = new List<IButts>();

  public void OnMouseClick() {
    foreach (var observer in observers) {
      observer.OnMouseClick();
    }
  }

  public void OnMouseIn() {
    foreach (var observer in observers) {
      observer.OnMouseIn();
    }
  }

  public void OnMouseOut() {
    foreach (var observer in observers) {
      observer.OnMouseOut();
    }
  }

  // Start is called before the first frame update
  void Start() {
    var components = GetComponentsInChildren<ClickListener>();
    foreach (var component in components) {
      component.observers.Add(this);
    }
  }

  // Update is called once per frame
  void Update() {

  }
}
