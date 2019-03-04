using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class TimeAnchorTTC {
  public readonly Root root;
  public readonly int id;
  public TimeAnchorTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public TimeAnchorTTCIncarnation incarnation { get { return root.GetTimeAnchorTTCIncarnation(id); } }
  public void AddObserver(ITimeAnchorTTCEffectObserver observer) {
    root.AddTimeAnchorTTCObserver(id, observer);
  }
  public void RemoveObserver(ITimeAnchorTTCEffectObserver observer) {
    root.RemoveTimeAnchorTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectTimeAnchorTTCDelete(id);
  }
  public static TimeAnchorTTC Null = new TimeAnchorTTC(null, 0);
  public bool Exists() { return root != null && root.TimeAnchorTTCExists(id); }
  public bool NullableIs(TimeAnchorTTC that) {
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
  public bool Is(TimeAnchorTTC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public int pastVersion {
    get { return incarnation.pastVersion; }
  }
}
}
