using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class ITerrainTileComponentMutBunch {
  public readonly Root root;
  public readonly int id;
  public ITerrainTileComponentMutBunch(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ITerrainTileComponentMutBunchIncarnation incarnation { get { return root.GetITerrainTileComponentMutBunchIncarnation(id); } }
  public void AddObserver(IITerrainTileComponentMutBunchEffectObserver observer) {
    root.AddITerrainTileComponentMutBunchObserver(id, observer);
  }
  public void RemoveObserver(IITerrainTileComponentMutBunchEffectObserver observer) {
    root.RemoveITerrainTileComponentMutBunchObserver(id, observer);
  }
  public void Delete() {
    root.EffectITerrainTileComponentMutBunchDelete(id);
  }
  public static ITerrainTileComponentMutBunch Null = new ITerrainTileComponentMutBunch(null, 0);
  public bool Exists() { return root != null && root.ITerrainTileComponentMutBunchExists(id); }
  public bool NullableIs(ITerrainTileComponentMutBunch that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
    return this.Is(that);
  }
  public bool Is(ITerrainTileComponentMutBunch that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public DecorativeTerrainTileComponentMutSet membersDecorativeTerrainTileComponentMutSet {
    get { return new DecorativeTerrainTileComponentMutSet(root, incarnation.membersDecorativeTerrainTileComponentMutSet); }
  }
  public UpStaircaseTerrainTileComponentMutSet membersUpStaircaseTerrainTileComponentMutSet {
    get { return new UpStaircaseTerrainTileComponentMutSet(root, incarnation.membersUpStaircaseTerrainTileComponentMutSet); }
  }
  public DownStaircaseTerrainTileComponentMutSet membersDownStaircaseTerrainTileComponentMutSet {
    get { return new DownStaircaseTerrainTileComponentMutSet(root, incarnation.membersDownStaircaseTerrainTileComponentMutSet); }
  }

  public static ITerrainTileComponentMutBunch New(Root root) {
    return root.EffectITerrainTileComponentMutBunchCreate(
      root.EffectDecorativeTerrainTileComponentMutSetCreate()
,
      root.EffectUpStaircaseTerrainTileComponentMutSetCreate()
,
      root.EffectDownStaircaseTerrainTileComponentMutSetCreate()
        );
  }
  public void Add(ITerrainTileComponent elementI) {
    if (elementI is DecorativeTerrainTileComponentAsITerrainTileComponent typeclassDecorativeTerrainTileComponent) {
      this.membersDecorativeTerrainTileComponentMutSet.Add(typeclassDecorativeTerrainTileComponent.obj);
      return;
    }
    if (elementI is UpStaircaseTerrainTileComponentAsITerrainTileComponent typeclassUpStaircaseTerrainTileComponent) {
      this.membersUpStaircaseTerrainTileComponentMutSet.Add(typeclassUpStaircaseTerrainTileComponent.obj);
      return;
    }
    if (elementI is DownStaircaseTerrainTileComponentAsITerrainTileComponent typeclassDownStaircaseTerrainTileComponent) {
      this.membersDownStaircaseTerrainTileComponentMutSet.Add(typeclassDownStaircaseTerrainTileComponent.obj);
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Remove(ITerrainTileComponent elementI) {
    if (elementI is DecorativeTerrainTileComponentAsITerrainTileComponent typeclassDecorativeTerrainTileComponent) {
      this.membersDecorativeTerrainTileComponentMutSet.Remove(typeclassDecorativeTerrainTileComponent.obj);
      return;
    }
    if (elementI is UpStaircaseTerrainTileComponentAsITerrainTileComponent typeclassUpStaircaseTerrainTileComponent) {
      this.membersUpStaircaseTerrainTileComponentMutSet.Remove(typeclassUpStaircaseTerrainTileComponent.obj);
      return;
    }
    if (elementI is DownStaircaseTerrainTileComponentAsITerrainTileComponent typeclassDownStaircaseTerrainTileComponent) {
      this.membersDownStaircaseTerrainTileComponentMutSet.Remove(typeclassDownStaircaseTerrainTileComponent.obj);
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Clear() {
    this.membersDecorativeTerrainTileComponentMutSet.Clear();
    this.membersUpStaircaseTerrainTileComponentMutSet.Clear();
    this.membersDownStaircaseTerrainTileComponentMutSet.Clear();
  }
  public int Count {
    get {
      return
        this.membersDecorativeTerrainTileComponentMutSet.Count +
        this.membersUpStaircaseTerrainTileComponentMutSet.Count +
        this.membersDownStaircaseTerrainTileComponentMutSet.Count
        ;
    }
  }
  public ITerrainTileComponent GetArbitrary() {
    foreach (var element in this) {
      return element;
    }
    throw new Exception("Can't get element from empty bunch!");
  }

  public IEnumerator<ITerrainTileComponent> GetEnumerator() {
    foreach (var element in this.membersDecorativeTerrainTileComponentMutSet) {
      yield return new DecorativeTerrainTileComponentAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersUpStaircaseTerrainTileComponentMutSet) {
      yield return new UpStaircaseTerrainTileComponentAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersDownStaircaseTerrainTileComponentMutSet) {
      yield return new DownStaircaseTerrainTileComponentAsITerrainTileComponent(element);
    }
  }
    public List<DecorativeTerrainTileComponent> GetAllDecorativeTerrainTileComponent() {
      var result = new List<DecorativeTerrainTileComponent>();
      foreach (var thing in this.membersDecorativeTerrainTileComponentMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<UpStaircaseTerrainTileComponent> GetAllUpStaircaseTerrainTileComponent() {
      var result = new List<UpStaircaseTerrainTileComponent>();
      foreach (var thing in this.membersUpStaircaseTerrainTileComponentMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<DownStaircaseTerrainTileComponent> GetAllDownStaircaseTerrainTileComponent() {
      var result = new List<DownStaircaseTerrainTileComponent>();
      foreach (var thing in this.membersDownStaircaseTerrainTileComponentMutSet) {
        result.Add(thing);
      }
      return result;
    }
}
}
