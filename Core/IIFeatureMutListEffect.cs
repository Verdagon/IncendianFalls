using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IIFeatureMutListEffect {
  int id { get; }
  void visit(IIFeatureMutListEffectVisitor visitor);
}

}
