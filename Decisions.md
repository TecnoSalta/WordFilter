# ADR-001: Default Word Search Strategy Selection

**Status:** Accepted

## Context

The application requires functionality to find the 10 most frequent words from a `wordStream` that also exist in a character `matrix`. Multiple implementation strategies for this task were developed and evaluated, with a focus on performance (execution speed) and efficiency (memory consumption).

Performance evaluation was conducted using `BenchmarkDotNet` on 32x32 and 64x64 matrices, revealing significant differences between implementations, though not warranting optimization considering the bottleneck will likely be the stream.

## Decision

**`SpaceSavingFindStrategy`** has been selected as the sole recommended and default strategy for word search and counting throughout the application.

The other strategies (`SimpleFindStrategy` and `OptimizedFindStrategy`) will be marked as obsolete and scheduled for removal from the codebase to prevent their use.

## Justification

The decision is based on overwhelming empirical evidence from performance benchmarks:

1.  **Execution Performance:** `SpaceSavingFindStrategy` operates in the order of **nanoseconds**, while the alternatives operate in **milliseconds**. This makes it over **a million times faster**.

2.  **Memory Efficiency:** `SpaceSavingFindStrategy` allocates a trivial amount of memory (approximately 408 bytes). The other strategies allocate over **160 MB**, creating unsustainable pressure on the Garbage Collector (GC), resulting in pauses, higher CPU usage, and poor scalability.

3.  **Idiomatic Implementation:** The chosen strategy uses a chain of LINQ operations with **lazy evaluation (streaming)**. This approach is idiomatic in modern C# and is highly optimized by the .NET framework to process data collections efficiently without materializing massive intermediate collections in memory.

## Alternatives Considered

### 1. `SimpleFindStrategy`

-   **Implementation:** Filtered the `wordStream` and materialized a complete list (`.ToList()`) of all matching words before grouping and counting them.
-   **Result:** Rejected due to its excessive memory consumption and the associated performance overhead of creating a large intermediate collection.

### 2. `OptimizedFindStrategy`

-   **Implementation:** Used a `Dictionary<string, int>` to count frequencies, employing the `CollectionsMarshal.GetValueRefOrAddDefault` technique for fast counter updates.
-   **Result:** Rejected. Although the name suggested optimization, its approach of storing every unique word found in the dictionary led to even higher memory consumption than the simple strategy, making it the worst option for large and varied data streams.

## Consequences

-   **Positive:**
    -   Drastic improvement in application performance and responsiveness.
    -   Massive reduction in memory consumption and GC pressure.
    -   The code is more robust, scalable, and follows modern C# best practices.
-   **Negative:**
    -   None identified. The solution relies on standard, stable features of the .NET framework.