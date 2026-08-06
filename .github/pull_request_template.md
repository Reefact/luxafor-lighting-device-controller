<!--
  Please write this PR in ENGLISH: title, summary, changes, testing notes, and related issue references.

  Title: name the whole change in English. A single-intention PR mirrors its commit
  header (type(scope): description); a multi-intention PR uses a short descriptive
  title. Issue links go in "Related issues" below, not the title.

  Fill in the applicable sections below.
  Do not invent information.
  Only check testing items that were actually run.
  Delete a section only if it truly does not apply.
-->

## Summary

<!-- One or two sentences: what does this PR change, and why? -->

## Type of change

* [ ] Bug fix
* [ ] New feature
* [ ] Breaking change
* [ ] Refactoring
* [ ] Tests
* [ ] Documentation
* [ ] Build / CI / tooling

## Changes

<!-- Bullet list of the concrete changes made in this PR. Keep it factual. -->

*

## Testing

<!-- Check only the commands that were actually run. Say so when one was not run, and why. -->

* [ ] `dotnet build -c Release`
* [ ] `dotnet test -c Release`
* [ ] `dotnet pack Reefact.LuxaforLightingDeviceController -c Release -o artifacts`
* [ ] `pwsh ./build/Sync-Snippets.ps1 -Check`
* [ ] `pwsh ./build/Validate-Package.ps1 -ArtifactsDirectory artifacts`
* [ ] `pwsh ./build/Test-PackageConsumption.ps1 -ArtifactsDirectory artifacts`
* [ ] Verified on a real device (which one, and what was observed)

## Public API

<!-- The exported surface is approved: PublicApi.approved.txt is the contract. -->

* [ ] No change to the public API
* [ ] `PublicApi.approved.txt` updated in the same commit — and, if the change breaks consumers,
  listed under "Breaking changes" in `CHANGELOG.md`

## Documentation

<!-- State whether documentation was updated, or why no documentation change was needed. -->

* [ ] `CHANGELOG.md` updated
* [ ] READMEs updated — all seven languages, not only one
* [ ] `docs/` pages updated
* [ ] Examples changed in `samples/` and the pages regenerated (`pwsh ./build/Sync-Snippets.ps1`)
* [ ] No documentation change required

## Related issues

<!-- e.g. Closes #123 -->
