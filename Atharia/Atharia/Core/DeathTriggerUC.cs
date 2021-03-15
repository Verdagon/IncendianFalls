using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class DeathTriggerUC {
  public readonly Root root;
  public readonly int id;
  public DeathTriggerUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DeathTriggerUCIncarnation incarnation { get { return root.GetDeathTriggerUCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IDeathTriggerUCEffectObserver observer) {
    broadcaster.AddDeathTriggerUCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IDeathTriggerUCEffectObserver observer) {
    broadcaster.RemoveDeathTriggerUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectDeathTriggerUCDelete(id);
  }
  public static DeathTriggerUC Null = new DeathTriggerUC(null, 0);
  public bool Exists() { return root != null && root.DeathTriggerUCExists(id); }
  public bool NullableIs(DeathTriggerUC that) {
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
  public bool Is(DeathTriggerUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public string triggerName {
    get { return incarnation.triggerName; }
  }
}
}
