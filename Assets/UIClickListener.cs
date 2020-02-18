using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public delegate void OnMouseIn();
public delegate void OnMouseOut();
public delegate void OnMouseClick();

public class UIClickListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
  public event OnMouseIn MouseEnter;
  public event OnMouseOut MouseExit;
  public event OnMouseClick MouseClick;

  void OnMouseDown() {
    MouseClick?.Invoke();
	}

  public void OnPointerEnter(PointerEventData eventData) {
    MouseEnter?.Invoke();
  }

  public void OnPointerExit(PointerEventData eventData) {
    MouseExit?.Invoke();
  }
}
