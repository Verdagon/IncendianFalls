using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class RetreatLevelController {
  public readonly Root root;
  public readonly int id;
  public RetreatLevelController(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public RetreatLevelControllerIncarnation incarnation { get { return root.GetRetreatLevelControllerIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IRetreatLevelControllerEffectObserver observer) {
    broadcaster.AddRetreatLevelControllerObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IRetreatLevelControllerEffectObserver observer) {
    broadcaster.RemoveRetreatLevelControllerObserver(id, observer);
  }
  public void Delete() {
    root.EffectRetreatLevelControllerDelete(id);
  }
  public static RetreatLevelController Null = new RetreatLevelController(null, 0);
  public bool Exists() { return root != null && root.RetreatLevelControllerExists(id); }
  public bool NullableIs(RetreatLevelController that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {

    if (!root.LevelExists(level.id)) {
      violations.Add("Null constraint violated! RetreatLevelController#" + id + ".level");
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    if (root.LevelExists(level.id)) {
      level.FindReachableObjects(foundIds);
    }
  }
  public bool Is(RetreatLevelController that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public Level level {

    get {
      if (root == null) {
        throw new Exception("Tried to get member level of null!");
      }
      return new Level(root, incarnation.level);
    }
                       }
}
}
