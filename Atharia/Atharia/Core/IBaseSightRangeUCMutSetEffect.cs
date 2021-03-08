using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBaseSightRangeUCMutSetEffect : IEffect {
  int id { get; }
  void visitIBaseSightRangeUCMutSetEffect(IBaseSightRangeUCMutSetEffectVisitor visitor);
}

}
