using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class PreGauntletLevelControllerAsILevelController : ILevelController {
  public readonly PreGauntletLevelController obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public PreGauntletLevelControllerAsILevelController(PreGauntletLevelController obj) {
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
    return new PreGauntletLevelControllerAsILevelController(obj);
  }

         public string GetName() {
    return PreGauntletLevelControllerExtensions.GetName(obj);
  }
  public bool ConsiderCornersAdjacent() {
    return PreGauntletLevelControllerExtensions.ConsiderCornersAdjacent(obj);
  }
  public Void SimpleTrigger(Game game, Superstate superstate, Unit triggeringUnit, Location location, string triggerName) {
    return PreGauntletLevelControllerExtensions.SimpleTrigger(obj, game, superstate, triggeringUnit, location, triggerName);
  }

}
public static class PreGauntletLevelControllerAsILevelControllerCaster {
  public static PreGauntletLevelControllerAsILevelController AsILevelController(this PreGauntletLevelController obj) {
    return new PreGauntletLevelControllerAsILevelController(obj);
  }
}

}
