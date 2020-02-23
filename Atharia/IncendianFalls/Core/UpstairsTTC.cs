using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class UpstairsTTC {
  public readonly Root root;
  public readonly int id;
  public UpstairsTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public UpstairsTTCIncarnation incarnation { get { return root.GetUpstairsTTCIncarnation(id); } }
  public void AddObserver(IUpstairsTTCEffectObserver observer) {
    root.AddUpstairsTTCObserver(id, observer);
  }
  public void RemoveObserver(IUpstairsTTCEffectObserver observer) {
    root.RemoveUpstairsTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectUpstairsTTCDelete(id);
  }
  public static UpstairsTTC Null = new UpstairsTTC(null, 0);
  public bool Exists() { return root != null && root.UpstairsTTCExists(id); }
  public bool NullableIs(UpstairsTTC that) {
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
  public bool Is(UpstairsTTC that) {
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
