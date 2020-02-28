using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IUpStairsTTCEffect {
  int id { get; }
  void visit(IUpStairsTTCEffectVisitor visitor);
}
       
}
