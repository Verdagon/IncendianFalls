using System;
using Atharia.Model;

namespace IncendianFalls {
  public static class UnitExtensions {
    public static void Destructor(this Unit unit) {
      var events = unit.events;
      var details = unit.details;
      var directive = unit.directive;
      unit.Delete();
      details.Delete();
      events.Delete();
      if (directive.Exists()) {
        directive.Delete();
      }
    }
  }
}
