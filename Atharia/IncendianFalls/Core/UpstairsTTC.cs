using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class UpStairsTTC {
  public readonly Root root;
  public readonly int id;
  public UpStairsTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public UpStairsTTCIncarnation incarnation { get { return root.GetUpStairsTTCIncarnation(id); } }
  public void AddObserver(IUpStairsTTCEffectObserver observer) {
    root.AddUpStairsTTCObserver(id, observer);
  }
  public void RemoveObserver(IUpStairsTTCEffectObserver observer) {
    root.RemoveUpStairsTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectUpStairsTTCDelete(id);
  }
  public static UpStairsTTC Null = new UpStairsTTC(null, 0);
  public bool Exists() { return root != null && root.UpStairsTTCExists(id); }
  public bool NullableIs(UpStairsTTC that) {
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
  public bool Is(UpStairsTTC that) {
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
