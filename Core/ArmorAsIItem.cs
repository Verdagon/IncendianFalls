using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class ArmorAsIItem : IItem {
  public readonly Armor obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public ArmorAsIItem(Armor obj) {
    this.obj = obj;
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    obj.FindReachableObjects(foundIds);
  }
  public bool Is(IItem that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(IItem that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IItem AsIItem() {
    return new ArmorAsIItem(obj);
  }

         public int AffectIncomingDamage(int incomingDamage) {
    return ArmorExtensions.AffectIncomingDamage(obj, incomingDamage);
  }
  public int AffectOutgoingDamage(int outgoingDamage) {
    return ArmorExtensions.AffectOutgoingDamage(obj, outgoingDamage);
  }

}
public static class ArmorAsIItemCaster {
  public static ArmorAsIItem AsIItem(this Armor obj) {
    return new ArmorAsIItem(obj);
  }
}

}
