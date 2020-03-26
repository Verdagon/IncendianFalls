using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IFireTTCMutSetEffect : IEffect {
  int id { get; }
  void visitIFireTTCMutSetEffect(IFireTTCMutSetEffectVisitor visitor);
}

}
