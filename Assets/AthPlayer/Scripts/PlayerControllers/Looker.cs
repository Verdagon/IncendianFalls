﻿using System;
using System.Collections.Generic;
using Atharia.Model;
using Domino;
using UnityEngine;

namespace AthPlayer {
  public class Looker : IUnitEffectObserver, IUnitEffectVisitor {
    LookPanelView lookPanelView;
    EffectBroadcaster broadcaster;
    Unit lookedUnit = Unit.Null;
    TerrainTile lookedTile = TerrainTile.Null;
    string tooltip;

    public Looker(LookPanelView lookPanelView, EffectBroadcaster broadcaster) {
      this.lookPanelView = lookPanelView;
      this.broadcaster = broadcaster;

      lookPanelView.SetStuff(false, "", "", new List<KeyValuePair<SymbolDescription, string>>());
      SetTooltip("");
    }

    public void SetTooltip(string newTooltip) {
      tooltip = newTooltip;
      if (tooltip.Length > 0) {
        lookPanelView.SetStuff(true, newTooltip, "", new List<KeyValuePair<SymbolDescription, string>>());
      } else {
        Look(lookedUnit, lookedTile);
      }
    }

    public void Look(Unit unit, TerrainTile tile) {
      if (unit.NullableIs(lookedUnit) && tile.NullableIs(lookedTile)) {
        return;
      }
      if (tooltip != "") {
        return;
      }


      if (lookedUnit.Exists()) {
        lookedUnit.RemoveObserver(broadcaster, this);
      }
      lookedUnit = unit;
      lookedTile = tile;

      bool displayedAnything = false;
      if (unit.Exists()) {
        displayedAnything = Look(unit);
      } else if (tile.Exists()) {
        displayedAnything = Look(tile);
      }
      
      if (!displayedAnything) {
        lookPanelView.SetStuff(false, "", "", new List<KeyValuePair<SymbolDescription, string>>());
      }
    }

    private bool Look(TerrainTile tile) {
      var symbolsAndLabels = new List<KeyValuePair<SymbolDescription, string>>();

      foreach (var detail in tile.components) {
        if (detail is ItemTTCAsITerrainTileComponent itemTTC) {
          var item = itemTTC.obj.item;

          if (item is ArmorAsIItem) {
            symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                    new SymbolDescription(
                        "0",
                        Vector4Animation.Color(1f, 1f, 1.0f, 1.5f),
                        0,
                        1,
                        OutlineMode.WithBackOutline),
              "Igneous Armor"));
          } else if (item is BlastRodAsIItem) {
            symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                    new SymbolDescription(
                        "w",
                        Vector4Animation.Color(1f, .5f, 0f, 1.5f),
                        0,
                        1,
                        OutlineMode.WithBackOutline),
              "Fire Rod"));
          } else if (item is BlazeRodAsIItem) {
            symbolsAndLabels.Add(
                new KeyValuePair<SymbolDescription, string>(
                    new SymbolDescription(
                        "w",
                        Vector4Animation.Color(1f, 1f, 0f, 1.5f),
                        0,
                        1,
                        OutlineMode.WithBackOutline),
                    "Blaze Rod"));
          } else if (item is ExplosionRodAsIItem) {
            symbolsAndLabels.Add(
                new KeyValuePair<SymbolDescription, string>(
                    new SymbolDescription(
                        "w",
                        Vector4Animation.Color(1f, 1f, 1f, 1.5f),
                        0,
                        1,
                        OutlineMode.WithBackOutline),
                    "Explosion Staff"));
          } else if (item is SlowRodAsIItem) {
            symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                    new SymbolDescription(
                        "w",
                        Vector4Animation.Color(0f, .5f, 1f, 1.5f),
                        0,
                        1,
                        OutlineMode.WithBackOutline),
              "Mire Staff"));
          } else if (item is GlaiveAsIItem) {
            symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                    new SymbolDescription(
                        "s",
                        Vector4Animation.Color(1f, 1f, 1f, 1.5f),
                        0,
                        1,
                        OutlineMode.WithBackOutline),
              "Glaive"));
          } else if (item is SpeedRingAsIItem) {
            symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                    new SymbolDescription(
                        "4",
                        Vector4Animation.Color(1f, 1f, 1f, 1.5f),
                        0,
                        1,
                        OutlineMode.WithBackOutline),
              "Speed Ring"));
          } else if (item is HealthPotionAsIItem) {
            symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                    new SymbolDescription(
                        "+",
                        Vector4Animation.Color(.8f, 0, .8f, 1.5f),
                        0,
                        1,
                        OutlineMode.WithBackOutline),
              "Life Potion"));
          } else if (item is ManaPotionAsIItem) {
            symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                    new SymbolDescription(
                        ",",
                        Vector4Animation.Color(.25f, .7f, 1.0f, 1.5f),
                        0,
                        1,
                        OutlineMode.WithBackOutline),
              "Mana Potion"));
          } else {
            Asserts.Assert(false, "Found item: " + item);
          }
        }
      }

      if (symbolsAndLabels.Count > 0) {
        lookPanelView.SetStuff(true, "", "", symbolsAndLabels);
        return true;
      } else {
        return false;
      }
    }


    private bool Look(Unit unit) {
      var symbolsAndLabels = new List<KeyValuePair<SymbolDescription, string>>();

      foreach (var detail in unit.components) {
        if (detail is InvincibilityUCAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                new SymbolDescription(
                    "q",
                    Vector4Animation.Color(0, .5f, 1, 1.5f),
                    0,
                    1,
                    OutlineMode.NoOutline,
                    Vector4Animation.Color(0, 0, 0)),
                "Defying"));
        } else if (detail is DefyingUCAsIUnitComponent Defying) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                new SymbolDescription(
                    "q",
                    Vector4Animation.GLOWY_WHITE,
                    0,
                    1,
                    OutlineMode.NoOutline,
                    Vector4Animation.Color(0, 0, 0)),
                "Defying"));
        } else if (detail is MiredUCAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                new SymbolDescription(
                    "f-3",
                    Vector4Animation.GLOWY_WHITE,
                    0,
                    1,
                    OutlineMode.NoOutline,
                    Vector4Animation.Color(0, 0, 0)),
                "Mired"));
        } else if (detail is OnFireUCAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                  new SymbolDescription(
                      "r",
                      Vector4Animation.ORANGE,
                      0,
                      1,
                      OutlineMode.NoOutline,
                      Vector4Animation.Color(0, 0, 0)),
                  "On fire"));
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
                        Vector4Animation.GLOWY_WHITE,
                        0,
                        1,
                        OutlineMode.NoOutline,
                        Vector4Animation.Color(0, 0, 0)),
                    "Biding"));
          }
        } else if (detail is HealthPotionAsIUnitComponent) {
        } else if (detail is ManaPotionAsIUnitComponent) {
        } else if (detail is SorcerousUCAsIUnitComponent) {
        } else if (detail is BaseCombatTimeUCAsIUnitComponent) {
        } else if (detail is BaseMovementTimeUCAsIUnitComponent) {
        } else if (detail is BaseSightRangeUCAsIUnitComponent) {
        } else if (detail is SummonAICapabilityUCAsIUnitComponent) {
        } else if (detail is BaseOffenseUCAsIUnitComponent) {
        } else if (detail is BaseDefenseUCAsIUnitComponent) {
        } else if (detail is SlowRodAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                new SymbolDescription(
                    "w",
                    Vector4Animation.Color(0, .5f, 1, 1.5f),
                    0,
                    1,
                    OutlineMode.NoOutline,
                    Vector4Animation.Color(0, 0, 0)),
                "Mire Staff"));
        } else if (detail is BlastRodAsIUnitComponent ||
            detail is BlazeRodAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                new SymbolDescription(
                    "w",
                    Vector4Animation.Color(1, .5f, 0, 1.5f),
                    0,
                    1,
                    OutlineMode.NoOutline,
                    Vector4Animation.Color(0, 0, 0)),
                "Blaze Rod"));
        } else if (detail is ExplosionRodAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                  new SymbolDescription(
                      "w",
                      Vector4Animation.Color(1, 1, 1, 1.5f),
                      0,
                      1,
                      OutlineMode.NoOutline,
                      Vector4Animation.Color(0, 0, 0)),
                  "Explosion Staff"));
        } else if (detail is ArmorAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                new SymbolDescription(
                    "0",
                    Vector4Animation.GLOWY_WHITE,
                    0,
                    1,
                    OutlineMode.NoOutline,
                    Vector4Animation.Color(0, 0, 0)),
                "Igneous Armor"));
        } else if (detail is GlaiveAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                new SymbolDescription(
                    "s",
                    Vector4Animation.GLOWY_WHITE,
                    0,
                    1,
                    OutlineMode.NoOutline,
                    Vector4Animation.Color(0, 0, 0)),
                "Obsidian Sword"));
        } else if (detail is SpeedRingAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                new SymbolDescription(
                    "4",
                    Vector4Animation.GLOWY_WHITE,
                    0,
                    1,
                    OutlineMode.NoOutline,
                    Vector4Animation.Color(0, 0, 0)),
                "Ring of Speed"));
        } else if (detail is TemporaryCloneAICapabilityUCAsIUnitComponent) {
        } else if (detail is EvolvifyAICapabilityUCAsIUnitComponent) {
        } else if (detail is BequeathUCAsIUnitComponent) {
        } else if (detail is TutorialDefyCounterUCAsIUnitComponent) {
        } else if (detail is LightningChargingUCAsIUnitComponent) {
        } else if (detail is DeathTriggerUCAsIUnitComponent) {
        } else if (detail is LightningChargedUCAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                  new SymbolDescription(
                      "r-3",
                      Vector4Animation.GLOWY_WHITE,
                      0,
                      1,
                      OutlineMode.NoOutline),
                  "Lightning Charged"));
        } else if (detail is TimeCloneAICapabilityUCAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                  new SymbolDescription(
                      "k",
                      Vector4Animation.GLOWY_WHITE,
                      0,
                      1,
                      OutlineMode.NoOutline),
                  "Previous Incarnation"));
        } else if (detail is DoomedUCAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                  new SymbolDescription(
                      "k",
                      Vector4Animation.GLOWY_WHITE,
                      0,
                      1,
                      OutlineMode.NoOutline),
                  "Previous Incarnation"));
        } else {
          Debug.LogError("Unknown detail type: " + detail);
        }
      }

      lookPanelView.SetStuff(true, unit.classId, unit.hp + " / " + unit.maxHp, symbolsAndLabels);

      unit.AddObserver(broadcaster, this);

      return true;
    }

    public void OnUnitEffect(IUnitEffect effect) { effect.visitIUnitEffect(this); }
    public void visitUnitCreateEffect(UnitCreateEffect effect) { }
    public void visitUnitSetHpEffect(UnitSetHpEffect effect) { }
    public void visitUnitSetEvventEffect(UnitSetEvventEffect effect) { }
    public void visitUnitSetMaxHpEffect(UnitSetMaxHpEffect effect) { }
    public void visitUnitSetLifeEndTimeEffect(UnitSetLifeEndTimeEffect effect) { }
    public void visitUnitSetLocationEffect(UnitSetLocationEffect effect) { }
    public void visitUnitSetNextActionTimeEffect(UnitSetNextActionTimeEffect effect) { }
    public void visitUnitDeleteEffect(UnitDeleteEffect effect) {
      if (!lookedUnit.Exists()) {
        Look(Unit.Null, lookedTile);
      }
    }
  }
}
