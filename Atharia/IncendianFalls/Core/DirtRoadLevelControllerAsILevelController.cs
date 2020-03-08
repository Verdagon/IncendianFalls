using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class DirtRoadLevelControllerAsILevelController : ILevelController {
  public readonly DirtRoadLevelController obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public DirtRoadLevelControllerAsILevelController(DirtRoadLevelController obj) {
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
    return new DirtRoadLevelControllerAsILevelController(obj);
  }

         public string GetName() {
    return DirtRoadLevelControllerExtensions.GetName(obj);
  }
  public bool ConsiderCornersAdjacent() {
    return DirtRoadLevelControllerExtensions.ConsiderCornersAdjacent(obj);
  }
  public Void SimpleTrigger(Game game, Superstate superstate, string triggerName) {
    return DirtRoadLevelControllerExtensions.SimpleTrigger(obj, game, superstate, triggerName);
  }
  public Void SimpleUnitTrigger(Game game, Superstate superstate, Unit triggeringUnit, Location location, string triggerName) {
    return DirtRoadLevelControllerExtensions.SimpleUnitTrigger(obj, game, superstate, triggeringUnit, location, triggerName);
  }

}
public static class DirtRoadLevelControllerAsILevelControllerCaster {
  public static DirtRoadLevelControllerAsILevelController AsILevelController(this DirtRoadLevelController obj) {
    return new DirtRoadLevelControllerAsILevelController(obj);
  }
}

}
