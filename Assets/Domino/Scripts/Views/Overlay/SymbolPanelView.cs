﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Domino {
  public class OverlayFont {
    public readonly string font;
    public readonly float fontSizeMultiplier;

    public OverlayFont(
        string font,
        float fontSizeMultiplier) {
      this.font = font;
      this.fontSizeMultiplier = fontSizeMultiplier;
    }
  }

  public class NewOverlayPanelView : MonoBehaviour {
    private class OverlayObject {
      public readonly int id;
      public readonly GameObject gameObject;
      public readonly HashSet<int> childIds;
      public FadeIn fadeIn; // Null if no fade in
      public FadeOut fadeOut; // Null if no fade out
      public Color color;
      public Color buttonPressedColor; // Or null if not button

      public OverlayObject(int id, GameObject gameObject) {
        this.id = id;
        this.gameObject = gameObject;
        this.childIds = new HashSet<int>();
      }
    }

    public class FadeIn {
      public readonly long fadeInStartTimeMs;
      public readonly long fadeInEndTimeMs;

      public FadeIn(long fadeInStartTimeMs, long fadeInEndTimeMs) {
        this.fadeInStartTimeMs = fadeInStartTimeMs;
        this.fadeInEndTimeMs = fadeInEndTimeMs;

        Asserts.Assert(fadeInStartTimeMs >= 0);
        Asserts.Assert(fadeInEndTimeMs >= 0);
      }
    }
    public class FadeOut {
      public readonly long fadeOutStartTimeMs;
      public readonly long fadeOutEndTimeMs;
      public FadeOut(
          long fadeOutStartTimeMs,
          long fadeOutEndTimeMs) {
        this.fadeOutStartTimeMs = fadeOutStartTimeMs;
        this.fadeOutEndTimeMs = fadeOutEndTimeMs;

        // These times are relative to when the overlay is destroyed.
        Asserts.Assert(fadeOutStartTimeMs <= 0);
        Asserts.Assert(fadeOutEndTimeMs <= 0);
      }
    }

    public delegate void OnClicked();

    Instantiator instantiator;
    IClock cinematicTimer;

    private long openTimeMs;
    private long closeTimeMs;
    private OnClicked closeCallback;

    //long fadeInEndMs;
    //long fadeOutStartMs;
    //long fadeOutEndMs;

    //long textFadeInStartMs;
    //long textFadeInEndMs;
    //long textFadeOutStartMs;
    //long textFadeOutEndMs;

    //int horizontalAlignmentPercent;
    //int verticalAlignmentPercent;
    //int widthPercent;
    //int heightPercent;
    int symbolsWide_;
    int symbolsHigh_;

    public int symbolsWide { get { return symbolsWide_; } }
    public int symbolsHigh { get { return symbolsHigh_; } }

    float symbolWidth;
    float symbolHeight;

    int nextId = 1000;
    private Dictionary<int, OverlayObject> overlayObjects;
    private HashSet<int> fadingObjectIds;
    private Dictionary<int, int> parentIdByChildId;

    public void Init(
        Instantiator instantiator,
        IClock cinematicTimer,
        GameObject parent,
        int horizontalAlignmentPercent,
        int verticalAlignmentPercent,
        int widthPercent,
        int heightPercent,
        int symbolsWide,
        int symbolsHigh) {
      this.instantiator = instantiator;
      //this.horizontalAlignmentPercent = horizontalAlignmentPercent;
      //this.verticalAlignmentPercent = verticalAlignmentPercent;
      //this.widthPercent = widthPercent;
      //this.heightPercent = heightPercent;
      this.symbolsWide_ = symbolsWide;
      this.symbolsHigh_ = symbolsHigh;
      parentIdByChildId = new Dictionary<int, int>();
      overlayObjects = new Dictionary<int, OverlayObject>();
      overlayObjects.Add(0, new OverlayObject(0, gameObject));
      fadingObjectIds = new HashSet<int>();

      this.cinematicTimer = cinematicTimer;
      this.openTimeMs = cinematicTimer.GetTimeMs();
      this.closeTimeMs = -1;

      //this.fadeInEndMs = fadeInEndMs;
      //this.fadeOutStartMs = fadeOutStartMs;
      //this.fadeOutEndMs = fadeOutEndMs;

      //this.textFadeInStartMs = textFadeInStartMs;
      //this.textFadeInEndMs = textFadeInEndMs;
      //this.textFadeOutStartMs = textFadeOutStartS;
      //this.textFadeOutEndMs = textFadeOutEndS;

      float horizontalAlignment = horizontalAlignmentPercent / 100.0f;
      float verticalAlignment = verticalAlignmentPercent / 100.0f;

      var parentRectTransform = parent.GetComponent<RectTransform>();
      var parentWidth = parentRectTransform.rect.width;
      var parentHeight = parentRectTransform.rect.height;

      float panelWidth = parentWidth * widthPercent / 100;
      float panelHeight = parentHeight * heightPercent / 100;

      symbolWidth = panelWidth / symbolsWide;
      symbolHeight = panelHeight / symbolsHigh;
      // Enforce squareness.
      symbolWidth = Mathf.Min(symbolWidth, symbolHeight);
      symbolHeight = symbolWidth;

      panelWidth = symbolWidth * symbolsWide;
      panelHeight = symbolHeight * symbolsHigh;

      float panelX = horizontalAlignment * parentWidth - horizontalAlignment * panelWidth;
      float panelY = verticalAlignment * parentHeight - verticalAlignment * panelHeight;

      var panelRectTransform = gameObject.GetComponent<RectTransform>();
      panelRectTransform.parent = parent.transform;
      panelRectTransform.anchorMin = new Vector2(0, 0);
      panelRectTransform.anchorMax = new Vector2(0, 0);
      panelRectTransform.localScale = new Vector3(1, 1, 1);
      panelRectTransform.pivot = new Vector2(0, 0);
      panelRectTransform.anchoredPosition = new Vector2(panelX, panelY);
      panelRectTransform.sizeDelta = new Vector2(panelWidth, panelHeight);
    }

    public void SetFadeIn(int id, FadeIn fadeIn) {
      Asserts.Assert(overlayObjects.ContainsKey(id));
      var overlayObject = overlayObjects[id];
      overlayObject.fadeIn = fadeIn;
      fadingObjectIds.Add(id);
      SetOpacity(overlayObject, 0);
    }

    public void SetFadeOut(int id, FadeOut fadeOut) {
      Asserts.Assert(overlayObjects.ContainsKey(id));
      var overlayObject = overlayObjects[id];
      overlayObject.fadeOut = fadeOut;
      fadingObjectIds.Add(id);
      UpdateOpacity(overlayObject);
    }

    public int AddSymbol(
        int parentId,
        float x,
        float y,
        float size,
        int z,
        Color color,
        OverlayFont font,
        string symbol) {
      var unityX = x * symbolWidth + symbolWidth / 2;
      var unityY = y * symbolHeight + symbolHeight / 2;

      var textGameObject = instantiator.CreateEmptyUiObject();
      textGameObject.transform.parent = gameObject.transform;
      var rectTransform = textGameObject.GetComponent<RectTransform>();
      rectTransform.anchorMin = new Vector2(0, 0);
      rectTransform.anchorMax = new Vector2(0, 0);
      rectTransform.anchoredPosition = new Vector2(unityX, unityY);
      var textView = textGameObject.AddComponent<Text>();
      textView.raycastTarget = false;
      textView.font = instantiator.GetFont(font.font);
      textView.alignment = TextAnchor.MiddleCenter;
      textView.fontSize = (int)(symbolHeight * size * font.fontSizeMultiplier);
      textView.color = color;
      textView.resizeTextForBestFit = false;
      //textView.
      rectTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y, z);

      textView.text = symbol;

      int id = nextId++;
      var overlayObject = new OverlayObject(id, textGameObject);
      overlayObject.color = color;
      overlayObjects.Add(id, overlayObject);
      parentIdByChildId.Add(id, parentId);
      overlayObjects[parentId].childIds.Add(id);

      return id;
    }

    public int AddRectangle(
        int parentId,
        float x,
        float y,
        float width,
        float height,
        int z,
        Color color) {
      var unityX = x * symbolWidth;
      var unityY = y * symbolHeight;
      var unityWidth = width * symbolWidth;
      var unityHeight = height * symbolHeight;
      Debug.LogError("width " + height + " symwid " + symbolHeight + " unitywid " + unityHeight);

      var rectGameObject = instantiator.CreateEmptyUiObject();
      rectGameObject.transform.parent = gameObject.transform;
      var rectTransform = rectGameObject.GetComponent<RectTransform>();
      rectTransform.pivot = new Vector2(0, 0);
      rectTransform.localScale = new Vector3(1, 1, 1);
      rectTransform.anchorMin = new Vector2(0, 0);
      rectTransform.anchorMax = new Vector2(0, 0);
      rectTransform.anchoredPosition = new Vector2(unityX, unityY);
      rectTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y, z);
      rectTransform.sizeDelta = new Vector2(unityWidth, unityHeight);
      var image = rectGameObject.AddComponent<Image>();
      image.color = color;

      int id = nextId++;
      var overlayObject = new OverlayObject(id, rectGameObject);
      overlayObject.color = color;
      overlayObjects.Add(id, overlayObject);
      parentIdByChildId.Add(id, parentId);
      overlayObjects[parentId].childIds.Add(id);

      return id;
    }

    public int AddButton(
        int parentId,
        float x,
        float y,
        float width,
        float height,
        int z,
        Color color,
        Color pressedColor,
        OnClicked onClicked) {
      var rectangleId = AddRectangle(parentId, x, y, width, height, z, color);
      var overlayObject = overlayObjects[rectangleId];

      var button = overlayObject.gameObject.AddComponent<Button>();
      var colors = new ColorBlock();
      colors.colorMultiplier = 1;
      colors.normalColor = color;
      colors.pressedColor = pressedColor;
      colors.highlightedColor = color;
      colors.selectedColor = color;
      colors.disabledColor = color;
      button.colors = colors;
      button.onClick.AddListener(() => onClicked());

      overlayObject.buttonPressedColor = pressedColor;

      return rectangleId;
    }

    public int AddBackground(Color color) {
      return AddRectangle(0, 0, 0, symbolsWide, symbolsHigh, 1, color);
    }

    public int AddBackgroundAndBorder(Color backgroundColor, Color borderColor) {
      int borderId = AddBackground(borderColor);
      AddRectangle(borderId, .5f, .5f, symbolsWide - 1, symbolsHigh - 1, 1, backgroundColor);
      return borderId;
    }

    public List<int> AddString(int parentId, float x, float y, int maxWide, Color color, OverlayFont font, string str) {
      var ids = new List<int>();
      for (int i = 0; i < str.Length; i++) {
        int id = AddSymbol(parentId, x + i, y, 1f, 1, color, font, str[i].ToString());
        ids.Add(id);
      }
      return ids;
    }

    public void Update() {
      foreach (var id in new HashSet<int>(fadingObjectIds))
        UpdateOpacity(overlayObjects[id]);

      var timeSinceOpenMs = cinematicTimer.GetTimeMs() - openTimeMs;
      if (closeTimeMs >= 0 && timeSinceOpenMs >= closeTimeMs) {
        closeTimeMs = 0;
        closeCallback();
        closeCallback = null;
        Destroy(gameObject);
      }
    }

    private void UpdateOpacity(OverlayObject overlayObject) {
      var timeSinceOpenMs = cinematicTimer.GetTimeMs() - openTimeMs;

      if (overlayObject.fadeIn != null) {
        var fadeIn = overlayObject.fadeIn;
        if (timeSinceOpenMs < fadeIn.fadeInStartTimeMs) {
          // Do nothing, they should already be transparent, from SetFadeIn.
        } else if (timeSinceOpenMs < fadeIn.fadeInEndTimeMs) {
          var ratio = (float)(timeSinceOpenMs - fadeIn.fadeInStartTimeMs) / (fadeIn.fadeInEndTimeMs - fadeIn.fadeInStartTimeMs);
          SetOpacity(overlayObject, ratio);
        } else {
          // Not expensive because we remove it afterwards and never see it again.
          SetOpacity(overlayObject, 1);
          overlayObject.fadeIn = null;
        }
      }

      if (closeTimeMs >= 0) {
        if (overlayObject.fadeOut != null) {
          var fadeOut = overlayObject.fadeOut;
          // Remember, fadeOut.fadeOutStart/EndTimeS are negative.
          var fadeOutStartTimeMs = closeTimeMs + fadeOut.fadeOutStartTimeMs;
          var fadeOutEndTimeMs = closeTimeMs + fadeOut.fadeOutEndTimeMs;

          if (timeSinceOpenMs < fadeOutStartTimeMs) {
            // Do nothing, they should already be opaque.
          } else if (timeSinceOpenMs < fadeOutEndTimeMs) {
            var ratio = 1 - (float)(timeSinceOpenMs - fadeOutStartTimeMs) / (fadeOutEndTimeMs - fadeOutStartTimeMs);
            SetOpacity(overlayObject, ratio);
          } else {
            // Not expensive because we remove it afterwards and never see it again.
            SetOpacity(overlayObject, 0);
            overlayObject.fadeOut = null;
          }
        }
      }

      if (overlayObject.fadeIn == null && overlayObject.fadeOut == null) {
        fadingObjectIds.Remove(overlayObject.id);
      }
    }

    private void SetOpacity(OverlayObject overlayObject, float ratio) {
      var overlayObjectGameObject = overlayObject.gameObject;

      var text = overlayObjectGameObject.GetComponent<Text>();
      if (text != null) {
        var fadedTextColor = overlayObject.color;
        fadedTextColor.a *= ratio;
        text.color = fadedTextColor;
      }

      var image = overlayObjectGameObject.GetComponent<Image>();
      if (image != null) {
        var fadedBackgroundColor = overlayObject.color;
        fadedBackgroundColor.a *= ratio;
        image.color = fadedBackgroundColor;
      }

      var button = overlayObjectGameObject.GetComponent<UnityEngine.UI.Button>();
      if (button != null) {
        var color = overlayObject.color;
        color.a = ratio;

        var buttonColors = button.colors;
        buttonColors.normalColor = color;
        buttonColors.disabledColor = color;
        buttonColors.highlightedColor = color;
        button.colors = buttonColors;
      }

      foreach (var childId in overlayObject.childIds) {
        SetOpacity(overlayObjects[childId], ratio);
      }
    }

    public void Remove(int id) {
      Asserts.Assert(id != 0);
      Asserts.Assert(parentIdByChildId.ContainsKey(id));
      Asserts.Assert(overlayObjects.ContainsKey(id));

      foreach (var childId in overlayObjects[id].childIds) {
        Remove(childId);
      }

      Asserts.Assert(overlayObjects[id].childIds.Count == 0);

      Destroy(overlayObjects[id].gameObject);
      overlayObjects.Remove(id);

      int parentId = parentIdByChildId[id];
      parentIdByChildId.Remove(id);

      overlayObjects[parentId].childIds.Remove(id);
    }

    //public void OnButtonClick(int id) {
    //  Clicked?.Invoke(id);
    //}

    //public void Cancel() {
    //  gameObject.SetActive(false);
    //}

    public void ScheduleClose(OnClicked onClose) {
      long earliestFadeOutBeginMs = 0;
      foreach (var fadingObjectId in fadingObjectIds) {
        var overlayObject = overlayObjects[fadingObjectId];
        if (overlayObject.fadeOut != null) {
          earliestFadeOutBeginMs = Math.Min(earliestFadeOutBeginMs, overlayObject.fadeOut.fadeOutStartTimeMs);
        }
      }
      Asserts.Assert(earliestFadeOutBeginMs <= 0);
      long delayUntilCloseMs = -earliestFadeOutBeginMs;
      closeTimeMs = cinematicTimer.GetTimeMs() + delayUntilCloseMs;
      closeCallback = onClose;
    }
  }
}
