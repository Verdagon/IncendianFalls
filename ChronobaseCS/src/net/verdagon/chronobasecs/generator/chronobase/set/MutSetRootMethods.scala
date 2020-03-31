package net.verdagon.chronobasecs.generator.chronobase.set

import net.verdagon.chronobasecs.generator.chronobase.Generator.toCS
import net.verdagon.chronobasecs.compiled.{ImmutableS, ListS, MutableS, SetS, WeakS}
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions

object MutSetRootMethods {

  def generateRootSetMethods(
                              opt: ChronobaseOptions,
                              set: SetS
  ): String = {
    val SetS(setName, MutableS, elementType) = set

    val elementTypeCSName = toCS(elementType)
    val flattenedElementTypeCSName = toCS(elementType.flatten)

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
       |      return TrustedEffect${setName}CreateWithId(NewId());
       |    }
       |    public ${setName} TrustedEffect${setName}CreateWithId(int id) {
       |      CheckUnlocked();
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
      s"""      NotifyEffect(effect);
       |    }
       |    public void Effect${setName}Delete(int id) {
       |      CheckUnlocked();
       |      var effect = new ${setName}DeleteEffect(id);
       |      NotifyEffect(effect);
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
       |    public void Effect${setName}Add(int setId, ${flattenedElementTypeCSName} element) {
       |      CheckUnlocked();
       |      CheckHas${setName}(setId);
       |      CheckHas${elementTypeCSName}(element);
       |
       |      var effect = new ${setName}AddEffect(setId, element);
       |
       |      var oldIncarnationAndVersion = rootIncarnation.incarnations${setName}[setId];
       |      if (oldIncarnationAndVersion.incarnation.set.Contains(element)) {
       |        throw new Exception("Element already exists!");
       |      }
       |      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
       |        oldIncarnationAndVersion.incarnation.set.Add(element);
       |""".stripMargin +
      (if (opt.hash) {
        s"""        this.rootIncarnation.hash += setId * rootIncarnation.version * element.GetDeterministicHashCode();
          |""".stripMargin
      } else "") +
      s"""      } else {
       |        var oldMap = oldIncarnationAndVersion.incarnation.set;
       |        var newMap = new SortedSet<int>(oldMap);
       |        newMap.Add(element);
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
       |      NotifyEffect(effect);
       |    }
       |    public void Effect${setName}Remove(int setId, int elementId) {
       |      CheckUnlocked();
       |      CheckHas${setName}(setId);
       |""".stripMargin +
      (if (elementType.ownership != WeakS) {
        s"""
        CheckHas${elementTypeCSName}(elementId);
           |""".stripMargin
      } else "") +
    s"""
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
       |      NotifyEffect(effect);
       |    }
       |
       """.stripMargin
  }
}
