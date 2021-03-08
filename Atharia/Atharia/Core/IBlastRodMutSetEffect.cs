using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBlastRodMutSetEffect : IEffect {
  int id { get; }
  void visitIBlastRodMutSetEffect(IBlastRodMutSetEffectVisitor visitor);
}

}
