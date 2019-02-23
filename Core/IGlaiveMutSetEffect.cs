using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IGlaiveMutSetEffect {
  int id { get; }
  void visit(IGlaiveMutSetEffectVisitor visitor);
}

}
