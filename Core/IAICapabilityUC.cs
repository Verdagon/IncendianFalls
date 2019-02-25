using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IAICapabilityUC {
  IAICapabilityUC AsIAICapabilityUC();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(IAICapabilityUC that);
  bool NullableIs(IAICapabilityUC that);
  IDestructible AsIDestructible();
  IUnitComponent AsIUnitComponent();
  IImpulse ProduceImpulse(Unit unit, Game game);
  Void Destruct();
}
}
