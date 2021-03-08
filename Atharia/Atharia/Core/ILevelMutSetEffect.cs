using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ILevelMutSetEffect : IEffect {
  int id { get; }
  void visitILevelMutSetEffect(ILevelMutSetEffectVisitor visitor);
}

}
