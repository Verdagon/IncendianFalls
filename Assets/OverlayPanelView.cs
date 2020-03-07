using System.Collections;
using System.Collections.Generic;
using Atharia.Model;
using Domino;
using AthPlayer;
using UnityEngine;
using UnityEngine.UI;

public delegate void OnOverlayClosed(int buttonIndex);

public class OverlayPanelView : MonoBehaviour {
  public event OnOverlayClosed OverlayClosed;

  public GameObject parent;
  public Text overlayText;
  public GameObject[] buttonGameObjects;

  IClock cinematicTimer;

  private long openTimeMs;

  private UnityEngine.Color backgroundColor;

  long fadeInEndMs;
  long fadeOutStartMs;
  long fadeOutEndMs;

  private UnityEngine.Color textColor;
  long textFadeInStartMs;
  long textFadeInEndMs;
  long textFadeOutStartMs;
  long textFadeOutEndMs;

  public void Init(
      IClock cinematicTimer,

      float sizeRatio,
      UnityEngine.Color backgroundColor,
      long fadeInEndMs,
      long fadeOutStartMs,
      long fadeOutEndMs,

      string text,
      UnityEngine.Color textColor,
      long textFadeInStartMs,
      long textFadeInEndMs,
      long textFadeOutStartS,
      long textFadeOutEndS,
      bool topAligned,
      bool leftAligned,

      List<string> buttonTexts) {
    this.cinematicTimer = cinematicTimer;
    this.openTimeMs = cinematicTimer.GetTimeMs();

    this.backgroundColor = backgroundColor;
    this.fadeInEndMs = fadeInEndMs;
    this.fadeOutStartMs = fadeOutStartMs;
    this.fadeOutEndMs = fadeOutEndMs;

    this.textColor = textColor;
    this.textFadeInStartMs = textFadeInStartMs;
    this.textFadeInEndMs = textFadeInEndMs;
    this.textFadeOutStartMs = textFadeOutStartS;
    this.textFadeOutEndMs = textFadeOutEndS;

    if (fadeInEndMs == 0) {
      SetChromeFadeRatio(1);
    } else {
      SetChromeFadeRatio(0);
    }

    if (textFadeInEndMs == 0) {
      SetTextFadeRatio(1);
    } else {
      SetTextFadeRatio(0);
    }

    overlayText.text = text;

    SetSize(sizeRatio);
    SetAlignment(topAligned, leftAligned);

    for (int i = 0; i < buttonGameObjects.Length; i++) {
      var buttonGameObject = buttonGameObjects[i];
      var button = buttonGameObject.GetComponent<UnityEngine.UI.Button>();
      if (i < buttonTexts.Count) {
        buttonGameObject.GetComponentInChildren<Text>().text = buttonTexts[buttonTexts.Count - 1 - i];
        buttonGameObject.SetActive(true);
      } else {
        buttonGameObject.SetActive(false);
      }
    }

    gameObject.SetActive(true);
  }

  public void Cancel() {
    gameObject.SetActive(false);
  }

  private void SetSize(float sizeRatio) {
    Debug.Log(parent.GetComponent<RectTransform>().rect.width);
    GetComponent<RectTransform>().localPosition =
      new Vector3(
        -parent.GetComponent<RectTransform>().rect.width * sizeRatio / 2,
        parent.GetComponent<RectTransform>().rect.height * sizeRatio / 2,
        0);
    GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
      RectTransform.Axis.Vertical,
      parent.GetComponent<RectTransform>().rect.height * sizeRatio);
    GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
      RectTransform.Axis.Horizontal,
      parent.GetComponent<RectTransform>().rect.width * sizeRatio);

  }

  private void SetAlignment(bool topAligned, bool leftAligned) {
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

  public void Update() {

    var timeSinceOpenMs = cinematicTimer.GetTimeMs() - openTimeMs;

    if (timeSinceOpenMs < fadeInEndMs) {
      var ratio = (float)timeSinceOpenMs / fadeInEndMs;
      SetChromeFadeRatio(ratio);
    } else {
      if (fadeOutEndMs == 0) {
        SetChromeFadeRatio(1f);
      } else {
        if (timeSinceOpenMs < fadeOutStartMs) {
          SetChromeFadeRatio(1f);
        } else if (timeSinceOpenMs < fadeOutEndMs) {
          var ratio = 1 - (float)(timeSinceOpenMs - fadeOutStartMs) / (fadeOutEndMs - fadeOutStartMs);
          SetChromeFadeRatio(ratio);
        }
      }
    }

    if (timeSinceOpenMs < textFadeInStartMs) {
      SetTextFadeRatio(0);
    } else if (timeSinceOpenMs < textFadeInEndMs) {
      var ratio = (float)(timeSinceOpenMs - textFadeInStartMs) / (textFadeInEndMs - textFadeInStartMs);
      SetTextFadeRatio(ratio);
    } else {
      if (textFadeOutEndMs == 0) {
        SetTextFadeRatio(1f);
      } else {
        if (timeSinceOpenMs < textFadeOutStartMs) {
          SetTextFadeRatio(1f);
        } else if (timeSinceOpenMs < textFadeOutEndMs) {
          var ratio = 1 - (float)(timeSinceOpenMs - textFadeOutStartMs) / (textFadeOutEndMs - textFadeOutStartMs);
          SetTextFadeRatio(ratio);
        } else {
          SetTextFadeRatio(0f);
        }
      }
    }

    if (fadeOutEndMs != 0 && timeSinceOpenMs >= fadeOutEndMs) {
      Close(0);
    }
  }

  private void SetChromeFadeRatio(float ratio) {
    var fadedBackgroundColor = backgroundColor;
    fadedBackgroundColor.a *= ratio;
    GetComponent<Image>().color = fadedBackgroundColor;

    foreach (var buttonGameObject in buttonGameObjects) {
      var text = buttonGameObject.GetComponentInChildren<Text>();
      var textColor = text.color;
      textColor.a = ratio;
      text.color = textColor;

      var image = buttonGameObject.GetComponent<Image>();
      var imageColor = image.color;
      imageColor.a = ratio;
      image.color = imageColor;

      var button = buttonGameObject.GetComponent<UnityEngine.UI.Button>();
      var buttonColors = button.colors;
      var normalColor = buttonColors.normalColor;
      normalColor.a = ratio;
      buttonColors.normalColor = normalColor;
      button.colors = buttonColors;
    }
  }

  private void SetTextFadeRatio(float ratio) {
    var fadedTextColor = textColor;
    fadedTextColor.a *= ratio;
    overlayText.color = fadedTextColor;
  }

  public void Clicked(int buttonIndex) {
    Close(buttonIndex);
  }

  private void Close(int buttonIndex) {
    SetChromeFadeRatio(0);
    gameObject.SetActive(false);
    OverlayClosed?.Invoke(buttonIndex);
  }
}
