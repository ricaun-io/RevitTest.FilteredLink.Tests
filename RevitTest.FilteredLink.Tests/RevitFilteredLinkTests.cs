using System.Linq;
using Autodesk.Revit.DB;
using NUnit.Framework;
using RevitTest.FilteredLink.Tests.Utils;

namespace RevitTest.FilteredLink.Tests
{
    public class RevitFilteredLinkTests : OneTimeOpenDocumentTest
    {
        protected override string FileName => "Files/Project2021.rvt";
        [Test]
        public void RevitFilteredLinkTests_Document()
        {
            var revitLinkInstance = new FilteredElementCollector(document)
                .OfClass(typeof(RevitLinkInstance))
                .FirstOrDefault() as RevitLinkInstance;

            Assert.IsNotNull(revitLinkInstance, "No RevitLinkInstance found in the document.");

            var viewInstance = new FilteredElementCollector(document)
                .OfClass(typeof(ViewPlan))
                .FirstOrDefault() as ViewPlan;

            Assert.IsNotNull(viewInstance, "No ViewPlan found in the document.");

            var linkDocument = revitLinkInstance.GetLinkDocument(); // Link document have 4 walls
            var walls = new FilteredElementCollector(linkDocument)
                .OfClass(typeof(Wall))
                .OfType<Wall>();

            Assert.IsNotEmpty(walls, "No walls found in the linked document.");

            var viewId = viewInstance.Id;
            var linkId = revitLinkInstance.Id;
            var filteredElementCollector = new FilteredElementCollector(document, viewId, linkId);

            // Here is the exception happens others filters seems to not work either
            filteredElementCollector = filteredElementCollector.WhereElementIsNotElementType(); 

            var elementsInLink = filteredElementCollector.ToElements();
            Assert.IsNotEmpty(elementsInLink, "No elements found in the linked document.");
        }
    }
}
