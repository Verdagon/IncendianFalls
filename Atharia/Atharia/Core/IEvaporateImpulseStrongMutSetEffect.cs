using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IEvaporateImpulseStrongMutSetEffect : IEffect {
  int id { get; }
  void visitIEvaporateImpulseStrongMutSetEffect(IEvaporateImpulseStrongMutSetEffectVisitor visitor);
}

}
