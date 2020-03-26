package net.verdagon.chronobasecs.generator.chronobase.set

import net.verdagon.chronobasecs.compiled.{MapS, MutableS, SetS, StructS}
import net.verdagon.chronobasecs.generator.chronobase.Generator.toCS
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions
import net.verdagon.chronobasecs.generator.chronobase.map.MutMapEffects

object SetGenerator {
  def generateHandles(opt: ChronobaseOptions, set: SetS): Map[String, String] = {
    set.mutability match {
      case MutableS => {
        MutSetHandle.generateMutSet(opt, set) ++
        MutSetIncarnation.generateIncarnation(opt, set) ++
        MutSetEffects.generateEffects(opt, set)
      }
    }
  }

  def generateRootMethods(opt: ChronobaseOptions, set: SetS): String = {
    set.mutability match {
      case MutableS => {
        MutSetRootMethods.generateRootSetMethods(opt, set)
      }
    }
  }

  def generateEffectBroadcasterMembers(opt: ChronobaseOptions, set: SetS): String = {
    val setCSType = toCS(set.tyype)
    s"""
       |  readonly Dictionary<int, List<I${setCSType}EffectObserver>> observersFor${setCSType} =
       |      new Dictionary<int, List<I${setCSType}EffectObserver>>();
       |""".stripMargin
  }

  def generateGlobalVisitorInterfaceMethods(set: SetS) = {
    MutSetEffects.generateGlobalVisitorInterfaceMethods(set)
  }

  def generateEffectBroadcasterMethods(set: SetS) = {
    MutSetEffects.generateEffectBroadcasterMethods(set)
  }

  def generateEffectApplierMethods(set: SetS) = {
    MutSetEffects.generateEffectApplierMethods(set)
  }
}
