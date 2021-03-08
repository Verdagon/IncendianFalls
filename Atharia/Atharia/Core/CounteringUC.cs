using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class CounteringUC {
  public readonly Root root;
  public readonly int id;
  public CounteringUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public CounteringUCIncarnation incarnation { get { return root.GetCounteringUCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, ICounteringUCEffectObserver observer) {
    broadcaster.AddCounteringUCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ICounteringUCEffectObserver observer) {
    broadcaster.RemoveCounteringUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectCounteringUCDelete(id);
  }
  public static CounteringUC Null = new CounteringUC(null, 0);
  public bool Exists() { return root != null && root.CounteringUCExists(id); }
  public bool NullableIs(CounteringUC that) {
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
  public bool Is(CounteringUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
       }
}
