using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IGameEffect : IEffect {
  int id { get; }
  void visitIGameEffect(IGameEffectVisitor visitor);
}
       
}
