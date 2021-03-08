using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IBaseSightRangeUCEffect : IEffect {
  int id { get; }
  void visitIBaseSightRangeUCEffect(IBaseSightRangeUCEffectVisitor visitor);
}
       
}
