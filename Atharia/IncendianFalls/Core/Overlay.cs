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
  public string overlayText {
    get { return incarnation.overlayText; }
  }
  public Color overlayTextColor {
    get { return incarnation.overlayTextColor; }
  }
  public bool topAligned {
    get { return incarnation.topAligned; }
  }
  public bool leftAligned {
    get { return incarnation.leftAligned; }
  }
  public int fadeInMs {
    get { return incarnation.fadeInMs; }
  }
  public int fadeOutMs {
    get { return incarnation.fadeOutMs; }
  }
  public ButtonImmList buttons {
    get { return incarnation.buttons; }
  }
  public int automaticDismissDelayMs {
    get { return incarnation.automaticDismissDelayMs; }
  }
  public string automaticDismissTriggerName {
    get { return incarnation.automaticDismissTriggerName; }
  }
}
}
