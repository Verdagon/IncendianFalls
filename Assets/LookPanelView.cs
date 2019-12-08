using System.Collections;
using System.Collections.Generic;
using Atharia.Model;
using Domino;
using UnityEngine;
using UnityEngine.UI;

namespace Domino {
  public class LookPanelView : MonoBehaviour {
    private GameObject panelObject {  get { return gameObject;  } }

    public GameObject lookTextObject;
    public GameObject lookSideTextObject;
    public GameObject[] symbolTextObjects;
    public GameObject[] labelTextObjects;

    public void ShowMessage(string message) {
      SetStuff(true, message, "", new List<KeyValuePair<SymbolDescription, string>>());
    }
    public void ClearMessage() {
      SetStuff(false, "", "", new List<KeyValuePair<SymbolDescription, string>>());
    }
    public void SetStuff(
        bool visible,
        string message,
        string status,
        List<KeyValuePair<SymbolDescription, string>> symbolsAndLabels) {

      panelObject.SetActive(visible);
      lookTextObject.GetComponent<Text>().text = message;
      lookSideTextObject.GetComponent<Text>().text = status;

      if (symbolsAndLabels.Count > symbolTextObjects.Length) {
        Debug.LogError("Too many symbols to show!");
      }
      //Vector3 currentPosition = symbolTextObjects[0].GetComponent<RectTransform>().localPosition; ;
      float currentX = 5;
      for (int i = 0; i < symbolTextObjects.Length; i++) {
        var symbolTextObject = symbolTextObjects[i];
        var descriptionTextObject = labelTextObjects[i];

        if (i < symbolsAndLabels.Count) {
          var symbolAndLabel = symbolsAndLabels[i];
          var symbolDescription = symbolAndLabel.Key;
          var label = symbolAndLabel.Value;
          descriptionTextObject.SetActive(true);
          symbolTextObject.SetActive(true);

          var oldSymbolPos = symbolTextObject.GetComponent<RectTransform>().localPosition;
          symbolTextObject.GetComponent<RectTransform>().localPosition =
              new Vector3(currentX, oldSymbolPos.y, oldSymbolPos.z);
          symbolTextObject.GetComponent<Text>().text = symbolDescription.symbolId;
          currentX += symbolTextObject.GetComponent<Text>().preferredWidth + 5;

          var oldDescriptionPos = descriptionTextObject.GetComponent<RectTransform>().localPosition;
          descriptionTextObject.GetComponent<RectTransform>().localPosition =
              new Vector3(currentX, oldDescriptionPos.y, oldDescriptionPos.z);
          descriptionTextObject.GetComponent<Text>().text = label;
          currentX += descriptionTextObject.GetComponent<Text>().preferredWidth + 15;
        } else {
          descriptionTextObject.SetActive(false);
          symbolTextObject.SetActive(false);
        }
      }
    }
  }
}
