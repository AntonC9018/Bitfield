// Template file generated by Kari. Feel free to change it or remove this message.
using System.Threading.Tasks;
using Kari.GeneratorCore.Workflow;

namespace Kari.Plugins.Bitfield
{
    // The plugin interface to Kari.
    // Kari will call methods of this class to analyze and then generate code.
    public class BitfieldAdministrator : IAdministrator
    {
        public BitfieldAnalyzer[] _analyzers;
        
        // Called after all of the projects have been loaded
        public void Initialize()
        {
            AnalyzerMaster.Initialize(ref _analyzers);
            var logger = new Logger("Bitfield Plugin");
            BitfieldSymbols.Initialize(logger);
        }
        
        // Called when it is time to find ("collect") the types and methods you need to analyze.
        // At this stage, info objects are created and stored, 
        // containing the final data needed to generate the output.
        public Task Collect()
        {
            return AnalyzerMaster.CollectAsync(_analyzers);
        }
        // Called to generate code. The name of the files can be anything.
        public Task Generate()
        {
            return Task.WhenAll(
                AnalyzerMaster.GenerateAsync(_analyzers, "Bitfields.cs"),
                // Write the attributes to an acessible file.
                MasterEnvironment.Instance.CommonPseudoProject.WriteFileAsync("BitfieldAnnotations.cs", GetAnnotations())
            );
        }
        
        public string GetAnnotations() => DummyBitfieldAnnotations.Text;
    }
}
