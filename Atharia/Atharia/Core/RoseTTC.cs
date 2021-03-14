using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class RoseTTC {
  public readonly Root root;
  public readonly int id;
  public RoseTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public RoseTTCIncarnation incarnation { get { return root.GetRoseTTCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IRoseTTCEffectObserver observer) {
    broadcaster.AddRoseTTCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IRoseTTCEffectObserver observer) {
    broadcaster.RemoveRoseTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectRoseTTCDelete(id);
  }
  public static RoseTTC Null = new RoseTTC(null, 0);
  public bool Exists() { return root != null && root.RoseTTCExists(id); }
  public bool NullableIs(RoseTTC that) {
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
  public bool Is(RoseTTC that) {
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
