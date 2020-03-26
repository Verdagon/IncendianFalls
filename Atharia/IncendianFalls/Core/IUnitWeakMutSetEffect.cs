using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IUnitWeakMutSetEffect : IEffect {
  int id { get; }
  void visitIUnitWeakMutSetEffect(IUnitWeakMutSetEffectVisitor visitor);
}

}
