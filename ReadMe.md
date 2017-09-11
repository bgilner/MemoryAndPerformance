[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)

# Memory and Performance

This sample project illustrates several memory and performance tests and concerns as well as optimization tips. Examples include:

  - Memory Information
  -- Allocation sizes of various types that use the [Large Object Heap]
  -- String intern pool usage

  - Memory Leaks
  -- Event leaks
  -- Timer thread leaks

  - Memory Efficiency
  -- Slow finally blocks
  -- Temp variable memory impact
  -- Enumeration vs List
  -- Dynamic vs fixed capacities of certain classes

  - Performance
  -- Exceptions vs Checked inputs
  -- Regex type implications (static, instance and compiled)
  -- Regex cache size

Read the in-code comments for tips and warnings.

```
Note: different versions of the .Net framework may result in different behavior.
```

### Todos

| Category | Test |
| ------ | ------ |
| Type Conversions | extraneous: build list then toarray vs change method return signature |
| Bytes | shifted byte[] reuse (vs new alloc) |
|| use stream overload with int and length vs new allocation |
|| toarray vs getbuffer |
| Memory | IDisposable that disposes 2 objects where the first bombs on null and prevents the second |
|| Event leak with pub/sub |

[//]: # 
   [Large Object Heap]: <https://blogs.msdn.microsoft.com/dotnet/2011/10/03/large-object-heap-improvements-in-net-4-5/>