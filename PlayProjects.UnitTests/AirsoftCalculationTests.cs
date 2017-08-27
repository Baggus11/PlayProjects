using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayProjects.UnitTests
{
    [TestClass]
    public class AirsoftTests
    {
        [TestMethod]
        public void Airsoft_BB_FPS_Calculator()
        {
            var possibleSpeeds_FPS = Enumerable.Range(10, 17).Select(n => n * 25);
            List<double> bb_masses = new List<double> { .2, .23, .25, .28, .3, .33, .38, .43, .45 };
            StringBuilder sb = new StringBuilder();
            foreach (var bbmass in bb_masses)
            {
                foreach (var fps in possibleSpeeds_FPS)
                {
                    var velocity_ms = (fps * 0.304);
                    var mass_kg = (bbmass / 1000);
                    var energy_joule = (0.5 * ((mass_kg) * (velocity_ms * velocity_ms)));

                    velocity_ms = Math.Round(velocity_ms * 100) / 100;
                    energy_joule = Math.Round(energy_joule * 100) / 100;

                    var bbvelocity_fps = (Math.Sqrt((2 * energy_joule) / mass_kg)) * 3.2894;
                    var bbvelocity_ms = (bbvelocity_fps * 0.304);
                    var effectiverange_ms = 0.45152364 * bbvelocity_ms;
                    var effectiverange_fps = 0.45152364 * bbvelocity_fps;

                    bbvelocity_fps = Math.Round(bbvelocity_fps * 100) / 100;
                    bbvelocity_ms = Math.Round(bbvelocity_ms * 100) / 100;
                    effectiverange_ms = Math.Round(effectiverange_ms * 100) / 100;
                    effectiverange_fps = Math.Round(effectiverange_fps * 100) / 100;

                    sb.AppendLine($"bb Mass: {bbmass}");
                    sb.Append($"\tFPS: {fps}");
                    sb.Append($"\tVelocity (m/s): {velocity_ms}");
                    sb.Append($"\tEnery (Joule): {energy_joule}");
                    sb.AppendLine("--**Muzzle Velocity**--");
                    sb.AppendLine($"Speed (FPS): {bbvelocity_fps}");
                    sb.Append($"\tVelocity (m/s): {bbvelocity_ms}");
                    sb.AppendLine("--**Estimated Range**--");
                    sb.AppendLine($"Feet: {effectiverange_fps}");
                    sb.Append($"\tMeters: {effectiverange_ms}");

                }
            }
        }

        public class FPSValues
        {
            float velocity_ms { set; get; }
            float mass_kg { set; get; }
            float energy_joule { set; get; }
            float bbvelocity_fps { set; get; }
            float bbvelocity_ms { set; get; }
            float effectiverange_ms { set; get; }
            float effectiverange_fps { set; get; }
        }

    }
}
