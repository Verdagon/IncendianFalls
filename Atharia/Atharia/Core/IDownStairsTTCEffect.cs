using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IDownStairsTTCEffect : IEffect {
  int id { get; }
  void visitIDownStairsTTCEffect(IDownStairsTTCEffectVisitor visitor);
}
       
}
