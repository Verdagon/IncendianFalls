using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IHealthPotionMutSetEffect {
  int id { get; }
  void visit(IHealthPotionMutSetEffectVisitor visitor);
}

}
