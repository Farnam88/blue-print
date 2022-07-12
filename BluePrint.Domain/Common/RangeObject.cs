namespace BluePrint.Domain.Common;

public class RangeObject<TLow, TUp>
{
    public TLow LowerBound { get; set; }
    public TUp UpperBound { get; set; }
}