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

    if (!root.ItemTTCMutSetExists(membersItemTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersItemTTCMutSet");
    }

    if (!root.DecorativeTTCMutSetExists(membersDecorativeTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersDecorativeTTCMutSet");
    }

    if (!root.UpStaircaseTTCMutSetExists(membersUpStaircaseTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersUpStaircaseTTCMutSet");
    }

    if (!root.DownStaircaseTTCMutSetExists(membersDownStaircaseTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersDownStaircaseTTCMutSet");
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
    if (root.ItemTTCMutSetExists(membersItemTTCMutSet.id)) {
      membersItemTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.DecorativeTTCMutSetExists(membersDecorativeTTCMutSet.id)) {
      membersDecorativeTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.UpStaircaseTTCMutSetExists(membersUpStaircaseTTCMutSet.id)) {
      membersUpStaircaseTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.DownStaircaseTTCMutSetExists(membersDownStaircaseTTCMutSet.id)) {
      membersDownStaircaseTTCMutSet.FindReachableObjects(foundIds);
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
  public UpStaircaseTTCMutSet membersUpStaircaseTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersUpStaircaseTTCMutSet of null!");
      }
      return new UpStaircaseTTCMutSet(root, incarnation.membersUpStaircaseTTCMutSet);
    }
                       }
  public DownStaircaseTTCMutSet membersDownStaircaseTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersDownStaircaseTTCMutSet of null!");
      }
      return new DownStaircaseTTCMutSet(root, incarnation.membersDownStaircaseTTCMutSet);
    }
                       }

  public static ITerrainTileComponentMutBunch New(Root root) {
    return root.EffectITerrainTileComponentMutBunchCreate(
      root.EffectTimeAnchorTTCMutSetCreate()
,
      root.EffectItemTTCMutSetCreate()
,
      root.EffectDecorativeTTCMutSetCreate()
,
      root.EffectUpStaircaseTTCMutSetCreate()
,
      root.EffectDownStaircaseTTCMutSetCreate()
        );
  }
  public void Add(ITerrainTileComponent elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.TimeAnchorTTCExists(elementI.id)) {
      this.membersTimeAnchorTTCMutSet.Add(root.GetTimeAnchorTTC(elementI.id));
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

    // Can optimize, check the type of element directly somehow
    if (root.UpStaircaseTTCExists(elementI.id)) {
      this.membersUpStaircaseTTCMutSet.Add(root.GetUpStaircaseTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.DownStaircaseTTCExists(elementI.id)) {
      this.membersDownStaircaseTTCMutSet.Add(root.GetDownStaircaseTTC(elementI.id));
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
    if (root.ItemTTCExists(elementI.id)) {
      this.membersItemTTCMutSet.Remove(root.GetItemTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.DecorativeTTCExists(elementI.id)) {
      this.membersDecorativeTTCMutSet.Remove(root.GetDecorativeTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.UpStaircaseTTCExists(elementI.id)) {
      this.membersUpStaircaseTTCMutSet.Remove(root.GetUpStaircaseTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.DownStaircaseTTCExists(elementI.id)) {
      this.membersDownStaircaseTTCMutSet.Remove(root.GetDownStaircaseTTC(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Clear() {
    this.membersTimeAnchorTTCMutSet.Clear();
    this.membersItemTTCMutSet.Clear();
    this.membersDecorativeTTCMutSet.Clear();
    this.membersUpStaircaseTTCMutSet.Clear();
    this.membersDownStaircaseTTCMutSet.Clear();
  }
  public int Count {
    get {
      return
        this.membersTimeAnchorTTCMutSet.Count +
        this.membersItemTTCMutSet.Count +
        this.membersDecorativeTTCMutSet.Count +
        this.membersUpStaircaseTTCMutSet.Count +
        this.membersDownStaircaseTTCMutSet.Count
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
    var tempMembersItemTTCMutSet = this.membersItemTTCMutSet;
    var tempMembersDecorativeTTCMutSet = this.membersDecorativeTTCMutSet;
    var tempMembersUpStaircaseTTCMutSet = this.membersUpStaircaseTTCMutSet;
    var tempMembersDownStaircaseTTCMutSet = this.membersDownStaircaseTTCMutSet;

    this.Delete();
    tempMembersTimeAnchorTTCMutSet.Destruct();
    tempMembersItemTTCMutSet.Destruct();
    tempMembersDecorativeTTCMutSet.Destruct();
    tempMembersUpStaircaseTTCMutSet.Destruct();
    tempMembersDownStaircaseTTCMutSet.Destruct();
  }
  public IEnumerator<ITerrainTileComponent> GetEnumerator() {
    foreach (var element in this.membersTimeAnchorTTCMutSet) {
      yield return new TimeAnchorTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersItemTTCMutSet) {
      yield return new ItemTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersDecorativeTTCMutSet) {
      yield return new DecorativeTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersUpStaircaseTTCMutSet) {
      yield return new UpStaircaseTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersDownStaircaseTTCMutSet) {
      yield return new DownStaircaseTTCAsITerrainTileComponent(element);
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
    public List<UpStaircaseTTC> GetAllUpStaircaseTTC() {
      var result = new List<UpStaircaseTTC>();
      foreach (var thing in this.membersUpStaircaseTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<UpStaircaseTTC> ClearAllUpStaircaseTTC() {
      var result = new List<UpStaircaseTTC>();
      this.membersUpStaircaseTTCMutSet.Clear();
      return result;
    }
    public UpStaircaseTTC GetOnlyUpStaircaseTTCOrNull() {
      var result = GetAllUpStaircaseTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return UpStaircaseTTC.Null;
      }
    }
    public List<DownStaircaseTTC> GetAllDownStaircaseTTC() {
      var result = new List<DownStaircaseTTC>();
      foreach (var thing in this.membersDownStaircaseTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<DownStaircaseTTC> ClearAllDownStaircaseTTC() {
      var result = new List<DownStaircaseTTC>();
      this.membersDownStaircaseTTCMutSet.Clear();
      return result;
    }
    public DownStaircaseTTC GetOnlyDownStaircaseTTCOrNull() {
      var result = GetAllDownStaircaseTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return DownStaircaseTTC.Null;
      }
    }
}
}
