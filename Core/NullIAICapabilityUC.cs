using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class NullIAICapabilityUC : IAICapabilityUC {
  public static NullIAICapabilityUC Null = new NullIAICapabilityUC();

  public Root root { get { return null; } }
  public int id { get { return 0; } }
  public void Delete() {
    throw new Exception("Can't delete a null!");
  }
  public bool Exists() { return false; }
  public bool Is(IAICapabilityUC that) {
    throw new Exception("Called Is on a null!");
  }
  public bool NullableIs(IAICapabilityUC that) {
    return !that.Exists();
  }
  public IAICapabilityUC AsIAICapabilityUC() {
    return this;
  }
         public IImpulse ProduceImpulse(Unit unit, Game game){ throw new Exception("Called method on a null!"); }
  public bool Is(IUnitComponent that) {
    throw new Exception("Called Is on a null!");
  }
  public bool NullableIs(IUnitComponent that) {
    return !that.Exists();
  }
  public IUnitComponent AsIUnitComponent() {
    return NullIUnitComponent.Null;
  }
}
}
