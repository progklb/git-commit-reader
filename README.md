# Git Snaphot

A simple utility to read state of a Git repository and write it to file. This is useful for build versioning purposes.

This project contains multiple utilities for use with different languages/frameworks.

## Prerequisites

As this utility relies on Git, the Git toolchain must be installed and be available on your `PATH`.

## Structure

Each framework-specific implementation consists of at least two components. Some frameworks may have additional components that makes sense for specifically for the framework they are for.

- **Utility**: A reader/writer that reads the Git repository's current state and writes the information to file for later access.
- **Interface**: A class used to access the current Git state to client code. This is the class you consume.
- **Other**: There could be other platform/framework-specific tools/code available, examples, etc.

## Git Commands

There are a number of useful git commands that are built into each interface:

```txt
git describe                                  // Requires that there is a tag nearby. Otherwise errors.
git describe --all
git describe --tags
git describe --tags --exact-match 88bd143     // Returns tag for commit. Otherwise errors.
git tag                                       // Returns all tags in repository. Otherwise errors.
git rev-parse HEAD                            // Returns the full commit ID of the current commit.
git rev-parse --short HEAD                    // Returns the short commit ID of the current commit.
```

## Framework Variants

See the framework-specific read me:

- [Unity](./Unity/README.md)