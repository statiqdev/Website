<!---
Title: Linking To APIs
Order: 2
Badge: Docs
--->
For each symbol, a [cross reference](xref:links-and-cross-references#cross-references) is generated that makes it easy to link to that symbol.

# Cross References

The cross reference (or "xref") works like other cross references and starts with `api-` followed by the fully qualified symbol name including namespaces (to avoid conflicts). For example, [`xref:api-Statiq.Common.ApplicationState`](xref:api-Statiq.Common.ApplicationState) (note that xrefs are case-insensitive).

Generic type information is included in the xref, but the angle brackets in the xref are replaced with dashes to avoid HTML escaping issues. For example, [`xref:api-Statiq.Common.Config-TValue-`](xref:api-Statiq.Common.Config-TValue-).

Methods follow a similar pattern but also include all parameters and their fully qualified types. For example, [`xref:api-Statiq.Common.Analyzer.AnalyzeAsync(Statiq.Common.IAnalyzerContext)`](xref:api-Statiq.Common.Analyzer.AnalyzeAsync(Statiq.Common.IAnalyzerContext)). This can make the xrefs for members, and especially methods, very long, but that's unavoidable since we need to be able to differentiate between overloads. 

Properties, constructors, events, and other types of symbols follow a similar pattern.