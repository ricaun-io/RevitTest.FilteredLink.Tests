using System.Collections.Generic;
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
            var viewInstance = new FilteredElementCollector(document)
                .OfClass(typeof(ViewPlan))
                .FirstOrDefault() as ViewPlan;

            Assert.IsNotNull(viewInstance, "No ViewPlan found in the document.");

            var revitLinkInstance = new FilteredElementCollector(document, viewInstance.Id)
                .OfClass(typeof(RevitLinkInstance))
                .FirstOrDefault() as RevitLinkInstance;

            Assert.IsNotNull(revitLinkInstance, "No RevitLinkInstance found in the document.");

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

            foreach (var element in elementsInLink)
            {
                System.Console.WriteLine(element.Id);
            }
        }

        [Test]
        public void SelectElementsInLinkedView_WorkAround()
        {
            var viewInstance = new FilteredElementCollector(document)
                .OfClass(typeof(ViewPlan))
                .FirstOrDefault() as ViewPlan;

            Assert.IsNotNull(viewInstance, "No ViewPlan found in the document.");

            var revitLinkInstance = new FilteredElementCollector(document)
                .OfClass(typeof(RevitLinkInstance))
                .FirstOrDefault() as RevitLinkInstance;

            Assert.IsNotNull(revitLinkInstance, "No RevitLinkInstance found in the document.");

            // This `FilteredElementCollector` is created to make the exception not happening when using viewId and linkId. A filter is required to be added.
            new FilteredElementCollector(document, viewInstance.Id)
                .WhereElementIsNotElementType();

            new FilteredElementCollector(document, viewInstance.Id, revitLinkInstance.Id).WhereElementIsNotElementType().ToElements();
        }

        [Test]
        public void SelectElementsInLinkedView_WorkAround_WithMethod()
        {
            var viewInstance = new FilteredElementCollector(document)
                .OfClass(typeof(ViewPlan))
                .FirstOrDefault() as ViewPlan;

            Assert.IsNotNull(viewInstance, "No ViewPlan found in the document.");

            var revitLinkInstance = new FilteredElementCollector(document)
                .OfClass(typeof(RevitLinkInstance))
                .FirstOrDefault() as RevitLinkInstance;

            Assert.IsNotNull(revitLinkInstance, "No RevitLinkInstance found in the document.");

            var elements = GetElementInViewLink(document, viewInstance, revitLinkInstance);
        }

        public IList<Element> GetElementInViewLink(Document document, View view, RevitLinkInstance revitLinkInstance)
        {
            // This `FilteredElementCollector` is created to make the exception not happening when using viewId and linkId. A filter is required to be added.
            new FilteredElementCollector(document, view.Id)
                .WhereElementIsNotElementType();

            return new FilteredElementCollector(document, view.Id, revitLinkInstance.Id)
                .WhereElementIsNotElementType()
                .ToElements();
        }
    }
}
