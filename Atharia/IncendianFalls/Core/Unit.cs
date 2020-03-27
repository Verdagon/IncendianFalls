using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class Unit {
  public readonly Root root;
  public readonly int id;
  public Unit(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public UnitIncarnation incarnation { get { return root.GetUnitIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IUnitEffectObserver observer) {
    broadcaster.AddUnitObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IUnitEffectObserver observer) {
    broadcaster.RemoveUnitObserver(id, observer);
  }
  public void Delete() {
    root.EffectUnitDelete(id);
  }
  public static Unit Null = new Unit(null, 0);
  public bool Exists() { return root != null && root.UnitExists(id); }
  public bool NullableIs(Unit that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {

    if (!root.IUnitComponentMutBunchExists(components.id)) {
      violations.Add("Null constraint violated! Unit#" + id + ".components");
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    if (root.IUnitComponentMutBunchExists(components.id)) {
      components.FindReachableObjects(foundIds);
    }
  }
  public bool Is(Unit that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public IUnitEvent evvent {
    get { return incarnation.evvent; }
    set { root.EffectUnitSetEvvent(id, value); }
  }
  public bool alive {
    get { return incarnation.alive; }
    set { root.EffectUnitSetAlive(id, value); }
  }
  public int lifeEndTime {
    get { return incarnation.lifeEndTime; }
    set { root.EffectUnitSetLifeEndTime(id, value); }
  }
  public Location location {
    get { return incarnation.location; }
    set { root.EffectUnitSetLocation(id, value); }
  }
  public string classId {
    get { return incarnation.classId; }
  }
  public int nextActionTime {
    get { return incarnation.nextActionTime; }
    set { root.EffectUnitSetNextActionTime(id, value); }
  }
  public int hp {
    get { return incarnation.hp; }
    set { root.EffectUnitSetHp(id, value); }
  }
  public int maxHp {
    get { return incarnation.maxHp; }
    set { root.EffectUnitSetMaxHp(id, value); }
  }
  public IUnitComponentMutBunch components {

    get {
      if (root == null) {
        throw new Exception("Tried to get member components of null!");
      }
      return new IUnitComponentMutBunch(root, incarnation.components);
    }
                       }
  public bool good {
    get { return incarnation.good; }
  }
}
}
