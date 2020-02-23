using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class NullIReactingToAttacksUC : IReactingToAttacksUC {
  public static NullIReactingToAttacksUC Null = new NullIReactingToAttacksUC();

  public Root root { get { return null; } }
  public int id { get { return 0; } }
  public void Delete() {
    throw new Exception("Can't delete a null!");
  }
  public bool Exists() { return false; }
  public bool Is(IReactingToAttacksUC that) {
    throw new Exception("Called Is on a null!");
  }
  public void FindReachableObjects(SortedSet<int> foundIds) { }
  public bool NullableIs(IReactingToAttacksUC that) {
    return !that.Exists();
  }
  public IReactingToAttacksUC AsIReactingToAttacksUC() {
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

  public bool React(Game game, Superstate superstate, Unit unit, Unit attacker) {
    throw new Exception("Called React on a null!");
  }
             
  public Void Destruct() {
    throw new Exception("Called Destruct on a null!");
  }
             }
}
