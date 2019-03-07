using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class StaircaseTTC {
  public readonly Root root;
  public readonly int id;
  public StaircaseTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public StaircaseTTCIncarnation incarnation { get { return root.GetStaircaseTTCIncarnation(id); } }
  public void AddObserver(IStaircaseTTCEffectObserver observer) {
    root.AddStaircaseTTCObserver(id, observer);
  }
  public void RemoveObserver(IStaircaseTTCEffectObserver observer) {
    root.RemoveStaircaseTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectStaircaseTTCDelete(id);
  }
  public static StaircaseTTC Null = new StaircaseTTC(null, 0);
  public bool Exists() { return root != null && root.StaircaseTTCExists(id); }
  public bool NullableIs(StaircaseTTC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    if (root.LevelExists(destinationLevel.id)) {
      destinationLevel.FindReachableObjects(foundIds);
    }
  }
  public bool Is(StaircaseTTC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public int portalIndex {
    get { return incarnation.portalIndex; }
  }
  public Level destinationLevel {

    get {
      if (root == null) {
        throw new Exception("Tried to get member destinationLevel of null!");
      }
      return new Level(root, incarnation.destinationLevel);
    }
                         set { root.EffectStaircaseTTCSetDestinationLevel(id, value); }
  }
  public int destinationLevelPortalIndex {
    get { return incarnation.destinationLevelPortalIndex; }
    set { root.EffectStaircaseTTCSetDestinationLevelPortalIndex(id, value); }
  }
}
}
