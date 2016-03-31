# AGDCWare
Wario-Ware style collection of VGDC minigames
Guidelines for contribution:

To be added as a contributor, contact our Gitmaster, Gitma Ster. Their email address is gster@uci.edu.
We prefer to avoid pull requests, as they result in more merge conflicts than direct commits.


Your game should be self-contained in a single scene.

To end your game, call the static funtion `AGDCWareFramework.LoadNextGame();`, which will load the next game.
For your game to be loaded, add your scene name to the file `SceneNames.txt`, which is above the Assets folder.

All of your assets should be in your own subfolder of Contributor Assets, to avoid conflicts.
All of your code should be in your own namespace, to avoid conflicts.
```
namespace MyNamespace
{
	public class MyScript : MonoBehaviour
	{
		//[...]
	}
}
```

There exist common library-style in the VGDC Assets folder. If you want to contribute to the VGDC Code library, your library should be

1. functional (it works)

2. not unoptimized (it doesn't lag)

3. well documented (other people can use it)


Documentation should include Assert statements and autodoc (`///`) comments in addition to normal documentation.

You can use art/sound assets from other people's folders, but please create your own copy for your own use. The other person can update or delete the asset in their folder without notifying you.

Status API Training Shop Blog About
© 2016 GitHub, Inc. Terms Privacy Security Contact Help
