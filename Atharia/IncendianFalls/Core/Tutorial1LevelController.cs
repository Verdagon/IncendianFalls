using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class Tutorial1LevelController {
  public readonly Root root;
  public readonly int id;
  public Tutorial1LevelController(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public Tutorial1LevelControllerIncarnation incarnation { get { return root.GetTutorial1LevelControllerIncarnation(id); } }
  public void AddObserver(ITutorial1LevelControllerEffectObserver observer) {
    root.AddTutorial1LevelControllerObserver(id, observer);
  }
  public void RemoveObserver(ITutorial1LevelControllerEffectObserver observer) {
    root.RemoveTutorial1LevelControllerObserver(id, observer);
  }
  public void Delete() {
    root.EffectTutorial1LevelControllerDelete(id);
  }
  public static Tutorial1LevelController Null = new Tutorial1LevelController(null, 0);
  public bool Exists() { return root != null && root.Tutorial1LevelControllerExists(id); }
  public bool NullableIs(Tutorial1LevelController that) {
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
      violations.Add("Null constraint violated! Tutorial1LevelController#" + id + ".level");
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
  public bool Is(Tutorial1LevelController that) {
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
