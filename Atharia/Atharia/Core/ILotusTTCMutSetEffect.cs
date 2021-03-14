using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ILotusTTCMutSetEffect : IEffect {
  int id { get; }
  void visitILotusTTCMutSetEffect(ILotusTTCMutSetEffectVisitor visitor);
}

}
