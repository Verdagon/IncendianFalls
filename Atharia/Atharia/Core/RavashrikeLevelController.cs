using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class RavashrikeLevelController {
  public readonly Root root;
  public readonly int id;
  public RavashrikeLevelController(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public RavashrikeLevelControllerIncarnation incarnation { get { return root.GetRavashrikeLevelControllerIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IRavashrikeLevelControllerEffectObserver observer) {
    broadcaster.AddRavashrikeLevelControllerObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IRavashrikeLevelControllerEffectObserver observer) {
    broadcaster.RemoveRavashrikeLevelControllerObserver(id, observer);
  }
  public void Delete() {
    root.EffectRavashrikeLevelControllerDelete(id);
  }
  public static RavashrikeLevelController Null = new RavashrikeLevelController(null, 0);
  public bool Exists() { return root != null && root.RavashrikeLevelControllerExists(id); }
  public bool NullableIs(RavashrikeLevelController that) {
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
      violations.Add("Null constraint violated! RavashrikeLevelController#" + id + ".level");
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
  public bool Is(RavashrikeLevelController that) {
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
