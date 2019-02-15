using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class Armor {
  public readonly Root root;
  public readonly int id;
  public Armor(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ArmorIncarnation incarnation { get { return root.GetArmorIncarnation(id); } }
  public void AddObserver(IArmorEffectObserver observer) {
    root.AddArmorObserver(id, observer);
  }
  public void RemoveObserver(IArmorEffectObserver observer) {
    root.RemoveArmorObserver(id, observer);
  }
  public void Delete() {
    root.EffectArmorDelete(id);
  }
  public static Armor Null = new Armor(null, 0);
  public bool Exists() { return root != null && root.ArmorExists(id); }
  public bool NullableIs(Armor that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
    return this.Is(that);
  }
  public bool Is(Armor that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
       }
}
