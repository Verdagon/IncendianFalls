using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class NestLevelController {
  public readonly Root root;
  public readonly int id;
  public NestLevelController(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public NestLevelControllerIncarnation incarnation { get { return root.GetNestLevelControllerIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, INestLevelControllerEffectObserver observer) {
    broadcaster.AddNestLevelControllerObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, INestLevelControllerEffectObserver observer) {
    broadcaster.RemoveNestLevelControllerObserver(id, observer);
  }
  public void Delete() {
    root.EffectNestLevelControllerDelete(id);
  }
  public static NestLevelController Null = new NestLevelController(null, 0);
  public bool Exists() { return root != null && root.NestLevelControllerExists(id); }
  public bool NullableIs(NestLevelController that) {
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
      violations.Add("Null constraint violated! NestLevelController#" + id + ".level");
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
  public bool Is(NestLevelController that) {
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
