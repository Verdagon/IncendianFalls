using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IFallsTTCMutSetEffect : IEffect {
  int id { get; }
  void visitIFallsTTCMutSetEffect(IFallsTTCMutSetEffectVisitor visitor);
}

}
