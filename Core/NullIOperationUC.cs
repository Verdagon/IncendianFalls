using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class NullIOperationUC : IOperationUC {
  public static NullIOperationUC Null = new NullIOperationUC();

  public Root root { get { return null; } }
  public int id { get { return 0; } }
  public void Delete() {
    throw new Exception("Can't delete a null!");
  }
  public bool Exists() { return false; }
  public bool Is(IOperationUC that) {
    throw new Exception("Called Is on a null!");
  }
  public bool NullableIs(IOperationUC that) {
    return !that.Exists();
  }
  public IOperationUC AsIOperationUC() {
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

  public Void OnImpulse(Unit unit, Game game, IImpulse impulse) {
    throw new Exception("Called OnImpulse on a null!");
  }
             
  public Void Destruct() {
    throw new Exception("Called Destruct on a null!");
  }
             }
}
