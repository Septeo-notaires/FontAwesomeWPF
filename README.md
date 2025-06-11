# 🛠 FontAwesome Icon Generator & Intégration WPF

Ce projet permet de :
✅ Générer automatiquement une énumération `FontAwesomeIconName` à partir du fichier `icons.json` officiel de FontAwesome.
✅ Intégrer facilement les icônes FontAwesome dans une application WPF via un package NuGet.

---

## 🎨 Ajouter le package `FontAwesomeWPF` à un projet WPF

1. Ajouter la source NuGet **ComptaFR** :

```bash
dotnet nuget add source "https://pkgs.dev.azure.com/Septeo-GenApi/Compta/_packaging/ComptaFR/nuget/v3/index.json" --name "ComptaFR"
```

2. Ajouter le package `FontAwesomeWPF` depuis cette source à ton projet WPF.

---

## 🧹 Utilisation du composant `FontAwesomeIcon`

1. Dans le XAML, importe le namespace :

```xml
xmlns:fa="clr-namespace:FontAwesomeWPF;assembly=FontAwesomeWPF"
```

2. Exemple d’utilisation :

```xml
<fa:FontAwesomeIcon
    IconColor="Violet"
    IconName="House"
    IconSize="50"
    IconStyle="Solid" />
```

---

### 📌 Bonnes pratiques

✅ Associe toujours `IconName` et `IconStyle` correctement :

* `Solid` → `IconStyle="Solid"`
* `Regular` → `IconStyle="Regular"`
* `Brands` → `IconStyle="Brands"`

> Exemple : l’icône `Facebook` nécessite `IconStyle="Brands"`.

---

## 🔓 Accès aux icônes gratuites

Sans licence Pro, seules les icônes gratuites sont disponibles.
👉 [Consulter la liste des icônes gratuites](https://fontawesome.com/search?ic=free)

---

# ⚙️ Fonctionnement du générateur

## 📆 Préparation

1. **Télécharger les fichiers nécessaires** :

   * `icons.json` depuis le dépôt officiel :
     ➔ [GitHub - FontAwesome metadata](https://github.com/FortAwesome/Font-Awesome/tree/6.x/metadata/icons.json)

   * Place le fichier à la racine du projet `FontAwesomeWPF`.

2. **Génération automatique des icônes** :

   * Lancer un build du projet `FontAwesomeWPF`, ou exécuter :

   ```bash
   msbuild FontAwesomeWPF/FontAwesomeWPF.csproj /t:Rebuild /v:detailed
   ```

   * Cela génère un fichier `FontAwesomeIcons.generated.cs` contenant l’énumération `FontAwesomeIconName`.

---

## 🏗 Référencement des polices `.otf`

1. **Télécharger les polices depuis le dépôt** :
   ➔ [GitHub - FontAwesome otfs](https://github.com/FortAwesome/Font-Awesome/tree/6.x/otfs)

   Exemple de fichiers :

   * `Font Awesome 6 Free-Regular-400.otf`
   * `Font Awesome 6 Free-Solid-900.otf`
   * `Font Awesome 6 Brands-Regular-400.otf`

2. **Inclure les polices dans le projet** :

   * Place-les dans le dossier `Fonts/` du projet `FontAwesomeWPF`.
   * Définir leurs propriétés :

     * **Build Action** : `Resource`
     * **Copy to Output Directory** : `Copy if newer`

---

## 📦 Génération & publication du package NuGet

1. **Incrémenter la version** dans `FontAwesomeWPF.csproj`.

2. **Compiler en Release**.

3. **Publier le package** :

```powershell
dotnet nuget push --source "ComptaFR" --api-key az ".\bin\Release\FontAwesomeWPF.x.x.x.nupkg"
```

### 🔐 Pré-requis

* Enrôler son PC dans ADO :

```powershell
iex "& { $(irm https://aka.ms/install-artifacts-credprovider.ps1) } -AddNetfx48"
```

* Ajouter la source `ComptaFR` si ce n’est pas déjà fait :

```powershell
dotnet nuget add source "https://pkgs.dev.azure.com/Septeo-GenApi/Compta/_packaging/ComptaFR/nuget/v3/index.json" --name "ComptaFR"
```

---

## 💡 Idées d’évolutions
- [ ] Animations (https://github.com/charri/Font-Awesome-WPF/tree/master/src/WPF/FontAwesome.WPF)
- [ ] Duocolor
- [ ] Forfait pro