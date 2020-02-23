package net.verdagon.chronobasecs.generator.chronobase.list

import net.verdagon.chronobasecs.compiled.{ImmutableS, ListS, MutabilityS, MutableS}
import net.verdagon.chronobasecs.generator.chronobase.Generator.toCS
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions

object MutListRootMethods {
  def generateRootMethods(opt: ChronobaseOptions, list: ListS): String = {
    val ListS(listName, MutableS, elementType) = list

    val elementCSType = toCS(elementType)
    val flattenedElementCSType = toCS(elementType.flatten)

    s"""
       |    public int Get${listName}Hash(int id, int version, ${listName}Incarnation incarnation) {
       |      int result = id * version;
       |      foreach (var element in incarnation.list) {
       |        result += id * version * element.GetDeterministicHashCode();
       |      }
       |      return result;
       |    }
       |    public ${listName}Incarnation Get${listName}Incarnation(int id) {
       |      return rootIncarnation.incarnations${listName}[id].incarnation;
       |    }
       |    public ${listName} Get${listName}(int id) {
       |      return new ${listName}(this, id);
       |    }
       |    public List<${listName}> All${listName}() {
       |      List<${listName}> result = new List<${listName}>(rootIncarnation.incarnations${listName}.Count);
       |      foreach (var id in rootIncarnation.incarnations${listName}.Keys) {
       |        result.Add(new ${listName}(this, id));
       |      }
       |      return result;
       |    }
       |    public bool ${listName}Exists(int id) {
       |      return rootIncarnation.incarnations${listName}.ContainsKey(id);
       |    }
       |    public void CheckHas${listName}(${listName} thing) {
       |      CheckRootsEqual(this, thing.root);
       |      CheckHas${listName}(thing.id);
       |    }
       |    public void CheckHas${listName}(int id) {
       |      if (!rootIncarnation.incarnations${listName}.ContainsKey(id)) {
       |        throw new System.Exception("Invalid ${listName}}: " + id);
       |      }
       |    }
       |    public ${listName} Effect${listName}Create() {
       |      CheckUnlocked();
       |      var id = NewId();
       |      EffectInternalCreate${listName}(id, rootIncarnation.version, new ${listName}Incarnation(new List<${flattenedElementCSType}>()));
       |      return new ${listName}(this, id);
       |    }
       |    public ${listName} Effect${listName}Create(IEnumerable<${elementCSType}> elements) {
       |      var id = NewId();
       |""".stripMargin +
    (elementType.kind.mutability match {
      case MutableS => {
        s"""      var elementsIds = new List<int>();
           |      foreach (var element in elements) {
           |        elementsIds.Add(element.id);
           |      }
           |      var incarnation = new ${listName}Incarnation(elementsIds);
           |      EffectInternalCreate${listName}(id, rootIncarnation.version, incarnation);
           |""".stripMargin
      }
      case ImmutableS => {
        s"""      var incarnation = new ${listName}Incarnation(new List<${elementCSType}>(elements));
           |      EffectInternalCreate${listName}(id, rootIncarnation.version, incarnation);
           |""".stripMargin
      }
    }) +
    s"""      return new ${listName}(this, id);
       |    }
       |    public void EffectInternalCreate${listName}(int id, int incarnationVersion, ${listName}Incarnation incarnation) {
       |      var effect = new ${listName}CreateEffect(id);
       |      rootIncarnation.incarnations${listName}
       |          .Add(
       |              id,
       |              new VersionAndIncarnation<${listName}Incarnation>(
       |                  incarnationVersion,
       |                  incarnation));
       |""".stripMargin +
      (if (opt.hash) {
        s"""      this.rootIncarnation.hash += Get${listName}Hash(id, incarnationVersion, incarnation);
       |
           |""".stripMargin
      } else "") +
      s"""      effects${listName}CreateEffect.Add(effect);
       |    }
       |    public void Effect${listName}Delete(int id) {
       |      CheckUnlocked();
       |      var effect = new ${listName}DeleteEffect(id);
       |      effects${listName}DeleteEffect.Add(effect);
       |      var versionAndIncarnation = rootIncarnation.incarnations${listName}[id];
       |""".stripMargin +
      (if (opt.hash) {
        s"""      this.rootIncarnation.hash -=
       |          Get${listName}Hash(id, versionAndIncarnation.version, versionAndIncarnation.incarnation);
       |
           |""".stripMargin
      } else "") +
      s"""      rootIncarnation.incarnations${listName}.Remove(id);
       |    }
       |    public void Effect${listName}Add(int listId, ${flattenedElementCSType} element) {
       |      CheckUnlocked();
       |      CheckHas${listName}(listId);
       |
    """.stripMargin+
    (elementType.kind.mutability match {
      case MutableS => s"      CheckHas${flattenedElementCSType}(element);"
      case ImmutableS => ""
    }) +
    s"""
       |      var effect = new ${listName}AddEffect(listId, element);
       |
       |      var oldIncarnationAndVersion = rootIncarnation.incarnations${listName}[listId];
       |      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
       |        oldIncarnationAndVersion.incarnation.list.Add(element);
       |""".stripMargin +
      (if (opt.hash) {
        s"""        this.rootIncarnation.hash += listId * rootIncarnation.version * element.GetDeterministicHashCode();
       |
           |""".stripMargin
      } else "") +
      s"""      } else {
       |        var oldMap = oldIncarnationAndVersion.incarnation.list;
       |        var newMap = new List<${flattenedElementCSType}>(oldMap);
       |        newMap.Add(element);
       |        var newIncarnation = new ${listName}Incarnation(newMap);
       |        rootIncarnation.incarnations${listName}[listId] =
       |            new VersionAndIncarnation<${listName}Incarnation>(
       |                rootIncarnation.version,
       |                newIncarnation);
       |""".stripMargin +
      (if (opt.hash) {
        s"""        this.rootIncarnation.hash -= Get${listName}Hash(listId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
       |        this.rootIncarnation.hash += Get${listName}Hash(listId, rootIncarnation.version, newIncarnation);
       |
           |""".stripMargin
      } else "") +
      s"""      }
       |      effects${listName}AddEffect.Add(effect);
       |    }
       |    public void Effect${listName}RemoveAt(int listId, int index) {
       |      CheckUnlocked();
       |      CheckHas${listName}(listId);
       |
       |      var effect = new ${listName}RemoveEffect(listId, index);
       |
       |      var oldIncarnationAndVersion = rootIncarnation.incarnations${listName}[listId];
       |      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
       |        var oldElement = oldIncarnationAndVersion.incarnation.list[index];
       |""".stripMargin +
      (if (opt.hash) {
        s"""        this.rootIncarnation.hash -= listId * rootIncarnation.version * oldElement.GetDeterministicHashCode();
       |
           |""".stripMargin
      } else "") +
      s"""        oldIncarnationAndVersion.incarnation.list.RemoveAt(index);
       |      } else {
       |        var oldMap = oldIncarnationAndVersion.incarnation.list;
       |        var newMap = new List<${flattenedElementCSType}>(oldMap);
       |        newMap.RemoveAt(index);
       |        var newIncarnation = new ${listName}Incarnation(newMap);
       |        rootIncarnation.incarnations${listName}[listId] =
       |            new VersionAndIncarnation<${listName}Incarnation>(
       |                rootIncarnation.version, newIncarnation);
       |""".stripMargin +
      (if (opt.hash) {
        s"""        this.rootIncarnation.hash -= Get${listName}Hash(listId, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
       |        this.rootIncarnation.hash += Get${listName}Hash(listId, rootIncarnation.version, newIncarnation);
           |""".stripMargin
      } else "") +
      s"""
       |      }
       |      effects${listName}RemoveEffect.Add(effect);
       |    }
       """.stripMargin +
    s"""
       |    public void Add${listName}Observer(int id, I${listName}EffectObserver observer) {
       |      List<I${listName}EffectObserver> obsies;
       |      if (!observersFor${listName}.TryGetValue(id, out obsies)) {
       |        obsies = new List<I${listName}EffectObserver>();
       |      }
       |      obsies.Add(observer);
       |      observersFor${listName}[id] = obsies;
       |    }
       |
       |    public void Remove${listName}Observer(int id, I${listName}EffectObserver observer) {
       |      if (observersFor${listName}.ContainsKey(id)) {
       |        var list = observersFor${listName}[id];
       |        list.Remove(observer);
       |        if (list.Count == 0) {
       |          observersFor${listName}.Remove(id);
       |        }
       |      } else {
       |        throw new Exception("Couldnt find!");
       |      }
       |    }
       |""".stripMargin +
    generateBroadcaster(opt, list)
  }

  def generateBroadcaster(opt: ChronobaseOptions, list: ListS): String = {
    val ListS(listName, MutableS, elementType) = list

    val listCSType = toCS(list.tyype)

    val observerName = s"I${listName}EffectObserver"
    val createEffectName = s"${listName}CreateEffect"
    val deleteEffectName = s"${listName}DeleteEffect"
    val addEffectName = s"${listName}AddEffect"
    val removeEffectName = s"${listName}RemoveEffect"

    // Delete has to be first. This is so it can clear away all those
    // observers observing this object, so they don't have to remove
    // themselves, and if something is ressurrected via revert, the
    // observers for the old existence won't be notified.
    s"""
       |  public void Broadcast${listCSType}Effects(
       |      SortedDictionary<int, List<I${listCSType}EffectObserver>> observers) {
       |    foreach (var effect in effects${deleteEffectName}) {
       |      if (observers.TryGetValue(0, out List<${observerName}> globalObservers)) {
       |        foreach (var observer in globalObservers) {
       |          observer.On${listCSType}Effect(effect);
       |        }
       |      }
       |      if (observers.TryGetValue(effect.id, out List<${observerName}> objObservers)) {
       |        foreach (var observer in objObservers) {
       |          observer.On${listCSType}Effect(effect);
       |        }
       |        observersFor${listCSType}.Remove(effect.id);
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
             |          observer.On${listCSType}Effect(effect);
             |        }
             |      }
             |      if (observers.TryGetValue(effect.id, out List<${observerName}> objObservers)) {
             |        foreach (var observer in objObservers) {
             |          observer.On${listCSType}Effect(effect);
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
