using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class BridgesLevelController {
  public readonly Root root;
  public readonly int id;
  public BridgesLevelController(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public BridgesLevelControllerIncarnation incarnation { get { return root.GetBridgesLevelControllerIncarnation(id); } }
  public void AddObserver(IBridgesLevelControllerEffectObserver observer) {
    root.AddBridgesLevelControllerObserver(id, observer);
  }
  public void RemoveObserver(IBridgesLevelControllerEffectObserver observer) {
    root.RemoveBridgesLevelControllerObserver(id, observer);
  }
  public void Delete() {
    root.EffectBridgesLevelControllerDelete(id);
  }
  public static BridgesLevelController Null = new BridgesLevelController(null, 0);
  public bool Exists() { return root != null && root.BridgesLevelControllerExists(id); }
  public bool NullableIs(BridgesLevelController that) {
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
      violations.Add("Null constraint violated! BridgesLevelController#" + id + ".level");
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
  public bool Is(BridgesLevelController that) {
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
