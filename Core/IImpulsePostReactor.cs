using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IImpulsePostReactor {
  IImpulsePostReactor AsIImpulsePostReactor();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(IImpulsePostReactor that);
  bool NullableIs(IImpulsePostReactor that);
  IDestructible AsIDestructible();
  IUnitComponent AsIUnitComponent();
  Void AfterImpulse(Game game, Superstate superstate, Unit unit, IAICapabilityUC originatingCapability, IImpulse impulse);
  Void Destruct();
}
}
