using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class LotusTTC {
  public readonly Root root;
  public readonly int id;
  public LotusTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public LotusTTCIncarnation incarnation { get { return root.GetLotusTTCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, ILotusTTCEffectObserver observer) {
    broadcaster.AddLotusTTCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ILotusTTCEffectObserver observer) {
    broadcaster.RemoveLotusTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectLotusTTCDelete(id);
  }
  public static LotusTTC Null = new LotusTTC(null, 0);
  public bool Exists() { return root != null && root.LotusTTCExists(id); }
  public bool NullableIs(LotusTTC that) {
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
  public bool Is(LotusTTC that) {
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
