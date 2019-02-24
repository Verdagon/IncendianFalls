using System;
using Atharia.Model;

namespace Atharia.Model {
  public static class UnitExtensions {
    public static Atharia.Model.Void Destruct(
        this Unit obj) {
      var events = obj.events;
      var components = obj.components;
      obj.Delete();
      components.Delete();
      events.Delete();
      return new Atharia.Model.Void();
    }
    public static IDirectiveUC GetDirectiveOrNull(this Unit unit) {
      return unit.components.GetOnlyIDirectiveUCOrNull();
    }
    public static void ReplaceDirective(this Unit unit, IDirectiveUC directive) {
      Asserts.Assert(directive.Exists());
      ClearDirective(unit);
      unit.components.Add(directive.AsIUnitComponent());
      Asserts.Assert(unit.components.GetAllIDirectiveUC().Count == 1);
      Asserts.Assert(unit.GetDirectiveOrNull().Exists());
    }
    public static void ClearDirective(this Unit unit) {
      var existingDirective = unit.components.GetOnlyIDirectiveUCOrNull();
      if (existingDirective.Exists()) {
        unit.root.logger.Info("Deleting existing directive " + existingDirective.id);
        unit.components.Remove(existingDirective);
        existingDirective.Destruct();
      }
    }
    public static IOperationUC GetOperationOrNull(this Unit unit) {
      return unit.components.GetOnlyIOperationUCOrNull();
    }
    public static void ReplaceOperation(this Unit unit, IOperationUC operation) {
      Asserts.Assert(operation.Exists());
      ClearOperation(unit);
      unit.components.Add(operation.AsIUnitComponent());
      Asserts.Assert(unit.components.GetAllIOperationUC().Count == 1);
      Asserts.Assert(unit.GetOperationOrNull().Exists());
    }
    public static void ClearOperation(this Unit unit) {
      var existingOperation = unit.components.GetOnlyIOperationUCOrNull();
      if (existingOperation.Exists()) {
        unit.root.logger.Info("Deleting existing operation " + existingOperation.id);
        unit.components.Remove(existingOperation);
        existingOperation.Destruct();
      }
    }
  }
}
