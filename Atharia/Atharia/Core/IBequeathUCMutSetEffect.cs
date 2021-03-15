using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBequeathUCMutSetEffect : IEffect {
  int id { get; }
  void visitIBequeathUCMutSetEffect(IBequeathUCMutSetEffectVisitor visitor);
}

}
