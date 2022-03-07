<!---
Title: Linking To APIs
Order: 2
Badge: Docs
--->
For each symbol, a [cross reference](xref:links-and-cross-references#cross-references) is generated that makes it easy to link to that symbol.

# Cross References

The cross reference (or "xref") works like other cross references and starts with `api-` followed by the fully qualified symbol name including namespaces (to avoid conflicts). For example, [`xref:api-Statiq.Common.ApplicationState`](xref:api-Statiq.Common.ApplicationState) (note that xrefs are case-insensitive).

Methods follow a similar pattern but also include all parameters and their fully qualified types. For example, [`xref:api-Statiq.Common.Analyzer.AnalyzeAsync(Statiq.Common.IAnalyzerContext)`](xref:api-Statiq.Common.Analyzer.AnalyzeAsync(Statiq.Common.IAnalyzerContext)).

Properties, constructors, events, and other types of symbols follow a similar pattern.