using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class NullIInertiaItem : IInertiaItem {
  public static NullIInertiaItem Null = new NullIInertiaItem();

  public Root root { get { return null; } }
  public int id { get { return 0; } }
  public void Delete() {
    throw new Exception("Can't delete a null!");
  }
  public bool Exists() { return false; }
  public bool Is(IInertiaItem that) {
    throw new Exception("Called Is on a null!");
  }
  public void FindReachableObjects(SortedSet<int> foundIds) { }
  public bool NullableIs(IInertiaItem that) {
    return !that.Exists();
  }
  public IInertiaItem AsIInertiaItem() {
    return this;
  }
         public bool Is(IDestructible that) {
    throw new Exception("Called Is on a null!");
  }
  public bool NullableIs(IDestructible that) {
    return !that.Exists();
  }
  public IDestructible AsIDestructible() {
    return NullIDestructible.Null;
  }
  public bool Is(IUnitComponent that) {
    throw new Exception("Called Is on a null!");
  }
  public bool NullableIs(IUnitComponent that) {
    return !that.Exists();
  }
  public IUnitComponent AsIUnitComponent() {
    return NullIUnitComponent.Null;
  }
  public bool Is(IItem that) {
    throw new Exception("Called Is on a null!");
  }
  public bool NullableIs(IItem that) {
    return !that.Exists();
  }
  public IItem AsIItem() {
    return NullIItem.Null;
  }

  public int AffectInertia(int inertia) {
    throw new Exception("Called AffectInertia on a null!");
  }
             
  public IItem ClonifyAndReturnNewReal(Root newRoot) {
    throw new Exception("Called ClonifyAndReturnNewReal on a null!");
  }
             
  public Void Destruct() {
    throw new Exception("Called Destruct on a null!");
  }
             }
}
