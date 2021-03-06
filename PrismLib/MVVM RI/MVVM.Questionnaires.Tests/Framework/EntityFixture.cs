//===================================================================================
// Microsoft patterns & practices
// Composite Application Guidance for Windows Presentation Foundation and Silverlight
//===================================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===================================================================================
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVVM.Questionnaires.Tests.TestObjects;

namespace MVVM.Questionnaires.Tests.Framework
{
    [TestClass]
    public class EntityFixture
    {
        [TestMethod]
        public void WhenInvokingRaisePropertyChanged_ThenRaisesEVent()
        {
            var testEntity = new TestEntity();

            string notifiedProperty = null;
            testEntity.PropertyChanged += (s, a) => notifiedProperty = a.PropertyName;

            testEntity.SomeValue = 10;

            Assert.AreEqual("SomeValue", notifiedProperty);
        }

        [TestMethod]
        public void WhenSettingAPropertyToAnInvalidValue_ThenEntityHasErrors()
        {
            var testEntity = new TestEntity();

            testEntity.SomeValue = 5;

            Assert.IsTrue(testEntity.HasErrors);
            Assert.IsTrue(testEntity.GetErrors("SomeValue").Cast<object>().Any());
            Assert.IsFalse(testEntity.GetErrors("SomeOtherValue").Cast<object>().Any());
        }
    }
}
