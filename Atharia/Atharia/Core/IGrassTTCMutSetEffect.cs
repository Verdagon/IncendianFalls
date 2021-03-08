using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IGrassTTCMutSetEffect : IEffect {
  int id { get; }
  void visitIGrassTTCMutSetEffect(IGrassTTCMutSetEffectVisitor visitor);
}

}
