using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class JumpingCaveLevelController {
  public readonly Root root;
  public readonly int id;
  public JumpingCaveLevelController(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public JumpingCaveLevelControllerIncarnation incarnation { get { return root.GetJumpingCaveLevelControllerIncarnation(id); } }
  public void AddObserver(EffectBroadcaster broadcaster, IJumpingCaveLevelControllerEffectObserver observer) {
    broadcaster.AddJumpingCaveLevelControllerObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IJumpingCaveLevelControllerEffectObserver observer) {
    broadcaster.RemoveJumpingCaveLevelControllerObserver(id, observer);
  }
  public void Delete() {
    root.EffectJumpingCaveLevelControllerDelete(id);
  }
  public static JumpingCaveLevelController Null = new JumpingCaveLevelController(null, 0);
  public bool Exists() { return root != null && root.JumpingCaveLevelControllerExists(id); }
  public bool NullableIs(JumpingCaveLevelController that) {
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
      violations.Add("Null constraint violated! JumpingCaveLevelController#" + id + ".level");
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
  public bool Is(JumpingCaveLevelController that) {
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
