using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class SlowRod {
  public readonly Root root;
  public readonly int id;
  public SlowRod(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public SlowRodIncarnation incarnation { get { return root.GetSlowRodIncarnation(id); } }
  public void AddObserver(ISlowRodEffectObserver observer) {
    root.AddSlowRodObserver(id, observer);
  }
  public void RemoveObserver(ISlowRodEffectObserver observer) {
    root.RemoveSlowRodObserver(id, observer);
  }
  public void Delete() {
    root.EffectSlowRodDelete(id);
  }
  public static SlowRod Null = new SlowRod(null, 0);
  public bool Exists() { return root != null && root.SlowRodExists(id); }
  public bool NullableIs(SlowRod that) {
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
  public bool Is(SlowRod that) {
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
