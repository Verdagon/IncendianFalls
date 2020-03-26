using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class RocksTTC {
  public readonly Root root;
  public readonly int id;
  public RocksTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public RocksTTCIncarnation incarnation { get { return root.GetRocksTTCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IRocksTTCEffectObserver observer) {
    broadcaster.AddRocksTTCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IRocksTTCEffectObserver observer) {
    broadcaster.RemoveRocksTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectRocksTTCDelete(id);
  }
  public static RocksTTC Null = new RocksTTC(null, 0);
  public bool Exists() { return root != null && root.RocksTTCExists(id); }
  public bool NullableIs(RocksTTC that) {
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
  public bool Is(RocksTTC that) {
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
