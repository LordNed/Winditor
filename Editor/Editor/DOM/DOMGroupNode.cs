namespace WindEditor
{
    public class WDOMGroupNode : WDOMNode
    {
        public FourCC FourCC;
        public SourceScene SceneLocation;

        public WDOMGroupNode(FourCC fourCC, WWorld world) : base(world)
        {
            FourCC = fourCC;
            IsVisible = true;
            IsRendered = true;
        }

        public override string ToString()
        {
            switch (FourCC)
            {
                case FourCC.ACTR:
                    return "ACTR\tActors";
                case FourCC.SCOB:
                    return "SCOB\tScaleable Objects";
                case FourCC.TRES:
                    return "TRES\tTreasure Chests";
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
            IsRendered = true;
        }

        public override string ToString()
        {
            return FourCCConversion.GetDescriptionFromEnum(FourCC);
        }
    }
}
