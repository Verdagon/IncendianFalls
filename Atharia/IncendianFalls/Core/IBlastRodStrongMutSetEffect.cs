using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBlastRodStrongMutSetEffect : IEffect {
  int id { get; }
  void visitIBlastRodStrongMutSetEffect(IBlastRodStrongMutSetEffectVisitor visitor);
}

}
