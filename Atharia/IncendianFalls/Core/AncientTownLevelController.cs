using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class AncientTownLevelController {
  public readonly Root root;
  public readonly int id;
  public AncientTownLevelController(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public AncientTownLevelControllerIncarnation incarnation { get { return root.GetAncientTownLevelControllerIncarnation(id); } }
  public void AddObserver(IAncientTownLevelControllerEffectObserver observer) {
    root.AddAncientTownLevelControllerObserver(id, observer);
  }
  public void RemoveObserver(IAncientTownLevelControllerEffectObserver observer) {
    root.RemoveAncientTownLevelControllerObserver(id, observer);
  }
  public void Delete() {
    root.EffectAncientTownLevelControllerDelete(id);
  }
  public static AncientTownLevelController Null = new AncientTownLevelController(null, 0);
  public bool Exists() { return root != null && root.AncientTownLevelControllerExists(id); }
  public bool NullableIs(AncientTownLevelController that) {
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
      violations.Add("Null constraint violated! AncientTownLevelController#" + id + ".level");
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
  public bool Is(AncientTownLevelController that) {
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
