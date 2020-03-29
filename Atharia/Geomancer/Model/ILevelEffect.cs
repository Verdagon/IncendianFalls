using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public interface ILevelEffect : IEffect {
  int id { get; }
  void visitILevelEffect(ILevelEffectVisitor visitor);
}
       
}
