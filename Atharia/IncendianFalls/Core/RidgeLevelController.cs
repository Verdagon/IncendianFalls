using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class RidgeLevelController {
  public readonly Root root;
  public readonly int id;
  public RidgeLevelController(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public RidgeLevelControllerIncarnation incarnation { get { return root.GetRidgeLevelControllerIncarnation(id); } }
  public void AddObserver(IRidgeLevelControllerEffectObserver observer) {
    root.AddRidgeLevelControllerObserver(id, observer);
  }
  public void RemoveObserver(IRidgeLevelControllerEffectObserver observer) {
    root.RemoveRidgeLevelControllerObserver(id, observer);
  }
  public void Delete() {
    root.EffectRidgeLevelControllerDelete(id);
  }
  public static RidgeLevelController Null = new RidgeLevelController(null, 0);
  public bool Exists() { return root != null && root.RidgeLevelControllerExists(id); }
  public bool NullableIs(RidgeLevelController that) {
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
      violations.Add("Null constraint violated! RidgeLevelController#" + id + ".level");
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
  public bool Is(RidgeLevelController that) {
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
