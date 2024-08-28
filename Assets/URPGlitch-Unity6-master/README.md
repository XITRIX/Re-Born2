# URPGlitch-Unity6

Glitch effect with URP (Universal Render Pipeline), with some fixes for Unity 6.

> **Note**  
> This project a fork of [mao-test-h/URPGlitch](https://github.com/mao-test-h/URPGlitch), which is a port of [keijiro/KinoGlitch](https://github.com/keijiro/KinoGlitch) to work with URP.  
> This was made to fix the issues that came with Unity 6.  
> This project is not actively maintained, like with URPGlitch.
>   
> I have fixed all deprecation errors in the code (such as regarding 'RenderTargetHandle's), and currently works with Unity 6 Preview (see below for the exact version).  
> *<ins>However</ins>*, there is still obsolete code that is yet to be fixed. With how it is now, the project does not use the new RenderGraph API, and will eventually not work in later Unity updates.

## Tested Unity versions

- Unity 6 Preview (6000.0.14f1)
    - URP 17.0.3


## Installing

Please add the following URL to `Package Manager -> [Add package from git URL...]`.

> `https://github.com/ditpowuh/URPGlitch-Unity6.git?path=Assets/URPGlitch`

or add the following URL to `Package/manifest.json -> dependencies`.

```json
{
  "dependencies": {
    "com.ditpowuh.urp-glitch": "https://github.com/ditpowuh/URPGlitch-Unity6.git?path=Assets/URPGlitch",
  }
}
```

# License

- [keijiro/KinoGlitch](https://github.com/keijiro/KinoGlitch)
