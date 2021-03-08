using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class NullIActingTTC : IActingTTC {
  public static NullIActingTTC Null = new NullIActingTTC();

  public Root root { get { return null; } }
  public int id { get { return 0; } }
  public void Delete() {
    throw new Exception("Can't delete a null!");
  }
  public bool Exists() { return false; }
  public bool Is(IActingTTC that) {
    throw new Exception("Called Is on a null!");
  }
  public void FindReachableObjects(SortedSet<int> foundIds) { }
  public bool NullableIs(IActingTTC that) {
    return !that.Exists();
  }
  public IActingTTC AsIActingTTC() {
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
  public bool Is(ITerrainTileComponent that) {
    throw new Exception("Called Is on a null!");
  }
  public bool NullableIs(ITerrainTileComponent that) {
    return !that.Exists();
  }
  public ITerrainTileComponent AsITerrainTileComponent() {
    return NullITerrainTileComponent.Null;
  }

  public Void Act(Game game, Superstate superstate, Location containingTileLocation) {
    throw new Exception("Called Act on a null!");
  }
             
  public Void Destruct() {
    throw new Exception("Called Destruct on a null!");
  }
             }
}
