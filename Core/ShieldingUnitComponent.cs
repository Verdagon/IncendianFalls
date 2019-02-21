using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class ShieldingUnitComponent {
  public readonly Root root;
  public readonly int id;
  public ShieldingUnitComponent(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ShieldingUnitComponentIncarnation incarnation { get { return root.GetShieldingUnitComponentIncarnation(id); } }
  public void AddObserver(IShieldingUnitComponentEffectObserver observer) {
    root.AddShieldingUnitComponentObserver(id, observer);
  }
  public void RemoveObserver(IShieldingUnitComponentEffectObserver observer) {
    root.RemoveShieldingUnitComponentObserver(id, observer);
  }
  public void Delete() {
    root.EffectShieldingUnitComponentDelete(id);
  }
  public static ShieldingUnitComponent Null = new ShieldingUnitComponent(null, 0);
  public bool Exists() { return root != null && root.ShieldingUnitComponentExists(id); }
  public bool NullableIs(ShieldingUnitComponent that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
    return this.Is(that);
  }
  public bool Is(ShieldingUnitComponent that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public int power {
    get { return incarnation.power; }
    set { root.EffectShieldingUnitComponentSetPower(id, value); }
  }
}
}
