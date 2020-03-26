using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class FallsTTC {
  public readonly Root root;
  public readonly int id;
  public FallsTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public FallsTTCIncarnation incarnation { get { return root.GetFallsTTCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IFallsTTCEffectObserver observer) {
    broadcaster.AddFallsTTCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IFallsTTCEffectObserver observer) {
    broadcaster.RemoveFallsTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectFallsTTCDelete(id);
  }
  public static FallsTTC Null = new FallsTTC(null, 0);
  public bool Exists() { return root != null && root.FallsTTCExists(id); }
  public bool NullableIs(FallsTTC that) {
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
  public bool Is(FallsTTC that) {
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
