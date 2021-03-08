using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class TutorialDefyCounterUCAsIUnitComponent : IUnitComponent {
  public readonly TutorialDefyCounterUC obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public TutorialDefyCounterUCAsIUnitComponent(TutorialDefyCounterUC obj) {
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
    return new TutorialDefyCounterUCAsIDestructible(obj);
  }
  public bool Is(IUnitComponent that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(IUnitComponent that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IUnitComponent AsIUnitComponent() {
    return new TutorialDefyCounterUCAsIUnitComponent(obj);
  }
  public bool Is(IImpulsePostReactor that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(IImpulsePostReactor that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IImpulsePostReactor AsIImpulsePostReactor() {
    return new TutorialDefyCounterUCAsIImpulsePostReactor(obj);
  }

         public Void Destruct() {
    return TutorialDefyCounterUCExtensions.Destruct(obj);
  }

}
public static class TutorialDefyCounterUCAsIUnitComponentCaster {
  public static TutorialDefyCounterUCAsIUnitComponent AsIUnitComponent(this TutorialDefyCounterUC obj) {
    return new TutorialDefyCounterUCAsIUnitComponent(obj);
  }
}

}
