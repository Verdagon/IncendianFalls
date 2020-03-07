using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class LakeLevelController {
  public readonly Root root;
  public readonly int id;
  public LakeLevelController(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public LakeLevelControllerIncarnation incarnation { get { return root.GetLakeLevelControllerIncarnation(id); } }
  public void AddObserver(ILakeLevelControllerEffectObserver observer) {
    root.AddLakeLevelControllerObserver(id, observer);
  }
  public void RemoveObserver(ILakeLevelControllerEffectObserver observer) {
    root.RemoveLakeLevelControllerObserver(id, observer);
  }
  public void Delete() {
    root.EffectLakeLevelControllerDelete(id);
  }
  public static LakeLevelController Null = new LakeLevelController(null, 0);
  public bool Exists() { return root != null && root.LakeLevelControllerExists(id); }
  public bool NullableIs(LakeLevelController that) {
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
      violations.Add("Null constraint violated! LakeLevelController#" + id + ".level");
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
  public bool Is(LakeLevelController that) {
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
