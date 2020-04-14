Order: 3
---
A module is a component that performs a specific action, usually by creating, processing, or manipulating documents.

A module is a small single-purpose component that takes [documents](/framework/concepts/documents) as input, does something based on those documents (possibly transforming them), and outputs documents as a result of whatever operation was performed. Modules are typically chained in a sequence called a [pipeline](/framework/concepts/pipelines).

# Configuration

Most modules that require configuration use a special `Config<TValue>` delegate. This type allows you to specify lazy logic that's evaluated at [execution time](/framework/concepts/execution) and optionally uses the document and/or [execution context](/framework/concepts/execution#execution-context), is async or not, or just converts from a simple value. Creating a `Config<TValue>` is often done via factory methods from the `Config` static class. For example, if a module needs a string for a given setting it will accept a `Config<string>` which can be created by passing a `string` directly:

``` csharp
new SomeModule("my-string")
```

or by using one of the many factory methods:

``` csharp
new SomeModule(
    Config.FromDocument(doc => doc.GetString("key")))
```

# Module Patterns

Different modules treat inputs and outputs in different ways. Some modules just "pass-through" the documents that are input, some transform them in some way and output the results, and some exhibit more complex behavior. Some modules even exhibit different behavior depending on how the module was configured. The behavior can get especially confusing when considering some modules evaluate child modules which also output documents. In these cases, there are different behaviors for how the input documents and the result documents from the child modules are combined. The way inputs, outputs, and child module results are related can generally be described as a few different patterns and even though these probably don't cover the way *every* module or combination of modules works, they should help you understand the concepts involved.

## Pass-Through

These modules just take the input documents and pass them on as the outputs:

<div class="mermaid">
    graph TD
        I1(I1)-->Module
        I2(I2)-->Module
        Module-->O1(I1)
        Module-->O2(I2)
</div>

## Transformation

These modules apply some sort of transformation to the input documents and output one result for each input:

<div class="mermaid">
    graph TD
        I1(I1)-->Module
        I2(I2)-->Module
        Module-->O1("O1 (from I1)")
        Module-->O2("O2 (from I2)")
</div>

## Aggregation

These modules take multiple input documents and combine them into a single output document:

<div class="mermaid">
    graph TD
        I1(I1)-->Module
        I2(I2)-->Module
        Module-->O1("O1 (from I1 + I2)")
</div>

## Splitting

This is the opposite behavior of aggregation. These modules split each input document into multiple output documents:

<div class="mermaid">
    graph TD
        I1(I1)-->Module
        I2(I2)-->Module
        Module-->O1A("O1A (from I1)")
        Module-->O1B("O1B (from I1)")
        Module-->O2A("O2A (from I2)")
        Module-->O2B("O2B (from I2)")
</div>

## Concatenation

In this case, output documents that are independent from the input documents are output, but instead of replacing the input document they are concatenated with them:

<div class="mermaid">
    graph TD
        I1(I1)-->Module
        I2(I2)-->Module
        Module-->O1(I1)
        Module-->O2(I2)
        Module-->O3(O1)
        Module-->O4(O2)
</div>

Note that the new documents may come from a sequence of child modules:

<div class="mermaid">
    graph TD
        I1(I1)-->Module
        I2(I2)-->Module
        Module-->Children["Module(s)"]
        subgraph Child Modules
            Children-->C1(C1)
            Children-->C2(C2)
        end
        Module-->O1(I1)
        Module-->O2(I2)
        Module-->O3(C1)
        Module-->O4(C2)
</div>

## Replacement

These modules just replace the entire input set of documents with a different output set:

<div class="mermaid">
    graph TD
        I1(I1)-->Module
        I2(I2)-->Module
        Module-->O1(O1)
        Module-->O2(O2)
</div>

Note that the new documents may come from a sequence of child modules:

<div class="mermaid">
    graph TD
        I1(I1)-->Module
        I2(I2)-->Module
        Module-->Children["Module(s)"]
        subgraph Child Modules
            Children-->C1(C1)
            Children-->C2(C2)
        end
        Module-->O1(C1)
        Module-->O2(C2)
</div>

## Combination

This pattern describes the combination of one or more input documents with the outputs from child modules:

<div class="mermaid">
    graph TD
        I1(I1)-->Module
        I2(I2)-->Module
        Module-->Children["Module(s)"]
        subgraph Child Modules
            Children-->C1(C1)
            Children-->C2(C2)
        end
        Module-->O1("O1 (from I1 + C1)")
        Module-->O2("O2 (from I1 + C2)")
        Module-->O3("O3 (from I2 + C1)")
        Module-->O4("O4 (from I2 + C2)")
</div>

# Concurrency

Some modules process documents in parallel while others process them sequentially. Modules that process documents in parallel can often be forced to process documents sequentially by using the `WithParallelExecution(false)` extension method. This might be useful when the documents being processed or a configuration setting aren't thread-safe.