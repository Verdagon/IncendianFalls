using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDirtTTCMutSetEffect : IEffect {
  int id { get; }
  void visitIDirtTTCMutSetEffect(IDirtTTCMutSetEffectVisitor visitor);
}

}
