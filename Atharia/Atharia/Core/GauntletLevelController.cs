using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class GauntletLevelController {
  public readonly Root root;
  public readonly int id;
  public GauntletLevelController(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public GauntletLevelControllerIncarnation incarnation { get { return root.GetGauntletLevelControllerIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IGauntletLevelControllerEffectObserver observer) {
    broadcaster.AddGauntletLevelControllerObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IGauntletLevelControllerEffectObserver observer) {
    broadcaster.RemoveGauntletLevelControllerObserver(id, observer);
  }
  public void Delete() {
    root.EffectGauntletLevelControllerDelete(id);
  }
  public static GauntletLevelController Null = new GauntletLevelController(null, 0);
  public bool Exists() { return root != null && root.GauntletLevelControllerExists(id); }
  public bool NullableIs(GauntletLevelController that) {
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
      violations.Add("Null constraint violated! GauntletLevelController#" + id + ".level");
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
  public bool Is(GauntletLevelController that) {
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
