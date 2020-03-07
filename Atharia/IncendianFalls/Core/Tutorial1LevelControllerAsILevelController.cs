using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class Tutorial1LevelControllerAsILevelController : ILevelController {
  public readonly Tutorial1LevelController obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public Tutorial1LevelControllerAsILevelController(Tutorial1LevelController obj) {
    this.obj = obj;
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    obj.FindReachableObjects(foundIds);
  }
  public bool Is(ILevelController that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(ILevelController that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public ILevelController AsILevelController() {
    return new Tutorial1LevelControllerAsILevelController(obj);
  }

         public string GetName() {
    return Tutorial1LevelControllerExtensions.GetName(obj);
  }
  public bool ConsiderCornersAdjacent() {
    return Tutorial1LevelControllerExtensions.ConsiderCornersAdjacent(obj);
  }
  public Void SimpleTrigger(Game game, Superstate superstate, string triggerName) {
    return Tutorial1LevelControllerExtensions.SimpleTrigger(obj, game, superstate, triggerName);
  }
  public Void SimpleUnitTrigger(Game game, Superstate superstate, Unit triggeringUnit, Location location, string triggerName) {
    return Tutorial1LevelControllerExtensions.SimpleUnitTrigger(obj, game, superstate, triggeringUnit, location, triggerName);
  }

}
public static class Tutorial1LevelControllerAsILevelControllerCaster {
  public static Tutorial1LevelControllerAsILevelController AsILevelController(this Tutorial1LevelController obj) {
    return new Tutorial1LevelControllerAsILevelController(obj);
  }
}

}
