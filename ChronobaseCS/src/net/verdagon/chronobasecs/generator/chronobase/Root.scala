package net.verdagon.chronobasecs.generator.chronobase

import net.verdagon.chronobasecs.compiled.{MutableS, SuperstructureS}
import net.verdagon.chronobasecs.generator.chronobase.interface.InterfaceGenerator
import net.verdagon.chronobasecs.generator.chronobase.list.ListGenerator
import net.verdagon.chronobasecs.generator.chronobase.map.MapGenerator
import net.verdagon.chronobasecs.generator.chronobase.set.SetGenerator
import net.verdagon.chronobasecs.generator.chronobase.struct.StructGenerator

object Root {

  def generateRoot(opt: ChronobaseOptions, ss: SuperstructureS): String = {
    val rootStructName =
      ss.structs.find(_.isRoot) match {
        case None => throw new RuntimeException("No root struct!")
        case Some(struct) => struct.name
      }
    val instanceTypeNames =
      (ss.structs.filter(_.mutability == MutableS).map(_.name) ++
        ss.lists.filter(_.mutability == MutableS).map(_.name) ++
        ss.sets.filter(_.mutability == MutableS).map(_.name) ++
        ss.maps.filter(_.mutability == MutableS).map(_.name))

    s"""
       |public interface ILogger {
       |  void Info(string str);
       |  void Warning(string str);
       |  void Error(string str);
       |}
       |
       |public struct VersionAndIncarnation<T> {
       |  public int version;
       |  public T incarnation;
       |  public VersionAndIncarnation(int version, T incarnation) {
       |    this.version = version;
       |    this.incarnation = incarnation;
       |  }
       |}
       |
       |public delegate void IEffectObserver(IEffect effect);
       |
       |public interface IEffect {
       |  // True for deletes/removes, false for creates/adds/sets.
       |  bool isSubtractive();
       |  void visitIEffect(IEffectVisitor visitor);
       |}
       |
       |public interface IEffectVisitor {
       |""".stripMargin +
      ss.structs.filter(_.mutability == MutableS).map(struct => {
        StructGenerator.generateGlobalVisitorInterfaceMethods(struct)
      }).mkString("") +
      ss.lists.filter(_.mutability == MutableS).map(list => {
        ListGenerator.generateGlobalVisitorInterfaceMethods(list)
      }).mkString("") +
      ss.sets.filter(_.mutability == MutableS).map(set => {
        SetGenerator.generateGlobalVisitorInterfaceMethods(set)
      }).mkString("") +
      ss.maps.filter(_.mutability == MutableS).map(map => {
        MapGenerator.generateGlobalVisitorInterfaceMethods(map)
      }).mkString("") +
      s"""
         |}
         |
         |public class Root {
         |  private static readonly int VERSION_HASH_MULTIPLIER = 179424673;
         |  private static readonly int NEXT_ID_HASH_MULTIPLIER = 373587883;
         |
         |  private void CheckRootsEqual(Root a, Root b) {
         |    if (a != b) {
         |      throw new System.Exception("Given objects aren't from the same root!");
         |    }
         |  }
         |
         |  public readonly ILogger logger;
         |
         |  public List<IEffectObserver> effectObservers;
         |
         |  // This *always* points to a live RootIncarnation. When we snapshot, we eagerly
         |  // make a new one of these.
         |  private RootIncarnation rootIncarnation;
         |
         |  bool locked;
         |
         |  // 0 means everything
         |  public Root(ILogger logger) {
         |    this.logger = logger;
         |    this.effectObservers = new List<IEffectObserver>();
         |    int initialVersion = 1;
         |    int initialNextId = 1;
         |    int initialHash = VERSION_HASH_MULTIPLIER * initialVersion + NEXT_ID_HASH_MULTIPLIER * initialNextId;
         |    rootIncarnation = new RootIncarnation(initialVersion, initialNextId, initialHash);
         |    this.locked = true;
         |  }
         |
         |  public Root(ILogger logger, RootIncarnation rootIncarnation) {
         |    this.logger = logger;
         |    this.rootIncarnation = rootIncarnation;
         |    this.locked = false;
         |    this.Snapshot();
         |    this.locked = true;
         |  }
         |
         |  public int nextId { get { return rootIncarnation.nextId; } }
         |
         |  public int version { get { return rootIncarnation.version; } }
         |
         |  public void AddObserver(IEffectObserver obs) {
         |    effectObservers.Add(obs);
         |  }
         |
         |  public void RemoveObserver(IEffectObserver obs) {
         |    effectObservers.Remove(obs);
         |  }
         |
         |  private void NotifyEffect(IEffect effect) {
         |    foreach (var obs in effectObservers) {
         |      obs(effect);
         |    }
         |  }
         |
         |  public RootIncarnation Snapshot() {
         |    CheckUnlocked();
         |    RootIncarnation oldIncarnation = rootIncarnation;
         |    int newHash = oldIncarnation.hash;
         |    int newVersion = oldIncarnation.version + 1;
         |""".stripMargin +
      (if (opt.hash) {
        s"""    newHash -= VERSION_HASH_MULTIPLIER * oldIncarnation.version + NEXT_ID_HASH_MULTIPLIER * oldIncarnation.nextId;
           |    newHash += VERSION_HASH_MULTIPLIER * newVersion + NEXT_ID_HASH_MULTIPLIER * oldIncarnation.nextId;
           |""".stripMargin
      } else "") +
      s"""    rootIncarnation =
         |        new RootIncarnation(
         |            newVersion, oldIncarnation.nextId, newHash, oldIncarnation);
         |    return oldIncarnation;
         |  }
         |
         |  public delegate T ITransaction<T>();
         |
         |  public (List<IEffect>, T) Transact<T>(ITransaction<T> transaction) {
         |    var stopwatch = new System.Diagnostics.Stopwatch();
         |    stopwatch.Start();
         |
         |    if (!locked) {
         |      throw new Exception("Can't unlock, not locked!");
         |    }
         |    locked = false;
         |    // var rollbackPoint = Snapshot();
         |
         |    var effects = new List<IEffect>();
         |    IEffectObserver effectObserver = (effect) => effects.Add(effect);
         |    AddObserver(effectObserver);
         |
         |    try {
         |      var result = transaction();
         |      return (effects, result);
         |    } catch (Exception e) {
         |      // logger.Error("Rolling back because of error: " + e.Message + "\\n" + e.StackTrace);
         |      // Revert(rollbackPoint);
         |      logger.Error("Encountered error in transaction: " + e.Message + "\\n" + e.StackTrace);
         |      throw;
         |    } finally {
         |      RemoveObserver(effectObserver);
         |      if (locked) {
         |        logger.Error("Can't lock, already locked!");
         |        Environment.Exit(1);
         |      }
         |      locked = true;
         |      // CheckForViolations();
         |
         |      stopwatch.Stop();
         |      var calculationDuration = stopwatch.Elapsed.TotalMilliseconds;
         |
         |      // logger.Info("Transaction run time " + calculationDuration + "ms");
         |    }
         |  }
         |
         |  public void CheckUnlocked() {
         |    if (locked) {
         |      throw new Exception("Can't proceed, superstructure is locked!");
         |    }
         |  }
         |
         |  private int NewId() {
         |    this.UpdateHashOnNextIdChange(rootIncarnation.nextId, rootIncarnation.nextId + 1);
         |    return rootIncarnation.nextId++;
         |  }
         |
         |  private void UpdateHashOnNextIdChange(int oldNextId, int newNextId) {
         |    int oldIdAndVersionHashContribution =
         |        VERSION_HASH_MULTIPLIER * rootIncarnation.version +
         |        NEXT_ID_HASH_MULTIPLIER * oldNextId;
         |    int newIdAndVersionHashContribution =
         |        VERSION_HASH_MULTIPLIER * rootIncarnation.version +
         |        NEXT_ID_HASH_MULTIPLIER * newNextId;
         |    rootIncarnation.hash =
         |        rootIncarnation.hash -
         |        oldIdAndVersionHashContribution +
         |        newIdAndVersionHashContribution;
         |  }
         |
         |  private int RecalculateEntireHash() {
         |    int result =
         |        VERSION_HASH_MULTIPLIER * rootIncarnation.version +
         |        NEXT_ID_HASH_MULTIPLIER * rootIncarnation.nextId;
         |
         |""".stripMargin +
      instanceTypeNames
        .map(instanceTypeName => {
          s"""    foreach (var entry in this.rootIncarnation.incarnations${instanceTypeName}) {
             |      result += Get${instanceTypeName}Hash(entry.Key, entry.Value.version, entry.Value.incarnation);
             |    }
             |""".stripMargin
        })
        .mkString("") +
      s"""    return result;
         |  }
         |
         |  public void CheckForViolations() {
         |    List<string> violations = new List<string>();
         |
         |""".stripMargin +
      instanceTypeNames
        .map(instanceTypeName => {
          s"""    foreach (var obj in this.All${instanceTypeName}()) {
             |      obj.CheckForNullViolations(violations);
             |    }
             |""".stripMargin
        })
        .mkString("") +
      s"""
         |    SortedSet<int> reachableIds = new SortedSet<int>();
         |    foreach (var rootStruct in this.All${rootStructName}()) {
         |      rootStruct.FindReachableObjects(reachableIds);
         |    }
         |""".stripMargin +
      instanceTypeNames
        .map(instanceTypeName => {
          s"""    foreach (var obj in this.All${instanceTypeName}()) {
             |      if (!reachableIds.Contains(obj.id)) {
             |        violations.Add("Unreachable: " + obj + "#" + obj.id);
             |      }
             |    }
             |""".stripMargin
        })
        .mkString("") +
      s"""
         |    if (violations.Count > 0) {
         |      string message = "Found violations!\\n";
         |      foreach (var violation in violations) {
         |        message += violation + "\\n";
         |      }
         |      throw new Exception(message);
         |    }
         |  }
         |
         |  public int GetDeterministicHashCode() {
         |    // int doubleCheckHash = RecalculateEntireHash();
         |    // Asserts.Assert(doubleCheckHash == this.rootIncarnation.hash);
         |    return this.rootIncarnation.hash;
         |  }
         |""".stripMargin +
      RootRevert.generateRootRevertMethod(opt, ss) +
      ss.structs.map(s => StructGenerator.generateRootMethods(opt, s)).mkString("") +
      ss.interfaces.map(s => InterfaceGenerator.generateRootMethods(opt, s)).mkString("") +
      ss.lists.map(s => ListGenerator.generateRootMethods(opt, s)).mkString("") +
      ss.sets.map(s => SetGenerator.generateRootMethods(opt, s)).mkString("") +
      ss.maps.map(s => MapGenerator.generateRootMethods(opt, s)).mkString("") +
      "}\n"
  }
}
