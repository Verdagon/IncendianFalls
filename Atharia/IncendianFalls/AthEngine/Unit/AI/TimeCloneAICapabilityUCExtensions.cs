using System;
using System.Collections.Generic;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class TimeCloneAICapabilityUCExtensions {
    public static Atharia.Model.Void Destruct(
        this TimeCloneAICapabilityUC obj) {
      var script = obj.script;
      obj.Delete();
      script.Destruct();
      return new Atharia.Model.Void();
    }

    public static bool PostAct(
        this TimeCloneAICapabilityUC obj,
        Game game,
        Superstate superstate,
        Unit unit) {
      return false;
    }

    public static Atharia.Model.Void AfterImpulse(
        TimeCloneAICapabilityUC self,
        Game game,
        Superstate superstate,
        Unit unit,
        IAICapabilityUC originatingCapability,
        IImpulse impulse) {
      Asserts.Assert(originatingCapability.NullableIs(self.AsIAICapabilityUC()), "Off script!");
      if (self.script.Count == 0) {
        var bunch = IImpulseStrongMutBunch.New(game.root);
        Asserts.Assert(bunch.GetOnlyEvaporateImpulseOrNull().Exists());
        bunch.Destruct();
        return new Atharia.Model.Void();
      } else {
        self.script.RemoveAt(0);
      }
      return new Atharia.Model.Void();
    }

    public static IImpulse ProduceImpulse(
        this TimeCloneAICapabilityUC obj,
        Game game,
        Superstate superstate,
        Unit unit) {
      var script = obj.script;
      while (script.Count > 0) {
        var request = script[0];
        int holdTime = 600; // soon, build this into the script.
        if (request is CheatRequestAsIRequest crI) {
          script.RemoveAt(0);
          continue;
        } else if (request is MoveRequestAsIRequest mrI) {
          var mr = mrI.obj;
          var destination = mr.destination;
          if (Actions.CanStep(game, superstate, unit, destination)) {
            return game.root.EffectMoveImpulseCreate(1000, destination).AsIImpulse();
          } else {
            return game.root.EffectEvaporateImpulseCreate().AsIImpulse();
          }
        } else if (request is AttackRequestAsIRequest arI) {
          var targetUnitId = arI.obj.targetUnitId;
          var targetUnit = game.root.GetUnit(targetUnitId);
          if (!targetUnit.Exists()) {
            return game.root.EffectHoldPositionImpulseCreate(1000, holdTime).AsIImpulse();
          }
          if (!game.level.units.Contains(targetUnit)) {
            return game.root.EffectHoldPositionImpulseCreate(1000, holdTime).AsIImpulse();
          }
          if (!game.level.terrain.pattern.LocationsAreAdjacent(unit.location, targetUnit.location, game.level.ConsiderCornersAdjacent())) {
            return game.root.EffectHoldPositionImpulseCreate(1000, holdTime).AsIImpulse();
          }
          return game.root.EffectAttackImpulseCreate(1000, targetUnit).AsIImpulse();
        } else if (request is MireRequestAsIRequest srI) {
          var targetUnitId = srI.obj.targetUnitId;
          var targetUnit = game.root.GetUnit(targetUnitId);
          if (!targetUnit.Exists()) {
            return game.root.EffectHoldPositionImpulseCreate(1000, holdTime).AsIImpulse();
          }
          if (!game.level.units.Contains(targetUnit)) {
            return game.root.EffectHoldPositionImpulseCreate(1000, holdTime).AsIImpulse();
          }
          return game.root.EffectMireImpulseCreate(1000, targetUnit).AsIImpulse();
        } else if (request is FireBombRequestAsIRequest fbrI) {
          var targetLocation = fbrI.obj.location;
          return game.root.EffectFireBombImpulseCreate(1000, targetLocation).AsIImpulse();
        } else if (request is CounterRequestAsIRequest drC) {
          return game.root.EffectCounterImpulseCreate(1000).AsIImpulse();
        } else if (request is InteractRequestAsIRequest irI) {
          // Deciding time clones can't interact.
          return game.root.EffectHoldPositionImpulseCreate(1000, holdTime).AsIImpulse();
        } else if (request is DefyRequestAsIRequest drI) {
          return game.root.EffectDefyImpulseCreate(1000).AsIImpulse();
        } else if (request is FireRequestAsIRequest frI) {
          var targetUnitId = frI.obj.targetUnitId;
          var targetUnit = game.root.GetUnit(targetUnitId);
          if (!targetUnit.Exists()) {
            return game.root.EffectHoldPositionImpulseCreate(1000, holdTime).AsIImpulse();
          }
          if (!game.level.units.Contains(targetUnit)) {
            return game.root.EffectHoldPositionImpulseCreate(1000, holdTime).AsIImpulse();
          }
          return game.root.EffectFireImpulseCreate(1000, targetUnit).AsIImpulse();
        } else {
          throw new Exception("Unknown request: " + request.DStr());
        }
      }
      return game.root.EffectEvaporateImpulseCreate().AsIImpulse();
    }
  }
}
