using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class CaveLevelController {
  public readonly Root root;
  public readonly int id;
  public CaveLevelController(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public CaveLevelControllerIncarnation incarnation { get { return root.GetCaveLevelControllerIncarnation(id); } }
  public void AddObserver(ICaveLevelControllerEffectObserver observer) {
    root.AddCaveLevelControllerObserver(id, observer);
  }
  public void RemoveObserver(ICaveLevelControllerEffectObserver observer) {
    root.RemoveCaveLevelControllerObserver(id, observer);
  }
  public void Delete() {
    root.EffectCaveLevelControllerDelete(id);
  }
  public static CaveLevelController Null = new CaveLevelController(null, 0);
  public bool Exists() { return root != null && root.CaveLevelControllerExists(id); }
  public bool NullableIs(CaveLevelController that) {
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
      violations.Add("Null constraint violated! CaveLevelController#" + id + ".level");
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
  public bool Is(CaveLevelController that) {
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
