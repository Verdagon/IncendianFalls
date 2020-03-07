using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class SotaventoLevelController {
  public readonly Root root;
  public readonly int id;
  public SotaventoLevelController(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public SotaventoLevelControllerIncarnation incarnation { get { return root.GetSotaventoLevelControllerIncarnation(id); } }
  public void AddObserver(ISotaventoLevelControllerEffectObserver observer) {
    root.AddSotaventoLevelControllerObserver(id, observer);
  }
  public void RemoveObserver(ISotaventoLevelControllerEffectObserver observer) {
    root.RemoveSotaventoLevelControllerObserver(id, observer);
  }
  public void Delete() {
    root.EffectSotaventoLevelControllerDelete(id);
  }
  public static SotaventoLevelController Null = new SotaventoLevelController(null, 0);
  public bool Exists() { return root != null && root.SotaventoLevelControllerExists(id); }
  public bool NullableIs(SotaventoLevelController that) {
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
      violations.Add("Null constraint violated! SotaventoLevelController#" + id + ".level");
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
  public bool Is(SotaventoLevelController that) {
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
