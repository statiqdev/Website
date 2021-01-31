Order: 2
---
Sidecar files let you define metadata in a separate file.

They are similar to [front matter](xref:web-front-matter) in that the metadata in a sidecar file is applied to a document, but instead of the metadata being defined at the top of a file, it's defined alongside the file. Sidecar files can be JSON or YAML and are typically named `_[filename].json` or `_[filename].yaml`.

Sidecar file support can be globally turned off by [setting](xref:web-settings) `ProcessSidecarFiles` to `false`.
