using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class AncientTownLevelControllerAsILevelController : ILevelController {
  public readonly AncientTownLevelController obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public AncientTownLevelControllerAsILevelController(AncientTownLevelController obj) {
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
    return new AncientTownLevelControllerAsILevelController(obj);
  }

         public string GetName() {
    return AncientTownLevelControllerExtensions.GetName(obj);
  }
  public bool ConsiderCornersAdjacent() {
    return AncientTownLevelControllerExtensions.ConsiderCornersAdjacent(obj);
  }
  public Void SimpleTrigger(Game game, Superstate superstate, string triggerName) {
    return AncientTownLevelControllerExtensions.SimpleTrigger(obj, game, superstate, triggerName);
  }
  public Void SimpleUnitTrigger(Game game, Superstate superstate, Unit triggeringUnit, Location location, string triggerName) {
    return AncientTownLevelControllerExtensions.SimpleUnitTrigger(obj, game, superstate, triggeringUnit, location, triggerName);
  }

}
public static class AncientTownLevelControllerAsILevelControllerCaster {
  public static AncientTownLevelControllerAsILevelController AsILevelController(this AncientTownLevelController obj) {
    return new AncientTownLevelControllerAsILevelController(obj);
  }
}

}
