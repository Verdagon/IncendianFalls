using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class NullIOffenseFactorUC : IOffenseFactorUC {
  public static NullIOffenseFactorUC Null = new NullIOffenseFactorUC();

  public Root root { get { return null; } }
  public int id { get { return 0; } }
  public void Delete() {
    throw new Exception("Can't delete a null!");
  }
  public bool Exists() { return false; }
  public bool Is(IOffenseFactorUC that) {
    throw new Exception("Called Is on a null!");
  }
  public void FindReachableObjects(SortedSet<int> foundIds) { }
  public bool NullableIs(IOffenseFactorUC that) {
    return !that.Exists();
  }
  public IOffenseFactorUC AsIOffenseFactorUC() {
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

  public int GetOutgoingDamageAddConstant() {
    throw new Exception("Called GetOutgoingDamageAddConstant on a null!");
  }
             
  public int GetOutgoingDamageMultiplierPercent() {
    throw new Exception("Called GetOutgoingDamageMultiplierPercent on a null!");
  }
             
  public Void Destruct() {
    throw new Exception("Called Destruct on a null!");
  }
             }
}
