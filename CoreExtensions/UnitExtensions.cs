using System;
using Atharia.Model;

namespace IncendianFalls {
  public static class UnitExtensions {
    public static void ReplaceDirective(this Unit unit, IDirectiveUC directive) {
      Asserts.Assert(directive.Exists());
      var existingDirective = unit.components.GetOnlyIDirectiveUCOrNull();
      if (existingDirective.Exists()) {
        unit.root.logger.Info("Deleting existing directive " + existingDirective.id);
        unit.components.Remove(existingDirective);
        existingDirective.Delete();
      }
      unit.components.Add(directive.AsIUnitComponent());
      Asserts.Assert(unit.components.GetAllIDirectiveUC().Count == 1);
      Asserts.Assert(unit.GetDirectiveOrNull().Exists());
    }
    public static IDirectiveUC GetDirectiveOrNull(this Unit unit) {
      return unit.components.GetOnlyIDirectiveUCOrNull();
    }
    public static void Destructor(this Unit unit) {
      var events = unit.events;
      var components = unit.components;
      unit.Delete();
      components.Delete();
      events.Delete();
    }
  }
}
