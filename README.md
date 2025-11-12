# RevitTest.FilteredLink.Tests

[![Revit 2024](https://img.shields.io/badge/Revit-2024+-blue.svg)](../..)
[![Visual Studio 2022](https://img.shields.io/badge/Visual%20Studio-2022-blue)](../..)
[![License MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)

This project test how the `FilteredElementCollector(Document hostDocument, ElementId viewId, ElementId linkId)` method using the [ricaun.RevitTest](https://ricaun.com/RevitTest) Framework.

## Tests

The `Project2021.rvt` have a single `RevitLinkInstance` of the project `Project2021.Link.rvt` with 4 walls.

The tests tries to use the `FilteredElementCollector(Document hostDocument, ElementId viewId, ElementId linkId)` and Revit fails to collect the elements.

In Revit 2024 the exception is: 
```
Autodesk.Revit.Exceptions.InternalException: A managed exception was thrown by Revit or by one of its external applications.
```
In Revit 2025+ the exception is:
```
System.AccessViolationException: Attempted to read or write protected memory. This is often an indication that other memory is corrupt.
```
When the `AccessViolationException` happen Revit crashes.

## Revit API Forum

* [Revit 2025.4 is crashing if I use results of FilteredElementCollector](https://forums.autodesk.com/t5/revit-api-forum/revit-2025-4-is-crashing-if-i-use-results-of/td-p/13892613)

## License

This project is [licensed](LICENSE) under the [MIT License](https://en.wikipedia.org/wiki/MIT_License).

---

Do you like this project? Please [star this project on GitHub](../../stargazers)!