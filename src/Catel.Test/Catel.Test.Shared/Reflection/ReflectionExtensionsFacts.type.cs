﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReflectionExtensionsFacts.type.cs" company="Catel development team">
//   Copyright (c) 2008 - 2015 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Catel.Test.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using Catel.IoC;
    using Catel.Reflection;

    using JetBrains.dotMemoryUnit;

    using NUnit.Framework;

    public partial class ReflectionExtensionsFacts
    {
        [TestFixture]
        public class TheImplementsInterfaceExMethod
        {
            public interface ISomeInterface
            {
                
            }

            public class A : ISomeInterface
            {
                
            }

            public class B : A
            {
                
            }

            [TestCase]
            public void ReturnsTrueForTypeDirectImplementingInterface()
            {
                var type = typeof(A);

                Assert.IsTrue(type.ImplementsInterfaceEx<ISomeInterface>());
            }

            [TestCase]
            public void ReturnsTrueForTypeDerivetiveImplementingInterface()
            {
                var type = typeof (B);

                Assert.IsTrue(type.ImplementsInterfaceEx<ISomeInterface>());
            }
        }

        [TestFixture]
        public class TheGetSafeFullNameMethod
        {
            [TestCase(typeof(string), false, "System.String")]
            [TestCase(typeof(string), true, "System.String, System")]
            [TestCase(typeof(TypeFactory), false, "Catel.IoC.typeFactory")]
            [TestCase(typeof(TypeFactory), true, "Catel.IoC.typeFactory, Catel.Core")]
            public void ReturnsFullName(Type type, bool includeAssembly, string expected)
            {
                
            }
        }

        [TestFixture]
        public class TheIsInstanceOfTypeExMethod
        {
            [TestCase]
            public void ThrowsArgumentNullExceptionForNullType()
            {
                ExceptionTester.CallMethodAndExpectException<ArgumentNullException>(() => ReflectionExtensions.IsInstanceOfTypeEx(null, new object()));
            }

            [TestCase]
            public void ThrowsArgumentNullExceptionForNullObjectToCheck()
            {
                ExceptionTester.CallMethodAndExpectException<ArgumentNullException>(() => ReflectionExtensions.IsInstanceOfTypeEx(typeof(object), null));
            }

            [TestCase]
            public void ReturnsTrueForEqualReferenceType()
            {
                var type = typeof (InvalidOperationException);
                var instance = new InvalidOperationException();

                Assert.IsTrue(type.IsInstanceOfTypeEx(instance));
            }

            [TestCase]
            public void ReturnsTrueForInheritingReferenceType()
            {
                var type = typeof(Exception);
                var instance = new InvalidOperationException();

                Assert.IsTrue(type.IsInstanceOfTypeEx(instance));              
            }

            [TestCase]
            public void ReturnsFalseForNonInheritingReferenceType()
            {
                var type = typeof(Exception);
                var instance = new EventArgs();

                Assert.IsFalse(type.IsInstanceOfTypeEx(instance));
            }

            [TestCase]
            public void ReturnsTrueForEqualValueType()
            {
                var type = typeof(int);
                var instance = 32;

                Assert.IsTrue(type.IsInstanceOfTypeEx(instance));
            }

            [TestCase]
            public void ReturnsTrueForSpecialValueTypes()
            {
                var type = typeof(Int64);
                var instance = 32;

                Assert.IsTrue(type.IsInstanceOfTypeEx(instance));
            }

            [TestCase]
            public void ReturnsFalseForNonInheritingValueType()
            {
                var type = typeof(bool);
                var instance = 32;

                Assert.IsFalse(type.IsInstanceOfTypeEx(instance));
            }            
        }

        [TestFixture]
        public class TheGetPropertyExMethod
        {
            public interface IPerson : INameProvider
            {
                
            }

            public interface INameProvider
            {
                string FirstName { get; }
            }

            public class Person : IPerson
            {
                string INameProvider.FirstName { get { return "John"; } }
            }

            [TestCase]
            public void ReturnsNoExplicitInterfacePropertiesWhenDisabled()
            {
                var propertyInfo = typeof (Person).GetPropertyEx("FirstName", allowExplicitInterfaceProperties: false);

                Assert.IsNull(propertyInfo);
            }

            [TestCase]
            public void ReturnsExplicitInterfacePropertiesWhenEnabled()
            {
                var propertyInfo = typeof(Person).GetPropertyEx("FirstName", allowExplicitInterfaceProperties: true);

                Assert.IsNotNull(propertyInfo);
            }
        }

        [TestFixture]
        public class TheMakeGenericExFacts
        {
            class ClassA<T>
            {
            }

            [Test]
            [DotMemoryUnit(CollectAllocations = true)]
            [Ignore("Requires dotMemory")]
            public void Allocates_Less_Type_Arrays_Than_MakeGenericType()
            {
                int count = int.MaxValue;

                var memoryCheckPoint1 = dotMemory.Check();
                var makeGenericTypeEx1 = typeof(ClassA<>).MakeGenericTypeEx(typeof(int));
                var makeGenericTypeEx2 = typeof(ClassA<>).MakeGenericTypeEx(typeof(int));
                var makeGenericTypeEx3 = typeof(ClassA<>).MakeGenericTypeEx(typeof(int));
                var makeGenericTypeEx4 = typeof(ClassA<>).MakeGenericTypeEx(typeof(int));
                var makeGenericTypeEx5 = typeof(ClassA<>).MakeGenericTypeEx(typeof(int));
                dotMemory.Check(memory => count = memory.GetDifference(memoryCheckPoint1).GetNewObjects(where => where.Type.Is<Type[]>()).ObjectsCount);

                var memoryCheckPoint2 = dotMemory.Check();
                var makeGenericType1 = typeof(ClassA<>).MakeGenericType(typeof(int));
                var makeGenericType2 = typeof(ClassA<>).MakeGenericType(typeof(int));
                var makeGenericType3 = typeof(ClassA<>).MakeGenericType(typeof(int));
                var makeGenericType4 = typeof(ClassA<>).MakeGenericType(typeof(int));
                var makeGenericType5 = typeof(ClassA<>).MakeGenericType(typeof(int));
                dotMemory.Check(memory => Assert.Less(count, memory.GetDifference(memoryCheckPoint2).GetNewObjects(where => where.Type.Is<Type[]>()).ObjectsCount));
            }
        }
    }
}