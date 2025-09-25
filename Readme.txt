Program: WordFinder
Objective
Implement a WordFinder class that identifies the 10 most frequent words from a data stream that are present in a character matrix, considering (1) horizontal and (2) vertical occurrences.

Constraints:
Inputs
Matrix: 64×64 chars max as IEnumerable<string>

WordStream: Potentially large IEnumerable<string> of words to search in the matrix.

Definitions:
Find: Words must be searched left-to-right and top-to-bottom.
Counting Process:
COUNT = Verify Existence + Accumulate Frequency

Step 1: Verify Existence (Is the word in the matrix?)

A word "exists" if it appears at least once in the matrix (horizontal or vertical)

Multiple occurrences/orientations don't matter → counts as 1 existence

Binary result: Exists/Does not exist

Step 2: Accumulate Frequency (How many times does it appear in the stream?)

For each occurrence in the stream where the word exists → +1 to counter

Stream with duplicates: Each unique appearance increments frequency

Final ranking: Ordered by this frequency counter

Frequency Counting Concept:
a. Count each appearance in wordstream to determine "most repeated words"
b. If a word exists multiple times in the matrix, it counts as single existence.

Result: Top 10 words by frequency of appearance in the stream

Empty case: Return empty collection if no matches

⚡ Performance Requirements
High efficiency for large word streams

Optimized system resource usage

Efficient algorithm considering limited matrix size (64×64)

🔍 Search Rules
Existence in Matrix
Example Matrix:

text
c o l d
w i n d  
h o t x
"cold" → ✓ (horizontal, row 1)

"wind" → ✓ (horizontal, row 2)

"down" → ✗ (not found)

Frequency Counting
WordStream: ["cold", "cold", "wind", "hot", "cold", "heat"]

Frequencies:

"cold" → 3 (appears 3 times in stream, exists in matrix)

"wind" → 1 (appears 1 time in stream, exists in matrix)

"hot" → 1 (appears 1 time in stream, exists in matrix)

"heat" → 0 (does not exist in matrix)

Result: ["cold", "wind", "hot"]