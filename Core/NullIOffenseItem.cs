using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class NullIOffenseItem : IOffenseItem {
  public static NullIOffenseItem Null = new NullIOffenseItem();

  public Root root { get { return null; } }
  public int id { get { return 0; } }
  public void Delete() {
    throw new Exception("Can't delete a null!");
  }
  public bool Exists() { return false; }
  public bool Is(IOffenseItem that) {
    throw new Exception("Called Is on a null!");
  }
  public void FindReachableObjects(SortedSet<int> foundIds) { }
  public bool NullableIs(IOffenseItem that) {
    return !that.Exists();
  }
  public IOffenseItem AsIOffenseItem() {
    return this;
  }
         public bool Is(ITerrainTileComponent that) {
    throw new Exception("Called Is on a null!");
  }
  public bool NullableIs(ITerrainTileComponent that) {
    return !that.Exists();
  }
  public ITerrainTileComponent AsITerrainTileComponent() {
    return NullITerrainTileComponent.Null;
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

  public int AffectOutgoingDamage(int outgoingDamage) {
    throw new Exception("Called AffectOutgoingDamage on a null!");
  }
             
  public Void Destruct() {
    throw new Exception("Called Destruct on a null!");
  }
             }
}
