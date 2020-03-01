using System.Collections;
using System.Collections.Generic;
using Atharia.Model;
using Domino;
using AthPlayer;
using UnityEngine;
using UnityEngine.UI;

public class OverlayPanelView : MonoBehaviour {
  public GameObject parent;
  public Text overlayText;
  public UnityEngine.UI.Button[] buttons;

  public void SetStuff(
      float ratio,
      float size,
      UnityEngine.Color backgroundColor,
      string text,
      UnityEngine.Color textColor,
      bool topAligned,
      bool leftAligned) {
    backgroundColor.a *= ratio;
    GetComponent<Image>().color = backgroundColor;
    textColor.a *= ratio;
    overlayText.text = text;
    overlayText.color = textColor;

    overlayText.GetComponent<RectTransform>().localPosition =
      new Vector3(
        parent.GetComponent<RectTransform>().rect.width * (1 - size) / 2,
        - parent.GetComponent<RectTransform>().rect.height * (1 - size) / 2,
        0);
    overlayText.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
      RectTransform.Axis.Vertical,
      parent.GetComponent<RectTransform>().rect.height * size);
    overlayText.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
      RectTransform.Axis.Horizontal,
      parent.GetComponent<RectTransform>().rect.width * size);

    if (topAligned) {
      if (leftAligned) {
        overlayText.alignment = TextAnchor.UpperLeft;
      } else {
        overlayText.alignment = TextAnchor.UpperRight;
      }
    } else {
      if (leftAligned) {
        overlayText.alignment = TextAnchor.LowerLeft;
      } else {
        overlayText.alignment = TextAnchor.LowerRight;
      }
    }
  }

  public void Start() {
    for (int i = 0; i < buttons.Length; i++) {
      //buttons[i].GetComponent<UIClickListener>().MouseEnter +=
      //    () => lookPanelView.ShowMessage(
      //        "(A) Time Anchor: Place a time anchor, which you can later Rewind to.");
      //buttons[i].GetComponent<UIClickListener>().MouseExit +=
      //    () => lookPanelView.ClearMessage();
    }
  }

  //public void Clear() {
  //  playerStatusText.SetActive(false);
  //}

  //public void ShowPlayerStatus(Level level, Unit unit) {
  //  playerStatusText.SetActive(true);
  //  playerStatusText.GetComponent<Text>().text =
  //      level.GetName() + "   " +
  //      "HP " + unit.hp + "/" + unit.maxHp + "   " +
  //      "MP " + unit.mp + "/" + unit.maxMp;
  //  var size = playerStatusText.GetComponent<RectTransform>().sizeDelta;
  //  size.x = playerStatusText.GetComponent<Text>().preferredWidth;
  //  playerStatusText.GetComponent<RectTransform>().sizeDelta = size; 
  //}
}
