using Domino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Geomancer {
  class MemberToViewMap {
    public static MemberToViewMapper MakeVivimap() {
      var entries = new Dictionary<string, List<MemberToViewMapper.IDescription>>();
      entries.Add("Grass", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.TopColorDescriptionForIDescription(Vector4Animation.Color(0, .3f, 0)),
        new MemberToViewMapper.SideColorDescriptionForIDescription(Vector4Animation.Color(0, .2f, 0)),
        new MemberToViewMapper.OutlineColorDescriptionForIDescription(Vector4Animation.BLACK)
      });
      entries.Add("Rocks", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.OverlayDescriptionForIDescription(
          new ExtrudedSymbolDescription(
            RenderPriority.TILE,
            new SymbolDescription("f-3", Vector4Animation.Color(.5f, .5f, .5f, .2f), 0, 1, OutlineMode.WithOutline),
            false,
            Vector4Animation.BLACK))
      });
      entries.Add("Obsidian", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.OverlayDescriptionForIDescription(
          new ExtrudedSymbolDescription(
            RenderPriority.TILE,
            new SymbolDescription("f-3", Vector4Animation.Color(0f, 0f, 0f, .8f), 0, 1, OutlineMode.WithOutline),
            false,
            Vector4Animation.BLACK))
      });
      entries.Add("HealthPotion", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.OverlayDescriptionForIDescription(
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        "plus",
                        Vector4Animation.Color(.8f, 0, .8f, 1.5f),
                        0,
                        1,
                        OutlineMode.WithBackOutline),
                    true,
                    Vector4Animation.BLACK))
      });
      entries.Add("ManaPotion", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.OverlayDescriptionForIDescription(
          new ExtrudedSymbolDescription(
              RenderPriority.SYMBOL,
              new SymbolDescription(
                  "comma",
                  Vector4Animation.Color(.25f, .7f, 1.0f, 1.5f),
                  0,
                  1,
                  OutlineMode.WithBackOutline),
              true,
              Vector4Animation.BLACK))
      });
      entries.Add("Cave", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.FeatureDescriptionForIDescription(
                  new ExtrudedSymbolDescription(
                      RenderPriority.SYMBOL,
                      new SymbolDescription(
                          "p",
                          Vector4Animation.BLACK,
                          0,
                          1,
                          OutlineMode.WithOutline,
                          Vector4Animation.WHITE),
                      false,
                      Vector4Animation.WHITE))
      });
      entries.Add("Tree", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.FeatureDescriptionForIDescription(
                  new ExtrudedSymbolDescription(
                      RenderPriority.SYMBOL,
                      new SymbolDescription(
                          "n",
                          Vector4Animation.Color(0, .5f, 0),
                          0,
                          1,
                          OutlineMode.WithOutline),
                      false,
                      Vector4Animation.Color(0f, .3f, 0f)))
      });
      entries.Add("Fire", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.FeatureDescriptionForIDescription(
                  new ExtrudedSymbolDescription(
                      RenderPriority.SYMBOL,
                      new SymbolDescription(
                          "r-3",
                          Vector4Animation.Color(.8f, .4f, 0, .5f),
                          0,
                          1,
                          OutlineMode.WithOutline),
                      false,
                      Vector4Animation.Color(0f, .3f, 0f)))
      });
      entries.Add("Dirt", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.TopColorDescriptionForIDescription(Vector4Animation.Color(.4f, .133f, 0)),
        new MemberToViewMapper.SideColorDescriptionForIDescription(Vector4Animation.Color(.266f, .1f, 0)),
        new MemberToViewMapper.OutlineColorDescriptionForIDescription(Vector4Animation.BLACK)
      });
      entries.Add("Mud", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.TopColorDescriptionForIDescription(Vector4Animation.Color(.35f, .11f, 0)),
        new MemberToViewMapper.SideColorDescriptionForIDescription(Vector4Animation.Color(.23f, .08f, 0)),
        new MemberToViewMapper.OutlineColorDescriptionForIDescription(Vector4Animation.BLACK)
      });
      entries.Add("ObsidianFloor", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.TopColorDescriptionForIDescription(Vector4Animation.Color(.1f, .1f, .05f)),
        new MemberToViewMapper.SideColorDescriptionForIDescription(Vector4Animation.Color(.05f, .05f, .05f)),
        new MemberToViewMapper.OutlineColorDescriptionForIDescription(Vector4Animation.BLACK)
      });
      entries.Add("Floor", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.TopColorDescriptionForIDescription(Vector4Animation.Color(.2f, .2f, .2f)),
        new MemberToViewMapper.SideColorDescriptionForIDescription(Vector4Animation.Color(.15f, .15f, .15f)),
        new MemberToViewMapper.OutlineColorDescriptionForIDescription(Vector4Animation.BLACK)
      });
      entries.Add("Wall", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.TopColorDescriptionForIDescription(Vector4Animation.Color(.3f, .3f, .3f)),
        new MemberToViewMapper.SideColorDescriptionForIDescription(Vector4Animation.Color(.2f, .2f, .2f)),
        new MemberToViewMapper.OutlineColorDescriptionForIDescription(Vector4Animation.BLACK)
      });
      entries.Add("CaveWall", new List<MemberToViewMapper.IDescription>() {
      new MemberToViewMapper.TopColorDescriptionForIDescription(Vector4Animation.Color(.24f, .08f, 0)),
        new MemberToViewMapper.SideColorDescriptionForIDescription(Vector4Animation.Color(.16f, .05f, 0)),
        new MemberToViewMapper.OutlineColorDescriptionForIDescription(Vector4Animation.BLACK)
      });
      entries.Add("Water", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.TopColorDescriptionForIDescription(Vector4Animation.Color(0f, .4f, .8f)),
        new MemberToViewMapper.SideColorDescriptionForIDescription(Vector4Animation.Color(0f, .45f, .85f)),
        new MemberToViewMapper.OutlineColorDescriptionForIDescription(Vector4Animation.BLACK)
      });
      entries.Add("Magma", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.TopColorDescriptionForIDescription(Vector4Animation.Color(.4f, 0f, 0f)),
        new MemberToViewMapper.SideColorDescriptionForIDescription(Vector4Animation.Color(.2f, 0f, 0f)),
        new MemberToViewMapper.OutlineColorDescriptionForIDescription(Vector4Animation.BLACK),
        new MemberToViewMapper.OverlayDescriptionForIDescription(
          new ExtrudedSymbolDescription(
              RenderPriority.SYMBOL,
              new SymbolDescription(
                  "f-3",
                  Vector4Animation.Color(.5f, .0f, 0f),
                  0,
                  1,
                  OutlineMode.NoOutline,
                  Vector4Animation.Color(0, 0, 0)),
              false,
              Vector4Animation.Color(0, 0, 0)))
    });
      entries.Add("Trigger", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.ItemDescriptionForIDescription(
          new ExtrudedSymbolDescription(
            RenderPriority.TILE,
            new SymbolDescription("n", Vector4Animation.Color(1f, 1f, 1f, 0.2f), 180, 1, OutlineMode.NoOutline, Vector4Animation.Color(1, 1, 1)),
            true,
            Vector4Animation.Color(.6f, .6f, .6f, 0.2f)))
      });
      entries.Add("Marker", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.ItemDescriptionForIDescription(
          new ExtrudedSymbolDescription(
            RenderPriority.TILE,
            new SymbolDescription(
                "e", Vector4Animation.Color(1f, .75f, .5f, 1.2f), 0, 1, OutlineMode.WithOutline),
            false,
            Vector4Animation.BLACK))
      });
      return new MemberToViewMapper(entries);
    }
  }
}
