using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class DirtRoadLevelController {
  public readonly Root root;
  public readonly int id;
  public DirtRoadLevelController(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DirtRoadLevelControllerIncarnation incarnation { get { return root.GetDirtRoadLevelControllerIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IDirtRoadLevelControllerEffectObserver observer) {
    broadcaster.AddDirtRoadLevelControllerObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IDirtRoadLevelControllerEffectObserver observer) {
    broadcaster.RemoveDirtRoadLevelControllerObserver(id, observer);
  }
  public void Delete() {
    root.EffectDirtRoadLevelControllerDelete(id);
  }
  public static DirtRoadLevelController Null = new DirtRoadLevelController(null, 0);
  public bool Exists() { return root != null && root.DirtRoadLevelControllerExists(id); }
  public bool NullableIs(DirtRoadLevelController that) {
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
      violations.Add("Null constraint violated! DirtRoadLevelController#" + id + ".level");
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
  public bool Is(DirtRoadLevelController that) {
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
