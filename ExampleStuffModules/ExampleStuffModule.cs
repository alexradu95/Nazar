using NazarInfrastructure.Scene;
using SKReleaseNotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleStuffModules
{
    internal class ExampleStuffModule : ISceneManager
    {
        private List<IScene> scenes = new List<IScene>();
        public List<IScene> Scenes => scenes;

        public void Initialize()
        {
            scenes = new List<IScene>(new IScene[]{
                new SceneWelcome(),
                new SceneControllers(),
                new SceneLighting(),
                new SceneRenderToTex(),
                new SceneUI(),
                new SceneUIBox(),
                new SceneMicrophone(),
                new SceneWand(),
                new SceneSoundInst(),
                new SceneThanks(),
            });
        }

        public void Step()
        {
            throw new NotImplementedException();
        }
    }
}
