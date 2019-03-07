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

    if (!root.TimeAnchorTTCMutSetExists(membersTimeAnchorTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersTimeAnchorTTCMutSet");
    }

    if (!root.StaircaseTTCMutSetExists(membersStaircaseTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersStaircaseTTCMutSet");
    }

    if (!root.ItemTTCMutSetExists(membersItemTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersItemTTCMutSet");
    }

    if (!root.DecorativeTTCMutSetExists(membersDecorativeTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersDecorativeTTCMutSet");
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    if (root.TimeAnchorTTCMutSetExists(membersTimeAnchorTTCMutSet.id)) {
      membersTimeAnchorTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.StaircaseTTCMutSetExists(membersStaircaseTTCMutSet.id)) {
      membersStaircaseTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.ItemTTCMutSetExists(membersItemTTCMutSet.id)) {
      membersItemTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.DecorativeTTCMutSetExists(membersDecorativeTTCMutSet.id)) {
      membersDecorativeTTCMutSet.FindReachableObjects(foundIds);
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
         public TimeAnchorTTCMutSet membersTimeAnchorTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersTimeAnchorTTCMutSet of null!");
      }
      return new TimeAnchorTTCMutSet(root, incarnation.membersTimeAnchorTTCMutSet);
    }
                       }
  public StaircaseTTCMutSet membersStaircaseTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersStaircaseTTCMutSet of null!");
      }
      return new StaircaseTTCMutSet(root, incarnation.membersStaircaseTTCMutSet);
    }
                       }
  public ItemTTCMutSet membersItemTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersItemTTCMutSet of null!");
      }
      return new ItemTTCMutSet(root, incarnation.membersItemTTCMutSet);
    }
                       }
  public DecorativeTTCMutSet membersDecorativeTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersDecorativeTTCMutSet of null!");
      }
      return new DecorativeTTCMutSet(root, incarnation.membersDecorativeTTCMutSet);
    }
                       }

  public static ITerrainTileComponentMutBunch New(Root root) {
    return root.EffectITerrainTileComponentMutBunchCreate(
      root.EffectTimeAnchorTTCMutSetCreate()
,
      root.EffectStaircaseTTCMutSetCreate()
,
      root.EffectItemTTCMutSetCreate()
,
      root.EffectDecorativeTTCMutSetCreate()
        );
  }
  public void Add(ITerrainTileComponent elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.TimeAnchorTTCExists(elementI.id)) {
      this.membersTimeAnchorTTCMutSet.Add(root.GetTimeAnchorTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.StaircaseTTCExists(elementI.id)) {
      this.membersStaircaseTTCMutSet.Add(root.GetStaircaseTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.ItemTTCExists(elementI.id)) {
      this.membersItemTTCMutSet.Add(root.GetItemTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.DecorativeTTCExists(elementI.id)) {
      this.membersDecorativeTTCMutSet.Add(root.GetDecorativeTTC(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Remove(ITerrainTileComponent elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.TimeAnchorTTCExists(elementI.id)) {
      this.membersTimeAnchorTTCMutSet.Remove(root.GetTimeAnchorTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.StaircaseTTCExists(elementI.id)) {
      this.membersStaircaseTTCMutSet.Remove(root.GetStaircaseTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.ItemTTCExists(elementI.id)) {
      this.membersItemTTCMutSet.Remove(root.GetItemTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.DecorativeTTCExists(elementI.id)) {
      this.membersDecorativeTTCMutSet.Remove(root.GetDecorativeTTC(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Clear() {
    this.membersTimeAnchorTTCMutSet.Clear();
    this.membersStaircaseTTCMutSet.Clear();
    this.membersItemTTCMutSet.Clear();
    this.membersDecorativeTTCMutSet.Clear();
  }
  public int Count {
    get {
      return
        this.membersTimeAnchorTTCMutSet.Count +
        this.membersStaircaseTTCMutSet.Count +
        this.membersItemTTCMutSet.Count +
        this.membersDecorativeTTCMutSet.Count
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
    var tempMembersTimeAnchorTTCMutSet = this.membersTimeAnchorTTCMutSet;
    var tempMembersStaircaseTTCMutSet = this.membersStaircaseTTCMutSet;
    var tempMembersItemTTCMutSet = this.membersItemTTCMutSet;
    var tempMembersDecorativeTTCMutSet = this.membersDecorativeTTCMutSet;

    this.Delete();
    tempMembersTimeAnchorTTCMutSet.Destruct();
    tempMembersStaircaseTTCMutSet.Destruct();
    tempMembersItemTTCMutSet.Destruct();
    tempMembersDecorativeTTCMutSet.Destruct();
  }
  public IEnumerator<ITerrainTileComponent> GetEnumerator() {
    foreach (var element in this.membersTimeAnchorTTCMutSet) {
      yield return new TimeAnchorTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersStaircaseTTCMutSet) {
      yield return new StaircaseTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersItemTTCMutSet) {
      yield return new ItemTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersDecorativeTTCMutSet) {
      yield return new DecorativeTTCAsITerrainTileComponent(element);
    }
  }
    public List<TimeAnchorTTC> GetAllTimeAnchorTTC() {
      var result = new List<TimeAnchorTTC>();
      foreach (var thing in this.membersTimeAnchorTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<TimeAnchorTTC> ClearAllTimeAnchorTTC() {
      var result = new List<TimeAnchorTTC>();
      this.membersTimeAnchorTTCMutSet.Clear();
      return result;
    }
    public TimeAnchorTTC GetOnlyTimeAnchorTTCOrNull() {
      var result = GetAllTimeAnchorTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return TimeAnchorTTC.Null;
      }
    }
    public List<StaircaseTTC> GetAllStaircaseTTC() {
      var result = new List<StaircaseTTC>();
      foreach (var thing in this.membersStaircaseTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<StaircaseTTC> ClearAllStaircaseTTC() {
      var result = new List<StaircaseTTC>();
      this.membersStaircaseTTCMutSet.Clear();
      return result;
    }
    public StaircaseTTC GetOnlyStaircaseTTCOrNull() {
      var result = GetAllStaircaseTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return StaircaseTTC.Null;
      }
    }
    public List<ItemTTC> GetAllItemTTC() {
      var result = new List<ItemTTC>();
      foreach (var thing in this.membersItemTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<ItemTTC> ClearAllItemTTC() {
      var result = new List<ItemTTC>();
      this.membersItemTTCMutSet.Clear();
      return result;
    }
    public ItemTTC GetOnlyItemTTCOrNull() {
      var result = GetAllItemTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return ItemTTC.Null;
      }
    }
    public List<DecorativeTTC> GetAllDecorativeTTC() {
      var result = new List<DecorativeTTC>();
      foreach (var thing in this.membersDecorativeTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<DecorativeTTC> ClearAllDecorativeTTC() {
      var result = new List<DecorativeTTC>();
      this.membersDecorativeTTCMutSet.Clear();
      return result;
    }
    public DecorativeTTC GetOnlyDecorativeTTCOrNull() {
      var result = GetAllDecorativeTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return DecorativeTTC.Null;
      }
    }
}
}
