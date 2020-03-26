using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IGlaiveMutSetEffect : IEffect {
  int id { get; }
  void visitIGlaiveMutSetEffect(IGlaiveMutSetEffectVisitor visitor);
}

}
