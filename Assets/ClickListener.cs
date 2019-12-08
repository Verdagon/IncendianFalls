using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IButts {
  void OnMouseIn();
  void OnMouseOut();
  void OnMouseClick();
}

public delegate void OnMouseIn();
public delegate void OnMouseOut();
public delegate void OnMouseClick();

public class ClickListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
  public List<IButts> observers = new List<IButts>();
  public event OnMouseIn MouseIn;
  public event OnMouseOut MouseOut;
  public event OnMouseClick MouseClick;

  void OnMouseDown() {
    if (!EventSystem.current.IsPointerOverGameObject()) {
      foreach (var observer in observers) {
        observer.OnMouseClick();
      }
      MouseClick?.Invoke();
    }
	}

  void OnMouseOver() {
    if (!EventSystem.current.IsPointerOverGameObject()) {
      foreach (var observer in observers) {
        observer.OnMouseIn();
      }
      MouseIn?.Invoke();
    }
  }

  void OnMouseExit() {
    if (!EventSystem.current.IsPointerOverGameObject()) {
      foreach (var observer in observers) {
        observer.OnMouseOut();
      }
      MouseOut?.Invoke();
    }
  }

  public void OnPointerEnter(PointerEventData eventData) {
    foreach (var observer in observers) {
      observer.OnMouseIn();
    }
    MouseIn?.Invoke();
  }

  public void OnPointerExit(PointerEventData eventData) {
    foreach (var observer in observers) {
      observer.OnMouseOut();
    }
    MouseOut?.Invoke();
  }
}
