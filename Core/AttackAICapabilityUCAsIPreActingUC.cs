using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class AttackAICapabilityUCAsIPreActingUC : IPreActingUC {
  public readonly AttackAICapabilityUC obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public AttackAICapabilityUCAsIPreActingUC(AttackAICapabilityUC obj) {
    this.obj = obj;
  }
  public bool Is(IAICapabilityUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(IAICapabilityUC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IAICapabilityUC AsIAICapabilityUC() {
    return new AttackAICapabilityUCAsIAICapabilityUC(obj);
  }
  public bool Is(IUnitComponent that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(IUnitComponent that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IUnitComponent AsIUnitComponent() {
    return new AttackAICapabilityUCAsIUnitComponent(obj);
  }
  public bool Is(IPreActingUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return root == that.root && obj.id == that.id;
  }
  public bool NullableIs(IPreActingUC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public IPreActingUC AsIPreActingUC() {
    return new AttackAICapabilityUCAsIPreActingUC(obj);
  }

         public Void PreAct(Game game, Unit unit) {
    return IncendianFalls.AttackAICapabilityUCExtensions.PreActImpl(obj, game, unit);
  }

}
public static class AttackAICapabilityUCAsIPreActingUCCaster {
  public static AttackAICapabilityUCAsIPreActingUC AsIPreActingUC(this AttackAICapabilityUC obj) {
    return new AttackAICapabilityUCAsIPreActingUC(obj);
  }
}

}
