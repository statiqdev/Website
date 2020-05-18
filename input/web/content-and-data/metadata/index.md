Order: 3
---
[Metadata](xref:documents-and-metadata#about-metadata) lets you add bits of information to [documents](xref:documents-and-metadata) and control their behavior.

There are several ways to add metadata to both [content](xref:web-content) and [data](xref:web-data) files and metadata is merged together from multiple sources to determine the complete set of metadata for a given document. This _data cascade_ (credit to [11ty](https://www.11ty.dev/docs/data-cascade/) for the excellent term) applies metadata from the lowest priority source to the highest. A document will contain metadata from all the sources, but if a higher priority metadata source contains the same key as a lower priority one, the value from the higher priority source will take precedence.

Note that [global settings cascade to documents](xref:settings#cascade-to-documents). This can be helpful because it means you can define metadata that's intended for use by documents as a global setting and it will cascade to every document as a default value.

Here are the default metadata sources from lowest priority to highest. Think of metadata being applied (or _cascaded_) from the top of this list to the bottom, with duplicate keys overwriting existing values along the way.

- Environment variables.
- [Configuration files](xref:settings#configuration-files).
- [Default Statiq Framework settings](xref:settings).
- [Default Statiq Web settings](xref:web-settings).
- [Settings you define via the Bootstrapper](xref:specifying-settings).
- [Directory metadata](xref:web-directory-metadata) (closer to the file have higher priority).
- [Sidecar files](xref:web-sidecar-files).
- [Front matter](xref:web-front-matter).
- Parsed data content (if a [data file](xref:web-data)).
