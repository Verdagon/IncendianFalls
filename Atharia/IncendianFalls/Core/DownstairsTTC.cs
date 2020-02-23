using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class DownstairsTTC {
  public readonly Root root;
  public readonly int id;
  public DownstairsTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DownstairsTTCIncarnation incarnation { get { return root.GetDownstairsTTCIncarnation(id); } }
  public void AddObserver(IDownstairsTTCEffectObserver observer) {
    root.AddDownstairsTTCObserver(id, observer);
  }
  public void RemoveObserver(IDownstairsTTCEffectObserver observer) {
    root.RemoveDownstairsTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectDownstairsTTCDelete(id);
  }
  public static DownstairsTTC Null = new DownstairsTTC(null, 0);
  public bool Exists() { return root != null && root.DownstairsTTCExists(id); }
  public bool NullableIs(DownstairsTTC that) {
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
  public bool Is(DownstairsTTC that) {
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
