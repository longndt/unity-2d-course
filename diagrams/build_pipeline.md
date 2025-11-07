# Build Pipeline

## Overview

This diagram illustrates Unity's build pipeline from source code to deployable game builds.

## Build Pipeline Flow

```
┌─────────────────────────────────────────────────────────────────┐
│                    SOURCE ASSETS                               │
│                                                                 │
│  • Scripts (.cs files)                                         │
│  • Scenes (.unity files)                                       │
│  • Textures (.png, .jpg)                                       │
│  • Audio (.wav, .mp3)                                          │
│  • Models (.fbx, .obj)                                         │
└─────────────────────────────────────────────────────────────────┘
                                │
                                ▼
┌─────────────────────────────────────────────────────────────────┐
│                    ASSET PROCESSING                            │
│                                                                 │
│  1. Import Settings - Configure asset properties               │
│  2. Compression - Reduce file sizes                            │
│  3. Platform Conversion - Format for target platform           │
│  4. Dependency Resolution - Link related assets                │
└─────────────────────────────────────────────────────────────────┘
                                │
                                ▼
┌─────────────────────────────────────────────────────────────────┐
│                    COMPILATION                                 │
│                                                                 │
│  1. Script Compilation - C# to IL                              │
│  2. Shader Compilation - Shader variants                       │
│  3. IL2CPP (Mobile) - IL to native code                        │
│  4. Optimization - Remove unused code                          │
└─────────────────────────────────────────────────────────────────┘
                                │
                                ▼
┌─────────────────────────────────────────────────────────────────┐
│                    BUILD GENERATION                            │
│                                                                 │
│  1. Scene Bundling - Combine scenes                            │
│  2. Asset Bundling - Group related assets                      │
│  3. Platform Packaging - Create platform-specific build        │
│  4. Compression - Final size optimization                      │
└─────────────────────────────────────────────────────────────────┘
                                │
                                ▼
┌─────────────────────────────────────────────────────────────────┐
│                    DEPLOYMENT                                  │
│                                                                 │
│  • Windows: .exe file                                          │
│  • Mac: .app bundle                                            │
│  • Android: .apk file                                          │
│  • iOS: .ipa file                                              │
│  • WebGL: Web build                                            │
└─────────────────────────────────────────────────────────────────┘
```

## Build Settings

### Platform Configuration
```csharp
// Android Settings
PlayerSettings.Android.bundleVersionCode = 1;
PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel21;
PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevel33;

// iOS Settings
PlayerSettings.iOS.buildNumber = "1";
PlayerSettings.iOS.targetOSVersionString = "12.0";

// Windows Settings
PlayerSettings.SetScriptingBackend(BuildTargetGroup.Standalone, ScriptingImplementation.Mono);
```

### Build Optimization
- **Strip Engine Code**: Remove unused Unity features
- **Compress Textures**: Reduce texture sizes
- **Optimize Meshes**: Simplify 3D models
- **Audio Compression**: Compress audio files

## Build Profiles

### Development Build
- **Debug Symbols**: Include debugging information
- **Development Console**: Enable console logging
- **Script Debugging**: Allow script debugging
- **Profiler**: Include profiling tools

### Release Build
- **Optimize**: Maximum performance
- **Strip Code**: Remove unused code
- **Compress**: Minimize file size
- **No Debug**: Remove debugging features

## Common Build Issues

### Missing References
- **Cause**: Broken prefab or script references
- **Solution**: Check all references in Inspector

### Build Size Too Large
- **Cause**: Unused assets included
- **Solution**: Use Addressables, optimize textures

### Platform-Specific Errors
- **Cause**: Platform-specific code issues
- **Solution**: Use platform defines, test on target platform

### Performance Issues
- **Cause**: Inefficient code or assets
- **Solution**: Profile and optimize, use object pooling

## Best Practices

### Build Preparation
- Test on target platform regularly
- Use version control for builds
- Document build settings
- Automate build process

### Optimization
- Profile before optimizing
- Use appropriate compression
- Remove unused assets
- Optimize for target platform

---

**Ready to start building?** Check the [Unity Build Settings](https://docs.unity3d.com/Manual/BuildSettings.html) documentation for detailed instructions.
