using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IManaPotionStrongMutSetEffect {
  int id { get; }
  void visit(IManaPotionStrongMutSetEffectVisitor visitor);
}

}
