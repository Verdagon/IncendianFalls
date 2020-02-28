using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IDownStairsTTCEffect {
  int id { get; }
  void visit(IDownStairsTTCEffectVisitor visitor);
}
       
}
