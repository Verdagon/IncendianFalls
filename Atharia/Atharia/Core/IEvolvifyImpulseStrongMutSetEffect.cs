using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IEvolvifyImpulseStrongMutSetEffect : IEffect {
  int id { get; }
  void visitIEvolvifyImpulseStrongMutSetEffect(IEvolvifyImpulseStrongMutSetEffectVisitor visitor);
}

}
