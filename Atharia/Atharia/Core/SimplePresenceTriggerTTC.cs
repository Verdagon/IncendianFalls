using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class SimplePresenceTriggerTTC {
  public readonly Root root;
  public readonly int id;
  public SimplePresenceTriggerTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public SimplePresenceTriggerTTCIncarnation incarnation { get { return root.GetSimplePresenceTriggerTTCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, ISimplePresenceTriggerTTCEffectObserver observer) {
    broadcaster.AddSimplePresenceTriggerTTCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ISimplePresenceTriggerTTCEffectObserver observer) {
    broadcaster.RemoveSimplePresenceTriggerTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectSimplePresenceTriggerTTCDelete(id);
  }
  public static SimplePresenceTriggerTTC Null = new SimplePresenceTriggerTTC(null, 0);
  public bool Exists() { return root != null && root.SimplePresenceTriggerTTCExists(id); }
  public bool NullableIs(SimplePresenceTriggerTTC that) {
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
  public bool Is(SimplePresenceTriggerTTC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public string name {
    get { return incarnation.name; }
  }
}
}
