using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class BequeathUC {
  public readonly Root root;
  public readonly int id;
  public BequeathUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public BequeathUCIncarnation incarnation { get { return root.GetBequeathUCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IBequeathUCEffectObserver observer) {
    broadcaster.AddBequeathUCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IBequeathUCEffectObserver observer) {
    broadcaster.RemoveBequeathUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectBequeathUCDelete(id);
  }
  public static BequeathUC Null = new BequeathUC(null, 0);
  public bool Exists() { return root != null && root.BequeathUCExists(id); }
  public bool NullableIs(BequeathUC that) {
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
  public bool Is(BequeathUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public string blueprintName {
    get { return incarnation.blueprintName; }
  }
}
}
