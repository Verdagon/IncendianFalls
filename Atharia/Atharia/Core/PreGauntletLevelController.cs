using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class PreGauntletLevelController {
  public readonly Root root;
  public readonly int id;
  public PreGauntletLevelController(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public PreGauntletLevelControllerIncarnation incarnation { get { return root.GetPreGauntletLevelControllerIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IPreGauntletLevelControllerEffectObserver observer) {
    broadcaster.AddPreGauntletLevelControllerObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IPreGauntletLevelControllerEffectObserver observer) {
    broadcaster.RemovePreGauntletLevelControllerObserver(id, observer);
  }
  public void Delete() {
    root.EffectPreGauntletLevelControllerDelete(id);
  }
  public static PreGauntletLevelController Null = new PreGauntletLevelController(null, 0);
  public bool Exists() { return root != null && root.PreGauntletLevelControllerExists(id); }
  public bool NullableIs(PreGauntletLevelController that) {
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
      violations.Add("Null constraint violated! PreGauntletLevelController#" + id + ".level");
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
  public bool Is(PreGauntletLevelController that) {
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
