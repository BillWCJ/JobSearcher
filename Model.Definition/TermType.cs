using System.ComponentModel;

namespace Model.Definition
{
    public enum TermType
    {
        [Description ("4?")]
        Four = 4,
        [Description("8?")]
        Eight = 8,
        [Description("4or8?")]
        Both = 12,
        [Description("???")]
        Unknown = 99
    }
}
