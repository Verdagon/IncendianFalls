using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class GrassTTC {
  public readonly Root root;
  public readonly int id;
  public GrassTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public GrassTTCIncarnation incarnation { get { return root.GetGrassTTCIncarnation(id); } }
  public void AddObserver(IGrassTTCEffectObserver observer) {
    root.AddGrassTTCObserver(id, observer);
  }
  public void RemoveObserver(IGrassTTCEffectObserver observer) {
    root.RemoveGrassTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectGrassTTCDelete(id);
  }
  public static GrassTTC Null = new GrassTTC(null, 0);
  public bool Exists() { return root != null && root.GrassTTCExists(id); }
  public bool NullableIs(GrassTTC that) {
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
  public bool Is(GrassTTC that) {
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
