using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class DecorativeTTC {
  public readonly Root root;
  public readonly int id;
  public DecorativeTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DecorativeTTCIncarnation incarnation { get { return root.GetDecorativeTTCIncarnation(id); } }
  public void AddObserver(IDecorativeTTCEffectObserver observer) {
    root.AddDecorativeTTCObserver(id, observer);
  }
  public void RemoveObserver(IDecorativeTTCEffectObserver observer) {
    root.RemoveDecorativeTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectDecorativeTTCDelete(id);
  }
  public static DecorativeTTC Null = new DecorativeTTC(null, 0);
  public bool Exists() { return root != null && root.DecorativeTTCExists(id); }
  public bool NullableIs(DecorativeTTC that) {
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
  public bool Is(DecorativeTTC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public string symbolId {
    get { return incarnation.symbolId; }
  }
}
}
