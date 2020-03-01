using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class TutorialLevelControllerAsILevelController : ILevelController {
  public readonly TutorialLevelController obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public TutorialLevelControllerAsILevelController(TutorialLevelController obj) {
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
    return new TutorialLevelControllerAsILevelController(obj);
  }

         public string GetName() {
    return TutorialLevelControllerExtensions.GetName(obj);
  }
  public bool ConsiderCornersAdjacent() {
    return TutorialLevelControllerExtensions.ConsiderCornersAdjacent(obj);
  }
  public Void SimpleTrigger(Game game, Superstate superstate, Unit triggeringUnit, Location location, string triggerName) {
    return TutorialLevelControllerExtensions.SimpleTrigger(obj, game, superstate, triggeringUnit, location, triggerName);
  }

}
public static class TutorialLevelControllerAsILevelControllerCaster {
  public static TutorialLevelControllerAsILevelController AsILevelController(this TutorialLevelController obj) {
    return new TutorialLevelControllerAsILevelController(obj);
  }
}

}
