using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class IPostActingUCWeakMutBunch {
  public readonly Root root;
  public readonly int id;
  public IPostActingUCWeakMutBunch(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public IPostActingUCWeakMutBunchIncarnation incarnation { get { return root.GetIPostActingUCWeakMutBunchIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IIPostActingUCWeakMutBunchEffectObserver observer) {
    broadcaster.AddIPostActingUCWeakMutBunchObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IIPostActingUCWeakMutBunchEffectObserver observer) {
    broadcaster.RemoveIPostActingUCWeakMutBunchObserver(id, observer);
  }
  public void Delete() {
    root.EffectIPostActingUCWeakMutBunchDelete(id);
  }
  public static IPostActingUCWeakMutBunch Null = new IPostActingUCWeakMutBunch(null, 0);
  public bool Exists() { return root != null && root.IPostActingUCWeakMutBunchExists(id); }
  public bool NullableIs(IPostActingUCWeakMutBunch that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {

    if (!root.LightningChargedUCWeakMutSetExists(membersLightningChargedUCWeakMutSet.id)) {
      violations.Add("Null constraint violated! IPostActingUCWeakMutBunch#" + id + ".membersLightningChargedUCWeakMutSet");
    }

    if (!root.TimeCloneAICapabilityUCWeakMutSetExists(membersTimeCloneAICapabilityUCWeakMutSet.id)) {
      violations.Add("Null constraint violated! IPostActingUCWeakMutBunch#" + id + ".membersTimeCloneAICapabilityUCWeakMutSet");
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    if (root.LightningChargedUCWeakMutSetExists(membersLightningChargedUCWeakMutSet.id)) {
      membersLightningChargedUCWeakMutSet.FindReachableObjects(foundIds);
    }
    if (root.TimeCloneAICapabilityUCWeakMutSetExists(membersTimeCloneAICapabilityUCWeakMutSet.id)) {
      membersTimeCloneAICapabilityUCWeakMutSet.FindReachableObjects(foundIds);
    }
  }
  public bool Is(IPostActingUCWeakMutBunch that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public LightningChargedUCWeakMutSet membersLightningChargedUCWeakMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersLightningChargedUCWeakMutSet of null!");
      }
      return new LightningChargedUCWeakMutSet(root, incarnation.membersLightningChargedUCWeakMutSet);
    }
                       }
  public TimeCloneAICapabilityUCWeakMutSet membersTimeCloneAICapabilityUCWeakMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersTimeCloneAICapabilityUCWeakMutSet of null!");
      }
      return new TimeCloneAICapabilityUCWeakMutSet(root, incarnation.membersTimeCloneAICapabilityUCWeakMutSet);
    }
                       }

  public static IPostActingUCWeakMutBunch New(Root root) {
    return root.EffectIPostActingUCWeakMutBunchCreate(
      root.EffectLightningChargedUCWeakMutSetCreate()
,
      root.EffectTimeCloneAICapabilityUCWeakMutSetCreate()
        );
  }
  public void Add(IPostActingUC elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.LightningChargedUCExists(elementI.id)) {
      this.membersLightningChargedUCWeakMutSet.Add(root.GetLightningChargedUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.TimeCloneAICapabilityUCExists(elementI.id)) {
      this.membersTimeCloneAICapabilityUCWeakMutSet.Add(root.GetTimeCloneAICapabilityUC(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Remove(IPostActingUC elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.LightningChargedUCExists(elementI.id)) {
      this.membersLightningChargedUCWeakMutSet.Remove(root.GetLightningChargedUC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.TimeCloneAICapabilityUCExists(elementI.id)) {
      this.membersTimeCloneAICapabilityUCWeakMutSet.Remove(root.GetTimeCloneAICapabilityUC(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Clear() {
    this.membersLightningChargedUCWeakMutSet.Clear();
    this.membersTimeCloneAICapabilityUCWeakMutSet.Clear();
  }
  public int Count {
    get {
      return
        this.membersLightningChargedUCWeakMutSet.Count +
        this.membersTimeCloneAICapabilityUCWeakMutSet.Count
        ;
    }
  }
  public IPostActingUC GetArbitrary() {
    foreach (var element in this) {
      return element;
    }
    throw new Exception("Can't get element from empty bunch!");
  }

  public void Destruct() {
    var tempMembersLightningChargedUCWeakMutSet = this.membersLightningChargedUCWeakMutSet;
    var tempMembersTimeCloneAICapabilityUCWeakMutSet = this.membersTimeCloneAICapabilityUCWeakMutSet;

    this.Delete();
    tempMembersLightningChargedUCWeakMutSet.Destruct();
    tempMembersTimeCloneAICapabilityUCWeakMutSet.Destruct();
  }
  public IEnumerator<IPostActingUC> GetEnumerator() {
    foreach (var element in this.membersLightningChargedUCWeakMutSet) {
      yield return new LightningChargedUCAsIPostActingUC(element);
    }
    foreach (var element in this.membersTimeCloneAICapabilityUCWeakMutSet) {
      yield return new TimeCloneAICapabilityUCAsIPostActingUC(element);
    }
  }
    public List<LightningChargedUC> GetAllLightningChargedUC() {
      var result = new List<LightningChargedUC>();
      foreach (var thing in this.membersLightningChargedUCWeakMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<LightningChargedUC> ClearAllLightningChargedUC() {
      var result = new List<LightningChargedUC>();
      this.membersLightningChargedUCWeakMutSet.Clear();
      return result;
    }
    public LightningChargedUC GetOnlyLightningChargedUCOrNull() {
      var result = GetAllLightningChargedUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return LightningChargedUC.Null;
      }
    }
    public List<TimeCloneAICapabilityUC> GetAllTimeCloneAICapabilityUC() {
      var result = new List<TimeCloneAICapabilityUC>();
      foreach (var thing in this.membersTimeCloneAICapabilityUCWeakMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<TimeCloneAICapabilityUC> ClearAllTimeCloneAICapabilityUC() {
      var result = new List<TimeCloneAICapabilityUC>();
      this.membersTimeCloneAICapabilityUCWeakMutSet.Clear();
      return result;
    }
    public TimeCloneAICapabilityUC GetOnlyTimeCloneAICapabilityUCOrNull() {
      var result = GetAllTimeCloneAICapabilityUC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return TimeCloneAICapabilityUC.Null;
      }
    }
}
}
