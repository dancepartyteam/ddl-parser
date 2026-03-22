using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace DDLParserWV
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Constant : ParseTreeItem<Constant>
    {
        public override EParseTreeElement Type { get; set; } = EParseTreeElement.Constant;
        [JsonProperty("variable")]
        public Variable Variable { get; set; } = new Variable();
        [JsonProperty("declUse")]
        public DeclarationUse DeclarationUse { get; set; }
        [JsonProperty("arraySize")]
        public uint ArraySize { get; set; }
        [JsonProperty("defaultValue")]
        public string DefaultValue { get; set; }

        protected override Constant ParseTyped(Stream s, StringBuilder log, uint depth, uint majorVersion)
        {
            string tabs = Utils.MakeTabs(depth);
            log.AppendLine($"{tabs}[Constant]");
            Variable.Parse(s, log, depth + 1, majorVersion);
            byte type = (byte)s.ReadByte();
            DeclarationUse = new DeclarationUse(s, (EParseTreeElement)type, log, depth + 1, majorVersion);
            ArraySize = Utils.ReadU32(s);
            log.AppendLine($"{tabs}\t[ArraySize: {ArraySize}]");
            DefaultValue = Utils.ReadString(s);
            log.AppendLine($"{tabs}\t[DefaultValue: {DefaultValue}]");
            return this;
        }
    }
}
