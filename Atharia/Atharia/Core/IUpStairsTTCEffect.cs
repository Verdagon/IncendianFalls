using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IUpStairsTTCEffect : IEffect {
  int id { get; }
  void visitIUpStairsTTCEffect(IUpStairsTTCEffectVisitor visitor);
}
       
}
