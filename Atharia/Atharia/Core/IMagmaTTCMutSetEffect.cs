using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMagmaTTCMutSetEffect : IEffect {
  int id { get; }
  void visitIMagmaTTCMutSetEffect(IMagmaTTCMutSetEffectVisitor visitor);
}

}
