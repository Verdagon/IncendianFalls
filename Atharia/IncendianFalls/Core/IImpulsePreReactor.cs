using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IImpulsePreReactor {
  IImpulsePreReactor AsIImpulsePreReactor();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(IImpulsePreReactor that);
  bool NullableIs(IImpulsePreReactor that);
  IDestructible AsIDestructible();
  IUnitComponent AsIUnitComponent();
  Void BeforeImpulse(Game game, Superstate superstate, Unit unit, IAICapabilityUC originatingCapability, IImpulse impulse);
  Void Destruct();
}
}
