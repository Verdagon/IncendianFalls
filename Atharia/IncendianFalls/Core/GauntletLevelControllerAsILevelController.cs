using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class GauntletLevelControllerAsILevelController : ILevelController {
  public readonly GauntletLevelController obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public GauntletLevelControllerAsILevelController(GauntletLevelController obj) {
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
    return new GauntletLevelControllerAsILevelController(obj);
  }

         public string GetName() {
    return GauntletLevelControllerExtensions.GetName(obj);
  }
  public bool ConsiderCornersAdjacent() {
    return GauntletLevelControllerExtensions.ConsiderCornersAdjacent(obj);
  }

}
public static class GauntletLevelControllerAsILevelControllerCaster {
  public static GauntletLevelControllerAsILevelController AsILevelController(this GauntletLevelController obj) {
    return new GauntletLevelControllerAsILevelController(obj);
  }
}

}
