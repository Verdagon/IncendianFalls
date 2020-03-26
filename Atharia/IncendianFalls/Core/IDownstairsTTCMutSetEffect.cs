using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDownStairsTTCMutSetEffect : IEffect {
  int id { get; }
  void visitIDownStairsTTCMutSetEffect(IDownStairsTTCMutSetEffectVisitor visitor);
}

}
