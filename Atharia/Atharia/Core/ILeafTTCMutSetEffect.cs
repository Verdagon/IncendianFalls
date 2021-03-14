using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ILeafTTCMutSetEffect : IEffect {
  int id { get; }
  void visitILeafTTCMutSetEffect(ILeafTTCMutSetEffectVisitor visitor);
}

}
