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

# MyHashSet

A custom hash set implementation in C#, using **separate chaining** with a `SingleLinkedList` for collision handling.

## Features
- Add, remove, and check for the presence of integer keys
- Automatic resizing when load factor is exceeded
- Collision resolution via separate chaining
