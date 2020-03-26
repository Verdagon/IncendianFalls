using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class MudTTC {
  public readonly Root root;
  public readonly int id;
  public MudTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public MudTTCIncarnation incarnation { get { return root.GetMudTTCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IMudTTCEffectObserver observer) {
    broadcaster.AddMudTTCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IMudTTCEffectObserver observer) {
    broadcaster.RemoveMudTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectMudTTCDelete(id);
  }
  public static MudTTC Null = new MudTTC(null, 0);
  public bool Exists() { return root != null && root.MudTTCExists(id); }
  public bool NullableIs(MudTTC that) {
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
  public bool Is(MudTTC that) {
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
