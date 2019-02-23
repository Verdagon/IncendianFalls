using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public static class IUnitEventParser {
  public static IUnitEvent Parse(ParseSource source) {
    var nextThingPeek = source.PeekNextWord();
    switch (nextThingPeek) {
      case "UnitAttackEvent":
        return new UnitAttackEventAsIUnitEvent(UnitAttackEvent.Parse(source));
      case "UnitStepEvent":
        return new UnitStepEventAsIUnitEvent(UnitStepEvent.Parse(source));
      default:
        throw new Exception("Unexpected: " + nextThingPeek);
    }
  }
}

}
