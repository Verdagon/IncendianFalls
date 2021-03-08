using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class NullIInteractableTTC : IInteractableTTC {
  public static NullIInteractableTTC Null = new NullIInteractableTTC();

  public Root root { get { return null; } }
  public int id { get { return 0; } }
  public void Delete() {
    throw new Exception("Can't delete a null!");
  }
  public bool Exists() { return false; }
  public bool Is(IInteractableTTC that) {
    throw new Exception("Called Is on a null!");
  }
  public void FindReachableObjects(SortedSet<int> foundIds) { }
  public bool NullableIs(IInteractableTTC that) {
    return !that.Exists();
  }
  public IInteractableTTC AsIInteractableTTC() {
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

  public string Interact(IncendianFalls.SSContext context, Game game, Superstate superstate, Unit interactingUnit, Location containingTileLocation) {
    throw new Exception("Called Interact on a null!");
  }
             
  public Void Destruct() {
    throw new Exception("Called Destruct on a null!");
  }
             }
}
