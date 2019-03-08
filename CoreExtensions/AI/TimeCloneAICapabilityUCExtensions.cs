using System;
using System.Collections.Generic;
using Atharia.Model;
using IncendianFalls;

namespace Atharia.Model {
  public static class TimeCloneAICapabilityUCExtensions {
    public static Atharia.Model.Void Destruct(
        this TimeCloneAICapabilityUC obj) {
      obj.Delete();
      return new Atharia.Model.Void();
    }

    public static bool PostAct(
        this TimeCloneAICapabilityUC obj,
        Game game,
        Superstate superstate,
        Unit unit) {
      return false;
    }

    public static IImpulse ProduceImpulse(
        this TimeCloneAICapabilityUC obj,
        Game game,
        Superstate superstate,
        Unit unit) {
      var directive = unit.GetDirectiveOrNull();
      if (directive is TimeScriptDirectiveUCAsIDirectiveUC tsdI) {
        var script = tsdI.obj.script;
        if (script.Count > 0) {
          var request = script[0];
          if (request is MoveRequestAsIRequest mrI) {
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
              return game.root.EffectEvaporateImpulseCreate().AsIImpulse();
            }
            if (!game.level.units.Contains(targetUnit)) {
              return game.root.EffectEvaporateImpulseCreate().AsIImpulse();
            }
            if (!game.level.terrain.pattern.LocationsAreAdjacent(unit.location, targetUnit.location, game.level.ConsiderCornersAdjacent())) {
              return game.root.EffectEvaporateImpulseCreate().AsIImpulse();
            }
            return game.root.EffectAttackImpulseCreate(1000, targetUnit).AsIImpulse();
          } else if (request is CounterRequestAsIRequest drC) {
            return game.root.EffectCounterImpulseCreate(1000).AsIImpulse();
          } else if (request is InteractRequestAsIRequest irI) {
            // Deciding time clones can't interact.
            return game.root.EffectEvaporateImpulseCreate().AsIImpulse();
          } else if (request is DefendRequestAsIRequest drI) {
            return game.root.EffectDefendImpulseCreate(1000).AsIImpulse();
          } else if (request is FireRequestAsIRequest frI) {
            var targetUnitId = frI.obj.targetUnitId;
            var targetUnit = game.root.GetUnit(targetUnitId);
            if (!targetUnit.Exists()) {
              return game.root.EffectEvaporateImpulseCreate().AsIImpulse();
            }
            if (!game.level.units.Contains(targetUnit)) {
              return game.root.EffectEvaporateImpulseCreate().AsIImpulse();
            }
            return game.root.EffectFireImpulseCreate(1000, targetUnit).AsIImpulse();
          } else {
            Asserts.Assert(false, request.DStr());
            return null;
          }
        } else {
          return game.root.EffectEvaporateImpulseCreate().AsIImpulse();
        }
      } else {
        Asserts.Assert(false);
        return null;
      }
    }
  }
}
