using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMudTTCMutSetEffect : IEffect {
  int id { get; }
  void visitIMudTTCMutSetEffect(IMudTTCMutSetEffectVisitor visitor);
}

}
