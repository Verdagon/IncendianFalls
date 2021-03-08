using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class Comm {
  public readonly Root root;
  public readonly int id;
  public Comm(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public CommIncarnation incarnation { get { return root.GetCommIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, ICommEffectObserver observer) {
    broadcaster.AddCommObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ICommEffectObserver observer) {
    broadcaster.RemoveCommObserver(id, observer);
  }
  public void Delete() {
    root.EffectCommDelete(id);
  }
  public static Comm Null = new Comm(null, 0);
  public bool Exists() { return root != null && root.CommExists(id); }
  public bool NullableIs(Comm that) {
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
  public bool Is(Comm that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public ICommTemplate template {
    get { return incarnation.template; }
  }
  public CommActionImmList actions {
    get { return incarnation.actions; }
  }
  public CommTextImmList texts {
    get { return incarnation.texts; }
  }
}
}
