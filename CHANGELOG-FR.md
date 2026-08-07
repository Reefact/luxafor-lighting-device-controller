_[English Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG.md) - [Nederlandse versie](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-NL.md) - [Svensk version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-SE.md) - [Deutsche Version](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-DE.md) - [Versión española](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-ES.md) - [Ελληνική έκδοση](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-GR.md) - [Wersja polska](https://github.com/Reefact/luxafor-lighting-device-controller/blob/main/CHANGELOG-PL.md)_

# Journal des modifications

Toutes les modifications notables de ce projet sont consignées dans ce fichier.

Le format s'appuie sur [Keep a Changelog](https://keepachangelog.com/fr/1.1.0/) et ce projet suit le
[versionnage sémantique](https://semver.org/lang/fr/spec/v2.0.0.html).

## [2.0.1]

Documentation, outillage et CI uniquement : la bibliothèque elle-même est inchangée depuis la 2.0.0, son API
publique également.

### Ajouté

- Un test de consommation du package produit, `build/Test-PackageConsumption.ps1` : un projet jetable créé
  hors du dépôt installe le `.nupkg` depuis un dépôt local dans un cache de packages privé, compile les
  exemples publics contre lui pour `net472` et `net10.0`, vérifie les assets résolus (`lib/net462`,
  `lib/netstandard2.0`, documentation XML) et la dépendance `hidlibrary`, puis exécute le résultat sous
  Windows. Lire l'archive ne peut pas détecter une dépendance qui ne se résout pas ou un asset qui n'atteint
  jamais le consommateur ; ce test, si. Il tourne dans la CI, et dans le workflow de publication avant que
  quoi que ce soit ne soit publié.
- Un dossier `samples` qui contient les exemples affichés dans la documentation, compilés avec les tests sur
  chaque framework cible, ainsi que `build/Sync-Snippets.ps1` qui les recopie dans les pages markdown. La CI
  le lance en mode vérification : un exemple ne peut donc plus se désynchroniser du code qu'il montre.
- Un `CONTRIBUTING.md` qui décrit comment compiler, ce que la CI vérifie et comment une version est publiée,
  ainsi qu'un modèle de pull request.
- Ce journal des modifications est désormais traduit dans les sept langues du README. `CHANGELOG.md` reste la
  version anglaise — la référence que les traductions suivent — parce que les notes de version du package y
  renvoient et que l'outillage attend les intitulés Keep a Changelog en anglais ; les autres langues sont
  suffixées (`CHANGELOG-FR.md`, `CHANGELOG-NL.md`, ...).
- Le polonais rejoint les langues de la documentation : `README-PL.md`, `docs/api-PL.md`,
  `docs/luxafor-PL.md` et `CHANGELOG-PL.md`.
- Le test de consommation épingle désormais le package testé sur le feed local via le Package Source
  Mapping de NuGet, et relit la source enregistrée dans `.nupkg.metadata` pour le prouver. Le projet
  jetable a les deux sources configurées : dès qu'une version est publiée, la restauration pourrait la
  servir à la place de celle qui vient d'être construite, et le test porterait sur le mauvais package. La
  dépendance, elle, doit toujours venir de nuget.org — épingler l'identifiant ne doit pas entraîner le
  reste du graphe vers le feed local.

### Modifié

- Documentation : chaque README présente désormais ce qu'un lecteur cherche en premier — à quoi sert la
  bibliothèque, comment l'installer, un démarrage rapide, les périphériques compatibles, les fonctionnalités,
  puis où lire la suite. La présentation de la société Luxafor et son catalogue de produits sont partis dans
  `docs/luxafor*.md`, et la revue commande par commande est devenue une référence de l'API dans
  `docs/api*.md`, l'une et l'autre dans les mêmes sept langues.
- `README.md` est désormais la version anglaise : c'est ce que GitHub affiche sur la page d'accueil du
  dépôt et ce que le package embarque, et l'anglais est la langue qu'attendent ses lecteurs
  internationaux. La version française est passée dans `README-FR.md`, et les six autres gardent leur
  suffixe. Tout lien pointant vers `README-EN.md` doit être mis à jour.
- Les workflows passent à `actions/checkout@v5`, `actions/setup-dotnet@v5` et `actions/upload-artifact@v6`,
  la première majeure de chacune qui tourne sur Node 24, Node 20 étant déprécié sur les agents.

### Corrigé

- La documentation XML de `TargetedLeds` ne dit plus « the on/off or animation will also be sequential and
  could: it can cause a visual ripple effect ».
- Le contrôle des métadonnées de `build/Validate-Package.ps1` combinait ses deux conditions avec `-and`, ce
  qu'aucune valeur ne pouvait satisfaire : une `<description>` vide passait. Il signale désormais aussi bien
  les éléments vides, blancs, absents que ceux dépourvus d'attributs.

## [2.0.0]

### Changements cassants

- `TargetedLeds.TabSide` et `TargetedLeds.BackSide` allument enfin la face qu'ils nomment. Ils étaient
  inversés : `TabSide` envoyait le code lux 66 (`0x42`) et `BackSide` 65 (`0x41`), alors que le protocole
  Luxafor attribue 65 à la face onglet (LEDs n° 1, 2 et 3) et 66 à la face arrière (LEDs n° 4, 5 et 6).
  L'API ne change pas et rien ne cesse de compiler, mais **les LEDs qui s'allument, elles, changent** : du
  code écrit pour la 1.x pilotait la face opposée, donc tout contournement qui échangeait les deux doit être
  retiré. Vérifié sur un périphérique avant la modification.
- L'interface de périphérique est renommée `LuxaforDevice` → `ILuxaforDevice`, ce qui aligne l'API publique
  sur la convention de nommage .NET que le reste de l'écosystème utilise (`IDisposable`, `IEnumerable<T>`,
  ...). Les consommateurs doivent mettre à jour le nom du type ; les membres, eux, sont inchangés, la
  migration se réduit donc à un renommage :
  `using ILuxaforDevice orb = Luxafor.GetDevices().First();`. Les implémentations continuent de nommer leur
  spécialisation (`HidLuxaforDevice`), et `Luxafor`, `LuxaforDeviceLocator` et
  `LuxaforDeviceNotFoundException` gardent leur nom — ce ne sont pas des interfaces.
- Les objets valeur (`BrightColor`, `FadeDuration`, `LedIndex`, `LightingCommand`, `Repeat`, `Speed`,
  `TargetedLeds`) ne dérivent plus de `Value.ValueType<T>` : ils implémentent leur propre égalité (`Equals`,
  `GetHashCode`, `==`, `!=`, `IEquatable<T>`) avec la même sémantique de valeur, et le package `Value` n'est
  plus une dépendance. Compatible au niveau des sources pour tout usage normal (comparaisons, clés de
  dictionnaire, `IEquatable<T>`), mais incompatible au niveau binaire : recompilez contre la 2.0.0. Seul le
  code qui référence explicitement le type de base `Value.ValueType<T>` (ou qui redéfinit
  `GetAllAttributesToBeUsedForEquality`) doit être adapté.
- `Luxafor.GetDevice(devicePath)` signale désormais explicitement les chemins invalides au lieu d'échouer sur
  une obscure `ArgumentNullException` : elle lève une `ArgumentException` sur un chemin vide et une
  `LuxaforDeviceNotFoundException` lorsqu'aucun périphérique ne se trouve au chemin indiqué, lorsque ce n'est
  pas un périphérique Luxafor supporté, ou lorsqu'il n'est plus connecté.
- Le package ne livre plus un assembly `net46` dans un dossier `net462` : la cible `net462` est réellement
  compilée contre .NET Framework 4.6.2.

### Ajouté

- `LuxaforDeviceNotFoundException`, qui porte le `DevicePath` fautif.
- `TargetedLeds.FromLedIndex(LedIndex)`, l'alternative nommée à la conversion implicite existante.
- Source Link, un package de symboles (`.snupkg`) et des builds déterministes.
- Une CI GitHub Actions (Windows) qui compile, teste, package et valide le contenu du `.nupkg`.
- Un workflow GitHub Actions de publication vers nuget.org lorsqu'un tag `v*` est poussé, après avoir
  vérifié que le tag correspond à la version du projet, exécuté les tests et validé le package. Il
  s'authentifie par trusted publishing (OIDC) : aucune clé d'API à longue durée de vie n'est stockée dans le
  dépôt.
- Des tests couvrant la propagation des échecs d'écriture, `Dispose`, les périphériques absents, les chemins
  HID invalides, les gardes sur les arguments, l'égalité des objets valeur et la surface de l'API publique.

### Modifié

- Un projet unique au format SDK ciblant à la fois `netstandard2.0` et `net462`, en remplacement du Shared
  Project, du projet .NET Framework non-SDK et du `.nuspec` écrit à la main ; le package est désormais
  produit par `dotnet pack -c Release` (à partir des binaires Release, là où le `.nuspec` précédent
  ramassait ceux du Debug).
- Les types référence nullables, les avertissements traités comme des erreurs et les analyseurs .NET sont
  activés.
- HidLibrary est utilisée à travers une abstraction interne (`IHidDeviceRegistry`, `IHidDeviceHandle`), ce
  qui la tient hors de l'API publique et rend la logique de pilotage testable sans matériel.
- Renommages internes, sans impact sur l'API publique : `LuxaforDeviceImp` devient `HidLuxaforDevice`, les
  fichiers et dossiers `Lightning*` deviennent `Lighting*` (à l'image du type `LightingCommand` qu'ils
  contiennent), et l'interface interne `LightingCommandFactory` devient `ILightingCommandFactory`.
- Documentation : les README (7 langues) sont synchronisés avec le code — exemples obsolètes `BasicColor` /
  `SetBasicColor` remplacés par `BrightColor` / `SetColor`, signatures `void` corrigées en `bool`, `using`
  montré sur `ILuxaforDevice`, plus les sections installation, recherche de périphérique, gestion des
  erreurs, périphériques supportés et licence.

### Corrigé

- Les codes lux envoyés pour `TargetedLeds.TabSide` et `TargetedLeds.BackSide` étaient inversés, si bien que
  les deux allumaient la face opposée du périphérique. Voir les changements cassants ci-dessus : celui-ci est
  silencieux, il change le comportement sans casser la compilation.
- Les commandes `FadeColor` ne se décrivent plus avec une coquille « duration od » dans `ToString()`.

## [1.2.0]

### Ajouté

- Les commandes de périphérique retournent un `bool` pour indiquer si l'opération a réussi.
- `LuxaforDevice` implémente `IDisposable` afin de permettre une libération correcte des ressources.
