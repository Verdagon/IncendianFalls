using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class VolcaetusLevelControllerAsILevelController : ILevelController {
  public readonly VolcaetusLevelController obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public VolcaetusLevelControllerAsILevelController(VolcaetusLevelController obj) {
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
    return new VolcaetusLevelControllerAsIDestructible(obj);
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
    return new VolcaetusLevelControllerAsILevelController(obj);
  }

         public Void Destruct() {
    return VolcaetusLevelControllerExtensions.Destruct(obj);
  }
  public string GetName() {
    return VolcaetusLevelControllerExtensions.GetName(obj);
  }
  public Void SimpleTrigger(IncendianFalls.SSContext context, Game game, Superstate superstate, string triggerName) {
    return VolcaetusLevelControllerExtensions.SimpleTrigger(obj, context, game, superstate, triggerName);
  }
  public Void SimpleUnitTrigger(IncendianFalls.SSContext context, Game game, Superstate superstate, Unit triggeringUnit, Location location, string triggerName) {
    return VolcaetusLevelControllerExtensions.SimpleUnitTrigger(obj, context, game, superstate, triggeringUnit, location, triggerName);
  }

}
public static class VolcaetusLevelControllerAsILevelControllerCaster {
  public static VolcaetusLevelControllerAsILevelController AsILevelController(this VolcaetusLevelController obj) {
    return new VolcaetusLevelControllerAsILevelController(obj);
  }
}

}
