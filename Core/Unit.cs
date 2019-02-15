using System;
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
  public void AddObserver(IUnitEffectObserver observer) {
    root.AddUnitObserver(id, observer);
  }
  public void RemoveObserver(IUnitEffectObserver observer) {
    root.RemoveUnitObserver(id, observer);
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
  public bool Is(Unit that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public IUnitEventMutList events {
    get { return new IUnitEventMutList(root, incarnation.events); }
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
  public int hp {
    get { return incarnation.hp; }
    set { root.EffectUnitSetHp(id, value); }
  }
  public int maxHp {
    get { return incarnation.maxHp; }
  }
  public int mp {
    get { return incarnation.mp; }
    set { root.EffectUnitSetMp(id, value); }
  }
  public int maxMp {
    get { return incarnation.maxMp; }
  }
  public int inertia {
    get { return incarnation.inertia; }
  }
  public int nextActionTime {
    get { return incarnation.nextActionTime; }
    set { root.EffectUnitSetNextActionTime(id, value); }
  }
  public IDirective directive {
    get { return root.GetIDirective(incarnation.directive); }
    set { root.EffectUnitSetDirective(id, value); }
  }
  public IDetailMutList details {
    get { return new IDetailMutList(root, incarnation.details); }
  }
  public IItemMutBunch items {
    get { return new IItemMutBunch(root, incarnation.items); }
  }
}
}
