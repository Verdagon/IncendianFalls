using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class DownStaircaseTTC {
  public readonly Root root;
  public readonly int id;
  public DownStaircaseTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DownStaircaseTTCIncarnation incarnation { get { return root.GetDownStaircaseTTCIncarnation(id); } }
  public void AddObserver(IDownStaircaseTTCEffectObserver observer) {
    root.AddDownStaircaseTTCObserver(id, observer);
  }
  public void RemoveObserver(IDownStaircaseTTCEffectObserver observer) {
    root.RemoveDownStaircaseTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectDownStaircaseTTCDelete(id);
  }
  public static DownStaircaseTTC Null = new DownStaircaseTTC(null, 0);
  public bool Exists() { return root != null && root.DownStaircaseTTCExists(id); }
  public bool NullableIs(DownStaircaseTTC that) {
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
  public bool Is(DownStaircaseTTC that) {
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
