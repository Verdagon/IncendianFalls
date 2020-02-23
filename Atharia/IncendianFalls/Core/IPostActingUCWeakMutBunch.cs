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
  public void AddObserver(IIPostActingUCWeakMutBunchEffectObserver observer) {
    root.AddIPostActingUCWeakMutBunchObserver(id, observer);
  }
  public void RemoveObserver(IIPostActingUCWeakMutBunchEffectObserver observer) {
    root.RemoveIPostActingUCWeakMutBunchObserver(id, observer);
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

    if (!root.TimeCloneAICapabilityUCWeakMutSetExists(membersTimeCloneAICapabilityUCWeakMutSet.id)) {
      violations.Add("Null constraint violated! IPostActingUCWeakMutBunch#" + id + ".membersTimeCloneAICapabilityUCWeakMutSet");
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
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
      root.EffectTimeCloneAICapabilityUCWeakMutSetCreate()
        );
  }
  public void Add(IPostActingUC elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.TimeCloneAICapabilityUCExists(elementI.id)) {
      this.membersTimeCloneAICapabilityUCWeakMutSet.Add(root.GetTimeCloneAICapabilityUC(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Remove(IPostActingUC elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.TimeCloneAICapabilityUCExists(elementI.id)) {
      this.membersTimeCloneAICapabilityUCWeakMutSet.Remove(root.GetTimeCloneAICapabilityUC(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Clear() {
    this.membersTimeCloneAICapabilityUCWeakMutSet.Clear();
  }
  public int Count {
    get {
      return
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
    var tempMembersTimeCloneAICapabilityUCWeakMutSet = this.membersTimeCloneAICapabilityUCWeakMutSet;

    this.Delete();
    tempMembersTimeCloneAICapabilityUCWeakMutSet.Destruct();
  }
  public IEnumerator<IPostActingUC> GetEnumerator() {
    foreach (var element in this.membersTimeCloneAICapabilityUCWeakMutSet) {
      yield return new TimeCloneAICapabilityUCAsIPostActingUC(element);
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
