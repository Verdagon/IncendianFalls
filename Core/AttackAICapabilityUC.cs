using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class AttackAICapabilityUC {
  public readonly Root root;
  public readonly int id;
  public AttackAICapabilityUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public AttackAICapabilityUCIncarnation incarnation { get { return root.GetAttackAICapabilityUCIncarnation(id); } }
  public void AddObserver(IAttackAICapabilityUCEffectObserver observer) {
    root.AddAttackAICapabilityUCObserver(id, observer);
  }
  public void RemoveObserver(IAttackAICapabilityUCEffectObserver observer) {
    root.RemoveAttackAICapabilityUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectAttackAICapabilityUCDelete(id);
  }
  public static AttackAICapabilityUC Null = new AttackAICapabilityUC(null, 0);
  public bool Exists() { return root != null && root.AttackAICapabilityUCExists(id); }
  public bool NullableIs(AttackAICapabilityUC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
    return this.Is(that);
  }
  public bool Is(AttackAICapabilityUC that) {
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
