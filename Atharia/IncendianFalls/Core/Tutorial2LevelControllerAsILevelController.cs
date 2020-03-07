using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class Tutorial2LevelControllerAsILevelController : ILevelController {
  public readonly Tutorial2LevelController obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public Tutorial2LevelControllerAsILevelController(Tutorial2LevelController obj) {
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
    return new Tutorial2LevelControllerAsILevelController(obj);
  }

         public string GetName() {
    return Tutorial2LevelControllerExtensions.GetName(obj);
  }
  public bool ConsiderCornersAdjacent() {
    return Tutorial2LevelControllerExtensions.ConsiderCornersAdjacent(obj);
  }
  public Void SimpleTrigger(Game game, Superstate superstate, string triggerName) {
    return Tutorial2LevelControllerExtensions.SimpleTrigger(obj, game, superstate, triggerName);
  }
  public Void SimpleUnitTrigger(Game game, Superstate superstate, Unit triggeringUnit, Location location, string triggerName) {
    return Tutorial2LevelControllerExtensions.SimpleUnitTrigger(obj, game, superstate, triggeringUnit, location, triggerName);
  }

}
public static class Tutorial2LevelControllerAsILevelControllerCaster {
  public static Tutorial2LevelControllerAsILevelController AsILevelController(this Tutorial2LevelController obj) {
    return new Tutorial2LevelControllerAsILevelController(obj);
  }
}

}
