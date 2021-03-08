using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class SquareCaveLevelController {
  public readonly Root root;
  public readonly int id;
  public SquareCaveLevelController(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public SquareCaveLevelControllerIncarnation incarnation { get { return root.GetSquareCaveLevelControllerIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, ISquareCaveLevelControllerEffectObserver observer) {
    broadcaster.AddSquareCaveLevelControllerObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, ISquareCaveLevelControllerEffectObserver observer) {
    broadcaster.RemoveSquareCaveLevelControllerObserver(id, observer);
  }
  public void Delete() {
    root.EffectSquareCaveLevelControllerDelete(id);
  }
  public static SquareCaveLevelController Null = new SquareCaveLevelController(null, 0);
  public bool Exists() { return root != null && root.SquareCaveLevelControllerExists(id); }
  public bool NullableIs(SquareCaveLevelController that) {
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
      violations.Add("Null constraint violated! SquareCaveLevelController#" + id + ".level");
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
  public bool Is(SquareCaveLevelController that) {
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
