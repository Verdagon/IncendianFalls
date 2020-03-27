using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class Wat {
  public readonly Root root;
  public readonly int id;
  public Wat(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public WatIncarnation incarnation { get { return root.GetWatIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IWatEffectObserver observer) {
    broadcaster.AddWatObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IWatEffectObserver observer) {
    broadcaster.RemoveWatObserver(id, observer);
  }
  public void Delete() {
    root.EffectWatDelete(id);
  }
  public static Wat Null = new Wat(null, 0);
  public bool Exists() { return root != null && root.WatExists(id); }
  public bool NullableIs(Wat that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {

    if (!root.IItemStrongMutBunchExists(items.id)) {
      violations.Add("Null constraint violated! Wat#" + id + ".items");
    }

    if (!root.IImpulseStrongMutBunchExists(impulses.id)) {
      violations.Add("Null constraint violated! Wat#" + id + ".impulses");
    }

    if (!root.IPostActingUCWeakMutBunchExists(blah.id)) {
      violations.Add("Null constraint violated! Wat#" + id + ".blah");
    }

    if (!root.IPreActingUCWeakMutBunchExists(bloop.id)) {
      violations.Add("Null constraint violated! Wat#" + id + ".bloop");
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    if (root.IItemStrongMutBunchExists(items.id)) {
      items.FindReachableObjects(foundIds);
    }
    if (root.IImpulseStrongMutBunchExists(impulses.id)) {
      impulses.FindReachableObjects(foundIds);
    }
    if (root.IPostActingUCWeakMutBunchExists(blah.id)) {
      blah.FindReachableObjects(foundIds);
    }
    if (root.IPreActingUCWeakMutBunchExists(bloop.id)) {
      bloop.FindReachableObjects(foundIds);
    }
  }
  public bool Is(Wat that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public IItemStrongMutBunch items {

    get {
      if (root == null) {
        throw new Exception("Tried to get member items of null!");
      }
      return new IItemStrongMutBunch(root, incarnation.items);
    }
                       }
  public IImpulseStrongMutBunch impulses {

    get {
      if (root == null) {
        throw new Exception("Tried to get member impulses of null!");
      }
      return new IImpulseStrongMutBunch(root, incarnation.impulses);
    }
                       }
  public IPostActingUCWeakMutBunch blah {

    get {
      if (root == null) {
        throw new Exception("Tried to get member blah of null!");
      }
      return new IPostActingUCWeakMutBunch(root, incarnation.blah);
    }
                       }
  public IPreActingUCWeakMutBunch bloop {

    get {
      if (root == null) {
        throw new Exception("Tried to get member bloop of null!");
      }
      return new IPreActingUCWeakMutBunch(root, incarnation.bloop);
    }
                       }
}
}
