# SmartWater

### Results
- The longest word made up of other words is: **ethylenediaminetetraacetates**
- The 2nd longest word made up of other words is: **electroencephalographically**
- The file wordlist.txt contains **97107** such composite words
- Performance: identify all composite words in **480 milliseconds** on a single core of an Intel i5 processor

### Architectural Notes
- Core functionality is provided by class WordAnalyzer.  Its constructor accepts a raw list of strings and does nothing signification.
- Asynchronous method WordAnalyzer.Process does analysis work and stores the list of word entiries to public property AllEntries.  The entries' property IsComposite may then be used to extract values such as longest word, etc.  This method is async to enable better scalability (when used at server-side) / user experience (when used at client-side).
- WordAnalyzer does not read from file, as it needs to remain reusable and not coupled to the file system / file format.
