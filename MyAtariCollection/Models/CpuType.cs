namespace MyAtariCollection.Models;

/// <summary>
/// Processor type in use, notably MC68000 everywhere, except for TT  Falcon (030),
/// however third party hardware modules existed to allow more impressive CPus on humble STs
/// </summary>
public enum CpuType
{
    MC68000 = 0,
    MC68010 = 1,
    MC68020 = 2,
    MC68030 = 3,
    MC68040 = 4,
    MC68060 = 5,
}