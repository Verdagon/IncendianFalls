package net.verdagon.chronobasecs.generator.chronobase.set

import net.verdagon.chronobasecs.generator.chronobase.Generator.toCS
import net.verdagon.chronobasecs.compiled.{ListS, MutableS, SetS}
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions

object MutSetRootMethods {

  def generateRootSetMethods(
                              opt: ChronobaseOptions,
                              set: SetS
  ): String = {
    val SetS(setName, MutableS, elementType) = set

    val elementTypeCSName = toCS(elementType)

    s"""
       |    public int Get${setName}Hash(int id, int version, ${setName}Incarnation incarnation) {
       |      int result = id * version;
       |      foreach (var element in incarnation.set) {
       |        result += id * version * element.GetDeterministicHashCode();
       |      }
       |      return result;
       |    }
       |    public ${setName}Incarnation Get${setName}Incarnation(int id) {
       |      return rootIncarnation.incarnations${setName}[id].incarnation;
       |    }
       |    public ${setName} Get${setName}(int id) {
       |      return new ${setName}(this, id);
       |    }
       |    public List<${setName}> All${setName}() {
       |      List<${setName}> result = new List<${setName}>(rootIncarnation.incarnations${setName}.Count);
       |      foreach (var id in rootIncarnation.incarnations${setName}.Keys) {
       |        result.Add(new ${setName}(this, id));
       |      }
       |      return result;
       |    }
       |    public bool ${setName}Exists(int id) {
       |      return rootIncarnation.incarnations${setName}.ContainsKey(id);
       |    }
       |    public void CheckHas${setName}(${setName} thing) {
       |      CheckRootsEqual(this, thing.root);
       |      CheckHas${setName}(thing.id);
       |    }
       |    public void CheckHas${setName}(int id) {
       |      if (!rootIncarnation.incarnations${setName}.ContainsKey(id)) {
       |        throw new System.Exception("Invalid ${setName}}: " + id);
       |      }
       |    }
       |    public ${setName} Effect${setName}Create() {
       |      CheckUnlocked();
       |      var id = NewId();
       |      var incarnation = new ${setName}Incarnation(new SortedSet<int>());
       |      EffectInternalCreate${setName}(id, rootIncarnation.version, incarnation);
       |      return new ${setName}(this, id);
       |    }
       |    public void EffectInternalCreate${setName}(int id, int incarnationVersion, ${setName}Incarnation incarnation) {
       |      var effect = new ${setName}CreateEffect(id);
       |      rootIncarnation.incarnations${setName}
       |          .Add(
       |              id,
       |              new VersionAndIncarnation<${setName}Incarnation>(
       |                  incarnationVersion,
       |                  incarnation));
       |""".stripMargin +
      (if (opt.hash) {
        s"""      this.rootIncarnation.hash += Get${setName}Hash(id, incarnationVersion, incarnation);
       |
           |""".stripMargin
      } else "") +
      s"""      effects${setName}CreateEffect.Add(effect);
       |    }
       |    public void Effect${setName}Delete(int id) {
       |      CheckUnlocked();
       |      var effect = new ${setName}DeleteEffect(id);
       |      effects${setName}DeleteEffect.Add(effect);
       |      var versionAndIncarnation = rootIncarnation.incarnations${setName}[id];
       |""".stripMargin +
      (if (opt.hash) {
        s"""      this.rootIncarnation.hash -=
       |          Get${setName}Hash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
       |
           |""".stripMargin
      } else "") +
      s"""      rootIncarnation.incarnations${setName}.Remove(id);
       |    }
       |
       """.stripMargin +
    s"""
       |    public void Effect${setName}Add(int setId, int elementId) {
       |      CheckUnlocked();
       |      CheckHas${setName}(setId);
       |      CheckHas${elementTypeCSName}(elementId);
       |
       |      var effect = new ${setName}AddEffect(setId, elementId);
       |
       |      var oldIncarnationAndVersion = rootIncarnation.incarnations${setName}[setId];
       |      if (oldIncarnationAndVersion.incarnation.set.Contains(elementId)) {
       |        throw new Exception("Element already exists!");
       |      }
       |      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
       |        oldIncarnationAndVersion.incarnation.set.Add(elementId);
       |""".stripMargin +
      (if (opt.hash) {
        s"""        this.rootIncarnation.hash += setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
       |
           |""".stripMargin
      } else "") +
      s"""      } else {
       |        var oldMap = oldIncarnationAndVersion.incarnation.set;
       |        var newMap = new SortedSet<int>(oldMap);
       |        newMap.Add(elementId);
       |        var newIncarnation = new ${setName}Incarnation(newMap);
       |        rootIncarnation.incarnations${setName}[setId] =
       |            new VersionAndIncarnation<${setName}Incarnation>(
       |                rootIncarnation.version,
       |                newIncarnation);
       |""".stripMargin +
      (if (opt.hash) {
        s"""        this.rootIncarnation.hash -= Get${setName}Hash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
       |        this.rootIncarnation.hash += Get${setName}Hash(setId, rootIncarnation.version, newIncarnation);
       |
           |""".stripMargin
      } else "") +
      s"""      }
       |      effects${setName}AddEffect.Add(effect);
       |    }
       |    public void Effect${setName}Remove(int setId, int elementId) {
       |      CheckUnlocked();
       |      CheckHas${setName}(setId);
       |      CheckHas${elementTypeCSName}(elementId);
       |
       |      var effect = new ${setName}RemoveEffect(setId, elementId);
       |
       |      var oldIncarnationAndVersion = rootIncarnation.incarnations${setName}[setId];
       |      if (!oldIncarnationAndVersion.incarnation.set.Contains(elementId)) {
       |        throw new Exception("Element not found! " + elementId);
       |      }
       |      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
       |""".stripMargin +
      (if (opt.hash) {
        s"""        this.rootIncarnation.hash -= setId * rootIncarnation.version * elementId.GetDeterministicHashCode();
       |
           |""".stripMargin
      } else "") +
      s"""        oldIncarnationAndVersion.incarnation.set.Remove(elementId);
       |      } else {
       |        var oldMap = oldIncarnationAndVersion.incarnation.set;
       |        var newMap = new SortedSet<int>(oldMap);
       |        newMap.Remove(elementId);
       |        var newIncarnation = new ${setName}Incarnation(newMap);
       |        rootIncarnation.incarnations${setName}[setId] =
       |            new VersionAndIncarnation<${setName}Incarnation>(
       |                rootIncarnation.version, newIncarnation);
       |""".stripMargin +
      (if (opt.hash) {
        s"""        this.rootIncarnation.hash -= Get${setName}Hash(setId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
       |        this.rootIncarnation.hash += Get${setName}Hash(setId, rootIncarnation.version, newIncarnation);
       |
           |""".stripMargin
      } else "") +
      s"""      }
       |      effects${setName}RemoveEffect.Add(effect);
       |    }
       |
       """.stripMargin +
    s"""
       |    public void Add${setName}Observer(int id, I${setName}EffectObserver observer) {
       |      List<I${setName}EffectObserver> obsies;
       |      if (!observersFor${setName}.TryGetValue(id, out obsies)) {
       |        obsies = new List<I${setName}EffectObserver>();
       |      }
       |      obsies.Add(observer);
       |      observersFor${setName}[id] = obsies;
       |    }
       |
       |    public void Remove${setName}Observer(int id, I${setName}EffectObserver observer) {
       |      if (observersFor${setName}.ContainsKey(id)) {
       |        var list = observersFor${setName}[id];
       |        list.Remove(observer);
       |        if (list.Count == 0) {
       |          observersFor${setName}.Remove(id);
       |        }
       |      } else {
       |        throw new Exception("Couldnt find!");
       |      }
       |    }
       """.stripMargin +
    generateBroadcaster(opt, set)
  }

  def generateBroadcaster(opt: ChronobaseOptions, set: SetS): String = {
    val SetS(setName, MutableS, elementType) = set

    val setCSType = toCS(set.tyype)

    val observerName = s"I${setName}EffectObserver"
    val createEffectName = s"${setName}CreateEffect"
    val deleteEffectName = s"${setName}DeleteEffect"
    val addEffectName = s"${setName}AddEffect"
    val removeEffectName = s"${setName}RemoveEffect"

    // Delete has to be first. This is so it can clear away all those
    // observers observing this object, so they don't have to remove
    // themselves, and if something is ressurrected via revert, the
    // observers for the old existence won't be notified.
    s"""
       |  public void Broadcast${setCSType}Effects(
       |      SortedDictionary<int, List<I${setCSType}EffectObserver>> observers) {
       |    foreach (var effect in effects${deleteEffectName}) {
       |      if (observers.TryGetValue(0, out List<${observerName}> globalObservers)) {
       |        foreach (var observer in globalObservers) {
       |          observer.On${setCSType}Effect(effect);
       |        }
       |      }
       |      if (observers.TryGetValue(effect.id, out List<${observerName}> objObservers)) {
       |        foreach (var observer in objObservers) {
       |          observer.On${setCSType}Effect(effect);
       |        }
       |        observersFor${setCSType}.Remove(effect.id);
       |      }
       |    }
       |    effects${deleteEffectName}.Clear();
       |""".stripMargin +
    List(addEffectName, removeEffectName, createEffectName)
        .map(effectCSType => {
          s"""
             |    foreach (var effect in effects${effectCSType}) {
             |      if (observers.TryGetValue(0, out List<${observerName}> globalObservers)) {
             |        foreach (var observer in globalObservers) {
             |          observer.On${setCSType}Effect(effect);
             |        }
             |      }
             |      if (observers.TryGetValue(effect.id, out List<${observerName}> objObservers)) {
             |        foreach (var observer in objObservers) {
             |          observer.On${setCSType}Effect(effect);
             |        }
             |      }
             |    }
             |    effects${effectCSType}.Clear();
             |""".stripMargin
        })
        .mkString("") +
      s"""
         |  }
         |""".stripMargin
  }
}
