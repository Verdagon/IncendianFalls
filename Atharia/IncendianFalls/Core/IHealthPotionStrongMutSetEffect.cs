using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IHealthPotionStrongMutSetEffect {
  int id { get; }
  void visit(IHealthPotionStrongMutSetEffectVisitor visitor);
}

}
