using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public static class IUnitEventParser {
  public static IUnitEvent Parse(ParseSource source) {
    var nextThingPeek = source.PeekNextWord();
    switch (nextThingPeek) {
      case "UnitUnleashBideEvent":
        return new UnitUnleashBideEventAsIUnitEvent(UnitUnleashBideEvent.Parse(source));
      case "UnitExplosionedEvent":
        return new UnitExplosionedEventAsIUnitEvent(UnitExplosionedEvent.Parse(source));
      case "UnitBurningEvent":
        return new UnitBurningEventAsIUnitEvent(UnitBurningEvent.Parse(source));
      case "UnitBlazedEvent":
        return new UnitBlazedEventAsIUnitEvent(UnitBlazedEvent.Parse(source));
      case "UnitFireBombedEvent":
        return new UnitFireBombedEventAsIUnitEvent(UnitFireBombedEvent.Parse(source));
      case "UnitExplosionEvent":
        return new UnitExplosionEventAsIUnitEvent(UnitExplosionEvent.Parse(source));
      case "UnitBlazeEvent":
        return new UnitBlazeEventAsIUnitEvent(UnitBlazeEvent.Parse(source));
      case "UnitFireEvent":
        return new UnitFireEventAsIUnitEvent(UnitFireEvent.Parse(source));
      case "UnitMireEvent":
        return new UnitMireEventAsIUnitEvent(UnitMireEvent.Parse(source));
      case "UnitAttackEvent":
        return new UnitAttackEventAsIUnitEvent(UnitAttackEvent.Parse(source));
      case "UnitCounteringEvent":
        return new UnitCounteringEventAsIUnitEvent(UnitCounteringEvent.Parse(source));
      case "UnitDefyingEvent":
        return new UnitDefyingEventAsIUnitEvent(UnitDefyingEvent.Parse(source));
      case "UnitStepEvent":
        return new UnitStepEventAsIUnitEvent(UnitStepEvent.Parse(source));
      case "WaitForUnitEvent":
        return new WaitForUnitEventAsIUnitEvent(WaitForUnitEvent.Parse(source));
      default:
        throw new Exception("Unexpected: " + nextThingPeek);
    }
  }
}

}
