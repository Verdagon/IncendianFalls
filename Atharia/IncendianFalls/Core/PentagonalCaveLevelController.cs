using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class PentagonalCaveLevelController {
  public readonly Root root;
  public readonly int id;
  public PentagonalCaveLevelController(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public PentagonalCaveLevelControllerIncarnation incarnation { get { return root.GetPentagonalCaveLevelControllerIncarnation(id); } }
  public void AddObserver(IPentagonalCaveLevelControllerEffectObserver observer) {
    root.AddPentagonalCaveLevelControllerObserver(id, observer);
  }
  public void RemoveObserver(IPentagonalCaveLevelControllerEffectObserver observer) {
    root.RemovePentagonalCaveLevelControllerObserver(id, observer);
  }
  public void Delete() {
    root.EffectPentagonalCaveLevelControllerDelete(id);
  }
  public static PentagonalCaveLevelController Null = new PentagonalCaveLevelController(null, 0);
  public bool Exists() { return root != null && root.PentagonalCaveLevelControllerExists(id); }
  public bool NullableIs(PentagonalCaveLevelController that) {
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
      violations.Add("Null constraint violated! PentagonalCaveLevelController#" + id + ".level");
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
  public bool Is(PentagonalCaveLevelController that) {
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
  public int depth {
    get { return incarnation.depth; }
  }
}
}
