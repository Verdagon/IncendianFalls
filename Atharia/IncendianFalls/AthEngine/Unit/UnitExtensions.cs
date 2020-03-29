using System;
using Atharia.Model;

namespace Atharia.Model {
  public static class UnitExtensions {
    public static Atharia.Model.Void Destruct(
        this Unit obj) {
      var components = obj.components;
      obj.Delete();
      components.Destruct();
      return new Atharia.Model.Void();
    }
    public static int CalculateCombatTimeCost(this Unit unit, int unmodifiedTimeCost) {
      int timeCost = unmodifiedTimeCost;
      foreach (var component in unit.components.GetAllICombatTimeFactorUC()) {
        timeCost += component.GetCombatTimeAddConstant();
      }
      foreach (var component in unit.components.GetAllICombatTimeFactorUC()) {
        timeCost = timeCost * component.GetCombatTimeMultiplierPercent() / 100;
      }
      return timeCost;
    }
    public static int CalculateMovementTimeCost(this Unit unit, int unmodifiedTimeCost) {
      int timeCost = unmodifiedTimeCost;
      foreach (var component in unit.components.GetAllIMovementTimeFactorUC()) {
        timeCost += component.GetMovementTimeAddConstant();
      }
      foreach (var component in unit.components.GetAllIMovementTimeFactorUC()) {
        timeCost = timeCost * component.GetMovementTimeMultiplierPercent() / 100;
      }
      return timeCost;
    }
    public static int CalculateOutgoingDamage(this Unit unit, int initialDamage) {
      int damage = initialDamage;
      foreach (var component in unit.components.GetAllIOffenseFactorUC()) {
        damage += component.GetOutgoingDamageAddConstant();
      }
      foreach (var component in unit.components.GetAllIOffenseFactorUC()) {
        damage = damage * component.GetOutgoingDamageMultiplierPercent() / 100;
      }
      return damage;
    }
    public static int CalculateIncomingDamage(this Unit unit, int initialDamage) {
      int damage = initialDamage;
      // Note how we flipped the order here, we do the multipliers before the constants.
      // Otherwise attackers would have a ridiculous advantage.
      foreach (var component in unit.components.GetAllIDefenseFactorUC()) {
        damage = damage * component.GetIncomingDamageMultiplierPercent() / 100;
      }
      foreach (var component in unit.components.GetAllIDefenseFactorUC()) {
        damage += component.GetIncomingDamageAddConstant();
      }
      return damage;
    }
    public static int CalculateSightRange(this Unit unit) {
      int sightRange = 8;
      foreach (var component in unit.components.GetAllISightRangeFactorUC()) {
        sightRange += component.GetSightRangeAddConstant();
      }
      foreach (var component in unit.components.GetAllISightRangeFactorUC()) {
        sightRange = sightRange * component.GetSightRangeMultiplierPercent() / 100;
      }
      return sightRange;
    }
    public static bool Alive(this Unit unit) {
      return unit.lifeEndTime == 0;
    }
    public static void AddEvent(this Unit unit, IUnitEvent e) {
      unit.evvent = e;
      unit.evvent = NullIUnitEvent.Null;
    }
    //public static IDirectiveUC GetDirectiveOrNull(this Unit unit) {
    //  return unit.components.GetOnlyIDirectiveUCOrNull();
    //}
    //public static void ReplaceDirective(this Unit unit, IDirectiveUC directive) {
    //  Asserts.Assert(directive.Exists(), "Given directive doesnt exist!");
    //  ClearDirective(unit);
    //  Asserts.Assert(unit.components.GetAllIDirectiveUC().Count == 0, "Couldnt delete existing directive!");
    //  unit.components.Add(directive.AsIUnitComponent());
    //  Asserts.Assert(unit.components.GetAllIDirectiveUC().Count == 1, "Couldn't add directive!");
    //  Asserts.Assert(unit.GetDirectiveOrNull().Exists());
    //}
    //public static void ClearDirective(this Unit unit) {
    //  var existingDirective = unit.components.GetOnlyIDirectiveUCOrNull();
    //  if (existingDirective.Exists()) {
    //    unit.components.Remove(existingDirective.AsIUnitComponent());
    //    existingDirective.Destruct();
    //  }
    //}
    //public static IOperationUC GetOperationOrNull(this Unit unit) {
    //  return unit.components.GetOnlyIOperationUCOrNull();
    //}
    //public static void ReplaceOperation(this Unit unit, IOperationUC operation) {
    //  Asserts.Assert(operation.Exists());
    //  ClearOperation(unit);
    //  unit.components.Add(operation.AsIUnitComponent());
    //  Asserts.Assert(unit.components.GetAllIOperationUC().Count == 1);
    //  Asserts.Assert(unit.GetOperationOrNull().Exists());
    //}
    //public static void ClearOperation(this Unit unit) {
    //  var existingOperation = unit.components.GetOnlyIOperationUCOrNull();
    //  if (existingOperation.Exists()) {
    //    unit.components.Remove(existingOperation.AsIUnitComponent());
    //    existingOperation.Destruct();
    //  }
    //}
  }
}
