# Custom Generic Dictionary in C#

A simple implementation of a generic hash table (dictionary) in C#,  
using **separate chaining** with a custom `SingleLinkedList` for collision handling.

## Features
- **Generic types** for both key and value
- **Hashing** with bucket indexing
- **Separate chaining** collision resolution
- **Dynamic resizing** when load factor exceeds threshold
- Basic dictionary operations:
  - `Add(key, value)`
  - `Remove(key)`
  - `Get(key)`
  - `ContainsKey(key)`
  - `Print()` for debugging

## D
