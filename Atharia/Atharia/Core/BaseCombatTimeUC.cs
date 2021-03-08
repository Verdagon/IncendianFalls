using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class BaseCombatTimeUC {
  public readonly Root root;
  public readonly int id;
  public BaseCombatTimeUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public BaseCombatTimeUCIncarnation incarnation { get { return root.GetBaseCombatTimeUCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IBaseCombatTimeUCEffectObserver observer) {
    broadcaster.AddBaseCombatTimeUCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IBaseCombatTimeUCEffectObserver observer) {
    broadcaster.RemoveBaseCombatTimeUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectBaseCombatTimeUCDelete(id);
  }
  public static BaseCombatTimeUC Null = new BaseCombatTimeUC(null, 0);
  public bool Exists() { return root != null && root.BaseCombatTimeUCExists(id); }
  public bool NullableIs(BaseCombatTimeUC that) {
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
  public bool Is(BaseCombatTimeUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public int combatTimeAddConstant {
    get { return incarnation.combatTimeAddConstant; }
  }
  public int combatTimeMultiplierPercent {
    get { return incarnation.combatTimeMultiplierPercent; }
  }
}
}
