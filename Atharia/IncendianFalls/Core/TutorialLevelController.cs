using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class TutorialLevelController {
  public readonly Root root;
  public readonly int id;
  public TutorialLevelController(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public TutorialLevelControllerIncarnation incarnation { get { return root.GetTutorialLevelControllerIncarnation(id); } }
  public void AddObserver(ITutorialLevelControllerEffectObserver observer) {
    root.AddTutorialLevelControllerObserver(id, observer);
  }
  public void RemoveObserver(ITutorialLevelControllerEffectObserver observer) {
    root.RemoveTutorialLevelControllerObserver(id, observer);
  }
  public void Delete() {
    root.EffectTutorialLevelControllerDelete(id);
  }
  public static TutorialLevelController Null = new TutorialLevelController(null, 0);
  public bool Exists() { return root != null && root.TutorialLevelControllerExists(id); }
  public bool NullableIs(TutorialLevelController that) {
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
      violations.Add("Null constraint violated! TutorialLevelController#" + id + ".level");
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
  public bool Is(TutorialLevelController that) {
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
