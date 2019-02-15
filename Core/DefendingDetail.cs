using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class DefendingDetail {
  public readonly Root root;
  public readonly int id;
  public DefendingDetail(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DefendingDetailIncarnation incarnation { get { return root.GetDefendingDetailIncarnation(id); } }
  public void AddObserver(IDefendingDetailEffectObserver observer) {
    root.AddDefendingDetailObserver(id, observer);
  }
  public void RemoveObserver(IDefendingDetailEffectObserver observer) {
    root.RemoveDefendingDetailObserver(id, observer);
  }
  public void Delete() {
    root.EffectDefendingDetailDelete(id);
  }
  public static DefendingDetail Null = new DefendingDetail(null, 0);
  public bool Exists() { return root != null && root.DefendingDetailExists(id); }
  public bool NullableIs(DefendingDetail that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
    return this.Is(that);
  }
  public bool Is(DefendingDetail that) {
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
    set { root.EffectDefendingDetailSetPower(id, value); }
  }
}
}
