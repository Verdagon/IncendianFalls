using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class CliffLevelController {
  public readonly Root root;
  public readonly int id;
  public CliffLevelController(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public CliffLevelControllerIncarnation incarnation { get { return root.GetCliffLevelControllerIncarnation(id); } }
  public void AddObserver(ICliffLevelControllerEffectObserver observer) {
    root.AddCliffLevelControllerObserver(id, observer);
  }
  public void RemoveObserver(ICliffLevelControllerEffectObserver observer) {
    root.RemoveCliffLevelControllerObserver(id, observer);
  }
  public void Delete() {
    root.EffectCliffLevelControllerDelete(id);
  }
  public static CliffLevelController Null = new CliffLevelController(null, 0);
  public bool Exists() { return root != null && root.CliffLevelControllerExists(id); }
  public bool NullableIs(CliffLevelController that) {
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
      violations.Add("Null constraint violated! CliffLevelController#" + id + ".level");
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
  public bool Is(CliffLevelController that) {
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
  public int depth {
    get { return incarnation.depth; }
  }
}
}
