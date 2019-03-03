using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class NullIItem : IItem {
  public static NullIItem Null = new NullIItem();

  public Root root { get { return null; } }
  public int id { get { return 0; } }
  public void Delete() {
    throw new Exception("Can't delete a null!");
  }
  public bool Exists() { return false; }
  public bool Is(IItem that) {
    throw new Exception("Called Is on a null!");
  }
  public void FindReachableObjects(SortedSet<int> foundIds) { }
  public bool NullableIs(IItem that) {
    return !that.Exists();
  }
  public IItem AsIItem() {
    return this;
  }
       
  public int AffectIncomingDamage(int incomingDamage) {
    throw new Exception("Called AffectIncomingDamage on a null!");
  }
             
  public int AffectOutgoingDamage(int outgoingDamage) {
    throw new Exception("Called AffectOutgoingDamage on a null!");
  }
             }
}
