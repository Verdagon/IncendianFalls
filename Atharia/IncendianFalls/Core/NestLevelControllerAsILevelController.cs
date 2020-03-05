using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class NestLevelControllerAsILevelController : ILevelController {
  public readonly NestLevelController obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public NestLevelControllerAsILevelController(NestLevelController obj) {
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
    return new NestLevelControllerAsILevelController(obj);
  }

         public string GetName() {
    return NestLevelControllerExtensions.GetName(obj);
  }
  public bool ConsiderCornersAdjacent() {
    return NestLevelControllerExtensions.ConsiderCornersAdjacent(obj);
  }
  public Void SimpleTrigger(Game game, Superstate superstate, string triggerName) {
    return NestLevelControllerExtensions.SimpleTrigger(obj, game, superstate, triggerName);
  }
  public Void SimpleUnitTrigger(Game game, Superstate superstate, Unit triggeringUnit, Location location, string triggerName) {
    return NestLevelControllerExtensions.SimpleUnitTrigger(obj, game, superstate, triggeringUnit, location, triggerName);
  }

}
public static class NestLevelControllerAsILevelControllerCaster {
  public static NestLevelControllerAsILevelController AsILevelController(this NestLevelController obj) {
    return new NestLevelControllerAsILevelController(obj);
  }
}

}
