using System;
using System.Collections.Generic;
using Autodesk.Revit.ApplicationServices;
using NUnit.Framework;

namespace RevitTest.FilteredLink.Tests
{
    public class RevitTests
    {
        protected Application application;

        [OneTimeSetUp]
        public void Setup(Application application)
        {
            this.application = application;
        }

        [Test]
        public void RevitTests_VersionName()
        {
            Assert.IsNotNull(application);
            Console.WriteLine(application.VersionName);
        }
    }
}
