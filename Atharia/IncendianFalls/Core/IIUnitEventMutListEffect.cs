using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IIUnitEventMutListEffect : IEffect {
  int id { get; }
  void visitIIUnitEventMutListEffect(IIUnitEventMutListEffectVisitor visitor);
}

}
