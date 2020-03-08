using System;
using System.Collections.Generic;
using Atharia.Model;
using Domino;
using UnityEngine;

namespace AthPlayer {
  public class Looker : IUnitEffectObserver, IUnitEffectVisitor {
    LookPanelView lookPanelView;
    Unit lookedUnit = Unit.Null;

    public Looker(LookPanelView lookPanelView) {
      this.lookPanelView = lookPanelView;

      this.Look(Unit.Null);
    }

    public void Look(Unit unit) {
      if (lookedUnit.Exists()) {
        lookedUnit.RemoveObserver(this);
        lookPanelView.SetStuff(false, "", "", new List<KeyValuePair<SymbolDescription, string>>());
      }

      if (!unit.Exists()) {
        lookPanelView.SetStuff(false, "", "", new List<KeyValuePair<SymbolDescription, string>>());
        return;
      }

      var symbolsAndLabels = new List<KeyValuePair<SymbolDescription, string>>();

      foreach (var detail in unit.components) {
        if (detail is InvincibilityUCAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                new SymbolDescription(
                    "q",
                    50,
                    new UnityEngine.Color(0, .5f, 1, 1.5f),
                    0,
                    OutlineMode.NoOutline,
                    new UnityEngine.Color(0, 0, 0)),
                "Defying"));
        } else if (detail is DefyingUCAsIUnitComponent Defying) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                new SymbolDescription(
                    "q",
                    50,
                    new UnityEngine.Color(1, 1, 1, 1.5f),
                    0,
                    OutlineMode.NoOutline,
                    new UnityEngine.Color(0, 0, 0)),
                "Defying"));
        } else if (detail is MiredUCAsIUnitComponent mired) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                new SymbolDescription(
                    "f",
                    50,
                    new UnityEngine.Color(1, 1, 1, 1.5f),
                    0,
                    OutlineMode.NoOutline,
                    new UnityEngine.Color(0, 0, 0)),
                "Mired"));
        } else if (detail is WanderAICapabilityUCAsIUnitComponent) {
        } else if (detail is KamikazeAICapabilityUCAsIUnitComponent) {
        } else if (detail is GuardAICapabilityUCAsIUnitComponent) {
        } else if (detail is AttackAICapabilityUCAsIUnitComponent) {
        } else if (detail is BideAICapabilityUCAsIUnitComponent bideI) {
          if (bideI.obj.charge > 0) {
            symbolsAndLabels.Add(
                new KeyValuePair<SymbolDescription, string>(
                    new SymbolDescription(
                        "n",
                    50,
                        new UnityEngine.Color(1, 1, 1, 1.5f),
                        0,
                        OutlineMode.NoOutline,
                        new UnityEngine.Color(0, 0, 0)),
                    "Biding"));
          }
        } else if (detail is HealthPotionAsIUnitComponent) {
        } else if (detail is ManaPotionAsIUnitComponent) {
        } else if (detail is SorcerousUCAsIUnitComponent) {
        } else if (detail is BaseCombatTimeUCAsIUnitComponent) {
        } else if (detail is BaseMovementTimeUCAsIUnitComponent) {
        } else if (detail is SummonAICapabilityUCAsIUnitComponent) {
        } else if (detail is BaseOffenseUCAsIUnitComponent) {
        } else if (detail is BaseDefenseUCAsIUnitComponent) {
        } else if (detail is BlastRodAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                new SymbolDescription(
                    "w",
                    50,
                    new UnityEngine.Color(1, 1, 1, 1.5f),
                    0,
                    OutlineMode.NoOutline,
                    new UnityEngine.Color(0, 0, 0)),
                "Fire Rod"));
        } else if (detail is ArmorAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                new SymbolDescription(
                    "/",
                    50,
                    new UnityEngine.Color(1, 1, 1, 1.5f),
                    0,
                    OutlineMode.NoOutline,
                    new UnityEngine.Color(0, 0, 0)),
                "Igneous Armor"));
        } else if (detail is GlaiveAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                new SymbolDescription(
                    "s",
                    50,
                    new UnityEngine.Color(1, 1, 1, 1.5f),
                    0,
                    OutlineMode.NoOutline,
                    new UnityEngine.Color(0, 0, 0)),
                "Obsidian Sword"));
        } else if (detail is SpeedRingAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                new SymbolDescription(
                    "4",
                    50,
                    new UnityEngine.Color(1, 1, 1, 1.5f),
                    0,
                    OutlineMode.NoOutline,
                    new UnityEngine.Color(0, 0, 0)),
                "Ring of Speed"));
        } else if (detail is TemporaryCloneAICapabilityUCAsIUnitComponent) {
        } else if (detail is TutorialDefyCounterUCAsIUnitComponent) {
        } else if (detail is LightningChargingUCAsIUnitComponent) {
        } else if (detail is LightningChargedUCAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                  new SymbolDescription(
                      "r",
                    50,
                      new UnityEngine.Color(1, 1, 1, 1.5f),
                      0,
                      OutlineMode.NoOutline,
                      new UnityEngine.Color(0, 0, 0)),
                  "Lightning Charged"));
        } else if (detail is TimeCloneAICapabilityUCAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                  new SymbolDescription(
                      "k",
                    50,
                      new UnityEngine.Color(1, 1, 1, 1.5f),
                      0,
                      OutlineMode.NoOutline,
                      new UnityEngine.Color(0, 0, 0)),
                  "Previous Incarnation"));
        } else if (detail is DoomedUCAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                  new SymbolDescription(
                      "k",
                    50,
                      new UnityEngine.Color(1, 1, 1, 1.5f),
                      0,
                      OutlineMode.NoOutline,
                      new UnityEngine.Color(0, 0, 0)),
                  "Previous Incarnation"));
        } else {
          Debug.LogError("Unknown detail type: " + detail);
        }
      }

      lookPanelView.SetStuff(true, unit.classId, unit.hp + " / " + unit.maxHp, symbolsAndLabels);

      unit.AddObserver(this);
    }

    public void OnUnitEffect(IUnitEffect effect) { effect.visit(this); }
    public void visitUnitCreateEffect(UnitCreateEffect effect) { }
    public void visitUnitSetAliveEffect(UnitSetAliveEffect effect) { }
    public void visitUnitSetHpEffect(UnitSetHpEffect effect) { }
    public void visitUnitSetLifeEndTimeEffect(UnitSetLifeEndTimeEffect effect) { }
    public void visitUnitSetLocationEffect(UnitSetLocationEffect effect) { }
    public void visitUnitSetNextActionTimeEffect(UnitSetNextActionTimeEffect effect) { }
    public void visitUnitDeleteEffect(UnitDeleteEffect effect) {
      if (!lookedUnit.Exists()) {
        Look(Unit.Null);
      }
    }
  }
}
