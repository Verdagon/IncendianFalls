using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TutorialDefyCounterUCIncarnation {
  public  int numDefiesRemaining;
  public readonly string onChangeTriggerName;
  public TutorialDefyCounterUCIncarnation(
      int numDefiesRemaining,
      string onChangeTriggerName) {
    this.numDefiesRemaining = numDefiesRemaining;
    this.onChangeTriggerName = onChangeTriggerName;
  }
}

}
