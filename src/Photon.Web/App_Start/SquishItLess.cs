[assembly: WebActivator.PreApplicationStartMethod(typeof(Photon.Web.App_Start.SquishItLess), "Start")]

namespace Photon.Web.App_Start
{
    using SquishIt.Framework;
    using SquishIt.Less;

    public class SquishItLess
    {
        public static void Start()
        {
            Bundle.RegisterStylePreprocessor(new LessPreprocessor());
        }
    }
}