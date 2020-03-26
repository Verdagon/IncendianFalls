using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class LevelLinkTTC {
  public readonly Root root;
  public readonly int id;
  public LevelLinkTTC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public LevelLinkTTCIncarnation incarnation { get { return root.GetLevelLinkTTCIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, ILevelLinkTTCEffectObserver observer) {
    broadcaster.AddLevelLinkTTCObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ILevelLinkTTCEffectObserver observer) {
    broadcaster.RemoveLevelLinkTTCObserver(id, observer);
  }
  public void Delete() {
    root.EffectLevelLinkTTCDelete(id);
  }
  public static LevelLinkTTC Null = new LevelLinkTTC(null, 0);
  public bool Exists() { return root != null && root.LevelLinkTTCExists(id); }
  public bool NullableIs(LevelLinkTTC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {

    if (!root.LevelExists(destinationLevel.id)) {
      violations.Add("Null constraint violated! LevelLinkTTC#" + id + ".destinationLevel");
    }
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
  public bool Is(LevelLinkTTC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public bool destroyThisLevel {
    get { return incarnation.destroyThisLevel; }
  }
  public Level destinationLevel {

    get {
      if (root == null) {
        throw new Exception("Tried to get member destinationLevel of null!");
      }
      return new Level(root, incarnation.destinationLevel);
    }
                       }
  public Location destinationLevelLocation {
    get { return incarnation.destinationLevelLocation; }
  }
}
}
