using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class UpStaircaseTTC {
  public readonly Root root;
  public readonly int id;
  public UpStaircaseTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public UpStaircaseTTCIncarnation incarnation { get { return root.GetUpStaircaseTTCIncarnation(id); } }
  public void AddObserver(IUpStaircaseTTCEffectObserver observer) {
    root.AddUpStaircaseTTCObserver(id, observer);
  }
  public void RemoveObserver(IUpStaircaseTTCEffectObserver observer) {
    root.RemoveUpStaircaseTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectUpStaircaseTTCDelete(id);
  }
  public static UpStaircaseTTC Null = new UpStaircaseTTC(null, 0);
  public bool Exists() { return root != null && root.UpStaircaseTTCExists(id); }
  public bool NullableIs(UpStaircaseTTC that) {
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
  public bool Is(UpStaircaseTTC that) {
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
