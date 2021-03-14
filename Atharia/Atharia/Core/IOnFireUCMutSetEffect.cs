using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IOnFireUCMutSetEffect : IEffect {
  int id { get; }
  void visitIOnFireUCMutSetEffect(IOnFireUCMutSetEffectVisitor visitor);
}

}
