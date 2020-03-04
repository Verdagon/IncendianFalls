using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class TreeTTC {
  public readonly Root root;
  public readonly int id;
  public TreeTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public TreeTTCIncarnation incarnation { get { return root.GetTreeTTCIncarnation(id); } }
  public void AddObserver(ITreeTTCEffectObserver observer) {
    root.AddTreeTTCObserver(id, observer);
  }
  public void RemoveObserver(ITreeTTCEffectObserver observer) {
    root.RemoveTreeTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectTreeTTCDelete(id);
  }
  public static TreeTTC Null = new TreeTTC(null, 0);
  public bool Exists() { return root != null && root.TreeTTCExists(id); }
  public bool NullableIs(TreeTTC that) {
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
  public bool Is(TreeTTC that) {
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
