using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class SquareCaveLevelControllerAsILevelController : ILevelController {
  public readonly SquareCaveLevelController obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public SquareCaveLevelControllerAsILevelController(SquareCaveLevelController obj) {
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
    return new SquareCaveLevelControllerAsILevelController(obj);
  }

         public string GetName() {
    return SquareCaveLevelControllerExtensions.GetName(obj);
  }
  public bool ConsiderCornersAdjacent() {
    return SquareCaveLevelControllerExtensions.ConsiderCornersAdjacent(obj);
  }

}
public static class SquareCaveLevelControllerAsILevelControllerCaster {
  public static SquareCaveLevelControllerAsILevelController AsILevelController(this SquareCaveLevelController obj) {
    return new SquareCaveLevelControllerAsILevelController(obj);
  }
}

}
