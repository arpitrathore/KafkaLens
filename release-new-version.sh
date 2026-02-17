#!/usr/bin/env bash

set -e

PROPS_FILE="Directory.Build.props"

# 1. Ensure we're on master
CURRENT_BRANCH=$(git rev-parse --abbrev-ref HEAD)
if [ "$CURRENT_BRANCH" != "master" ]; then
  echo "❌ You must be on master branch."
  exit 1
fi

# 2. Ensure working tree is clean
if [ -n "$(git status --porcelain)" ]; then
  echo "❌ Working directory not clean. Commit or stash changes."
  exit 1
fi

# 3. Extract version from Directory.Build.props
VERSION=$(grep -oPm1 "(?<=<Version>)[^<]+" "$PROPS_FILE")

if [ -z "$VERSION" ]; then
  echo "❌ Could not find <Version> in $PROPS_FILE"
  exit 1
fi

TAG="v$VERSION"

# 4. Check if tag already exists
if git rev-parse "$TAG" >/dev/null 2>&1; then
  echo "❌ Tag $TAG already exists."
  exit 1
fi

# 5. Create annotated tag (opens editor for release notes)
git tag -a "$TAG"

# 6. Push tag
git push origin "$TAG"

echo "✅ Release $TAG created and pushed."
