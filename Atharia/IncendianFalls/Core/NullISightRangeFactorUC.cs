using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class NullISightRangeFactorUC : ISightRangeFactorUC {
  public static NullISightRangeFactorUC Null = new NullISightRangeFactorUC();

  public Root root { get { return null; } }
  public int id { get { return 0; } }
  public void Delete() {
    throw new Exception("Can't delete a null!");
  }
  public bool Exists() { return false; }
  public bool Is(ISightRangeFactorUC that) {
    throw new Exception("Called Is on a null!");
  }
  public void FindReachableObjects(SortedSet<int> foundIds) { }
  public bool NullableIs(ISightRangeFactorUC that) {
    return !that.Exists();
  }
  public ISightRangeFactorUC AsISightRangeFactorUC() {
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

  public int GetSightRangeAddConstant() {
    throw new Exception("Called GetSightRangeAddConstant on a null!");
  }
             
  public int GetSightRangeMultiplierPercent() {
    throw new Exception("Called GetSightRangeMultiplierPercent on a null!");
  }
             
  public Void Destruct() {
    throw new Exception("Called Destruct on a null!");
  }
             }
}
