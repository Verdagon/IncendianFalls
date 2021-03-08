using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IUpStairsTTCMutSetEffect : IEffect {
  int id { get; }
  void visitIUpStairsTTCMutSetEffect(IUpStairsTTCMutSetEffectVisitor visitor);
}

}
