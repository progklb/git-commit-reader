# Git Commit Reader

A simple utility to read/write a repository's Git commit to file. Can be used for versioning a build with a Git commit hash.

This project contains multiple utilities for use with different languages/frameworks.

## Prerequisites

As this utility relies on Git, the Git toolchain must be installed and be available on your PATH.

## Structure

Each framework-specific implementation consists of at least two components. Some frameworks may have additional components that makes sense for specifically for the framework they are for.

- **Reader/Writer**: A utility that reads the Git repository's current state and writes the information to file for later access.
- **Interface**: A class used to expose the current Git state to client code. This is the class you consume.

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