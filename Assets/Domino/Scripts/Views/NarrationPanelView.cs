using System;
using UnityEngine;
using UnityEngine.UI;

namespace Domino {
  public class NarrationPanelView : MonoBehaviour {
    public Text messageText;

    private GameObject panelObject { get { return gameObject; } }

    public void ShowMessage(string message) {
      panelObject.SetActive(true);

      messageText.text = message;
      float height = messageText.preferredHeight;

      var panelTransform = panelObject.GetComponent<RectTransform>();
      panelTransform.sizeDelta = new Vector2(panelTransform.sizeDelta.x, height + 22);
    }

    public void ClearMessage() {
      panelObject.SetActive(false);
    }
  }
}
