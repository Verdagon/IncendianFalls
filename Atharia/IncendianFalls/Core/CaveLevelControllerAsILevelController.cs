using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class CaveLevelControllerAsILevelController : ILevelController {
  public readonly CaveLevelController obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public CaveLevelControllerAsILevelController(CaveLevelController obj) {
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
    return new CaveLevelControllerAsILevelController(obj);
  }

         public string GetName() {
    return CaveLevelControllerExtensions.GetName(obj);
  }
  public bool ConsiderCornersAdjacent() {
    return CaveLevelControllerExtensions.ConsiderCornersAdjacent(obj);
  }
  public Void SimpleTrigger(Game game, Superstate superstate, string triggerName) {
    return CaveLevelControllerExtensions.SimpleTrigger(obj, game, superstate, triggerName);
  }
  public Void SimpleUnitTrigger(Game game, Superstate superstate, Unit triggeringUnit, Location location, string triggerName) {
    return CaveLevelControllerExtensions.SimpleUnitTrigger(obj, game, superstate, triggeringUnit, location, triggerName);
  }

}
public static class CaveLevelControllerAsILevelControllerCaster {
  public static CaveLevelControllerAsILevelController AsILevelController(this CaveLevelController obj) {
    return new CaveLevelControllerAsILevelController(obj);
  }
}

}