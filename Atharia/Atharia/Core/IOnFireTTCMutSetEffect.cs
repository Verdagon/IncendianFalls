using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IOnFireTTCMutSetEffect : IEffect {
  int id { get; }
  void visitIOnFireTTCMutSetEffect(IOnFireTTCMutSetEffectVisitor visitor);
}

}
