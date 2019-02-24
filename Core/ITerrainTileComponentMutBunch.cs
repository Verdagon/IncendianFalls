using System;
using System.Collections;

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
  public void CheckForNullViolations(List<string> violations) {

    if (!root.DecorativeTerrainTileComponentMutSetExists(membersDecorativeTerrainTileComponentMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersDecorativeTerrainTileComponentMutSet");
    }

    if (!root.UpStaircaseTerrainTileComponentMutSetExists(membersUpStaircaseTerrainTileComponentMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersUpStaircaseTerrainTileComponentMutSet");
    }

    if (!root.DownStaircaseTerrainTileComponentMutSetExists(membersDownStaircaseTerrainTileComponentMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersDownStaircaseTerrainTileComponentMutSet");
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    if (root.DecorativeTerrainTileComponentMutSetExists(membersDecorativeTerrainTileComponentMutSet.id)) {
      membersDecorativeTerrainTileComponentMutSet.FindReachableObjects(foundIds);
    }
    if (root.UpStaircaseTerrainTileComponentMutSetExists(membersUpStaircaseTerrainTileComponentMutSet.id)) {
      membersUpStaircaseTerrainTileComponentMutSet.FindReachableObjects(foundIds);
    }
    if (root.DownStaircaseTerrainTileComponentMutSetExists(membersDownStaircaseTerrainTileComponentMutSet.id)) {
      membersDownStaircaseTerrainTileComponentMutSet.FindReachableObjects(foundIds);
    }
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

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersDecorativeTerrainTileComponentMutSet of null!");
      }
      return new DecorativeTerrainTileComponentMutSet(root, incarnation.membersDecorativeTerrainTileComponentMutSet);
    }
                       }
  public UpStaircaseTerrainTileComponentMutSet membersUpStaircaseTerrainTileComponentMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersUpStaircaseTerrainTileComponentMutSet of null!");
      }
      return new UpStaircaseTerrainTileComponentMutSet(root, incarnation.membersUpStaircaseTerrainTileComponentMutSet);
    }
                       }
  public DownStaircaseTerrainTileComponentMutSet membersDownStaircaseTerrainTileComponentMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersDownStaircaseTerrainTileComponentMutSet of null!");
      }
      return new DownStaircaseTerrainTileComponentMutSet(root, incarnation.membersDownStaircaseTerrainTileComponentMutSet);
    }
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

    // Can optimize, check the type of element directly somehow
    if (root.DecorativeTerrainTileComponentExists(elementI.id)) {
      this.membersDecorativeTerrainTileComponentMutSet.Add(root.GetDecorativeTerrainTileComponent(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.UpStaircaseTerrainTileComponentExists(elementI.id)) {
      this.membersUpStaircaseTerrainTileComponentMutSet.Add(root.GetUpStaircaseTerrainTileComponent(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.DownStaircaseTerrainTileComponentExists(elementI.id)) {
      this.membersDownStaircaseTerrainTileComponentMutSet.Add(root.GetDownStaircaseTerrainTileComponent(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Remove(ITerrainTileComponent elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.DecorativeTerrainTileComponentExists(elementI.id)) {
      this.membersDecorativeTerrainTileComponentMutSet.Remove(root.GetDecorativeTerrainTileComponent(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.UpStaircaseTerrainTileComponentExists(elementI.id)) {
      this.membersUpStaircaseTerrainTileComponentMutSet.Remove(root.GetUpStaircaseTerrainTileComponent(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.DownStaircaseTerrainTileComponentExists(elementI.id)) {
      this.membersDownStaircaseTerrainTileComponentMutSet.Remove(root.GetDownStaircaseTerrainTileComponent(elementI.id));
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

  public void Destruct() {
    var tempMembersDecorativeTerrainTileComponentMutSet = this.membersDecorativeTerrainTileComponentMutSet;
    var tempMembersUpStaircaseTerrainTileComponentMutSet = this.membersUpStaircaseTerrainTileComponentMutSet;
    var tempMembersDownStaircaseTerrainTileComponentMutSet = this.membersDownStaircaseTerrainTileComponentMutSet;

    this.Delete();
    tempMembersDecorativeTerrainTileComponentMutSet.Destruct();
    tempMembersUpStaircaseTerrainTileComponentMutSet.Destruct();
    tempMembersDownStaircaseTerrainTileComponentMutSet.Destruct();
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
    public List<DecorativeTerrainTileComponent> ClearAllDecorativeTerrainTileComponent() {
      var result = new List<DecorativeTerrainTileComponent>();
      this.membersDecorativeTerrainTileComponentMutSet.Clear();
      return result;
    }
    public DecorativeTerrainTileComponent GetOnlyDecorativeTerrainTileComponentOrNull() {
      var result = GetAllDecorativeTerrainTileComponent();
      if (result.Count > 0) {
        return result[0];
      } else {
        return DecorativeTerrainTileComponent.Null;
      }
    }
    public List<UpStaircaseTerrainTileComponent> GetAllUpStaircaseTerrainTileComponent() {
      var result = new List<UpStaircaseTerrainTileComponent>();
      foreach (var thing in this.membersUpStaircaseTerrainTileComponentMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<UpStaircaseTerrainTileComponent> ClearAllUpStaircaseTerrainTileComponent() {
      var result = new List<UpStaircaseTerrainTileComponent>();
      this.membersUpStaircaseTerrainTileComponentMutSet.Clear();
      return result;
    }
    public UpStaircaseTerrainTileComponent GetOnlyUpStaircaseTerrainTileComponentOrNull() {
      var result = GetAllUpStaircaseTerrainTileComponent();
      if (result.Count > 0) {
        return result[0];
      } else {
        return UpStaircaseTerrainTileComponent.Null;
      }
    }
    public List<DownStaircaseTerrainTileComponent> GetAllDownStaircaseTerrainTileComponent() {
      var result = new List<DownStaircaseTerrainTileComponent>();
      foreach (var thing in this.membersDownStaircaseTerrainTileComponentMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<DownStaircaseTerrainTileComponent> ClearAllDownStaircaseTerrainTileComponent() {
      var result = new List<DownStaircaseTerrainTileComponent>();
      this.membersDownStaircaseTerrainTileComponentMutSet.Clear();
      return result;
    }
    public DownStaircaseTerrainTileComponent GetOnlyDownStaircaseTerrainTileComponentOrNull() {
      var result = GetAllDownStaircaseTerrainTileComponent();
      if (result.Count > 0) {
        return result[0];
      } else {
        return DownStaircaseTerrainTileComponent.Null;
      }
    }
}
}
