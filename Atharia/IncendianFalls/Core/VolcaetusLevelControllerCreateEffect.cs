using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct VolcaetusLevelControllerCreateEffect : IVolcaetusLevelControllerEffect {
  public readonly int id;
  public readonly VolcaetusLevelControllerIncarnation incarnation;
  public VolcaetusLevelControllerCreateEffect(int id, VolcaetusLevelControllerIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IVolcaetusLevelControllerEffect.id => id;
  public void visitIVolcaetusLevelControllerEffect(IVolcaetusLevelControllerEffectVisitor visitor) {
    visitor.visitVolcaetusLevelControllerCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitVolcaetusLevelControllerEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
