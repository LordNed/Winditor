namespace WindEditor
{
    public class WDOMGroupNode : WDOMNode
    {
        public FourCC FourCC;

        public WDOMGroupNode(FourCC fourCC, WWorld world) : base(world)
        {
            FourCC = fourCC;
            IsVisible = true;
        }

        public override string ToString()
        {
            switch (FourCC)
            {
                case FourCC.ACTR:
                    return "Actors";
                case FourCC.SCOB:
                    return "Scaleable Objects";
                case FourCC.TRES:
                    return "Treasure Chests";
                default:
                    return FourCCConversion.GetDescriptionFromEnum(FourCC);
            }
        }
    }

    public class WDOMLayeredGroupNode : WDOMNode
    {
        public FourCC FourCC;
        public MapLayer Layer;

        public WDOMLayeredGroupNode(FourCC fourCC, MapLayer layer, WWorld world) : base(world)
        {
            FourCC = fourCC;
            Layer = layer;
            IsVisible = true;
        }

        public override string ToString()
        {
            return FourCCConversion.GetDescriptionFromEnum(FourCC);
        }
    }
}
