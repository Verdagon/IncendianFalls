using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class MagmaTTC {
  public readonly Root root;
  public readonly int id;
  public MagmaTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public MagmaTTCIncarnation incarnation { get { return root.GetMagmaTTCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IMagmaTTCEffectObserver observer) {
    broadcaster.AddMagmaTTCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IMagmaTTCEffectObserver observer) {
    broadcaster.RemoveMagmaTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectMagmaTTCDelete(id);
  }
  public static MagmaTTC Null = new MagmaTTC(null, 0);
  public bool Exists() { return root != null && root.MagmaTTCExists(id); }
  public bool NullableIs(MagmaTTC that) {
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
  public bool Is(MagmaTTC that) {
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
