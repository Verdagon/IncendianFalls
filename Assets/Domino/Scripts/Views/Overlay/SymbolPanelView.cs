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
    public delegate void OnClicked(int id);

    public event OnClicked Clicked;

    Instantiator instantiator;
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
    private Dictionary<int, GameObject> gameObjects;
    private Dictionary<int, int> parentIdByChildId;
    private Dictionary<int, HashSet<int>> childsIdByParentId;

    public void Init(
        Instantiator instantiator,
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
      childsIdByParentId = new Dictionary<int, HashSet<int>>();
      childsIdByParentId.Add(0, new HashSet<int>());

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

      // Debug.LogError("parentWidth " + parentWidth + " parentHeight " + parentHeight + " panelX " + panelX + " panelY " + panelY + " panelWidth " + panelWidth + " panelHeight " + panelHeight + " symbolWidth " + symbolWidth + " symbolHeight " + symbolHeight);

      gameObjects = new Dictionary<int, GameObject>();
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
      gameObjects.Add(id, textGameObject);

      parentIdByChildId.Add(id, parentId);
      childsIdByParentId.Add(id, new HashSet<int>());
      childsIdByParentId[parentId].Add(id);
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
      gameObjects.Add(id, rectGameObject);

      parentIdByChildId.Add(id, parentId);
      childsIdByParentId.Add(id, new HashSet<int>());
      childsIdByParentId[parentId].Add(id);

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
        Color pressedColor) {
      var rectangleId = AddRectangle(parentId, x, y, width, height, z, color);
      var gameObject = gameObjects[rectangleId];

      var button = gameObject.AddComponent<Button>();
      var colors = new ColorBlock();
      colors.colorMultiplier = 1;
      colors.normalColor = color;
      colors.pressedColor = pressedColor;
      colors.highlightedColor = color;
      colors.selectedColor = color;
      colors.disabledColor = color;
      button.colors = colors;
      button.onClick.AddListener(() => OnButtonClick(rectangleId));

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

    public void Remove(int id) {
      Asserts.Assert(id != 0);
      Asserts.Assert(parentIdByChildId.ContainsKey(id));
      Asserts.Assert(childsIdByParentId.ContainsKey(id));

      foreach (var childId in childsIdByParentId[id]) {
        Remove(childId);
      }

      Asserts.Assert(childsIdByParentId[id].Count == 0);

      Destroy(gameObjects[id]);
      gameObjects.Remove(id);

      int parentId = parentIdByChildId[id];
      parentIdByChildId.Remove(id);
      childsIdByParentId.Remove(id);

      childsIdByParentId[parentId].Remove(id);
    }

    public void OnButtonClick(int id) {
      Clicked?.Invoke(id);
    }
  }
}
