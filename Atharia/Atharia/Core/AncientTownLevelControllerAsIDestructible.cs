using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class AncientTownLevelControllerAsIDestructible : IDestructible {
  public readonly AncientTownLevelController obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public AncientTownLevelControllerAsIDestructible(AncientTownLevelController obj) {
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
    return new AncientTownLevelControllerAsIDestructible(obj);
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

         public Void Destruct() {
    return AncientTownLevelControllerExtensions.Destruct(obj);
  }

}
public static class AncientTownLevelControllerAsIDestructibleCaster {
  public static AncientTownLevelControllerAsIDestructible AsIDestructible(this AncientTownLevelController obj) {
    return new AncientTownLevelControllerAsIDestructible(obj);
  }
}

}
