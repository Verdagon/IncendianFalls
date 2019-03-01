using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class NullIPreActingUC : IPreActingUC {
  public static NullIPreActingUC Null = new NullIPreActingUC();

  public Root root { get { return null; } }
  public int id { get { return 0; } }
  public void Delete() {
    throw new Exception("Can't delete a null!");
  }
  public bool Exists() { return false; }
  public bool Is(IPreActingUC that) {
    throw new Exception("Called Is on a null!");
  }
  public void FindReachableObjects(SortedSet<int> foundIds) { }
  public bool NullableIs(IPreActingUC that) {
    return !that.Exists();
  }
  public IPreActingUC AsIPreActingUC() {
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

  public bool PreAct(Game game, LiveUnitByLocationMap liveUnitByLocationMap, Unit unit) {
    throw new Exception("Called PreAct on a null!");
  }
             
  public Void Destruct() {
    throw new Exception("Called Destruct on a null!");
  }
             }
}
