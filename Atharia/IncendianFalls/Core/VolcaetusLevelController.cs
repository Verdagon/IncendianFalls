using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class VolcaetusLevelController {
  public readonly Root root;
  public readonly int id;
  public VolcaetusLevelController(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public VolcaetusLevelControllerIncarnation incarnation { get { return root.GetVolcaetusLevelControllerIncarnation(id); } }
  public void AddObserver(IVolcaetusLevelControllerEffectObserver observer) {
    root.AddVolcaetusLevelControllerObserver(id, observer);
  }
  public void RemoveObserver(IVolcaetusLevelControllerEffectObserver observer) {
    root.RemoveVolcaetusLevelControllerObserver(id, observer);
  }
  public void Delete() {
    root.EffectVolcaetusLevelControllerDelete(id);
  }
  public static VolcaetusLevelController Null = new VolcaetusLevelController(null, 0);
  public bool Exists() { return root != null && root.VolcaetusLevelControllerExists(id); }
  public bool NullableIs(VolcaetusLevelController that) {
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
      violations.Add("Null constraint violated! VolcaetusLevelController#" + id + ".level");
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
  public bool Is(VolcaetusLevelController that) {
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
