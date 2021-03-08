using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class NullIImpulsePreReactor : IImpulsePreReactor {
  public static NullIImpulsePreReactor Null = new NullIImpulsePreReactor();

  public Root root { get { return null; } }
  public int id { get { return 0; } }
  public void Delete() {
    throw new Exception("Can't delete a null!");
  }
  public bool Exists() { return false; }
  public bool Is(IImpulsePreReactor that) {
    throw new Exception("Called Is on a null!");
  }
  public void FindReachableObjects(SortedSet<int> foundIds) { }
  public bool NullableIs(IImpulsePreReactor that) {
    return !that.Exists();
  }
  public IImpulsePreReactor AsIImpulsePreReactor() {
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

  public Void BeforeImpulse(Game game, Superstate superstate, Unit unit, IAICapabilityUC originatingCapability, IImpulse impulse) {
    throw new Exception("Called BeforeImpulse on a null!");
  }
             
  public Void Destruct() {
    throw new Exception("Called Destruct on a null!");
  }
             }
}
