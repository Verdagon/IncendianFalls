using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBloodTTCMutSetEffect : IEffect {
  int id { get; }
  void visitIBloodTTCMutSetEffect(IBloodTTCMutSetEffectVisitor visitor);
}

}
