using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class IPostActingUnitComponentMutBunch {
  public readonly Root root;
  public readonly int id;
  public IPostActingUnitComponentMutBunch(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public IPostActingUnitComponentMutBunchIncarnation incarnation { get { return root.GetIPostActingUnitComponentMutBunchIncarnation(id); } }
  public void AddObserver(IIPostActingUnitComponentMutBunchEffectObserver observer) {
    root.AddIPostActingUnitComponentMutBunchObserver(id, observer);
  }
  public void RemoveObserver(IIPostActingUnitComponentMutBunchEffectObserver observer) {
    root.RemoveIPostActingUnitComponentMutBunchObserver(id, observer);
  }
  public void Delete() {
    root.EffectIPostActingUnitComponentMutBunchDelete(id);
  }
  public static IPostActingUnitComponentMutBunch Null = new IPostActingUnitComponentMutBunch(null, 0);
  public bool Exists() { return root != null && root.IPostActingUnitComponentMutBunchExists(id); }
  public bool NullableIs(IPostActingUnitComponentMutBunch that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
    return this.Is(that);
  }
  public bool Is(IPostActingUnitComponentMutBunch that) {
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

  public static IPostActingUnitComponentMutBunch New(Root root) {
    return root.EffectIPostActingUnitComponentMutBunchCreate(
      root.EffectShieldingUnitComponentMutSetCreate()
        );
  }
  public void Add(IPostActingUnitComponent elementI) {
    if (elementI is ShieldingUnitComponentAsIPostActingUnitComponent typeclassShieldingUnitComponent) {
      this.membersShieldingUnitComponentMutSet.Add(typeclassShieldingUnitComponent.obj);
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Remove(IPostActingUnitComponent elementI) {
    if (elementI is ShieldingUnitComponentAsIPostActingUnitComponent typeclassShieldingUnitComponent) {
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
  public IPostActingUnitComponent GetArbitrary() {
    foreach (var element in this) {
      return element;
    }
    throw new Exception("Can't get element from empty bunch!");
  }

  public IEnumerator<IPostActingUnitComponent> GetEnumerator() {
    foreach (var element in this.membersShieldingUnitComponentMutSet) {
      yield return new ShieldingUnitComponentAsIPostActingUnitComponent(element);
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
