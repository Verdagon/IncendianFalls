using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class ITerrainTileComponentMutBunch {
  public readonly Root root;
  public readonly int id;
  public ITerrainTileComponentMutBunch(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ITerrainTileComponentMutBunchIncarnation incarnation { get { return root.GetITerrainTileComponentMutBunchIncarnation(id); } }
  public void AddObserver(IITerrainTileComponentMutBunchEffectObserver observer) {
    root.AddITerrainTileComponentMutBunchObserver(id, observer);
  }
  public void RemoveObserver(IITerrainTileComponentMutBunchEffectObserver observer) {
    root.RemoveITerrainTileComponentMutBunchObserver(id, observer);
  }
  public void Delete() {
    root.EffectITerrainTileComponentMutBunchDelete(id);
  }
  public static ITerrainTileComponentMutBunch Null = new ITerrainTileComponentMutBunch(null, 0);
  public bool Exists() { return root != null && root.ITerrainTileComponentMutBunchExists(id); }
  public bool NullableIs(ITerrainTileComponentMutBunch that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {

    if (!root.ArmorMutSetExists(membersArmorMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersArmorMutSet");
    }

    if (!root.InertiaRingMutSetExists(membersInertiaRingMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersInertiaRingMutSet");
    }

    if (!root.GlaiveMutSetExists(membersGlaiveMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersGlaiveMutSet");
    }

    if (!root.ManaPotionMutSetExists(membersManaPotionMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersManaPotionMutSet");
    }

    if (!root.HealthPotionMutSetExists(membersHealthPotionMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersHealthPotionMutSet");
    }

    if (!root.TimeAnchorTTCMutSetExists(membersTimeAnchorTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersTimeAnchorTTCMutSet");
    }

    if (!root.StaircaseTTCMutSetExists(membersStaircaseTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersStaircaseTTCMutSet");
    }

    if (!root.WallTTCMutSetExists(membersWallTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersWallTTCMutSet");
    }

    if (!root.BloodTTCMutSetExists(membersBloodTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersBloodTTCMutSet");
    }

    if (!root.RocksTTCMutSetExists(membersRocksTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersRocksTTCMutSet");
    }

    if (!root.DownstairsTTCMutSetExists(membersDownstairsTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersDownstairsTTCMutSet");
    }

    if (!root.UpstairsTTCMutSetExists(membersUpstairsTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersUpstairsTTCMutSet");
    }

    if (!root.CaveTTCMutSetExists(membersCaveTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersCaveTTCMutSet");
    }

    if (!root.FallsTTCMutSetExists(membersFallsTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersFallsTTCMutSet");
    }

    if (!root.MagmaTTCMutSetExists(membersMagmaTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersMagmaTTCMutSet");
    }

    if (!root.CliffTTCMutSetExists(membersCliffTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersCliffTTCMutSet");
    }

    if (!root.RavaNestTTCMutSetExists(membersRavaNestTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersRavaNestTTCMutSet");
    }

    if (!root.CliffLandingTTCMutSetExists(membersCliffLandingTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersCliffLandingTTCMutSet");
    }

    if (!root.StoneTTCMutSetExists(membersStoneTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersStoneTTCMutSet");
    }

    if (!root.GrassTTCMutSetExists(membersGrassTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersGrassTTCMutSet");
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    if (root.ArmorMutSetExists(membersArmorMutSet.id)) {
      membersArmorMutSet.FindReachableObjects(foundIds);
    }
    if (root.InertiaRingMutSetExists(membersInertiaRingMutSet.id)) {
      membersInertiaRingMutSet.FindReachableObjects(foundIds);
    }
    if (root.GlaiveMutSetExists(membersGlaiveMutSet.id)) {
      membersGlaiveMutSet.FindReachableObjects(foundIds);
    }
    if (root.ManaPotionMutSetExists(membersManaPotionMutSet.id)) {
      membersManaPotionMutSet.FindReachableObjects(foundIds);
    }
    if (root.HealthPotionMutSetExists(membersHealthPotionMutSet.id)) {
      membersHealthPotionMutSet.FindReachableObjects(foundIds);
    }
    if (root.TimeAnchorTTCMutSetExists(membersTimeAnchorTTCMutSet.id)) {
      membersTimeAnchorTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.StaircaseTTCMutSetExists(membersStaircaseTTCMutSet.id)) {
      membersStaircaseTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.WallTTCMutSetExists(membersWallTTCMutSet.id)) {
      membersWallTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.BloodTTCMutSetExists(membersBloodTTCMutSet.id)) {
      membersBloodTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.RocksTTCMutSetExists(membersRocksTTCMutSet.id)) {
      membersRocksTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.DownstairsTTCMutSetExists(membersDownstairsTTCMutSet.id)) {
      membersDownstairsTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.UpstairsTTCMutSetExists(membersUpstairsTTCMutSet.id)) {
      membersUpstairsTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.CaveTTCMutSetExists(membersCaveTTCMutSet.id)) {
      membersCaveTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.FallsTTCMutSetExists(membersFallsTTCMutSet.id)) {
      membersFallsTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.MagmaTTCMutSetExists(membersMagmaTTCMutSet.id)) {
      membersMagmaTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.CliffTTCMutSetExists(membersCliffTTCMutSet.id)) {
      membersCliffTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.RavaNestTTCMutSetExists(membersRavaNestTTCMutSet.id)) {
      membersRavaNestTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.CliffLandingTTCMutSetExists(membersCliffLandingTTCMutSet.id)) {
      membersCliffLandingTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.StoneTTCMutSetExists(membersStoneTTCMutSet.id)) {
      membersStoneTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.GrassTTCMutSetExists(membersGrassTTCMutSet.id)) {
      membersGrassTTCMutSet.FindReachableObjects(foundIds);
    }
  }
  public bool Is(ITerrainTileComponentMutBunch that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public ArmorMutSet membersArmorMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersArmorMutSet of null!");
      }
      return new ArmorMutSet(root, incarnation.membersArmorMutSet);
    }
                       }
  public InertiaRingMutSet membersInertiaRingMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersInertiaRingMutSet of null!");
      }
      return new InertiaRingMutSet(root, incarnation.membersInertiaRingMutSet);
    }
                       }
  public GlaiveMutSet membersGlaiveMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersGlaiveMutSet of null!");
      }
      return new GlaiveMutSet(root, incarnation.membersGlaiveMutSet);
    }
                       }
  public ManaPotionMutSet membersManaPotionMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersManaPotionMutSet of null!");
      }
      return new ManaPotionMutSet(root, incarnation.membersManaPotionMutSet);
    }
                       }
  public HealthPotionMutSet membersHealthPotionMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersHealthPotionMutSet of null!");
      }
      return new HealthPotionMutSet(root, incarnation.membersHealthPotionMutSet);
    }
                       }
  public TimeAnchorTTCMutSet membersTimeAnchorTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersTimeAnchorTTCMutSet of null!");
      }
      return new TimeAnchorTTCMutSet(root, incarnation.membersTimeAnchorTTCMutSet);
    }
                       }
  public StaircaseTTCMutSet membersStaircaseTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersStaircaseTTCMutSet of null!");
      }
      return new StaircaseTTCMutSet(root, incarnation.membersStaircaseTTCMutSet);
    }
                       }
  public WallTTCMutSet membersWallTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersWallTTCMutSet of null!");
      }
      return new WallTTCMutSet(root, incarnation.membersWallTTCMutSet);
    }
                       }
  public BloodTTCMutSet membersBloodTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersBloodTTCMutSet of null!");
      }
      return new BloodTTCMutSet(root, incarnation.membersBloodTTCMutSet);
    }
                       }
  public RocksTTCMutSet membersRocksTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersRocksTTCMutSet of null!");
      }
      return new RocksTTCMutSet(root, incarnation.membersRocksTTCMutSet);
    }
                       }
  public DownstairsTTCMutSet membersDownstairsTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersDownstairsTTCMutSet of null!");
      }
      return new DownstairsTTCMutSet(root, incarnation.membersDownstairsTTCMutSet);
    }
                       }
  public UpstairsTTCMutSet membersUpstairsTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersUpstairsTTCMutSet of null!");
      }
      return new UpstairsTTCMutSet(root, incarnation.membersUpstairsTTCMutSet);
    }
                       }
  public CaveTTCMutSet membersCaveTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersCaveTTCMutSet of null!");
      }
      return new CaveTTCMutSet(root, incarnation.membersCaveTTCMutSet);
    }
                       }
  public FallsTTCMutSet membersFallsTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersFallsTTCMutSet of null!");
      }
      return new FallsTTCMutSet(root, incarnation.membersFallsTTCMutSet);
    }
                       }
  public MagmaTTCMutSet membersMagmaTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersMagmaTTCMutSet of null!");
      }
      return new MagmaTTCMutSet(root, incarnation.membersMagmaTTCMutSet);
    }
                       }
  public CliffTTCMutSet membersCliffTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersCliffTTCMutSet of null!");
      }
      return new CliffTTCMutSet(root, incarnation.membersCliffTTCMutSet);
    }
                       }
  public RavaNestTTCMutSet membersRavaNestTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersRavaNestTTCMutSet of null!");
      }
      return new RavaNestTTCMutSet(root, incarnation.membersRavaNestTTCMutSet);
    }
                       }
  public CliffLandingTTCMutSet membersCliffLandingTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersCliffLandingTTCMutSet of null!");
      }
      return new CliffLandingTTCMutSet(root, incarnation.membersCliffLandingTTCMutSet);
    }
                       }
  public StoneTTCMutSet membersStoneTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersStoneTTCMutSet of null!");
      }
      return new StoneTTCMutSet(root, incarnation.membersStoneTTCMutSet);
    }
                       }
  public GrassTTCMutSet membersGrassTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersGrassTTCMutSet of null!");
      }
      return new GrassTTCMutSet(root, incarnation.membersGrassTTCMutSet);
    }
                       }

  public static ITerrainTileComponentMutBunch New(Root root) {
    return root.EffectITerrainTileComponentMutBunchCreate(
      root.EffectArmorMutSetCreate()
,
      root.EffectInertiaRingMutSetCreate()
,
      root.EffectGlaiveMutSetCreate()
,
      root.EffectManaPotionMutSetCreate()
,
      root.EffectHealthPotionMutSetCreate()
,
      root.EffectTimeAnchorTTCMutSetCreate()
,
      root.EffectStaircaseTTCMutSetCreate()
,
      root.EffectWallTTCMutSetCreate()
,
      root.EffectBloodTTCMutSetCreate()
,
      root.EffectRocksTTCMutSetCreate()
,
      root.EffectDownstairsTTCMutSetCreate()
,
      root.EffectUpstairsTTCMutSetCreate()
,
      root.EffectCaveTTCMutSetCreate()
,
      root.EffectFallsTTCMutSetCreate()
,
      root.EffectMagmaTTCMutSetCreate()
,
      root.EffectCliffTTCMutSetCreate()
,
      root.EffectRavaNestTTCMutSetCreate()
,
      root.EffectCliffLandingTTCMutSetCreate()
,
      root.EffectStoneTTCMutSetCreate()
,
      root.EffectGrassTTCMutSetCreate()
        );
  }
  public void Add(ITerrainTileComponent elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.ArmorExists(elementI.id)) {
      this.membersArmorMutSet.Add(root.GetArmor(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.InertiaRingExists(elementI.id)) {
      this.membersInertiaRingMutSet.Add(root.GetInertiaRing(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.GlaiveExists(elementI.id)) {
      this.membersGlaiveMutSet.Add(root.GetGlaive(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.ManaPotionExists(elementI.id)) {
      this.membersManaPotionMutSet.Add(root.GetManaPotion(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.HealthPotionExists(elementI.id)) {
      this.membersHealthPotionMutSet.Add(root.GetHealthPotion(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.TimeAnchorTTCExists(elementI.id)) {
      this.membersTimeAnchorTTCMutSet.Add(root.GetTimeAnchorTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.StaircaseTTCExists(elementI.id)) {
      this.membersStaircaseTTCMutSet.Add(root.GetStaircaseTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.WallTTCExists(elementI.id)) {
      this.membersWallTTCMutSet.Add(root.GetWallTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.BloodTTCExists(elementI.id)) {
      this.membersBloodTTCMutSet.Add(root.GetBloodTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.RocksTTCExists(elementI.id)) {
      this.membersRocksTTCMutSet.Add(root.GetRocksTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.DownstairsTTCExists(elementI.id)) {
      this.membersDownstairsTTCMutSet.Add(root.GetDownstairsTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.UpstairsTTCExists(elementI.id)) {
      this.membersUpstairsTTCMutSet.Add(root.GetUpstairsTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.CaveTTCExists(elementI.id)) {
      this.membersCaveTTCMutSet.Add(root.GetCaveTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.FallsTTCExists(elementI.id)) {
      this.membersFallsTTCMutSet.Add(root.GetFallsTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.MagmaTTCExists(elementI.id)) {
      this.membersMagmaTTCMutSet.Add(root.GetMagmaTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.CliffTTCExists(elementI.id)) {
      this.membersCliffTTCMutSet.Add(root.GetCliffTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.RavaNestTTCExists(elementI.id)) {
      this.membersRavaNestTTCMutSet.Add(root.GetRavaNestTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.CliffLandingTTCExists(elementI.id)) {
      this.membersCliffLandingTTCMutSet.Add(root.GetCliffLandingTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.StoneTTCExists(elementI.id)) {
      this.membersStoneTTCMutSet.Add(root.GetStoneTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.GrassTTCExists(elementI.id)) {
      this.membersGrassTTCMutSet.Add(root.GetGrassTTC(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Remove(ITerrainTileComponent elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.ArmorExists(elementI.id)) {
      this.membersArmorMutSet.Remove(root.GetArmor(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.InertiaRingExists(elementI.id)) {
      this.membersInertiaRingMutSet.Remove(root.GetInertiaRing(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.GlaiveExists(elementI.id)) {
      this.membersGlaiveMutSet.Remove(root.GetGlaive(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.ManaPotionExists(elementI.id)) {
      this.membersManaPotionMutSet.Remove(root.GetManaPotion(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.HealthPotionExists(elementI.id)) {
      this.membersHealthPotionMutSet.Remove(root.GetHealthPotion(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.TimeAnchorTTCExists(elementI.id)) {
      this.membersTimeAnchorTTCMutSet.Remove(root.GetTimeAnchorTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.StaircaseTTCExists(elementI.id)) {
      this.membersStaircaseTTCMutSet.Remove(root.GetStaircaseTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.WallTTCExists(elementI.id)) {
      this.membersWallTTCMutSet.Remove(root.GetWallTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.BloodTTCExists(elementI.id)) {
      this.membersBloodTTCMutSet.Remove(root.GetBloodTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.RocksTTCExists(elementI.id)) {
      this.membersRocksTTCMutSet.Remove(root.GetRocksTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.DownstairsTTCExists(elementI.id)) {
      this.membersDownstairsTTCMutSet.Remove(root.GetDownstairsTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.UpstairsTTCExists(elementI.id)) {
      this.membersUpstairsTTCMutSet.Remove(root.GetUpstairsTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.CaveTTCExists(elementI.id)) {
      this.membersCaveTTCMutSet.Remove(root.GetCaveTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.FallsTTCExists(elementI.id)) {
      this.membersFallsTTCMutSet.Remove(root.GetFallsTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.MagmaTTCExists(elementI.id)) {
      this.membersMagmaTTCMutSet.Remove(root.GetMagmaTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.CliffTTCExists(elementI.id)) {
      this.membersCliffTTCMutSet.Remove(root.GetCliffTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.RavaNestTTCExists(elementI.id)) {
      this.membersRavaNestTTCMutSet.Remove(root.GetRavaNestTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.CliffLandingTTCExists(elementI.id)) {
      this.membersCliffLandingTTCMutSet.Remove(root.GetCliffLandingTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.StoneTTCExists(elementI.id)) {
      this.membersStoneTTCMutSet.Remove(root.GetStoneTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.GrassTTCExists(elementI.id)) {
      this.membersGrassTTCMutSet.Remove(root.GetGrassTTC(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Clear() {
    this.membersArmorMutSet.Clear();
    this.membersInertiaRingMutSet.Clear();
    this.membersGlaiveMutSet.Clear();
    this.membersManaPotionMutSet.Clear();
    this.membersHealthPotionMutSet.Clear();
    this.membersTimeAnchorTTCMutSet.Clear();
    this.membersStaircaseTTCMutSet.Clear();
    this.membersWallTTCMutSet.Clear();
    this.membersBloodTTCMutSet.Clear();
    this.membersRocksTTCMutSet.Clear();
    this.membersDownstairsTTCMutSet.Clear();
    this.membersUpstairsTTCMutSet.Clear();
    this.membersCaveTTCMutSet.Clear();
    this.membersFallsTTCMutSet.Clear();
    this.membersMagmaTTCMutSet.Clear();
    this.membersCliffTTCMutSet.Clear();
    this.membersRavaNestTTCMutSet.Clear();
    this.membersCliffLandingTTCMutSet.Clear();
    this.membersStoneTTCMutSet.Clear();
    this.membersGrassTTCMutSet.Clear();
  }
  public int Count {
    get {
      return
        this.membersArmorMutSet.Count +
        this.membersInertiaRingMutSet.Count +
        this.membersGlaiveMutSet.Count +
        this.membersManaPotionMutSet.Count +
        this.membersHealthPotionMutSet.Count +
        this.membersTimeAnchorTTCMutSet.Count +
        this.membersStaircaseTTCMutSet.Count +
        this.membersWallTTCMutSet.Count +
        this.membersBloodTTCMutSet.Count +
        this.membersRocksTTCMutSet.Count +
        this.membersDownstairsTTCMutSet.Count +
        this.membersUpstairsTTCMutSet.Count +
        this.membersCaveTTCMutSet.Count +
        this.membersFallsTTCMutSet.Count +
        this.membersMagmaTTCMutSet.Count +
        this.membersCliffTTCMutSet.Count +
        this.membersRavaNestTTCMutSet.Count +
        this.membersCliffLandingTTCMutSet.Count +
        this.membersStoneTTCMutSet.Count +
        this.membersGrassTTCMutSet.Count
        ;
    }
  }
  public ITerrainTileComponent GetArbitrary() {
    foreach (var element in this) {
      return element;
    }
    throw new Exception("Can't get element from empty bunch!");
  }

  public void Destruct() {
    var tempMembersArmorMutSet = this.membersArmorMutSet;
    var tempMembersInertiaRingMutSet = this.membersInertiaRingMutSet;
    var tempMembersGlaiveMutSet = this.membersGlaiveMutSet;
    var tempMembersManaPotionMutSet = this.membersManaPotionMutSet;
    var tempMembersHealthPotionMutSet = this.membersHealthPotionMutSet;
    var tempMembersTimeAnchorTTCMutSet = this.membersTimeAnchorTTCMutSet;
    var tempMembersStaircaseTTCMutSet = this.membersStaircaseTTCMutSet;
    var tempMembersWallTTCMutSet = this.membersWallTTCMutSet;
    var tempMembersBloodTTCMutSet = this.membersBloodTTCMutSet;
    var tempMembersRocksTTCMutSet = this.membersRocksTTCMutSet;
    var tempMembersDownstairsTTCMutSet = this.membersDownstairsTTCMutSet;
    var tempMembersUpstairsTTCMutSet = this.membersUpstairsTTCMutSet;
    var tempMembersCaveTTCMutSet = this.membersCaveTTCMutSet;
    var tempMembersFallsTTCMutSet = this.membersFallsTTCMutSet;
    var tempMembersMagmaTTCMutSet = this.membersMagmaTTCMutSet;
    var tempMembersCliffTTCMutSet = this.membersCliffTTCMutSet;
    var tempMembersRavaNestTTCMutSet = this.membersRavaNestTTCMutSet;
    var tempMembersCliffLandingTTCMutSet = this.membersCliffLandingTTCMutSet;
    var tempMembersStoneTTCMutSet = this.membersStoneTTCMutSet;
    var tempMembersGrassTTCMutSet = this.membersGrassTTCMutSet;

    this.Delete();
    tempMembersArmorMutSet.Destruct();
    tempMembersInertiaRingMutSet.Destruct();
    tempMembersGlaiveMutSet.Destruct();
    tempMembersManaPotionMutSet.Destruct();
    tempMembersHealthPotionMutSet.Destruct();
    tempMembersTimeAnchorTTCMutSet.Destruct();
    tempMembersStaircaseTTCMutSet.Destruct();
    tempMembersWallTTCMutSet.Destruct();
    tempMembersBloodTTCMutSet.Destruct();
    tempMembersRocksTTCMutSet.Destruct();
    tempMembersDownstairsTTCMutSet.Destruct();
    tempMembersUpstairsTTCMutSet.Destruct();
    tempMembersCaveTTCMutSet.Destruct();
    tempMembersFallsTTCMutSet.Destruct();
    tempMembersMagmaTTCMutSet.Destruct();
    tempMembersCliffTTCMutSet.Destruct();
    tempMembersRavaNestTTCMutSet.Destruct();
    tempMembersCliffLandingTTCMutSet.Destruct();
    tempMembersStoneTTCMutSet.Destruct();
    tempMembersGrassTTCMutSet.Destruct();
  }
  public IEnumerator<ITerrainTileComponent> GetEnumerator() {
    foreach (var element in this.membersArmorMutSet) {
      yield return new ArmorAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersInertiaRingMutSet) {
      yield return new InertiaRingAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersGlaiveMutSet) {
      yield return new GlaiveAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersManaPotionMutSet) {
      yield return new ManaPotionAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersHealthPotionMutSet) {
      yield return new HealthPotionAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersTimeAnchorTTCMutSet) {
      yield return new TimeAnchorTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersStaircaseTTCMutSet) {
      yield return new StaircaseTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersWallTTCMutSet) {
      yield return new WallTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersBloodTTCMutSet) {
      yield return new BloodTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersRocksTTCMutSet) {
      yield return new RocksTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersDownstairsTTCMutSet) {
      yield return new DownstairsTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersUpstairsTTCMutSet) {
      yield return new UpstairsTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersCaveTTCMutSet) {
      yield return new CaveTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersFallsTTCMutSet) {
      yield return new FallsTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersMagmaTTCMutSet) {
      yield return new MagmaTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersCliffTTCMutSet) {
      yield return new CliffTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersRavaNestTTCMutSet) {
      yield return new RavaNestTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersCliffLandingTTCMutSet) {
      yield return new CliffLandingTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersStoneTTCMutSet) {
      yield return new StoneTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersGrassTTCMutSet) {
      yield return new GrassTTCAsITerrainTileComponent(element);
    }
  }
    public List<Armor> GetAllArmor() {
      var result = new List<Armor>();
      foreach (var thing in this.membersArmorMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<Armor> ClearAllArmor() {
      var result = new List<Armor>();
      this.membersArmorMutSet.Clear();
      return result;
    }
    public Armor GetOnlyArmorOrNull() {
      var result = GetAllArmor();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return Armor.Null;
      }
    }
    public List<InertiaRing> GetAllInertiaRing() {
      var result = new List<InertiaRing>();
      foreach (var thing in this.membersInertiaRingMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<InertiaRing> ClearAllInertiaRing() {
      var result = new List<InertiaRing>();
      this.membersInertiaRingMutSet.Clear();
      return result;
    }
    public InertiaRing GetOnlyInertiaRingOrNull() {
      var result = GetAllInertiaRing();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return InertiaRing.Null;
      }
    }
    public List<Glaive> GetAllGlaive() {
      var result = new List<Glaive>();
      foreach (var thing in this.membersGlaiveMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<Glaive> ClearAllGlaive() {
      var result = new List<Glaive>();
      this.membersGlaiveMutSet.Clear();
      return result;
    }
    public Glaive GetOnlyGlaiveOrNull() {
      var result = GetAllGlaive();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return Glaive.Null;
      }
    }
    public List<ManaPotion> GetAllManaPotion() {
      var result = new List<ManaPotion>();
      foreach (var thing in this.membersManaPotionMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<ManaPotion> ClearAllManaPotion() {
      var result = new List<ManaPotion>();
      this.membersManaPotionMutSet.Clear();
      return result;
    }
    public ManaPotion GetOnlyManaPotionOrNull() {
      var result = GetAllManaPotion();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return ManaPotion.Null;
      }
    }
    public List<HealthPotion> GetAllHealthPotion() {
      var result = new List<HealthPotion>();
      foreach (var thing in this.membersHealthPotionMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<HealthPotion> ClearAllHealthPotion() {
      var result = new List<HealthPotion>();
      this.membersHealthPotionMutSet.Clear();
      return result;
    }
    public HealthPotion GetOnlyHealthPotionOrNull() {
      var result = GetAllHealthPotion();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return HealthPotion.Null;
      }
    }
    public List<TimeAnchorTTC> GetAllTimeAnchorTTC() {
      var result = new List<TimeAnchorTTC>();
      foreach (var thing in this.membersTimeAnchorTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<TimeAnchorTTC> ClearAllTimeAnchorTTC() {
      var result = new List<TimeAnchorTTC>();
      this.membersTimeAnchorTTCMutSet.Clear();
      return result;
    }
    public TimeAnchorTTC GetOnlyTimeAnchorTTCOrNull() {
      var result = GetAllTimeAnchorTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return TimeAnchorTTC.Null;
      }
    }
    public List<StaircaseTTC> GetAllStaircaseTTC() {
      var result = new List<StaircaseTTC>();
      foreach (var thing in this.membersStaircaseTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<StaircaseTTC> ClearAllStaircaseTTC() {
      var result = new List<StaircaseTTC>();
      this.membersStaircaseTTCMutSet.Clear();
      return result;
    }
    public StaircaseTTC GetOnlyStaircaseTTCOrNull() {
      var result = GetAllStaircaseTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return StaircaseTTC.Null;
      }
    }
    public List<WallTTC> GetAllWallTTC() {
      var result = new List<WallTTC>();
      foreach (var thing in this.membersWallTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<WallTTC> ClearAllWallTTC() {
      var result = new List<WallTTC>();
      this.membersWallTTCMutSet.Clear();
      return result;
    }
    public WallTTC GetOnlyWallTTCOrNull() {
      var result = GetAllWallTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return WallTTC.Null;
      }
    }
    public List<BloodTTC> GetAllBloodTTC() {
      var result = new List<BloodTTC>();
      foreach (var thing in this.membersBloodTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<BloodTTC> ClearAllBloodTTC() {
      var result = new List<BloodTTC>();
      this.membersBloodTTCMutSet.Clear();
      return result;
    }
    public BloodTTC GetOnlyBloodTTCOrNull() {
      var result = GetAllBloodTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return BloodTTC.Null;
      }
    }
    public List<RocksTTC> GetAllRocksTTC() {
      var result = new List<RocksTTC>();
      foreach (var thing in this.membersRocksTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<RocksTTC> ClearAllRocksTTC() {
      var result = new List<RocksTTC>();
      this.membersRocksTTCMutSet.Clear();
      return result;
    }
    public RocksTTC GetOnlyRocksTTCOrNull() {
      var result = GetAllRocksTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return RocksTTC.Null;
      }
    }
    public List<DownstairsTTC> GetAllDownstairsTTC() {
      var result = new List<DownstairsTTC>();
      foreach (var thing in this.membersDownstairsTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<DownstairsTTC> ClearAllDownstairsTTC() {
      var result = new List<DownstairsTTC>();
      this.membersDownstairsTTCMutSet.Clear();
      return result;
    }
    public DownstairsTTC GetOnlyDownstairsTTCOrNull() {
      var result = GetAllDownstairsTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return DownstairsTTC.Null;
      }
    }
    public List<UpstairsTTC> GetAllUpstairsTTC() {
      var result = new List<UpstairsTTC>();
      foreach (var thing in this.membersUpstairsTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<UpstairsTTC> ClearAllUpstairsTTC() {
      var result = new List<UpstairsTTC>();
      this.membersUpstairsTTCMutSet.Clear();
      return result;
    }
    public UpstairsTTC GetOnlyUpstairsTTCOrNull() {
      var result = GetAllUpstairsTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return UpstairsTTC.Null;
      }
    }
    public List<CaveTTC> GetAllCaveTTC() {
      var result = new List<CaveTTC>();
      foreach (var thing in this.membersCaveTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<CaveTTC> ClearAllCaveTTC() {
      var result = new List<CaveTTC>();
      this.membersCaveTTCMutSet.Clear();
      return result;
    }
    public CaveTTC GetOnlyCaveTTCOrNull() {
      var result = GetAllCaveTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return CaveTTC.Null;
      }
    }
    public List<FallsTTC> GetAllFallsTTC() {
      var result = new List<FallsTTC>();
      foreach (var thing in this.membersFallsTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<FallsTTC> ClearAllFallsTTC() {
      var result = new List<FallsTTC>();
      this.membersFallsTTCMutSet.Clear();
      return result;
    }
    public FallsTTC GetOnlyFallsTTCOrNull() {
      var result = GetAllFallsTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return FallsTTC.Null;
      }
    }
    public List<MagmaTTC> GetAllMagmaTTC() {
      var result = new List<MagmaTTC>();
      foreach (var thing in this.membersMagmaTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<MagmaTTC> ClearAllMagmaTTC() {
      var result = new List<MagmaTTC>();
      this.membersMagmaTTCMutSet.Clear();
      return result;
    }
    public MagmaTTC GetOnlyMagmaTTCOrNull() {
      var result = GetAllMagmaTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return MagmaTTC.Null;
      }
    }
    public List<CliffTTC> GetAllCliffTTC() {
      var result = new List<CliffTTC>();
      foreach (var thing in this.membersCliffTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<CliffTTC> ClearAllCliffTTC() {
      var result = new List<CliffTTC>();
      this.membersCliffTTCMutSet.Clear();
      return result;
    }
    public CliffTTC GetOnlyCliffTTCOrNull() {
      var result = GetAllCliffTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return CliffTTC.Null;
      }
    }
    public List<RavaNestTTC> GetAllRavaNestTTC() {
      var result = new List<RavaNestTTC>();
      foreach (var thing in this.membersRavaNestTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<RavaNestTTC> ClearAllRavaNestTTC() {
      var result = new List<RavaNestTTC>();
      this.membersRavaNestTTCMutSet.Clear();
      return result;
    }
    public RavaNestTTC GetOnlyRavaNestTTCOrNull() {
      var result = GetAllRavaNestTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return RavaNestTTC.Null;
      }
    }
    public List<CliffLandingTTC> GetAllCliffLandingTTC() {
      var result = new List<CliffLandingTTC>();
      foreach (var thing in this.membersCliffLandingTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<CliffLandingTTC> ClearAllCliffLandingTTC() {
      var result = new List<CliffLandingTTC>();
      this.membersCliffLandingTTCMutSet.Clear();
      return result;
    }
    public CliffLandingTTC GetOnlyCliffLandingTTCOrNull() {
      var result = GetAllCliffLandingTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return CliffLandingTTC.Null;
      }
    }
    public List<StoneTTC> GetAllStoneTTC() {
      var result = new List<StoneTTC>();
      foreach (var thing in this.membersStoneTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<StoneTTC> ClearAllStoneTTC() {
      var result = new List<StoneTTC>();
      this.membersStoneTTCMutSet.Clear();
      return result;
    }
    public StoneTTC GetOnlyStoneTTCOrNull() {
      var result = GetAllStoneTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return StoneTTC.Null;
      }
    }
    public List<GrassTTC> GetAllGrassTTC() {
      var result = new List<GrassTTC>();
      foreach (var thing in this.membersGrassTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<GrassTTC> ClearAllGrassTTC() {
      var result = new List<GrassTTC>();
      this.membersGrassTTCMutSet.Clear();
      return result;
    }
    public GrassTTC GetOnlyGrassTTCOrNull() {
      var result = GetAllGrassTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return GrassTTC.Null;
      }
    }
    public List<IOffenseItem> GetAllIOffenseItem() {
      var result = new List<IOffenseItem>();
      foreach (var obj in this.membersGlaiveMutSet) {
        result.Add(
            new GlaiveAsIOffenseItem(obj));
      }
      return result;
    }
    public List<IOffenseItem> ClearAllIOffenseItem() {
      var result = new List<IOffenseItem>();
      this.membersGlaiveMutSet.Clear();
      return result;
    }
    public IOffenseItem GetOnlyIOffenseItemOrNull() {
      var result = GetAllIOffenseItem();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIOffenseItem.Null;
      }
    }
                 public List<IUnwalkableTTC> GetAllIUnwalkableTTC() {
      var result = new List<IUnwalkableTTC>();
      foreach (var obj in this.membersFallsTTCMutSet) {
        result.Add(
            new FallsTTCAsIUnwalkableTTC(obj));
      }
      foreach (var obj in this.membersMagmaTTCMutSet) {
        result.Add(
            new MagmaTTCAsIUnwalkableTTC(obj));
      }
      return result;
    }
    public List<IUnwalkableTTC> ClearAllIUnwalkableTTC() {
      var result = new List<IUnwalkableTTC>();
      this.membersFallsTTCMutSet.Clear();
      this.membersMagmaTTCMutSet.Clear();
      return result;
    }
    public IUnwalkableTTC GetOnlyIUnwalkableTTCOrNull() {
      var result = GetAllIUnwalkableTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIUnwalkableTTC.Null;
      }
    }
                 public List<IItem> GetAllIItem() {
      var result = new List<IItem>();
      foreach (var obj in this.membersArmorMutSet) {
        result.Add(
            new ArmorAsIItem(obj));
      }
      foreach (var obj in this.membersInertiaRingMutSet) {
        result.Add(
            new InertiaRingAsIItem(obj));
      }
      foreach (var obj in this.membersGlaiveMutSet) {
        result.Add(
            new GlaiveAsIItem(obj));
      }
      foreach (var obj in this.membersManaPotionMutSet) {
        result.Add(
            new ManaPotionAsIItem(obj));
      }
      foreach (var obj in this.membersHealthPotionMutSet) {
        result.Add(
            new HealthPotionAsIItem(obj));
      }
      return result;
    }
    public List<IItem> ClearAllIItem() {
      var result = new List<IItem>();
      this.membersArmorMutSet.Clear();
      this.membersInertiaRingMutSet.Clear();
      this.membersGlaiveMutSet.Clear();
      this.membersManaPotionMutSet.Clear();
      this.membersHealthPotionMutSet.Clear();
      return result;
    }
    public IItem GetOnlyIItemOrNull() {
      var result = GetAllIItem();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIItem.Null;
      }
    }
                 public List<IInertiaItem> GetAllIInertiaItem() {
      var result = new List<IInertiaItem>();
      foreach (var obj in this.membersInertiaRingMutSet) {
        result.Add(
            new InertiaRingAsIInertiaItem(obj));
      }
      return result;
    }
    public List<IInertiaItem> ClearAllIInertiaItem() {
      var result = new List<IInertiaItem>();
      this.membersInertiaRingMutSet.Clear();
      return result;
    }
    public IInertiaItem GetOnlyIInertiaItemOrNull() {
      var result = GetAllIInertiaItem();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIInertiaItem.Null;
      }
    }
                 public List<IDefenseItem> GetAllIDefenseItem() {
      var result = new List<IDefenseItem>();
      foreach (var obj in this.membersArmorMutSet) {
        result.Add(
            new ArmorAsIDefenseItem(obj));
      }
      return result;
    }
    public List<IDefenseItem> ClearAllIDefenseItem() {
      var result = new List<IDefenseItem>();
      this.membersArmorMutSet.Clear();
      return result;
    }
    public IDefenseItem GetOnlyIDefenseItemOrNull() {
      var result = GetAllIDefenseItem();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIDefenseItem.Null;
      }
    }
                 public List<IUsableItem> GetAllIUsableItem() {
      var result = new List<IUsableItem>();
      foreach (var obj in this.membersManaPotionMutSet) {
        result.Add(
            new ManaPotionAsIUsableItem(obj));
      }
      foreach (var obj in this.membersHealthPotionMutSet) {
        result.Add(
            new HealthPotionAsIUsableItem(obj));
      }
      return result;
    }
    public List<IUsableItem> ClearAllIUsableItem() {
      var result = new List<IUsableItem>();
      this.membersManaPotionMutSet.Clear();
      this.membersHealthPotionMutSet.Clear();
      return result;
    }
    public IUsableItem GetOnlyIUsableItemOrNull() {
      var result = GetAllIUsableItem();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIUsableItem.Null;
      }
    }
             }
}
