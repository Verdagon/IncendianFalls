using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SimplePresenceTriggerTTCMutSetIncarnation {
  public readonly SortedSet<int> elements;

  public SimplePresenceTriggerTTCMutSetIncarnation(SortedSet<int> elements) {
    this.elements = new SortedSet<int>(elements);
  }

  public SimplePresenceTriggerTTCMutSetIncarnation Copy() {
    return new SimplePresenceTriggerTTCMutSetIncarnation(new SortedSet<int>(elements));
  }
}

}
