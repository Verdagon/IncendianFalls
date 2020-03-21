using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBaseSightRangeUCMutSetEffect {
  int id { get; }
  void visit(IBaseSightRangeUCMutSetEffectVisitor visitor);
}

}
