using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class BideAICapabilityUCAsIUnitComponent : IUnitComponent {
  public readonly BideAICapabilityUC obj;
  public int id => obj.id;
  public Root root => obj.root;
  public void Delete() { obj.Delete(); }
  public bool Exists() { return obj.Exists(); }
  public BideAICapabilityUCAsIUnitComponent(BideAICapabilityUC obj) {
    this.obj = obj;
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
    return new BideAICapabilityUCAsIUnitComponent(obj);
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
    return new BideAICapabilityUCAsIAICapabilityUC(obj);
  }

       
}
public static class BideAICapabilityUCAsIUnitComponentCaster {
  public static BideAICapabilityUCAsIUnitComponent AsIUnitComponent(this BideAICapabilityUC obj) {
    return new BideAICapabilityUCAsIUnitComponent(obj);
  }
}

}
