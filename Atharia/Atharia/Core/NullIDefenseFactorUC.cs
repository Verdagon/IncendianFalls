using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class NullIDefenseFactorUC : IDefenseFactorUC {
  public static NullIDefenseFactorUC Null = new NullIDefenseFactorUC();

  public Root root { get { return null; } }
  public int id { get { return 0; } }
  public void Delete() {
    throw new Exception("Can't delete a null!");
  }
  public bool Exists() { return false; }
  public bool Is(IDefenseFactorUC that) {
    throw new Exception("Called Is on a null!");
  }
  public void FindReachableObjects(SortedSet<int> foundIds) { }
  public bool NullableIs(IDefenseFactorUC that) {
    return !that.Exists();
  }
  public IDefenseFactorUC AsIDefenseFactorUC() {
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

  public int GetIncomingDamageAddConstant() {
    throw new Exception("Called GetIncomingDamageAddConstant on a null!");
  }
             
  public int GetIncomingDamageMultiplierPercent() {
    throw new Exception("Called GetIncomingDamageMultiplierPercent on a null!");
  }
             
  public Void Destruct() {
    throw new Exception("Called Destruct on a null!");
  }
             }
}
