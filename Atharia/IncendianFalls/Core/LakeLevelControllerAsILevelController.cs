using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class LakeLevelControllerAsILevelController : ILevelController {
  public readonly LakeLevelController obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public LakeLevelControllerAsILevelController(LakeLevelController obj) {
    this.obj = obj;
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    obj.FindReachableObjects(foundIds);
  }
  public bool Is(IDestructible that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(IDestructible that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IDestructible AsIDestructible() {
    return new LakeLevelControllerAsIDestructible(obj);
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
    return new LakeLevelControllerAsILevelController(obj);
  }

         public Void Destruct() {
    return LakeLevelControllerExtensions.Destruct(obj);
  }
  public string GetName() {
    return LakeLevelControllerExtensions.GetName(obj);
  }
  public bool ConsiderCornersAdjacent() {
    return LakeLevelControllerExtensions.ConsiderCornersAdjacent(obj);
  }
  public Void SimpleTrigger(Game game, Superstate superstate, string triggerName) {
    return LakeLevelControllerExtensions.SimpleTrigger(obj, game, superstate, triggerName);
  }
  public Void SimpleUnitTrigger(Game game, Superstate superstate, Unit triggeringUnit, Location location, string triggerName) {
    return LakeLevelControllerExtensions.SimpleUnitTrigger(obj, game, superstate, triggeringUnit, location, triggerName);
  }

}
public static class LakeLevelControllerAsILevelControllerCaster {
  public static LakeLevelControllerAsILevelController AsILevelController(this LakeLevelController obj) {
    return new LakeLevelControllerAsILevelController(obj);
  }
}

}
