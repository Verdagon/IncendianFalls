using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class MiredUC {
  public readonly Root root;
  public readonly int id;
  public MiredUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public MiredUCIncarnation incarnation { get { return root.GetMiredUCIncarnation(id); } }
  public void AddObserver(IMiredUCEffectObserver observer) {
    root.AddMiredUCObserver(id, observer);
  }
  public void RemoveObserver(IMiredUCEffectObserver observer) {
    root.RemoveMiredUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectMiredUCDelete(id);
  }
  public static MiredUC Null = new MiredUC(null, 0);
  public bool Exists() { return root != null && root.MiredUCExists(id); }
  public bool NullableIs(MiredUC that) {
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
  public bool Is(MiredUC that) {
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
