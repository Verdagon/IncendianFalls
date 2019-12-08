using System;
using System.Collections.Generic;
using Atharia.Model;
using Domino;
using UnityEngine;

namespace IncendianFalls {
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
        if (detail is ShieldingUCAsIUnitComponent shielding) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                new SymbolDescription(
                    "q",
                    new Color(1, 1, 1, 1.5f),
                    0,
                    OutlineMode.NoOutline,
                    new Color(0, 0, 0)),
                "Shielding"));
        } else if (detail is WanderAICapabilityUCAsIUnitComponent) {
        } else if (detail is AttackAICapabilityUCAsIUnitComponent) {
        } else if (detail is BideAICapabilityUCAsIUnitComponent bideI) {
          if (bideI.obj.charge > 0) {
            symbolsAndLabels.Add(
                new KeyValuePair<SymbolDescription, string>(
                    new SymbolDescription(
                        "n",
                        new Color(1, 1, 1, 1.5f),
                        0,
                        OutlineMode.NoOutline,
                        new Color(0, 0, 0)),
                    "Biding"));
          }
        } else if (detail is HealthPotionAsIUnitComponent) {
        } else if (detail is ManaPotionAsIUnitComponent) {
        } else if (detail is ArmorAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                new SymbolDescription(
                    "/",
                    new Color(1, 1, 1, 1.5f),
                    0,
                    OutlineMode.NoOutline,
                    new Color(0, 0, 0)),
                "Igneous Armor"));
        } else if (detail is GlaiveAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                new SymbolDescription(
                    "s",
                    new Color(1, 1, 1, 1.5f),
                    0,
                    OutlineMode.NoOutline,
                    new Color(0, 0, 0)),
                "Obsidian Sword"));
        } else if (detail is InertiaRingAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                new SymbolDescription(
                    "4",
                    new Color(1, 1, 1, 1.5f),
                    0,
                    OutlineMode.NoOutline,
                    new Color(0, 0, 0)),
                "Ring of Speed"));
        } else if (detail is TimeCloneAICapabilityUCAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                  new SymbolDescription(
                      "k",
                      new Color(1, 1, 1, 1.5f),
                      0,
                      OutlineMode.NoOutline,
                      new Color(0, 0, 0)),
                  "Previous Self"));
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
    public void visitUnitSetMpEffect(UnitSetMpEffect effect) { }
    public void visitUnitSetNextActionTimeEffect(UnitSetNextActionTimeEffect effect) { }
    public void visitUnitDeleteEffect(UnitDeleteEffect effect) {
      if (!lookedUnit.Exists()) {
        Look(Unit.Null);
      }
    }
  }
}
