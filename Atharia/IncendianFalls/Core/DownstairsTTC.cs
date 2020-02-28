using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class DownStairsTTC {
  public readonly Root root;
  public readonly int id;
  public DownStairsTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DownStairsTTCIncarnation incarnation { get { return root.GetDownStairsTTCIncarnation(id); } }
  public void AddObserver(IDownStairsTTCEffectObserver observer) {
    root.AddDownStairsTTCObserver(id, observer);
  }
  public void RemoveObserver(IDownStairsTTCEffectObserver observer) {
    root.RemoveDownStairsTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectDownStairsTTCDelete(id);
  }
  public static DownStairsTTC Null = new DownStairsTTC(null, 0);
  public bool Exists() { return root != null && root.DownStairsTTCExists(id); }
  public bool NullableIs(DownStairsTTC that) {
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
  public bool Is(DownStairsTTC that) {
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
