namespace Kronos.Machina.Infrastructure.Misc.Probing
{
    public interface IProbeAccessor
    {
        public ProbeResult Probe(string blobPath);
    }
}
