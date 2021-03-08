using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class SotaventoLevelControllerAsIDestructible : IDestructible {
  public readonly SotaventoLevelController obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public SotaventoLevelControllerAsIDestructible(SotaventoLevelController obj) {
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
    return new SotaventoLevelControllerAsIDestructible(obj);
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
    return new SotaventoLevelControllerAsILevelController(obj);
  }

         public Void Destruct() {
    return SotaventoLevelControllerExtensions.Destruct(obj);
  }

}
public static class SotaventoLevelControllerAsIDestructibleCaster {
  public static SotaventoLevelControllerAsIDestructible AsIDestructible(this SotaventoLevelController obj) {
    return new SotaventoLevelControllerAsIDestructible(obj);
  }
}

}
