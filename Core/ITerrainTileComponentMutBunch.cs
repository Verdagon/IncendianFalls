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

    if (!root.DecorativeTTCMutSetExists(membersDecorativeTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersDecorativeTTCMutSet");
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
    if (root.DecorativeTTCMutSetExists(membersDecorativeTTCMutSet.id)) {
      membersDecorativeTTCMutSet.FindReachableObjects(foundIds);
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
  public DecorativeTTCMutSet membersDecorativeTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersDecorativeTTCMutSet of null!");
      }
      return new DecorativeTTCMutSet(root, incarnation.membersDecorativeTTCMutSet);
    }
                       }

  public static ITerrainTileComponentMutBunch New(Root root) {
    return root.EffectITerrainTileComponentMutBunchCreate(
      root.EffectArmorMutSetCreate()
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
      root.EffectDecorativeTTCMutSetCreate()
        );
  }
  public void Add(ITerrainTileComponent elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.ArmorExists(elementI.id)) {
      this.membersArmorMutSet.Add(root.GetArmor(elementI.id));
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
    if (root.DecorativeTTCExists(elementI.id)) {
      this.membersDecorativeTTCMutSet.Add(root.GetDecorativeTTC(elementI.id));
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
    if (root.DecorativeTTCExists(elementI.id)) {
      this.membersDecorativeTTCMutSet.Remove(root.GetDecorativeTTC(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Clear() {
    this.membersArmorMutSet.Clear();
    this.membersGlaiveMutSet.Clear();
    this.membersManaPotionMutSet.Clear();
    this.membersHealthPotionMutSet.Clear();
    this.membersTimeAnchorTTCMutSet.Clear();
    this.membersStaircaseTTCMutSet.Clear();
    this.membersDecorativeTTCMutSet.Clear();
  }
  public int Count {
    get {
      return
        this.membersArmorMutSet.Count +
        this.membersGlaiveMutSet.Count +
        this.membersManaPotionMutSet.Count +
        this.membersHealthPotionMutSet.Count +
        this.membersTimeAnchorTTCMutSet.Count +
        this.membersStaircaseTTCMutSet.Count +
        this.membersDecorativeTTCMutSet.Count
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
    var tempMembersGlaiveMutSet = this.membersGlaiveMutSet;
    var tempMembersManaPotionMutSet = this.membersManaPotionMutSet;
    var tempMembersHealthPotionMutSet = this.membersHealthPotionMutSet;
    var tempMembersTimeAnchorTTCMutSet = this.membersTimeAnchorTTCMutSet;
    var tempMembersStaircaseTTCMutSet = this.membersStaircaseTTCMutSet;
    var tempMembersDecorativeTTCMutSet = this.membersDecorativeTTCMutSet;

    this.Delete();
    tempMembersArmorMutSet.Destruct();
    tempMembersGlaiveMutSet.Destruct();
    tempMembersManaPotionMutSet.Destruct();
    tempMembersHealthPotionMutSet.Destruct();
    tempMembersTimeAnchorTTCMutSet.Destruct();
    tempMembersStaircaseTTCMutSet.Destruct();
    tempMembersDecorativeTTCMutSet.Destruct();
  }
  public IEnumerator<ITerrainTileComponent> GetEnumerator() {
    foreach (var element in this.membersArmorMutSet) {
      yield return new ArmorAsITerrainTileComponent(element);
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
    foreach (var element in this.membersDecorativeTTCMutSet) {
      yield return new DecorativeTTCAsITerrainTileComponent(element);
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
    public List<DecorativeTTC> GetAllDecorativeTTC() {
      var result = new List<DecorativeTTC>();
      foreach (var thing in this.membersDecorativeTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<DecorativeTTC> ClearAllDecorativeTTC() {
      var result = new List<DecorativeTTC>();
      this.membersDecorativeTTCMutSet.Clear();
      return result;
    }
    public DecorativeTTC GetOnlyDecorativeTTCOrNull() {
      var result = GetAllDecorativeTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return DecorativeTTC.Null;
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
                 public List<IItem> GetAllIItem() {
      var result = new List<IItem>();
      foreach (var obj in this.membersArmorMutSet) {
        result.Add(
            new ArmorAsIItem(obj));
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
             }
}
