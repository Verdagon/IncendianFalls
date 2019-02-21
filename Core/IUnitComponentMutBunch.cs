using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class IUnitComponentMutBunch {
  public readonly Root root;
  public readonly int id;
  public IUnitComponentMutBunch(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public IUnitComponentMutBunchIncarnation incarnation { get { return root.GetIUnitComponentMutBunchIncarnation(id); } }
  public void AddObserver(IIUnitComponentMutBunchEffectObserver observer) {
    root.AddIUnitComponentMutBunchObserver(id, observer);
  }
  public void RemoveObserver(IIUnitComponentMutBunchEffectObserver observer) {
    root.RemoveIUnitComponentMutBunchObserver(id, observer);
  }
  public void Delete() {
    root.EffectIUnitComponentMutBunchDelete(id);
  }
  public static IUnitComponentMutBunch Null = new IUnitComponentMutBunch(null, 0);
  public bool Exists() { return root != null && root.IUnitComponentMutBunchExists(id); }
  public bool NullableIs(IUnitComponentMutBunch that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
    return this.Is(that);
  }
  public bool Is(IUnitComponentMutBunch that) {
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

  public static IUnitComponentMutBunch New(Root root) {
    return root.EffectIUnitComponentMutBunchCreate(
      root.EffectShieldingUnitComponentMutSetCreate()
        );
  }
  public void Add(IUnitComponent elementI) {
    if (elementI is ShieldingUnitComponentAsIUnitComponent typeclassShieldingUnitComponent) {
      this.membersShieldingUnitComponentMutSet.Add(typeclassShieldingUnitComponent.obj);
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Remove(IUnitComponent elementI) {
    if (elementI is ShieldingUnitComponentAsIUnitComponent typeclassShieldingUnitComponent) {
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
  public IUnitComponent GetArbitrary() {
    foreach (var element in this) {
      return element;
    }
    throw new Exception("Can't get element from empty bunch!");
  }

  public IEnumerator<IUnitComponent> GetEnumerator() {
    foreach (var element in this.membersShieldingUnitComponentMutSet) {
      yield return new ShieldingUnitComponentAsIUnitComponent(element);
    }
  }
    public List<ShieldingUnitComponent> GetAllShieldingUnitComponent() {
      var result = new List<ShieldingUnitComponent>();
      foreach (var thing in this.membersShieldingUnitComponentMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<IPostActingUnitComponent> GetAllIPostActingUnitComponent() {
      var result = new List<IPostActingUnitComponent>();
      foreach (var obj in this.membersShieldingUnitComponentMutSet) {
        result.Add(
            new ShieldingUnitComponentAsIPostActingUnitComponent(obj));
      }
      return result;
    }
                 public List<IPreActingUnitComponent> GetAllIPreActingUnitComponent() {
      var result = new List<IPreActingUnitComponent>();
      foreach (var obj in this.membersShieldingUnitComponentMutSet) {
        result.Add(
            new ShieldingUnitComponentAsIPreActingUnitComponent(obj));
      }
      return result;
    }
                 public List<IDefenseUnitComponent> GetAllIDefenseUnitComponent() {
      var result = new List<IDefenseUnitComponent>();
      foreach (var obj in this.membersShieldingUnitComponentMutSet) {
        result.Add(
            new ShieldingUnitComponentAsIDefenseUnitComponent(obj));
      }
      return result;
    }
             }
}
