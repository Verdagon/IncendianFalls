using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class PentagonalCaveLevelControllerAsILevelController : ILevelController {
  public readonly PentagonalCaveLevelController obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public PentagonalCaveLevelControllerAsILevelController(PentagonalCaveLevelController obj) {
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
    return new PentagonalCaveLevelControllerAsILevelController(obj);
  }

         public string GetName() {
    return PentagonalCaveLevelControllerExtensions.GetName(obj);
  }
  public bool ConsiderCornersAdjacent() {
    return PentagonalCaveLevelControllerExtensions.ConsiderCornersAdjacent(obj);
  }
  public Void SimpleTrigger(Game game, Superstate superstate, Unit triggeringUnit, Location location, string triggerName) {
    return PentagonalCaveLevelControllerExtensions.SimpleTrigger(obj, game, superstate, triggeringUnit, location, triggerName);
  }

}
public static class PentagonalCaveLevelControllerAsILevelControllerCaster {
  public static PentagonalCaveLevelControllerAsILevelController AsILevelController(this PentagonalCaveLevelController obj) {
    return new PentagonalCaveLevelControllerAsILevelController(obj);
  }
}

}
