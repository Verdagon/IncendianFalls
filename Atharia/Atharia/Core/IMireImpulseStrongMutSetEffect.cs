using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMireImpulseStrongMutSetEffect : IEffect {
  int id { get; }
  void visitIMireImpulseStrongMutSetEffect(IMireImpulseStrongMutSetEffectVisitor visitor);
}

}
