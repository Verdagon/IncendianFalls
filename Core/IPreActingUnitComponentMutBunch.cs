using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class IPreActingUnitComponentMutBunch {
  public readonly Root root;
  public readonly int id;
  public IPreActingUnitComponentMutBunch(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public IPreActingUnitComponentMutBunchIncarnation incarnation { get { return root.GetIPreActingUnitComponentMutBunchIncarnation(id); } }
  public void AddObserver(IIPreActingUnitComponentMutBunchEffectObserver observer) {
    root.AddIPreActingUnitComponentMutBunchObserver(id, observer);
  }
  public void RemoveObserver(IIPreActingUnitComponentMutBunchEffectObserver observer) {
    root.RemoveIPreActingUnitComponentMutBunchObserver(id, observer);
  }
  public void Delete() {
    root.EffectIPreActingUnitComponentMutBunchDelete(id);
  }
  public static IPreActingUnitComponentMutBunch Null = new IPreActingUnitComponentMutBunch(null, 0);
  public bool Exists() { return root != null && root.IPreActingUnitComponentMutBunchExists(id); }
  public bool NullableIs(IPreActingUnitComponentMutBunch that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
    return this.Is(that);
  }
  public bool Is(IPreActingUnitComponentMutBunch that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public ShieldingUnitComponentMutSet membersShieldingUnitComponentMutSet {
    get { return new ShieldingUnitComponentMutSet(root, incarnation.membersShieldingUnitComponentMutSet); }
  }

  public static IPreActingUnitComponentMutBunch New(Root root) {
    return root.EffectIPreActingUnitComponentMutBunchCreate(
      root.EffectShieldingUnitComponentMutSetCreate()
        );
  }
  public void Add(IPreActingUnitComponent elementI) {
    if (elementI is ShieldingUnitComponentAsIPreActingUnitComponent typeclassShieldingUnitComponent) {
      this.membersShieldingUnitComponentMutSet.Add(typeclassShieldingUnitComponent.obj);
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Remove(IPreActingUnitComponent elementI) {
    if (elementI is ShieldingUnitComponentAsIPreActingUnitComponent typeclassShieldingUnitComponent) {
      this.membersShieldingUnitComponentMutSet.Remove(typeclassShieldingUnitComponent.obj);
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Clear() {
    this.membersShieldingUnitComponentMutSet.Clear();
  }
  public int Count {
    get {
      return
        this.membersShieldingUnitComponentMutSet.Count
        ;
    }
  }
  public IPreActingUnitComponent GetArbitrary() {
    foreach (var element in this) {
      return element;
    }
    throw new Exception("Can't get element from empty bunch!");
  }

  public IEnumerator<IPreActingUnitComponent> GetEnumerator() {
    foreach (var element in this.membersShieldingUnitComponentMutSet) {
      yield return new ShieldingUnitComponentAsIPreActingUnitComponent(element);
    }
  }
    public List<ShieldingUnitComponent> GetAllShieldingUnitComponent() {
      var result = new List<ShieldingUnitComponent>();
      foreach (var thing in this.membersShieldingUnitComponentMutSet) {
        result.Add(thing);
      }
      return result;
    }
}
}
