Title: Pipelines and Modules
Order: 2
---
Pipelines and modules are the building blocks of a Statiq application. A generator defines pipelines that contain modules, and together they operate on documents to turn input into the final product.

A given [execution](xref:execution) can have multiple pipelines which are executed in sequence, and subsequent pipelines have access to the documents from the previous pipelines. Conceptually, a simple pipeline looks like:

<div class="mermaid">
    graph TD
        D1("Empty Document")
        D1-->Module1["Module 1"]
        Module1-->D2("Document A")
        Module1-->D3("Document B")
        D2-->Module2["Module 2"]
        D3-->Module2
        Module2-->D4("Document C")
        Module2-->D5("Document D")
</div>

In the visualization above, the first module may have read some files (in this case 2 files) and stuck some information about those files such as name and path in the document metadata. Then the second module may have transformed the files (for example, from Markdown to HTML).

It's not unusual for a real-world generation to contain many different pipelines. Many times this is helpful if you need to reuse the output from one of the pipelines or want to separate the different generation steps.

<div class="mermaid">
    graph TD
        subgraph Second Pipeline
            D6("Empty Document")
            D6-->Module3["Module 3"]
            Module3-->D7("Document E")
            D7-->Module4["Module 4"]
            Module4-->D8("Document F")
        end
        subgraph First Pipeline
            D1("Empty Document")
            D1-->Module1["Module 1"]
            Module1-->D2("Document A")
            Module1-->D3("Document B")
            D2-->Module2["Module 2"]
            D3-->Module2
            Module2-->D4("Document C")
            Module2-->D5("Document D")
        end
</div>

# Concurrency and Dependencies

Pipelines generally execute most [phases](#phases) in parallel for performance reasons. However, a pipeline can depend on other pipelines by adding the name of pipelines it depends on to the `Dependencies` collection. This will result in the dependent pipeline(s) executing before the pipeline(s) that depend on them. At startup a pipeline dependency graph is formed and any circular dependencies will cause an error.

# Phases

Executing the modules in a pipeline happens in four phases. The phases are distinct due to the way concurrency and output documents are handled. The input documents to the first module in each phase are the output documents from the previous phase.

## Input Phase

The input phase is generally used for fetching data from an outside source and creating documents from that data. For example: the file system, a database, a web API, etc. The input phase is immediatly started for all pipelines concurrently and cannot access outputs from other pipelines.

## Process Phase

This is the main phase and it should contain most of the modules for a pipeline. All process phases across all pipelines are generally executed in parallel unless a pipeline declares dependencies in which case its process phase will be executed after the process phase of its dependencies. A module can access output documents from the process phase of its dependencies but not from other pipelines.

## Post-Process Phase

The post-process phase can be used for modules that need access to output documents from the process phase of every pipeline regardless of dependencies (except [isolated pipelines](#isolated)). All post-process phases across all pipelines are executed in parallel, but only after all process phases have completed.

## Output Phase

The output phase is used for outputting documents to disk, a service, etc. All output phases across all pipelines are executed in parallel. Modules in the output phase cannot access output documents from any other pipelines (except for those in a [deployment](#deployment) pipeline).

# Special Types Of Pipelines

In addition to the phases above, there are several special types of pipelines you can define.

## Isolated

An isolated pipeline is one that you know don't need access to output documents from any other pipelines. You can declare a pipeline to be isolated by setting the `Isolated` flag to `true`. As isolated pipeline will execute all for phases as quickly as possible in parallel with other pipelines. This kind of pipeline is useful for things like processing Less or Sass files that don't rely on coordination with other pipelines.

## Deployment

Deployment pipelines are special in that they don't execute by default. To execute your deployment pipelines you run Statiq with the `deploy` command. Modules in deployment pipelines can read document outputs from other pipelines inside their output phase (unlike a normal pipeline). Deployment pipelines are also executed last so that the output phase of other non-deployment pipelines has completed. In general, deployment pipelines contain modules that deploy final output somewhere (like a web host) inside their output phase.

## Execution Policy

A pipeline's execution policy determines if a pipeline is executed. By default, non-deployment pipelines are executed automatically and deployment pipelines are only executed when Statiq is launched with the `deploy` command. Pipelines are also executed if they are a dependency of an executing pipeline, regardless of policy.

You can adjust this behavior and create sophisticated behaviors for your Statiq application by changing the `ExecutionPolicy` property for each pipeline.

### Default

Default pipelines are normally executed (I.e., they effectively have a policy of `Normal`, see below) _unless_ the pipeline is a [Deployment pipeline](#deployment) in which case the pipeline is manually executed or executed when the `deploy` command is run from the command-line interface. This policy is the most common and should be used for most pipelines.

### Normal

Normal pipelines are always executed unless other pipelines are explicitly specified. A normal pipeline will also be executed if it’s the dependent of an executing pipeline.

### Manual

Manual pipelines are only executed if explicity specified. A manual pipeline will also be executed if it’s the dependent of an executing pipeline. This policy is useful for specialized pipelines that should only be executed on-demand and are not part of the normal generation process.

### Always

This policy means that the pipeline should always execute regardless of what pipelines are explicitly specified (if any). Pipelines with an always policy are useful for housekeeping and other tasks that should be carried out no matter what.