using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IUnitMutSetEffect : IEffect {
  int id { get; }
  void visitIUnitMutSetEffect(IUnitMutSetEffectVisitor visitor);
}

}
