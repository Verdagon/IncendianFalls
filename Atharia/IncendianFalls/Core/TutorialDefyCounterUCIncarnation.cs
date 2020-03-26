using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TutorialDefyCounterUCIncarnation : ITutorialDefyCounterUCEffectVisitor {
  public  int numDefiesRemaining;
  public readonly string onChangeTriggerName;
  public TutorialDefyCounterUCIncarnation(
      int numDefiesRemaining,
      string onChangeTriggerName) {
    this.numDefiesRemaining = numDefiesRemaining;
    this.onChangeTriggerName = onChangeTriggerName;
  }
  public TutorialDefyCounterUCIncarnation Copy() {
    return new TutorialDefyCounterUCIncarnation(
numDefiesRemaining,
onChangeTriggerName    );
  }

  public void visitTutorialDefyCounterUCCreateEffect(TutorialDefyCounterUCCreateEffect e) {}
  public void visitTutorialDefyCounterUCDeleteEffect(TutorialDefyCounterUCDeleteEffect e) {}
public void visitTutorialDefyCounterUCSetNumDefiesRemainingEffect(TutorialDefyCounterUCSetNumDefiesRemainingEffect e) { this.numDefiesRemaining = e.newValue; }

  public void ApplyEffect(ITutorialDefyCounterUCEffect effect) { effect.visitITutorialDefyCounterUCEffect(this); }
}

}
