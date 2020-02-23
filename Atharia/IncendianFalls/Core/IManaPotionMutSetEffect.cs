using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IManaPotionMutSetEffect {
  int id { get; }
  void visit(IManaPotionMutSetEffectVisitor visitor);
}

}
