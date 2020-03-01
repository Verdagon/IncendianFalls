using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class Overlay {
  public readonly Root root;
  public readonly int id;
  public Overlay(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public OverlayIncarnation incarnation { get { return root.GetOverlayIncarnation(id); } }
  public void AddObserver(IOverlayEffectObserver observer) {
    root.AddOverlayObserver(id, observer);
  }
  public void RemoveObserver(IOverlayEffectObserver observer) {
    root.RemoveOverlayObserver(id, observer);
  }
  public void Delete() {
    root.EffectOverlayDelete(id);
  }
  public static Overlay Null = new Overlay(null, 0);
  public bool Exists() { return root != null && root.OverlayExists(id); }
  public bool NullableIs(Overlay that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
  }
  public bool Is(Overlay that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public int sizePercent {
    get { return incarnation.sizePercent; }
  }
  public Color backgroundColor {
    get { return incarnation.backgroundColor; }
  }
  public int fadeInEndMs {
    get { return incarnation.fadeInEndMs; }
  }
  public int fadeOutStartMs {
    get { return incarnation.fadeOutStartMs; }
  }
  public int fadeOutEndMs {
    get { return incarnation.fadeOutEndMs; }
  }
  public string automaticActionTriggerName {
    get { return incarnation.automaticActionTriggerName; }
  }
  public string text {
    get { return incarnation.text; }
  }
  public Color textColor {
    get { return incarnation.textColor; }
  }
  public int textFadeInStartMs {
    get { return incarnation.textFadeInStartMs; }
  }
  public int textFadeInEndMs {
    get { return incarnation.textFadeInEndMs; }
  }
  public int textFadeOutStartMs {
    get { return incarnation.textFadeOutStartMs; }
  }
  public int textFadeOutEndMs {
    get { return incarnation.textFadeOutEndMs; }
  }
  public bool topAligned {
    get { return incarnation.topAligned; }
  }
  public bool leftAligned {
    get { return incarnation.leftAligned; }
  }
  public ButtonImmList buttons {
    get { return incarnation.buttons; }
  }
}
}
